using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net;
using System.Net.Http;
using System.Web.Mvc;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Net.Mail;
using Newtonsoft.Json;

namespace MaterialGateRegister.Controllers
{
    public class LoginController : Controller
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();

        // GET: Login
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Permission()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Check_login(Models.Login Lo)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Check_login";
                cmd.Parameters.Add("@Parameter", SqlDbType.NVarChar, 150).Value = Lo.Employeename;
                cmd.Parameters.Add("@Parameter1", SqlDbType.NVarChar, 150).Value = Lo.Password;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                DataSet d2 = new DataSet();
                SqlCommand cmd4 = new SqlCommand("SP_GetSettings_data", con);
                cmd4.CommandType = CommandType.StoredProcedure;
                cmd4.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Check_pass";
                cmd4.Parameters.Add("@Parameter", SqlDbType.NVarChar, 150).Value = Lo.Employeename;
                SqlDataAdapter da1 = new SqlDataAdapter(cmd4);
                da1.Fill(d2);
                string pass = d2.Tables[0].Rows[0]["Password"].ToString();
                string user_pass = Lo.Password;
                if (pass == user_pass || pass == "")
                {
                    if (ds.Tables[0].Rows.Count != 0)
                    {
                        Session["UserName"] = ds.Tables[0].Rows[0]["Employeename"].ToString();
                        Session["Role"] = ds.Tables[0].Rows[0]["Userrole"].ToString();
                        Session["Role_id"] = ds.Tables[0].Rows[0]["Role_id"].ToString();
                        Session["UserID"] = ds.Tables[0].Rows[0]["id"].ToString();

                        using (SqlConnection con2 = new SqlConnection(connectionstring))
                        {
                            string role = "";
                            role = Session["Role"].ToString();
                            DataSet ds3 = new DataSet();
                            SqlCommand cmd2 = new SqlCommand("SP_GetSettings_data", con2);
                            cmd2.CommandType = CommandType.StoredProcedure;
                            cmd2.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_roleid";
                            cmd2.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = role;
                            SqlDataAdapter sda1 = new SqlDataAdapter(cmd2);
                            sda1.Fill(ds3);
                            List<Models.Role> role1 = new List<Models.Role>();
                            if (ds3.Tables[0].Rows.Count != 0)
                            {
                                Session["Roleid"] = ds3.Tables[0].Rows[0]["id"].ToString();
                            }
                        }
                        var id = Session["Roleid"].ToString();
                        if (id != null)
                        {
                            using (SqlConnection con1 = new SqlConnection(connectionstring))
                            {
                                con1.Open();
                                DataSet ds4 = new DataSet();
                                SqlCommand cmd1 = new SqlCommand("select Module_name,Add_form,View_form,Edit_form,Delete_form from tbl_permission where Role_id=" + id, con1);
                                SqlDataAdapter sda = new SqlDataAdapter(cmd1);
                                sda.Fill(ds4);
                                if (ds4.Tables[0].Rows.Count != 0)
                                {
                                    Session["Settings"] = ds4.Tables[0].Rows[0]["Module_name"].ToString();
                                    Session["SettingsAdd_form"] = ds4.Tables[0].Rows[0]["Add_form"].ToString();
                                    Session["SettingsView_form"] = ds4.Tables[0].Rows[0]["View_form"].ToString();
                                    Session["SettingsEdit_form"] = ds4.Tables[0].Rows[0]["Edit_form"].ToString();
                                    Session["SettingsDelete_form"] = ds4.Tables[0].Rows[0]["Delete_form"].ToString();

                                    Session["Entries"] = ds4.Tables[0].Rows[1]["Module_name"].ToString();
                                    Session["EntriesAdd_form"] = ds4.Tables[0].Rows[1]["Add_form"].ToString();
                                    Session["EntriesView_form"] = ds4.Tables[0].Rows[1]["View_form"].ToString();
                                    Session["EntriesEdit_form"] = ds4.Tables[0].Rows[1]["Edit_form"].ToString();
                                    Session["EntriesDelete_form"] = ds4.Tables[0].Rows[1]["Delete_form"].ToString();

                                    Session["Registers"] = ds4.Tables[0].Rows[2]["Module_name"].ToString();
                                    Session["RegistersAdd_form"] = ds4.Tables[0].Rows[2]["Add_form"].ToString();
                                    Session["RegistersView_form"] = ds4.Tables[0].Rows[2]["View_form"].ToString();
                                    Session["RegistersEdit_form"] = ds4.Tables[0].Rows[2]["Edit_form"].ToString();
                                    Session["RegistersDelete_form"] = ds4.Tables[0].Rows[2]["Delete_form"].ToString();

                                    Session["Pendingregister"] = ds4.Tables[0].Rows[3]["Module_name"].ToString();
                                    Session["PendingRegistersAdd_form"] = ds4.Tables[0].Rows[3]["Add_form"].ToString();
                                    Session["PendingRegistersView_form"] = ds4.Tables[0].Rows[3]["View_form"].ToString();
                                    Session["PendingRegistersEdit_form"] = ds4.Tables[0].Rows[3]["Edit_form"].ToString();
                                    Session["PendingRegistersDelete_form"] = ds4.Tables[0].Rows[3]["Delete_form"].ToString();
                                }
                            }

                            if (Session["Role_id"].ToString() == "1")
                            {

                                return RedirectToAction("Dashboard", "Login");
                            }
                            else
                            {
                                return RedirectToAction("Material_Entry", "MaterialEntries");
                            }
                        }
                    }
                }
                else
                {
                    ViewBag.Message = "Invalid Login Details...!";
                    return View("Index");
                }
                return View("Index");
            }

        }

        public ActionResult Getemp(Models.Login s)
        {

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_employee";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = new List<Models.Login>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(item: new Models.Login
                    {
                        id = Convert.ToString(row["id"]),
                        Employeename = Convert.ToString(row["Employeename"])
                    });
                }
                return Json(result, JsonRequestBehavior.AllowGet);

            }

        }

        [HttpPost]
        public ActionResult Check_role(string id)
        {

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Check_role";
                cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = id;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = ds.Tables[0];
                string json = JsonConvert.SerializeObject(result);
                return Json(json, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public ActionResult getRoles()
        {

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                string query = "select * from tbl_role";
                SqlDataAdapter da = new SqlDataAdapter(query, con);
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

        [HttpPost]
        public ActionResult getuserroles(string id)
        {

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Getpermission";
                cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = id;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = new List<Models.permission>();

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(item: new Models.permission
                    {
                        Permission_id = Convert.ToInt32(row["Permission_id"]),
                        Role_id = Convert.ToString(row["Role_id"]),
                        Module_name = Convert.ToString(row["Module_name"]),
                        Add_form = Convert.ToString(row["Add_form"]),
                        View_form = Convert.ToString(row["View_form"]),
                        Edit_form = Convert.ToString(row["Edit_form"]),
                        Delete_form = Convert.ToString(row["Delete_form"]),
                    });
                }
                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public ActionResult AddUserrole(List<Models.permission> permissions)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        string response = string.Empty;
                        con.Open();
                        foreach (var details in permissions)
                        {
                            string Employeename = Session["Username"].ToString();

                            SqlCommand cmd = new SqlCommand("SP_Permission", con);
                            cmd.CommandType = CommandType.StoredProcedure;

                            if (permissions == null)
                            {
                                permissions = new List<Models.permission>();
                            }
                            cmd.Parameters.Add("@Role_id", SqlDbType.VarChar, 50).Value = details.Role_id;
                            cmd.Parameters.Add("@Module_name", SqlDbType.VarChar, 50).Value = details.Module_name;
                            cmd.Parameters.Add("@Add_form", SqlDbType.VarChar, 50).Value = details.Add_form;
                            cmd.Parameters.Add("@View_form", SqlDbType.VarChar, 50).Value = details.View_form;
                            cmd.Parameters.Add("@Edit_form", SqlDbType.VarChar, 50).Value = details.Edit_form;
                            cmd.Parameters.Add("@Delete_form", SqlDbType.VarChar, 50).Value = details.Delete_form;
                            SqlParameter SQLReturn = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                            SQLReturn.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(SQLReturn);
                            cmd.ExecuteNonQuery();
                            response = SQLReturn.Value.ToString().Trim();
                        }
                        TempData["Message"] = response;
                        return Json(response, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Add/Update Userrole Details...! ";
                    return View("Permission");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        [HttpPost]
        public ActionResult Add_Permission(List<Models.permission> permissions, List<Models.Role> role)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string response1 = string.Empty;

                    foreach (var roledetails in role)
                    {
                        roledetails.QueryType = roledetails.QueryType;

                        using (SqlConnection con = new SqlConnection(connectionstring))
                        {
                            string ID = "";
                            if (roledetails.QueryType == "Insert")
                            {
                                ID = "0";
                            }
                            else
                            {
                                ID = roledetails.id;
                            }
                            string response = string.Empty;
                            con.Open();
                            SqlCommand cmd = new SqlCommand("SP_role", con);
                            cmd.CommandType = CommandType.StoredProcedure;
                            cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = roledetails.QueryType;
                            cmd.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = ID;
                            cmd.Parameters.Add("@Userrole", SqlDbType.VarChar, 50).Value = roledetails.Userrole;
                            SqlParameter SQLReturn = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                            SQLReturn.Direction = ParameterDirection.Output;
                            cmd.Parameters.Add(SQLReturn);
                            cmd.ExecuteNonQuery();

                            response = SQLReturn.Value.ToString().Trim();

                            if (response == "1" || response == "2")
                            {

                                using (SqlConnection con1 = new SqlConnection(connectionstring))
                                {

                                    con1.Open();


                                    foreach (var details in permissions)
                                    {
                                        string ID1 = "";
                                        if (roledetails.QueryType == "Insert")
                                        {
                                            ID1 = "0";
                                        }
                                        else
                                        {
                                            ID1 = roledetails.id;
                                        }

                                        string Employeename = Session["Username"].ToString();

                                        SqlCommand cmd1 = new SqlCommand("SP_Permission", con);
                                        cmd1.CommandType = CommandType.StoredProcedure;

                                        if (permissions == null)
                                        {
                                            permissions = new List<Models.permission>();
                                        }
                                        //Loop and insert records.
                                        cmd1.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = roledetails.QueryType;
                                        cmd1.Parameters.Add("@UniqueId", SqlDbType.VarChar, 50).Value = ID1;
                                        cmd1.Parameters.Add("@Module_name", SqlDbType.VarChar, 50).Value = details.Module_name;
                                        cmd1.Parameters.Add("@Add_form", SqlDbType.VarChar, 50).Value = details.Add_form;
                                        cmd1.Parameters.Add("@View_form", SqlDbType.VarChar, 50).Value = details.View_form;
                                        cmd1.Parameters.Add("@Edit_form", SqlDbType.VarChar, 50).Value = details.Edit_form;
                                        cmd1.Parameters.Add("@Delete_form", SqlDbType.VarChar, 50).Value = details.Delete_form;
                                        SqlParameter SQLReturn1 = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                                        SQLReturn1.Direction = ParameterDirection.Output;
                                        cmd1.Parameters.Add(SQLReturn1);
                                        cmd1.ExecuteNonQuery();
                                        response1 = SQLReturn1.Value.ToString().Trim();
                                    }

                                }
                            }
                            else if (response == "-1")
                            {
                                return Json("-1", JsonRequestBehavior.AllowGet);
                            }

                        }

                    }
                    return Json(response1, JsonRequestBehavior.AllowGet);

                }
                catch (Exception e)
                {
                    return RedirectToAction("Error_Page", "Main");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        [HttpPost]
        public ActionResult Get_Email(string id)
        {
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_email";
                cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = id;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                if (ds.Tables[0].Rows.Count != 0)
                {
                    Session["Email"] = ds.Tables[0].Rows[0]["Emailid"].ToString();

                }
                return Json("ok", JsonRequestBehavior.AllowGet);
            }
        }


        public ActionResult Forgot(string email)
        {

            string a = email;
            return View(email);

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
        public ActionResult Send_mail(Models.Login lb)
        {

            string message = "";

            string ran_pass = RandomPassword(6);
            DataTable dt1 = new DataTable();

            using (SqlConnection con1 = new SqlConnection(connectionstring))
            {
                con1.Open();
                SqlCommand cmd1 = new SqlCommand("Select * from tbl_gmail_settings", con1);
                SqlDataAdapter sda1 = new SqlDataAdapter(cmd1);
                sda1.Fill(dt1);
            }

            using (SqlConnection con2 = new SqlConnection(connectionstring))
            {
                con2.Open();
                SqlCommand cmd2 = new SqlCommand("Update tbl_Employee SET Password=@Password WHERE Emailid=@Emailid", con2);
                cmd2.Parameters.AddWithValue("@Emailid", lb.Emailid);
                cmd2.Parameters.AddWithValue("@Password", ran_pass);
                cmd2.ExecuteNonQuery();
            }

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                SqlCommand cmd = new SqlCommand("Select Emailid,Password from tbl_Employee where Emailid=@Emailid ", con);
                cmd.Parameters.AddWithValue("@Emailid", lb.Emailid);
                SqlDataAdapter sda = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                sda.Fill(dt);

                if (dt.Rows.Count != 0)
                {
                    string password = dt.Rows[0]["Password"].ToString();
                    message = dt.Rows[0]["Emailid"].ToString();
                    MailMessage mail = new MailMessage();
                    mail.To.Add(message);
                    mail.From = new MailAddress(dt1.Rows[0]["FGmail"].ToString());
                    mail.Subject = "Forgot Password Details ";
                    mail.Body = string.Format("<b> Your New Password is :  </b> {0} ", password);
                    mail.IsBodyHtml = true;
                    SmtpClient smtp = new SmtpClient();
                    smtp.Host = dt1.Rows[0]["Smtp_host"].ToString(); //SMTP Server Address of gmail
                    smtp.Port = Convert.ToInt32(dt1.Rows[0]["Smtp_port"].ToString());
                    smtp.UseDefaultCredentials = true;
                    smtp.Credentials = new System.Net.NetworkCredential(dt1.Rows[0]["Smtp_user"].ToString(), dt1.Rows[0]["Smtp_pass"].ToString()); // Enter seders User name and password  
                    smtp.EnableSsl = true;
                    smtp.Send(mail);
                    ViewBag.Message = "Password is Sent to your email id .Please Login!";
                    return View("Forgot");
                }
                else
                {
                    ViewBag.Message = "Email ID Not Valid...!";
                    return View("Forgot");
                }
            }

        }

        [HttpPost]
        public ActionResult CheckPwd(Models.Login User)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string oldpassword = User.Oldpassword;
                    string password = User.Password;
                    string message = string.Empty;
                    string UserID = Session["UserID"].ToString();

                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {

                        con.Open();
                        DataSet ds = new DataSet();
                        SqlCommand cmd = new SqlCommand("select Password from tbl_Employee where id=" + UserID, con);
                        SqlDataReader sdr = cmd.ExecuteReader();
                        while (sdr.Read())
                        {
                            Session["pass"] = sdr["Password"].ToString();
                        }
                    }
                    if (oldpassword == Session["pass"].ToString())
                        using (SqlConnection con = new SqlConnection(connectionstring))
                        {
                            {
                                con.Open();
                                SqlCommand cmd = new SqlCommand("update tbl_Employee set Password=@Password where id=" + UserID, con);

                                cmd.Parameters.Add("@Password", SqlDbType.NVarChar, 150).Value = password;
                                cmd.ExecuteNonQuery();
                                message = "Password Changed Successfully...!";
                                ViewBag.SMessage = message;
                                return View("Change_Password");
                            }

                        }
                    else
                    {
                        message = "Old Password is Incorret...!";
                        ViewBag.Message = message;
                        return View("Change_Password");
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Change Password";
                    return View("Change_Password");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        public ActionResult Dashboard()
        {
            if (Session["UserName"] != null)
            {
                return View();
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        [HttpGet]
        public ActionResult Change_Password()
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    return View();
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Load...!";
                    return View("Change_Password");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult Logout()
        {
            try
            {
                Session["UserName"] = "";
                Session["Role"] = "";
                Session["UserID"] = "";
                ViewBag.LMessage = "Logout Successfully...!";
                return RedirectToAction("Index", "Login");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public ActionResult Unauth_page()
        {
            return View();
        }
    }

}


