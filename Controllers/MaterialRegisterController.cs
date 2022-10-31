using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.IO;
using System.Data;
using ClosedXML.Excel;
using System.Data.SqlClient;
using System.Configuration;

namespace MaterialGateRegister.Controllers
{
    public class MaterialRegisterController : Controller
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();
        // GET: MaterialRegister
        public ActionResult Pending_Register()
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
        public JsonResult get_Pending_Returnable_internal()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_Pending_Returnable_internal";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    List<Models.Returnable_Internal_outward> r_inwardlist = new List<Models.Returnable_Internal_outward>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        r_inwardlist.Add(item: new Models.Returnable_Internal_outward
                        {
                            Transaction_id = Convert.ToString(row["Transaction_id"]),
                            M_From = Convert.ToString(row["M_From"]),
                            M_To = Convert.ToString(row["M_To"]),
                            EDR = Convert.ToString(row["VechicleNo"]),
                            RefDCNo = Convert.ToString(row["Ref_DCNo"]),
                            Timeout = Convert.ToString(row["Timeout"]),
                            Outwardedby = Convert.ToString(row["Outwardedby"]),                       
                            Pending_item=Convert.ToString(row["Pending_item"])
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
        public JsonResult Show_Pending_Returnable_internal()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Show_Pending_Returnable_internal";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    List<Models.Returnable_Internal_outward> r_inwardlist = new List<Models.Returnable_Internal_outward>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        r_inwardlist.Add(item: new Models.Returnable_Internal_outward
                        {
                            Transaction_id = Convert.ToString(row["Transaction_id"]),
                            M_From = Convert.ToString(row["M_From"]),
                            M_To = Convert.ToString(row["M_To"]),
                            EDR = Convert.ToString(row["VechicleNo"]),
                            RefDCNo = Convert.ToString(row["Ref_DCNo"]),
                            Timeout = Convert.ToString(row["Timeout"]),
                            Outwardedby = Convert.ToString(row["Outwardedby"]),                            
                            Pending_item=Convert.ToString(row["Pending_item"])
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
                            ReceivedQty = Convert.ToString(row["Received_qty"]),
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
        public JsonResult get_Pending_Returnable_external()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "get_Pending_Returnable_external";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    List<Models.Returnable_External_outward> r_inwardlist = new List<Models.Returnable_External_outward>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        r_inwardlist.Add(item: new Models.Returnable_External_outward
                        {
                            Transaction_id = Convert.ToString(row["Transaction_id"]),
                            M_From = Convert.ToString(row["M_From"]),
                            M_To = Convert.ToString(row["M_To"]),
                            EDR = Convert.ToString(row["VechicleNo"]),
                            RefDCNo = Convert.ToString(row["Ref_DCNo"]),
                            Timein = Convert.ToString(row["Timein"]),
                            Inwardedby = Convert.ToString(row["Inwardedby"]),                           
                            Pending_item=Convert.ToString(row["Pending_item"])
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
        public JsonResult Show_Pending_Returnable_external()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "show_Pending_Returnable_external";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    List<Models.Returnable_External_outward> r_inwardlist = new List<Models.Returnable_External_outward>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        r_inwardlist.Add(item: new Models.Returnable_External_outward
                        {
                            Transaction_id = Convert.ToString(row["Transaction_id"]),
                            M_From = Convert.ToString(row["M_From"]),
                            M_To = Convert.ToString(row["M_To"]),
                            EDR = Convert.ToString(row["VechicleNo"]),
                            RefDCNo = Convert.ToString(row["Ref_DCNo"]),
                            Timein = Convert.ToString(row["Timein"]),
                            Inwardedby = Convert.ToString(row["Inwardedby"]),                           
                            Pending_item=Convert.ToString(row["Pending_item"])
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
        public JsonResult get_outward_details_list(List<Models.Returnable_External_outward> registers)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_Re_external_list";
                    cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = registers[0].RefDCNo;
                    cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = registers[0].M_From;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    List<Models.Returnable_External_outward_list> r_inwardlist = new List<Models.Returnable_External_outward_list>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        r_inwardlist.Add(item: new Models.Returnable_External_outward_list
                        {
                            Material_name = Convert.ToString(row["Material_name"]),
                            Qty = Convert.ToString(row["Qty"]),
                            UOM = Convert.ToString(row["UOM"]),                          
                            Send_qty = Convert.ToString(row["Send_qty"]),
                            Pending_qty = Convert.ToString(row["Pending_qty"])
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

        public ActionResult Get_daywise_data(string Fromdate, string Todate)
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
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "day_intpen";
                        cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = Fromdate;
                        cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = Todate;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        List<Models.Returnable_Internal_outward> r_inwardlist = new List<Models.Returnable_Internal_outward>();

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            r_inwardlist.Add(item: new Models.Returnable_Internal_outward
                            {
                                Transaction_id = Convert.ToString(row["Transaction_id"]),
                                M_From = Convert.ToString(row["M_From"]),
                                M_To = Convert.ToString(row["M_To"]),
                                EDR = Convert.ToString(row["VechicleNo"]),
                                RefDCNo = Convert.ToString(row["Ref_DCNo"]),
                                Timeout = Convert.ToString(row["Timeout"]),
                                Outwardedby = Convert.ToString(row["Outwardedby"]),                               
                                Pending_item=Convert.ToString(row["Pending_item"])
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
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Get_daywise_externalpending(string Fromdate, string Todate)
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
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "day_extpen";
                        cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = Fromdate;
                        cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = Todate;
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);
                        List<Models.Returnable_External_outward> r_inwardlist = new List<Models.Returnable_External_outward>();

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            r_inwardlist.Add(item: new Models.Returnable_External_outward
                            {
                                Transaction_id = Convert.ToString(row["Transaction_id"]),
                                M_From = Convert.ToString(row["M_From"]),
                                M_To = Convert.ToString(row["M_To"]),
                                EDR = Convert.ToString(row["VechicleNo"]),
                                RefDCNo = Convert.ToString(row["Ref_DCNo"]),
                                Timein = Convert.ToString(row["Timein"]),
                                Inwardedby = Convert.ToString(row["Inwardedby"]),                                
                                Pending_item=Convert.ToString(row["Pending_item"])
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
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Export_Returnable_internal()
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();                   
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_Pending_Returnable_internaldownload";
                   
                    SqlDataAdapter da = new SqlDataAdapter(cmd);             
                    da.Fill(ds);
                    ds.Tables[0].TableName = "Returnable Internal";
                    ds.Tables[1].TableName = "Returnable Internal List";                  

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (DataTable dt in ds.Tables)
                        {
                            wb.Worksheets.Add(dt);
                        }
                        
                        wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wb.Style.Font.Bold = true;
                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename= Returnable_internal_details.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Pending_Register", "MaterialRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }       
        /// Export Returnable External
        
        public ActionResult Exportpending_Returnable_internal()
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {

                    con.Open();                    
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Show_Pending_Returnable_internal";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    ds.Tables[0].TableName = "Pending Returnable Internal";
                    ds.Tables[1].TableName = "Pending Returnable List";
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (DataTable dt in ds.Tables)
                        {
                            wb.Worksheets.Add(dt);
                        }

                        wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wb.Style.Font.Bold = true;

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename= Pending_Returnable_internal_details.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Pending_Register", "MaterialRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Exportdatewise_Returnable_internal(Models.Datewise d)
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {

                    con.Open();                    
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "day_intpen";
                    cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = d.Fromdate;
                    cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = d.Todate;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    ds.Tables[0].TableName = "Daywise Returnable Internal";
                    ds.Tables[1].TableName = "Daywise Returnable List";

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (DataTable dt in ds.Tables)
                        {
                            wb.Worksheets.Add(dt);
                        }
                        wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wb.Style.Font.Bold = true;

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename= Datewise_Returnable_internal_details.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Pending_Register", "MaterialRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }       
        /// Export Returnable External       
        public ActionResult Export_Returnable_external()
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {

                    con.Open();
                    DataSet ds = new DataSet();                   
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "get_Pending_Returnable_externaldownload";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    ds.Tables[0].TableName = "Returnable External";
                    ds.Tables[1].TableName = "Returnable External List";
                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (DataTable dt in ds.Tables)
                        {
                            wb.Worksheets.Add(dt);
                        }

                        wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wb.Style.Font.Bold = true;

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename= Returnable_external_details.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Pending_Register", "MaterialRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Exportpending_Returnable_external()
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {

                    con.Open();                   
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "show_Pending_Returnable_external";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);                   
                    da.Fill(ds);
                    ds.Tables[0].TableName = "Pending Returnable External";
                    ds.Tables[1].TableName = "Pending Returnable List";

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        foreach (DataTable dt in ds.Tables)
                        {
                            wb.Worksheets.Add(dt);
                        }

                        wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wb.Style.Font.Bold = true;

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=Pending_Returnable_external_details.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Pending_Register", "MaterialRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Exportdatewise_Returnable_external(Models.Datewise d)
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {

                    con.Open();                   
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "day_extpen";
                    cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = d.Fromdate;
                    cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = d.Todate;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);                  
                    da.Fill(ds);
                    ds.Tables[0].TableName = "Daywise Returnable External";
                    ds.Tables[1].TableName = "Daywise Returnable List";

                    using (XLWorkbook wb = new XLWorkbook())
                    {                        
                        foreach (DataTable dt in ds.Tables)
                        {
                            wb.Worksheets.Add(dt);
                        }
                        wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wb.Style.Font.Bold = true;

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename=Datewise_Returnable_external_details.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Pending_Register", "MaterialRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
        /// Export Returnable External
        
        public ActionResult Export_Returnable_internal_list(Models.Returnable_External_outward S)
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {

                    con.Open();
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_Re_internal_list";
                    cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = S.RefDCNo;
                    cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = S.M_To;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt.TableName = "Returnable_internal_list";
                    da.Fill(dt);

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wb.Style.Font.Bold = true;

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename= Returnable_internal_list.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Pending_Register", "MaterialRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }
               
        /// Export Returnable External
       
        public ActionResult Export_Returnable_external_list(Models.Returnable_External_outward S)
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {

                    con.Open();
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Get_Re_external_list";
                    cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = S.RefDCNo;
                    cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = S.M_From;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt.TableName = "Returnable_external_list";
                    da.Fill(dt);

                    using (XLWorkbook wb = new XLWorkbook())
                    {
                        wb.Worksheets.Add(dt);
                        wb.Style.Alignment.Horizontal = XLAlignmentHorizontalValues.Center;
                        wb.Style.Font.Bold = true;

                        Response.Clear();
                        Response.Buffer = true;
                        Response.Charset = "";
                        Response.ContentType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                        Response.AddHeader("content-disposition", "attachment;filename= Returnable_external_list.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Pending_Register", "MaterialRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        private void releaseObject(object obj)
        {
            try
            {
                System.Runtime.InteropServices.Marshal.ReleaseComObject(obj);
                obj = null;
            }
            catch
            {
                obj = null;
            }
            finally
            {
                GC.Collect();
            }
        }

    }
}