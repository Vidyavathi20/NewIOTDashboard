using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using System.Text;
using System.Net.Mail;

namespace MaterialGateRegister.Controllers
{
    public class EmployeeController : Controller
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();
        // GET: Employee
        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                IEnumerable<Models.Login> data = Employee_data();
                return View(data);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        private IEnumerable<Models.Login> Employee_data()
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_all";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    yield return new Models.Login
                    {
                        Employeename = Convert.ToString(row["Employeename"]),
                        Userrole = Convert.ToString(row["Userrole"]),                        
                        id = Convert.ToString(row["id"]),
                        Emailid = Convert.ToString(row["Emailid"]),
                        
                    };
                }
            }
        }

        public string RandomString(int size, bool lowerCase)
        {
            StringBuilder builder = new StringBuilder();
            Random random = new Random();
            char ch;
            for (int i = 0; i < size; i++)
            {
                ch = Convert.ToChar(Convert.ToInt32(Math.Floor(26 * random.NextDouble() + 65)));
                builder.Append(ch);
            }
            if (lowerCase)
                return builder.ToString().ToLower();
            return builder.ToString();
        }

        public int RandomNumber(int min, int max)
        {
            Random random = new Random();
            return random.Next(min, max);
        }
        public string RandomPassword(int size = 0)
        {
            StringBuilder builder = new StringBuilder();
            builder.Append(RandomString(2, true));
            builder.Append(RandomNumber(10, 99));
            builder.Append(RandomString(2, true));
            builder.Append(RandomNumber(10, 99));
            return builder.ToString();
        }

        [HttpPost]
        public ActionResult Insert_employee(Models.Login S)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string ran_pass = RandomPassword(6);

                    DataTable dt1 = new DataTable();

                    using (SqlConnection con1 = new SqlConnection(connectionstring))
                    {
                        con1.Open();
                        SqlCommand cmd1 = new SqlCommand("Select * from tbl_gmail_settings", con1);
                        SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                        sda1.Fill(dt1);
                    }

                    if (S.Employeename != "" && S.Userrole != "0")
                    {

                        if (S.Userrole != "1")
                        {
                            S.Emailid = "";
                        }
                        if(S.Userrole != "1")
                        {
                            ran_pass = "";
                        }
                        using (SqlConnection con = new SqlConnection(connectionstring))
                        {
                            string ID = "";
                            if (S.QueryType == "Insert")
                            {
                                ID = "0";
                            }
                            else
                            {
                                ID = S.id;
                            }

                            string response = string.Empty;
                            con.Open();
                            SqlCommand cmd = new SqlCommand("SP_employee", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = S.QueryType;
                            cmd.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = ID;
                            cmd.Parameters.Add("@Employeename", SqlDbType.VarChar, 50).Value = S.Employeename;
                            cmd.Parameters.Add("@Role_id", SqlDbType.VarChar, 50).Value = S.Userrole;
                            cmd.Parameters.Add("@Emailid", SqlDbType.VarChar, 50).Value = S.Emailid.ToLower();
                            cmd.Parameters.Add("@Password", SqlDbType.VarChar, 50).Value = ran_pass;
                            cmd.Parameters.Add("@Superadmin", SqlDbType.VarChar, 50).Value = S.Superadmin;
                            SqlParameter SQLReturn = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                            SQLReturn.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(SQLReturn);
                            cmd.ExecuteNonQuery();
                            response = SQLReturn.Value.ToString().Trim();

                            if (S.Userrole == "1")
                            {
                                MailMessage mail = new MailMessage();
                                mail.To.Add(S.Emailid);
                                mail.From = new MailAddress(dt1.Rows[0]["FGmail"].ToString());
                                mail.Subject = "Password Details ";
                                mail.Body = string.Format("<b> Your Login Password is :  </b> {0} ", ran_pass);
                                mail.IsBodyHtml = true;
                                SmtpClient smtp = new SmtpClient();
                                smtp.Host = dt1.Rows[0]["Smtp_host"].ToString(); //SMTP Server Address of gmail
                                smtp.Port = Convert.ToInt32(dt1.Rows[0]["Smtp_port"].ToString());
                                smtp.UseDefaultCredentials = true;
                                smtp.Credentials = new System.Net.NetworkCredential(dt1.Rows[0]["Smtp_user"].ToString(), dt1.Rows[0]["Smtp_pass"].ToString()); // Enter seders User name and password  
                                smtp.EnableSsl = true;
                                smtp.Send(mail);
                            }

                            return RedirectToAction("Index", "Employee", new { ac = response });
                        }
                    }
                    else
                    {
                        return RedirectToAction("Index", "Employee", new { ac = 3 });
                 }
                }

                catch (Exception ex)
                
                {
                    ViewBag.Message = "Failed to Add Employee Details ";
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Edit_emp(string id)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Edit_emp";
                        cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = id;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        var result = new List<Models.Login>();
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            result.Add(item: new Models.Login
                            {
                                Userrole = Convert.ToString(row["Userrole"]),
                                Employeename = Convert.ToString(row["Employeename"]),
                                Role_id = Convert.ToString(row["Role_id"]),
                                Emailid = Convert.ToString(row["Emailid"]),                               
                                id = Convert.ToString(row["id"]),
                                Superadmin=Convert.ToString(row["Superadmin"])
                            });
                        }

                        return Json(result, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed Edit Details ";
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        public ActionResult Delete_emp(string ID)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        string QueryType = "Check_superadmin";
                        con.Open();
                        DataSet dt = new DataSet();
                        SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = QueryType;
                        cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = ID;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(dt);
                        
                        if (dt.Tables[0].Rows[0]["Superadmin"].ToString() == "Yes")
                        {
                            return Json("-1", JsonRequestBehavior.AllowGet);
                        }
                        else
                        {

                            SqlCommand cmd1 = new SqlCommand("SP_delete_settings", con);
                            cmd1.CommandType = CommandType.StoredProcedure;
                            cmd1.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Delete_emp";
                            cmd1.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = ID;
                            cmd1.ExecuteNonQuery();
                            return Json("1", JsonRequestBehavior.AllowGet);
                        }
                    }
                }              
                    
                
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Delete Employee";
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult Getrole(Models.Role s)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();

                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_role";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = new List<Models.Role>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(item: new Models.Role
                    {
                        id = Convert.ToString(row["id"]),
                        Userrole = Convert.ToString(row["Userrole"])
                    });
                }
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }
        public ActionResult Getsuperadmin(Models.Login s)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_superadmin";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = new List<Models.Login>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(item: new Models.Login
                    {
                        id=Convert.ToString(row["id"]),
                        Superadmin = Convert.ToString(row["Superadmin"])
                    });
                }
                return Json(result, JsonRequestBehavior.AllowGet);

            }
        }

    }
}