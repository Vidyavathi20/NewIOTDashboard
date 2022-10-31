using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using Newtonsoft.Json;

namespace MaterialGateRegister.Controllers
{
    public class MaterialEntriesController : Controller
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();
        // GET: Returnable_Internal
        public ActionResult Material_Entry()
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
        public ActionResult Insert_Update_returnable_internal_outward(List<Models.Returnable_Internal_outward_list> register_lists, List<Models.Returnable_Internal_outward> registers)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string ID = "";
                    registers[0].QueryType = "Insert";
                    string response = string.Empty;
                    string response1 = string.Empty;
                    string RefDC_No = registers[0].RefDCNo;
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SP_Retunable_internal", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = registers[0].QueryType;
                        cmd.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = ID;
                        cmd.Parameters.Add("@M_From", SqlDbType.VarChar, 50).Value = registers[0].M_From;
                        cmd.Parameters.Add("@M_To", SqlDbType.VarChar, 50).Value = registers[0].M_To;
                        cmd.Parameters.Add("@VechicleNo", SqlDbType.VarChar, 50).Value = registers[0].EDR;
                        cmd.Parameters.Add("@RefDCNo", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                        cmd.Parameters.Add("@Outwardedby", SqlDbType.VarChar, 50).Value = registers[0].Outwardedby;
                        SqlParameter SQLReturn = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                        SQLReturn.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(SQLReturn);
                        cmd.ExecuteNonQuery();
                        response = SQLReturn.Value.ToString().Trim();
                        TempData["message"] = response;
                    }

                    if (response == "1" || response == "2")
                    {
                        foreach (var L in register_lists)
                        {

                            using (SqlConnection con = new SqlConnection(connectionstring))
                            {

                                L.QueryType = "Insert_list";
                                con.Open();
                                SqlCommand cmd1 = new SqlCommand("SP_Retunable_internal", con);
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = L.QueryType;
                                cmd1.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = ID;
                                cmd1.Parameters.Add("@Material_name", SqlDbType.VarChar, 50).Value = L.Material_name;
                                cmd1.Parameters.Add("@RefDCNo", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                                cmd1.Parameters.Add("@M_To", SqlDbType.VarChar, 50).Value = registers[0].M_To;
                                cmd1.Parameters.Add("@Qty", SqlDbType.VarChar, 50).Value = L.Qty;
                                cmd1.Parameters.Add("@UOM", SqlDbType.VarChar, 50).Value = L.UOM;
                                cmd1.Parameters.Add("@Remarks1", SqlDbType.VarChar, 50).Value = L.Remarks;
                                SqlParameter SQLReturn1 = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                                SQLReturn1.Direction = ParameterDirection.Output;
                                cmd1.Parameters.Add(SQLReturn1);
                                cmd1.ExecuteNonQuery();
                                response1 = SQLReturn1.Value.ToString().Trim();
                                TempData["message"] = response1;
                            }
                        }
                    }
                    else
                    {
                        return Json(response, JsonRequestBehavior.AllowGet);
                    }
                    return Json(response1, JsonRequestBehavior.AllowGet);              
               

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Add Details ";
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Insert_Update_returnable_external_inward(List<Models.Returnable_External_outward_list> register_lists, List<Models.Returnable_External_outward> registers)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string ID = "";
                    registers[0].QueryType = "Insert";
                    string response = string.Empty;
                    string response1 = string.Empty;

                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SP_Retunable_external", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = registers[0].QueryType;
                        cmd.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = ID;
                        cmd.Parameters.Add("@M_From", SqlDbType.VarChar, 50).Value = registers[0].M_From;
                        cmd.Parameters.Add("@M_To", SqlDbType.VarChar, 50).Value = registers[0].M_To;
                        cmd.Parameters.Add("@VechicleNo", SqlDbType.VarChar, 50).Value = registers[0].EDR;                        
                        cmd.Parameters.Add("@Ref_DCNo", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                        cmd.Parameters.Add("@Inwardedby", SqlDbType.VarChar, 50).Value = registers[0].Inwardedby;
                        SqlParameter SQLReturn = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                        SQLReturn.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(SQLReturn);
                        cmd.ExecuteNonQuery();
                        response = SQLReturn.Value.ToString().Trim();
                        TempData["message"] = response;
                    }

                    if (response == "1" || response == "2")
                    {
                        foreach (var L in register_lists)
                        {
                            using (SqlConnection con = new SqlConnection(connectionstring))
                            {
                                L.QueryType = "Insert_list";

                                con.Open();
                                SqlCommand cmd1 = new SqlCommand("SP_Retunable_external", con);
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = L.QueryType;
                                cmd1.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = ID;
                                cmd1.Parameters.Add("@Material_name", SqlDbType.VarChar, 50).Value = L.Material_name;
                                cmd1.Parameters.Add("@M_From", SqlDbType.VarChar, 50).Value = registers[0].M_From;
                                cmd1.Parameters.Add("@Qty", SqlDbType.VarChar, 50).Value = L.Qty;
                                cmd1.Parameters.Add("@UOM", SqlDbType.VarChar, 50).Value = L.UOM;
                                cmd1.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = L.Remarks;
                                cmd1.Parameters.Add("@Ref_DCNo", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                                SqlParameter SQLReturn1 = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                                SQLReturn1.Direction = ParameterDirection.Output;
                                cmd1.Parameters.Add(SQLReturn1);
                                cmd1.ExecuteNonQuery();
                                response1 = SQLReturn1.Value.ToString().Trim();
                                TempData["message"] = response1;
                            }
                        }
                    }
                    else
                    {
                        return Json(response, JsonRequestBehavior.AllowGet);
                    }
                    return Json(response1, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Add Details ";
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Insert_Update_nonreturnable_internal_outward(List<Models.NonReturnable_Internal_outward_list> register_lists, List<Models.NonReturnable_Internal_outward> registers)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string ID = "";
                    registers[0].QueryType = "Insert";
                    string response = string.Empty;
                    string response1 = string.Empty;
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SP_Nonretunable_internal", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = registers[0].QueryType;
                        cmd.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = ID;
                        cmd.Parameters.Add("@M_From", SqlDbType.VarChar, 50).Value = registers[0].M_From;
                        cmd.Parameters.Add("@M_To", SqlDbType.VarChar, 50).Value = registers[0].M_To;
                        cmd.Parameters.Add("@Ref_DCNo", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                        cmd.Parameters.Add("@Outwardedby", SqlDbType.VarChar, 50).Value = registers[0].Outwardedby;
                        cmd.Parameters.Add("@VechicleNo", SqlDbType.VarChar, 50).Value = registers[0].VechicleNo;
                        SqlParameter SQLReturn = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                        SQLReturn.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(SQLReturn);
                        cmd.ExecuteNonQuery();
                        response = SQLReturn.Value.ToString().Trim();
                        TempData["message"] = response;
                    }

                    if (response == "1" || response == "2")
                    {
                        foreach (var L in register_lists)
                        {
                            using (SqlConnection con = new SqlConnection(connectionstring))
                            {
                                L.QueryType = "Insert_list";
                                con.Open();
                                SqlCommand cmd1 = new SqlCommand("SP_Nonretunable_internal", con);
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = L.QueryType;
                                cmd1.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = ID;
                                cmd1.Parameters.Add("@Material_name", SqlDbType.VarChar, 50).Value = L.Material_name;
                                cmd1.Parameters.Add("@Qty", SqlDbType.VarChar, 50).Value = L.Qty;
                                cmd1.Parameters.Add("@UOM", SqlDbType.VarChar, 50).Value = L.UOM;
                                cmd1.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = L.Remarks;
                                cmd1.Parameters.Add("@Ref_DCNo", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                                SqlParameter SQLReturn1 = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                                SQLReturn1.Direction = 
                                    ParameterDirection.Output;
                                cmd1.Parameters.Add(SQLReturn1);
                                cmd1.ExecuteNonQuery();
                                response1 = SQLReturn1.Value.ToString().Trim();
                                TempData["message"] = response1;
                            }
                        }
                    }
                    else
                    {
                        return Json(response, JsonRequestBehavior.AllowGet);
                    }
                    return Json(response1, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Add Details ";
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Insert_Update_nonreturnable_external_inward(List<Models.NonReturnable_external_outward_list> register_lists, List<Models.NonReturnable_external_outward> registers)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string ID = "";
                    registers[0].QueryType = "Insert";
                    string response = string.Empty;
                    string response1 = string.Empty;

                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SP_NonRetunable_external", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = registers[0].QueryType;
                        cmd.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = ID;
                        cmd.Parameters.Add("@M_From", SqlDbType.VarChar, 50).Value = registers[0].M_From;
                        cmd.Parameters.Add("@M_To", SqlDbType.VarChar, 50).Value = registers[0].M_To;
                        cmd.Parameters.Add("@Ref_DCNo", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                        cmd.Parameters.Add("@Inwardedby", SqlDbType.VarChar, 50).Value = registers[0].Inwardedby;
                        cmd.Parameters.Add("@Timein", SqlDbType.VarChar, 50).Value = registers[0].Timein;
                        cmd.Parameters.Add("@VechicleNo", SqlDbType.VarChar, 50).Value = registers[0].VechicleNo;
                        SqlParameter SQLReturn = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                        SQLReturn.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(SQLReturn);
                        cmd.ExecuteNonQuery();
                        response = SQLReturn.Value.ToString().Trim();
                        TempData["message"] = response;
                    }

                    if (response == "1" || response == "2")
                    {
                        foreach (var L in register_lists)
                        {
                            using (SqlConnection con = new SqlConnection(connectionstring))
                            {
                                L.QueryType = "Insert_list";

                                con.Open();
                                SqlCommand cmd1 = new SqlCommand("SP_NonRetunable_external", con);
                                cmd1.CommandType = CommandType.StoredProcedure;
                                cmd1.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = L.QueryType;
                                cmd1.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = ID;
                                cmd1.Parameters.Add("@Material_name", SqlDbType.VarChar, 50).Value = L.Material_name;
                                cmd1.Parameters.Add("@Qty", SqlDbType.VarChar, 50).Value = L.Qty;
                                cmd1.Parameters.Add("@UOM", SqlDbType.VarChar, 50).Value = L.UOM;
                                cmd1.Parameters.Add("@Ref_DCNo", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                                cmd1.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = L.Remarks;
                                SqlParameter SQLReturn1 = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                                SQLReturn1.Direction = ParameterDirection.Output;
                                cmd1.Parameters.Add(SQLReturn1);
                                cmd1.ExecuteNonQuery();
                                response1 = SQLReturn1.Value.ToString().Trim();
                                TempData["message"] = response1;
                            }
                        }
                    }
                    else
                    {
                        return Json(response, JsonRequestBehavior.AllowGet);
                    }
                    return Json(response1, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Add Details ";
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public JsonResult GetVendors()
        {
            List<Models.Vendor> vendors = new List<Models.Vendor>();

            string query = string.Format("SELECT  [Id], [Customer_name]" +
                " FROM [dbo].[tbl_Vendor] ");

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();
                    while (reader.Read())
                    {
                        vendors.Add(
                            new Models.Vendor
                            {
                                Id = reader.GetValue(0).ToString(),
                                Customer_name = reader.GetValue(1).ToString()
                            }
                        );
                    }
                }
            }

            return Json(vendors, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult daywise_in_and_out()
        {
            List<Models.summary> vendors = new List<Models.summary>();

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "daywise_inandout";               
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = new List<Models.summary>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(item: new Models.summary
                    {
                        Overalldc = Convert.ToString(row["Overalldc"]),
                        Overallreceived = Convert.ToString(row["Overallreceived"]),                        
                        Overalldc1 = Convert.ToString(row["Overalldc1"]),
                        Overallreceived1 = Convert.ToString(row["Overallreceived1"]),                       
                        Overalldc2 = Convert.ToString(row["Overalldc2"]),
                        Overallreceived2 = Convert.ToString(row["Overallreceived2"]),
                        Overalldc3 = Convert.ToString(row["Overalldc3"]),
                        Overallreceived3 = Convert.ToString(row["Overallreceived3"]),

                        Overallreceiveddc = Convert.ToString(row["Overallreceiveddc"]),
                        Overalloutwarddc = Convert.ToString(row["Overalloutwarddc"]),

                        Overallreceiveddc1 = Convert.ToString(row["Overallreceiveddc1"]),
                        Overallinwarddc1 = Convert.ToString(row["Overallinwarddc1"]),

                    });
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
       

        [HttpPost]
        public JsonResult TotalsummaryLineitem()
        {
            List<Models.summary> vendors = new List<Models.summary>();

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Todaysummarylineitem";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = new List<Models.summary>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(item: new Models.summary
                    {
                        Overalldc = Convert.ToString(row["Overalldc"]),
                        Overallreceived = Convert.ToString(row["Overallreceived"]),

                    });
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Fullyopen_reint(string monthyear, string year)
        {
            List<Models.summary> vendors = new List<Models.summary>();

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "fullyopen_reint";
                cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = year;
                cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = monthyear;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = new List<Models.summary>();
               
                    result.Add(item: new Models.summary
                    {
                        Overallfullyopen = Convert.ToString(ds.Tables[0].Rows.Count),
                        Overall_monthfullyopen = Convert.ToString(ds.Tables[1].Rows.Count),
                        Overall_yearfullyopen = Convert.ToString(ds.Tables[2].Rows.Count),
                        Overall_monthpartiallypending = Convert.ToString(ds.Tables[3].Rows.Count),
                        Overall_yearpartiallypending = Convert.ToString(ds.Tables[4].Rows.Count),
                        Overall_monthfullyopen1 = Convert.ToString(ds.Tables[5].Rows.Count),
                        Overall_yearfullyopen1 = Convert.ToString(ds.Tables[6].Rows.Count),
                        Overall_monthpartiallypending1 = Convert.ToString(ds.Tables[7].Rows.Count),
                        Overall_yearpartiallypending1 = Convert.ToString(ds.Tables[8].Rows.Count),
                    });
                

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Partiallypending_reint()
        {
            List<Models.summary> vendors = new List<Models.summary>();

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Overallpartiallypending";                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = new List<Models.summary>();
               
                    result.Add(item: new Models.summary
                    {
                        Overallpartiallypending = Convert.ToString(ds.Tables[0].Rows.Count),                        

                    });
                

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public JsonResult Fullyopen_reext()
        {
            List<Models.summary> vendors = new List<Models.summary>();

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "fullyopen_reext";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = new List<Models.summary>();               
                    result.Add(item: new Models.summary
                    {
                        Overallfullyopen1 = Convert.ToString(ds.Tables[0].Rows.Count),
                       

                    });
                

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Partiallypending_reext()
        {
            List<Models.summary> vendors = new List<Models.summary>();

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Overallpartiallypendingext";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = new List<Models.summary>();
                
                    result.Add(item: new Models.summary
                    {
                        Overallpartiallypending1 = Convert.ToString(ds.Tables[0].Rows.Count),
                        

                    });
                

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }



        [HttpPost]
        public JsonResult overall_reint()
        {
            List<Models.summary> vendors = new List<Models.summary>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Overallreint";
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = new List<Models.summary>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(item: new Models.summary
                    {
                        Overalldc = Convert.ToString(row["Overalldc"]),
                        Overallreceived = Convert.ToString(row["Overallreceived"]),                                         
                        Overalldc1 = Convert.ToString(row["Overalldc1"]),
                        Overallreceived1 = Convert.ToString(row["Overallreceived1"]),                       
                        Overalldc2 = Convert.ToString(row["Overalldc2"]),
                        Overallreceived2 = Convert.ToString(row["Overallreceived2"]),
                        Overalldc3 = Convert.ToString(row["Overalldc3"]),
                        Overallreceived3 = Convert.ToString(row["Overallreceived3"]),

                    });
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public JsonResult overall_monthwisereint(string monthyear)
        {
            List<Models.summary> vendors = new List<Models.summary>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Overallreint_month";
                cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = monthyear;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = new List<Models.summary>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(item: new Models.summary
                    {
                        Overalldc = Convert.ToString(row["Overalldc"]),
                        Overallreceived = Convert.ToString(row["Overallreceived"]),
                       
                        Overalldc1 = Convert.ToString(row["Overalldc1"]),
                        Overallreceived1 = Convert.ToString(row["Overallreceived1"]),
                        
                        Overalldc2 = Convert.ToString(row["Overalldc2"]),
                        Overallreceived2 = Convert.ToString(row["Overallreceived2"]),
                        Overalldc3 = Convert.ToString(row["Overalldc3"]),
                        Overallreceived3 = Convert.ToString(row["Overallreceived3"]),

                    });
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult overall_yearwisereint(string year)
        {
            List<Models.summary> vendors = new List<Models.summary>();
            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Overallreint_year";
                cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = year;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = new List<Models.summary>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(item: new Models.summary
                    {
                        Overalldc = Convert.ToString(row["Overalldc"]),
                        Overallreceived = Convert.ToString(row["Overallreceived"]),
                       
                        Overalldc1 = Convert.ToString(row["Overalldc1"]),
                        Overallreceived1 = Convert.ToString(row["Overallreceived1"]),
                        
                        Overalldc2 = Convert.ToString(row["Overalldc2"]),
                        Overallreceived2 = Convert.ToString(row["Overallreceived2"]),
                        Overalldc3 = Convert.ToString(row["Overalldc3"]),
                        Overallreceived3 = Convert.ToString(row["Overallreceived3"]),

                    });
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult GetUOMs()
        {

            List<Models.UOM_list> uoms = new List<Models.UOM_list>();

            string query = string.Format("SELECT  [Id], [UOM]" +
                " FROM [dbo].[tbl_uom] ");

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    con.Open();
                    SqlDataReader reader = cmd.ExecuteReader();

                    while (reader.Read())
                    {
                        uoms.Add(
                            new Models.UOM_list
                            {
                                Id = reader.GetValue(0).ToString(),
                                UOM = reader.GetValue(1).ToString()
                            }
                        );
                    }
                }
            }

            return Json(uoms, JsonRequestBehavior.AllowGet);
        }

        public ActionResult Get_reintDCNO()
        {
            if (Session["UserName"] != null)
            {
                List<Models.Returnable_Internal_outward> result = new List<Models.Returnable_Internal_outward>();
                string query = "select DISTINCT Ref_DCNo from tbl_Returnable_internal";

                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            result.Add(new Models.Returnable_Internal_outward
                            {
                                
                                RefDCNo = reader.GetValue(0).ToString()
                            }
                            );
                        }
                    }
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Get_reextDCNO()
        {
            if (Session["UserName"] != null)
            {
                List<Models.Returnable_Internal_outward> result = new List<Models.Returnable_Internal_outward>();
                string query = "select DISTINCT Ref_DCNo from tbl_Returnable_external";

                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    using (SqlCommand cmd = new SqlCommand(query, con))
                    {
                        con.Open();
                        SqlDataReader reader = cmd.ExecuteReader();

                        while (reader.Read())
                        {
                            result.Add(new Models.Returnable_Internal_outward
                            {
                                
                                RefDCNo = reader.GetValue(0).ToString()
                            }
                            );
                        }
                    }
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public JsonResult Get_reintvendor(string RefDCNo)
        {
            List<Models.Returnable_Internal_outward> vendors = new List<Models.Returnable_Internal_outward>();

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_vendor";
                cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = RefDCNo;                
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = new List<Models.Returnable_Internal_outward>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(item: new Models.Returnable_Internal_outward
                    {
                       id= Convert.ToString(row["Id"]),
                        M_To = Convert.ToString(row["M_To"])
                    });
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult Get_reextvendor(string RefDCNo)
        {
            List<Models.NonReturnable_external_outward> vendors = new List<Models.NonReturnable_external_outward>();
            

            using (SqlConnection con = new SqlConnection(connectionstring))
            {
                con.Open();
                DataSet ds = new DataSet();
                SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                cmd.CommandType = CommandType.StoredProcedure;
                cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_vendorext";
                cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = RefDCNo;
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(ds);
                var result = new List<Models.Returnable_External_outward>();
                foreach (DataRow row in ds.Tables[0].Rows)
                {
                    result.Add(item: new Models.Returnable_External_outward
                    {
                        id = Convert.ToString(row["Id"]),
                        M_From = Convert.ToString(row["M_From"]),

                    });
                }

                return Json(result, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult get_inward_details(List<Models.Returnable_Internal_outward> registers)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_Re_internal_details";
                    cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                    cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = registers[0].id;
                    SqlDataReader sdr = cmd.ExecuteReader();
                    List < Models.Returnable_Internal_outward > r_inward= new List<Models.Returnable_Internal_outward>();                    
                    while(sdr.Read())
                    {
                        r_inward.Add(new Models.Returnable_Internal_outward
                        {
                            M_From = sdr.GetValue(2).ToString(),
                            M_To = sdr.GetValue(3).ToString(),
                            EDR = sdr.GetValue(4).ToString(),                            
                            RefDCNo = sdr.GetValue(5).ToString(),
                            Timeout = sdr.GetValue(6).ToString(),                           
                            Outwardedby = sdr.GetValue(7).ToString(),
                                                   
                        });
                    }
                    return Json(r_inward, JsonRequestBehavior.AllowGet);
                }             
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]
        public JsonResult get_inward_details_list(List<Models.Returnable_Internal_outward> registers)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_Re_internal_list";
                    cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                    cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = registers[0].M_To;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    List<Models.Returnable_internal_inwardlist> r_inwardlist = new List<Models.Returnable_internal_inwardlist>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        r_inwardlist.Add(item: new Models.Returnable_internal_inwardlist
                        {
                            Material_name = Convert.ToString(row["Material_name"]),
                            Qty = Convert.ToString(row["Qty"]),
                            UOM = Convert.ToString(row["UOM"]),
                            PendingQty = Convert.ToString(row["Pending_qty"]),
                        });
                    }
                    return Json(r_inwardlist,JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }


        [HttpPost]
        public ActionResult Insert_Update_returnable_internal_inward(List<Models.Returnable_internal_inwardlist> register_lists, List<Models.Returnable_Internal_inward> registers)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string ID = "";
                    ID = "0";
                    registers[0].QueryType = "Insert";
                    string response = string.Empty;
                    string response1 = string.Empty;
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SP_Retunable_inward", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = registers[0].QueryType;
                        cmd.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = ID;
                        cmd.Parameters.Add("@M_From", SqlDbType.VarChar, 50).Value = registers[0].M_From;
                        cmd.Parameters.Add("@M_To", SqlDbType.VarChar, 50).Value = registers[0].M_To;
                        cmd.Parameters.Add("@VechicleNo", SqlDbType.VarChar, 50).Value = registers[0].VechicleNo;
                        cmd.Parameters.Add("@RefDCNo", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                        cmd.Parameters.Add("@Timeout", SqlDbType.VarChar, 50).Value = registers[0].Timeout;
                        cmd.Parameters.Add("@Outwardedby", SqlDbType.VarChar, 50).Value = registers[0].Outwardedby;
                        cmd.Parameters.Add("@ReturnDCNo", SqlDbType.VarChar, 50).Value = registers[0].ReturnDC_No;
                        cmd.Parameters.Add("@Inwardedby", SqlDbType.VarChar, 50).Value = registers[0].Inwardedby;
                        SqlParameter SQLReturn = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                        SQLReturn.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(SQLReturn);
                        cmd.ExecuteNonQuery();
                        response = SQLReturn.Value.ToString().Trim();
                    }

                    if (response == "1" || response == "2")
                    {
                        foreach (var L in register_lists)
                        {
                            if (L.ReceivedQty != "0")
                            {
                                using (SqlConnection con = new SqlConnection(connectionstring))
                                {
                                    L.QueryType = "Insert_list";
                                    con.Open();
                                    SqlCommand cmd1 = new SqlCommand("SP_Retunable_inward", con);
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = L.QueryType;
                                    cmd1.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = ID;
                                    cmd1.Parameters.Add("@Material_name", SqlDbType.VarChar, 50).Value = L.Material_name;
                                    cmd1.Parameters.Add("@Qty", SqlDbType.VarChar, 50).Value = L.Qty;
                                    cmd1.Parameters.Add("@UOM", SqlDbType.VarChar, 50).Value = L.UOM;
                                    cmd1.Parameters.Add("@M_To", SqlDbType.VarChar, 50).Value = registers[0].M_To;
                                    cmd1.Parameters.Add("@PendingQty", SqlDbType.VarChar, 50).Value = L.PendingQty;
                                    cmd1.Parameters.Add("@RemainingQty", SqlDbType.VarChar, 50).Value = L.RemainingQty;
                                    cmd1.Parameters.Add("@ReceivedQty", SqlDbType.VarChar, 50).Value = L.ReceivedQty;
                                    cmd1.Parameters.Add("@RefDCNo", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                                    cmd1.Parameters.Add("@Remarks2", SqlDbType.VarChar, 50).Value = L.Remarks;
                                    SqlParameter SQLReturn1 = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                                    SQLReturn1.Direction = ParameterDirection.Output;
                                    cmd1.Parameters.Add(SQLReturn1);
                                    cmd1.ExecuteNonQuery();
                                    response1 = SQLReturn1.Value.ToString().Trim();
                                    TempData["message"] = response1;
                                }
                            }
                        }
                    }
                    return Json(response1, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Add Details ";
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        [HttpPost]
        public ActionResult Insert_Update_returnable_external_return(List<Models.Returnable_external_returnlist> register_lists, List<Models.Returnable_external_return> registers)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string ID = "";
                    ID = "0";
                    registers[0].QueryType = "Insert";
                    string response = string.Empty;
                    string response1 = string.Empty;
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SP_Retunable_external_inward", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = registers[0].QueryType;
                        cmd.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = ID;
                        cmd.Parameters.Add("@M_From", SqlDbType.VarChar, 50).Value = registers[0].M_From;
                        cmd.Parameters.Add("@M_To", SqlDbType.VarChar, 50).Value = registers[0].M_To;
                        cmd.Parameters.Add("@VechicleNo", SqlDbType.VarChar, 50).Value = registers[0].EDR;
                        cmd.Parameters.Add("@RefDCNo", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                        cmd.Parameters.Add("@Timeout", SqlDbType.VarChar, 50).Value = registers[0].Timeout;
                        cmd.Parameters.Add("@Outwardedby", SqlDbType.VarChar, 50).Value = registers[0].Outwardedby;
                        cmd.Parameters.Add("@ReturnDCNo", SqlDbType.VarChar, 50).Value = registers[0].ReturnDC_No;
                        cmd.Parameters.Add("@Inwardedby", SqlDbType.VarChar, 50).Value = registers[0].Inwardedby;
                        SqlParameter SQLReturn = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                        SQLReturn.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(SQLReturn);
                        cmd.ExecuteNonQuery();
                        response = SQLReturn.Value.ToString().Trim();
                    }

                    if (response == "1" || response == "2")
                    {
                        foreach (var L in register_lists)
                        {
                            if (L.ReturnedQty != "0")
                            {
                                using (SqlConnection con = new SqlConnection(connectionstring))
                                {
                                    L.QueryType = "Insert_list";
                                    con.Open();
                                    SqlCommand cmd1 = new SqlCommand("SP_Retunable_external_inward", con);
                                    cmd1.CommandType = CommandType.StoredProcedure;
                                    cmd1.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = L.QueryType;
                                    cmd1.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = ID;
                                    cmd1.Parameters.Add("@Material_name", SqlDbType.VarChar, 50).Value = L.Material_name;
                                    cmd1.Parameters.Add("@M_From", SqlDbType.VarChar, 50).Value = registers[0].M_From;
                                    cmd1.Parameters.Add("@Qty", SqlDbType.VarChar, 50).Value = L.Qty;
                                    cmd1.Parameters.Add("@UOM", SqlDbType.VarChar, 50).Value = L.UOM;
                                    cmd1.Parameters.Add("@PendingQty", SqlDbType.VarChar, 50).Value = L.PendingQty;
                                    cmd1.Parameters.Add("@RemainingQty", SqlDbType.VarChar, 50).Value = L.RemainingQty;
                                    cmd1.Parameters.Add("@ReturnedQty", SqlDbType.VarChar, 50).Value = L.ReturnedQty;
                                    cmd1.Parameters.Add("@RefDCNo", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                                    cmd1.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = L.Remarks;
                                    SqlParameter SQLReturn1 = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                                    SQLReturn1.Direction = ParameterDirection.Output;
                                    cmd1.Parameters.Add(SQLReturn1);
                                    cmd1.ExecuteNonQuery();
                                    response1 = SQLReturn1.Value.ToString().Trim();
                                    TempData["message"] = response1;
                                }
                            }
                        }
                    }
                    return Json(response1, JsonRequestBehavior.AllowGet);
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Add Details ";
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public JsonResult get_outward_details(List<Models.Returnable_External_outward> registers)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_Re_external_details";
                    cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                    cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = registers[0].id;

                    SqlDataReader sdr = cmd.ExecuteReader();
                    List<Models.Returnable_External_outward> r_outward = new List<Models.Returnable_External_outward>();
                     while(sdr.Read())
                    {
                        r_outward.Add(new Models.Returnable_External_outward
                        {
                            M_From = sdr.GetValue(2).ToString(),
                            M_To = sdr.GetValue(3).ToString(),
                            EDR = sdr.GetValue(4).ToString(),
                            RefDCNo = sdr.GetValue(5).ToString(),
                            Timein = sdr.GetValue(6).ToString(),
                            Inwardedby = sdr.GetValue(7).ToString(),
                        });
                    }
                    return Json(r_outward, JsonRequestBehavior.AllowGet);
                }            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }

        public JsonResult get_outward_details_list(List<Models.Returnable_External_outward> registers)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data",con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_Re_external_list";
                    cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                    cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = registers[0].M_From;

                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    List<Models.Returnable_external_returnlist> r_inwardlist = new List<Models.Returnable_external_returnlist>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        r_inwardlist.Add(item: new Models.Returnable_external_returnlist
                        {
                            Material_name = Convert.ToString(row["Material_name"]),
                            Qty = Convert.ToString(row["Qty"]),
                            UOM = Convert.ToString(row["UOM"]),
                            PendingQty = Convert.ToString(row["Pending_qty"]),
                        });
                    }
                    return Json(r_inwardlist, JsonRequestBehavior.AllowGet);

                }
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }

        [HttpPost]

        public ActionResult Addcustomer(string Customername)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        string response = string.Empty;
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SP_vendor", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Insert";
                        cmd.Parameters.Add("@Customername", SqlDbType.VarChar, 50).Value = Customername;
                        SqlParameter SQLReturn = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                        SQLReturn.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(SQLReturn);
                        cmd.ExecuteNonQuery();
                        response = SQLReturn.Value.ToString().Trim();
                        return Json(Customername, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Add vendor Details ";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public ActionResult Adduom(string UOM)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string response = string.Empty;
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SP_adduom", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Insert";
                        cmd.Parameters.Add("@UOM", SqlDbType.VarChar, 50).Value = UOM;
                        SqlParameter SQLReturn = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                        SQLReturn.Direction = ParameterDirection.Output;
                        cmd.Parameters.Add(SQLReturn);
                        cmd.ExecuteNonQuery();
                        response = SQLReturn.Value.ToString().Trim();
                        return Json(response, JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Add vendor Details ";
                    return View();
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        [HttpPost]
        public JsonResult Get_internal_master10(string x)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Re_int10";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    var result = new List<Models.Returnable_internal_pending>();

                    var time_in = "";

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        if (Convert.ToString(row["Time_in"]) != "")
                        {
                            time_in = Convert.ToString(row["Time_in"]);
                        }
                        else
                        {
                            time_in = "";
                        }
                        result.Add(item: new Models.Returnable_internal_pending
                        {
                            id = Convert.ToString(row["Id"]),                            
                            Transaction_id = Convert.ToString(row["Transaction_id"]),
                            M_From = Convert.ToString(row["M_From"]),
                            M_To = Convert.ToString(row["M_To"]),
                            EDR = Convert.ToString(row["VechicleNo"]),
                            Material_name = Convert.ToString(row["Material_name"]),
                            UOM = Convert.ToString(row["UOM"]),
                            RefDCNo = Convert.ToString(row["Ref_DCNo"]),
                            Timeout = Convert.ToString(row["Timeout"]),
                            Outwardedby = Convert.ToString(row["Outwardedby"]),
                            ReceivedQty = Convert.ToString(row["ReceivedQty"]),
                            Qty = Convert.ToString(row["Qty"]),
                            Remarks = Convert.ToString(row["Remarks1"]),
                            RemainingQty = Convert.ToString(row["RemainingQty"]),
                            PendingQty = Convert.ToString(row["PendingQty"]),                           
                            Timein = time_in,
                            ReturnDCNo = Convert.ToString(row["ReturnDCNo"]),
                            Inwardedby = Convert.ToString(row["Inwardedby"]),
                            Remarks2 = Convert.ToString(row["Remarks2"]),
                            Dept = Convert.ToString(row["Dept"]),                            
                        });
                    }
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Get_external_master10()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Re_ext10";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    var result = new List<Models.Returnable_internal_pending>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        result.Add(item: new Models.Returnable_internal_pending
                        {
                           
                            Transaction_id = Convert.ToString(row["Transaction_id"]),
                            M_From = Convert.ToString(row["M_From"]),
                            M_To = Convert.ToString(row["M_To"]),
                            EDR = Convert.ToString(row["VechicleNo"]),
                            Material_name = Convert.ToString(row["Material_name"]),
                            UOM = Convert.ToString(row["UOM"]),
                            RefDCNo = Convert.ToString(row["Ref_DCNo"]),
                            Timeout = Convert.ToString(row["Timeout"]),
                            Outwardedby = Convert.ToString(row["Outwardedby"]),
                            ReturnedQty = Convert.ToString(row["ReturnedQty"]),
                            Qty = Convert.ToString(row["Qty"]),
                            Remarks = Convert.ToString(row["Remarks1"]),
                            RemainingQty = Convert.ToString(row["RemainingQty"]),
                            Timein = Convert.ToString(row["Timein"]),
                            ReturnDCNo = Convert.ToString(row["ReturnDCNo"]),
                            Inwardedby = Convert.ToString(row["Inwardedby"]),
                            Remarks2 = Convert.ToString(row["Remarks2"]),
                            Dept = Convert.ToString(row["Dept"]),                           
                        }); ;
                    }
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }

        }

        [HttpPost]
        public JsonResult Get_noninternal_master10()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();

                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "get_Return_noninternal_trans10";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    var result = new List<Models.Returnable_internal_pending>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        result.Add(item: new Models.Returnable_internal_pending
                        {                           
                            id2 = Convert.ToString(row["Sort"]),
                            Transaction_id = Convert.ToString(row["Transaction_id"]),
                            M_From = Convert.ToString(row["M_From"]),
                            M_To = Convert.ToString(row["M_To"]),
                            Material_name = Convert.ToString(row["Material_name"]),
                            UOM = Convert.ToString(row["UOM"]),
                            RefDCNo = Convert.ToString(row["Ref_DCNo"]),
                            Timeout = Convert.ToString(row["Timeout"]),
                            Outwardedby = Convert.ToString(row["Outwardedby"]),
                            Dept = Convert.ToString(row["Dept"]),
                            Qty = Convert.ToString(row["Qty"]),
                            Remarks = Convert.ToString(row["Remarks"]),
                            VechicleNo = Convert.ToString(row["VechicleNo"])

                        }); ;
                    }
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }
        [HttpPost]
        public JsonResult Get_nonexternal_master10()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "get_Return_nonexternal_trans10";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    var result = new List<Models.Returnable_internal_pending>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        result.Add(item: new Models.Returnable_internal_pending
                        {                           
                            id2 = Convert.ToString(row["Sort"]),
                            Transaction_id = Convert.ToString(row["Transaction_id"]),
                            M_From = Convert.ToString(row["M_From"]),
                            M_To = Convert.ToString(row["M_To"]),
                            Material_name = Convert.ToString(row["Material_name"]),
                            UOM = Convert.ToString(row["UOM"]),
                            RefDCNo = Convert.ToString(row["Ref_DCNo"]),
                            Timein = Convert.ToString(row["Timein"]),
                            Inwardedby = Convert.ToString(row["Inwardedby"]),
                            VechicleNo = Convert.ToString(row["VechicleNo"]),
                            Qty = Convert.ToString(row["Qty"]),
                            Remarks = Convert.ToString(row["Remarks"]),
                            Dept = Convert.ToString(row["Dept"])

                        }); ;
                    }
                    return Json(result, JsonRequestBehavior.AllowGet);
                }
            }
            catch (Exception ex)
            {
                return Json(ex, JsonRequestBehavior.AllowGet);
            }
        }

    }

}