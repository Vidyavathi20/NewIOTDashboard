using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;

namespace MaterialGateRegister.Controllers
{
    public class RolesController : Controller
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();
        // GET: Roles

        public ActionResult Index()
        {
            if (Session["UserName"] != null)
            {
                IEnumerable<Models.Role> data = Role_Datas();
                return View(data);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        private IEnumerable<Models.Role> Role_Datas()
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

                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    yield return new Models.Role
                    {
                        id = Convert.ToString(row["id"]),
                        Userrole = Convert.ToString(row["Userrole"])

                    };
                }
            }
        }

        [HttpPost]
        public ActionResult Checkrole(Models.Role s)
        {
            List<Models.Role> vendors = new List<Models.Role>();


            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "checkrole";
                cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = s.Userrole;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = "";
                if (ds.Tables[0].Rows.Count != 0)
                {
                    result = "1";
                }
                else
                {
                    result = "0";
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult Insert_update_role(Models.Role S)
        {
            if (Session["UserName"] != null)
            {
                try
                {
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
                        SqlCommand cmd = new SqlCommand("SP_role", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = S.QueryType;
                        cmd.Parameters.Add("@id", SqlDbType.VarChar, 50).Value = ID;
                        cmd.Parameters.Add("@Userrole", SqlDbType.VarChar, 50).Value = S.Userrole;
                        SqlParameter SQLReturn = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                        SQLReturn.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(SQLReturn);
                        cmd.ExecuteNonQuery();
                        response = SQLReturn.Value.ToString().Trim();
                        TempData["message"] = response;
                        return RedirectToAction("Index", "Roles", new { ac = response });
                        //return Json(response,JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Add Role Details ";
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Edit_role(string id)
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
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Edit_role";
                        cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = id;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);

                        var result = new List<Models.Role>();

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            result.Add(item: new Models.Role
                            {
                                Userrole = Convert.ToString(row["Userrole"]),
                                id = Convert.ToString(row["Id"]),
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


        public ActionResult Edit_permission(string id)
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
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Edit_role";
                        cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = id;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);

                        var result = new List<Models.permission>();

                        foreach (DataRow row in ds.Tables[1].Rows)
                        {
                            result.Add(item: new Models.permission
                            {
                                Module_name = Convert.ToString(row["Module_name"]),
                                Add_form = Convert.ToString(row["Add_form"]),
                                Edit_form = Convert.ToString(row["Edit_form"]),
                                Delete_form = Convert.ToString(row["Delete_form"]),
                                View_form = Convert.ToString(row["View_form"]),
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

        public ActionResult Delete_role(string id, string Role)
        {

            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {

                    string QueryType = "Check_delrole";
                    con.Open();
                    DataSet dt = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = QueryType;
                    cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = Role;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(dt);
                    if (dt.Tables[0].Rows.Count != 0)
                    {
                        return Json("-1", JsonRequestBehavior.AllowGet);
                    }
                    else
                    {
                        SqlCommand cmd1 = new SqlCommand("SP_delete_settings", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Delete_role";
                        cmd1.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = id;
                        cmd1.ExecuteNonQuery();
                        return Json("1", JsonRequestBehavior.AllowGet);
                    }
                }

            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult Getrole()
        {
            if (Session["UserName"] != null)
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
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
    }

}


