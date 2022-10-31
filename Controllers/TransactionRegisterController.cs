using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data;
using System.Data.SqlClient;
using System.Configuration;
using ClosedXML.Excel;
using System.IO;
using MaterialGateRegister.Models;

namespace MaterialGateRegister.Controllers
{
    public class TransactionRegisterController : Controller
    {
        private string connectionstring = ConfigurationManager.ConnectionStrings["con"].ToString();
        // GET: TransactionRegister
        public ActionResult Transaction_Register()
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
        public JsonResult Get_internal_master()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                                      SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Re_int";
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
                            id2 = Convert.ToString(row["Sort"]),
                            editid = Convert.ToString(row["editid"]),
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
                            ReceivedQty1 = Convert.ToString(row["ReceivedQty1"]),
                            Timein = time_in,
                            ReturnDCNo = Convert.ToString(row["ReturnDCNo"]),
                            Inwardedby = Convert.ToString(row["Inwardedby"]),
                            Remarks2 = Convert.ToString(row["Remarks2"]),
                            Dept = Convert.ToString(row["Dept"]),
                            Lastupdated = Convert.ToString(row["Lastupdated"])
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
        public JsonResult Get_external_master()
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                                      SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Re_ext";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    var result = new List<Models.Returnable_internal_pending>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        result.Add(item: new Models.Returnable_internal_pending
                        {
                            id = Convert.ToString(row["Id"]),
                            id2 = Convert.ToString(row["Sort"]),
                            editid = Convert.ToString(row["editid"]),
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
                            ReceivedQty1 = Convert.ToString(row["ReceivedQty1"]),
                            Lastupdated = Convert.ToString(row["Lastupdated"]),
                            PendingQty = Convert.ToString(row["PendingQty"])
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
        public JsonResult Get_noninternal_master()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                   
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "get_Return_noninternal_trans";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    var result = new List<Models.Returnable_internal_pending>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        result.Add(item: new Models.Returnable_internal_pending
                        {
                            id = Convert.ToString(row["Id"]),
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
        public JsonResult Get_nonexternal_master()
        {

            try
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataSet ds = new DataSet();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "get_Return_nonexternal_trans";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    da.Fill(ds);
                    var result = new List<Models.Returnable_internal_pending>();

                    foreach (DataRow row in ds.Tables[0].Rows)
                    {
                        result.Add(item: new Models.Returnable_internal_pending
                        {
                            id = Convert.ToString(row["Id"]),
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

       
        public ActionResult Update_returnable_internal_outward(Models.Returnable_internal_inwardlist L)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string QueryType = string.Empty;
                    string response = string.Empty;
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        if (L.Transaction_id == "RIO")
                        {
                            QueryType = "Update";
                        }
                        else
                        {
                            QueryType = "Updatelist";
                        }
                        con.Open();
                        SqlCommand cmd1 = new SqlCommand("SP_Retunable_inward", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = QueryType;
                        cmd1.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = L.Id;
                        cmd1.Parameters.Add("@Id2", SqlDbType.VarChar, 50).Value = L.Id2;
                        cmd1.Parameters.Add("@Material_name", SqlDbType.VarChar, 50).Value = L.Material_name;
                        cmd1.Parameters.Add("@Qty", SqlDbType.VarChar, 50).Value = L.Qty;
                        cmd1.Parameters.Add("@Remarks2", SqlDbType.VarChar, 50).Value = L.Remarks2;
                        cmd1.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = L.Remarks;
                        cmd1.Parameters.Add("@UOM", SqlDbType.VarChar, 50).Value = L.UOM;
                        cmd1.Parameters.Add("@ReceivedQty", SqlDbType.VarChar, 50).Value = L.ReceivedQty;
                        cmd1.Parameters.Add("@RemainingQty", SqlDbType.VarChar, 50).Value = L.RemainingQty;
                        cmd1.Parameters.Add("@RefDCNo", SqlDbType.VarChar, 50).Value = L.RefDCNo;
                        cmd1.Parameters.Add("@PendingQty", SqlDbType.VarChar, 50).Value = L.PendingQty;
                        SqlParameter SQLReturn1 = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                        SQLReturn1.Direction = ParameterDirection.Output;
                        cmd1.Parameters.Add(SQLReturn1);
                        cmd1.ExecuteNonQuery();
                        response = SQLReturn1.Value.ToString().Trim();
                        return Json(response, JsonRequestBehavior.AllowGet);
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Update Details ";
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Update_non_returnable_internal_outward(Models.Returnable_internal_inwardlist L)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string response = string.Empty;
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        SqlCommand cmd1 = new SqlCommand("SP_Nonretunable_internal", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Update_list";
                        cmd1.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = L.Id;
                        cmd1.Parameters.Add("@Material_name", SqlDbType.VarChar, 50).Value = L.Material_name;
                        cmd1.Parameters.Add("@Qty", SqlDbType.VarChar, 50).Value = L.Qty;
                        cmd1.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = L.Remarks;
                        cmd1.Parameters.Add("@UOM", SqlDbType.VarChar, 50).Value = L.UOM;                       
                        SqlParameter SQLReturn1 = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                        SQLReturn1.Direction = ParameterDirection.Output;
                        cmd1.Parameters.Add(SQLReturn1);
                        cmd1.ExecuteNonQuery();
                        response = SQLReturn1.Value.ToString().Trim();
                        return Json(response, JsonRequestBehavior.AllowGet);
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Update Details ";
                    return View("Index");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

      
        public ActionResult Update_returnable_external_inward(Models.Returnable_external_returnlist L)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string QueryType = string.Empty;
                    string response = string.Empty;

                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        if (L.Transaction_id == "REI")
                        {
                            QueryType = "Update";
                        }
                        else
                        {
                            QueryType = "Update_list";
                        }
                        con.Open();
                        SqlCommand cmd1 = new SqlCommand("SP_Retunable_external_inward", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = QueryType;
                        cmd1.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = L.id;
                        cmd1.Parameters.Add("@Id2", SqlDbType.VarChar, 50).Value = L.id2;
                        cmd1.Parameters.Add("@Material_name", SqlDbType.VarChar, 50).Value = L.Material_name;
                        cmd1.Parameters.Add("@Qty", SqlDbType.VarChar, 50).Value = L.Qty;
                        cmd1.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = L.Remarks2;
                        cmd1.Parameters.Add("@Remarks1", SqlDbType.VarChar, 50).Value = L.Remarks;
                        cmd1.Parameters.Add("@UOM", SqlDbType.VarChar, 50).Value = L.UOM;
                        cmd1.Parameters.Add("@ReturnedQty", SqlDbType.VarChar, 50).Value = L.ReturnedQty;
                        cmd1.Parameters.Add("@RemainingQty", SqlDbType.VarChar, 50).Value = L.RemainingQty;
                        cmd1.Parameters.Add("@RefDCNo", SqlDbType.VarChar, 50).Value = L.RefDCNo;
                        cmd1.Parameters.Add("@PendingQty", SqlDbType.VarChar, 50).Value = L.PendingQty;
                        SqlParameter SQLReturn1 = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                        SQLReturn1.Direction = ParameterDirection.Output;
                        cmd1.Parameters.Add(SQLReturn1);
                        cmd1.ExecuteNonQuery();
                        response = SQLReturn1.Value.ToString().Trim();
                        return Json(response, JsonRequestBehavior.AllowGet);
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Update Details ";
                    return View("Transaction_Register");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }


        public ActionResult Update_non_returnable_external_inward(Models.Returnable_external_returnlist L)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string response = string.Empty;

                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {

                        con.Open();
                        SqlCommand cmd1 = new SqlCommand("SP_NonRetunable_external", con);
                        cmd1.CommandType = CommandType.StoredProcedure;
                        cmd1.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Update_list";
                        cmd1.Parameters.Add("@Id", SqlDbType.VarChar, 50).Value = L.id;
                        cmd1.Parameters.Add("@Material_name", SqlDbType.VarChar, 50).Value = L.Material_name;
                        cmd1.Parameters.Add("@Qty", SqlDbType.VarChar, 50).Value = L.Qty;
                        cmd1.Parameters.Add("@Remarks", SqlDbType.VarChar, 50).Value = L.Remarks;
                        cmd1.Parameters.Add("@UOM", SqlDbType.VarChar, 50).Value = L.UOM;
                        SqlParameter SQLReturn1 = new SqlParameter("@SQLReturn", SqlDbType.NVarChar, 50);
                        SQLReturn1.Direction = ParameterDirection.Output;
                        cmd1.Parameters.Add(SQLReturn1);
                        cmd1.ExecuteNonQuery();
                        response = SQLReturn1.Value.ToString().Trim();
                        return Json(response, JsonRequestBehavior.AllowGet);
                    }

                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed to Update Details ";
                    return View("Transaction_Register");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Delete_reinter(Models.Returnable_Internal_outward s)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string QueryType = string.Empty;
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        if (s.Transaction_id == "RIO")
                        {
                            QueryType = "Delete_reintrio";
                        }
                        else
                        {
                            QueryType = "Delete_reint";

                        }
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SP_delete_settings", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = QueryType;
                        cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = s.id;
                        cmd.ExecuteNonQuery();
                        return Json("ok", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed Delete Returnable Internal Outward";
                    return View("Transaction_Register");

                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Delete_reexter(Models.Returnable_Internal_outward s)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    string QueryType = string.Empty;
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        if (s.Transaction_id == "REI")
                        {
                            QueryType = "Delete_reextrei";
                        }
                        else
                        {
                            QueryType = "Delete_reext";

                        }
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SP_delete_settings", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = QueryType;
                        cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = s.id;
                        cmd.ExecuteNonQuery();
                        return Json("ok", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed Delete Returnable External Inward";
                    return View("Transaction_Register");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }

        }

        public ActionResult Delete_nonreinter(string ID)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SP_delete_settings", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Delete_nonreint";
                        cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = ID;
                        cmd.ExecuteNonQuery();
                        return Json("ok", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed Delete NonReturnable Internal Outward";
                    return View("Transaction_Register");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Delete_nonreexter(string ID)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        con.Open();
                        SqlCommand cmd = new SqlCommand("SP_delete_settings", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Delete_nonreext";
                        cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = ID;
                        cmd.ExecuteNonQuery();
                        return Json("ok", JsonRequestBehavior.AllowGet);
                    }
                }
                catch (Exception ex)
                {
                    ViewBag.Message = "Failed Delete NonReturnable External Inward";
                    return View("Transaction_Register");
                }
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Export_returnable_internalmaster()
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {

                    con.Open();
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Re_intdownload";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt.TableName = "Returnable_internal_Master";
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
                        Response.AddHeader("content-disposition", "attachment;filename= Returnable_internal_Masterdetails.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Transaction_Register", "TransactionRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Export_returnable_externalmaster()
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Re_extdownload";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt.TableName = "Returnable_external_Master";
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
                        Response.AddHeader("content-disposition", "attachment;filename= Returnable_external_Masterdetails.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Transaction_Register", "TransactionRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Export_nonreturnable_internalmaster()
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {

                    con.Open();
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "get_Return_noninternal_trans";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt.TableName = "NonReturnable_internal_Master";
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
                        Response.AddHeader("content-disposition", "attachment;filename=NonReturnable_internal_Masterdetails.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Transaction_Register", "TransactionRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Export_nonreturnable_externalmaster()
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {

                    con.Open();
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "get_Return_nonexternal_trans";
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt.TableName = "NonReturnable_external_Master";
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
                        Response.AddHeader("content-disposition", "attachment;filename=NonReturnable_external_Masterdetails.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Transaction_Register", "TransactionRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Get_daywise_internal_data(Returnable_Internal_outward L)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        string response = string.Empty;
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = L.QueryType;
                        cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = L.FromDate;
                        cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = L.ToDate;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);

                        var result = new List<Models.Returnable_internal_pending>();

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            result.Add(item: new Models.Returnable_internal_pending
                            {
                                id = Convert.ToString(row["Id"]),
                                editid= Convert.ToString(row["editid"]),
                                Transaction_id = Convert.ToString(row["Transaction_id"]),
                                M_From = Convert.ToString(row["M_From"]),
                                M_To = Convert.ToString(row["M_To"]),
                                EDR = Convert.ToString(row["VechicleNo"]),
                                Material_name = Convert.ToString(row["Material_name"]),
                                UOM = Convert.ToString(row["UOM"]),
                                RefDCNo = Convert.ToString(row["Ref_DCNo"]),
                                Timeout = Convert.ToString(row["Time_out"]),
                                Outwardedby = Convert.ToString(row["Outwardedby"]),
                                ReceivedQty = Convert.ToString(row["ReceivedQty"]),
                                ReceivedQty1 = Convert.ToString(row["ReceivedQty1"]),
                                PendingQty = Convert.ToString(row["PendingQty"]),
                                Qty = Convert.ToString(row["Qty"]),
                                Remarks = Convert.ToString(row["Remarks1"]),
                                RemainingQty = Convert.ToString(row["RemainingQty"]),
                                Timein = Convert.ToString(row["Timein"]),
                                ReturnDCNo = Convert.ToString(row["ReturnDCNo"]),
                                Inwardedby = Convert.ToString(row["Inwardedby"]),
                                Remarks2 = Convert.ToString(row["Remarks2"]),
                                Dept = Convert.ToString(row["Dept"]),
                                Lastupdated = Convert.ToString(row["Lastupdated"])
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
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Export_daywise_internal_data(Models.Datewise L)
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {

                    con.Open();
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Getdaywise_internaldownload";
                    cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = L.Fromdate;
                    cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = L.Todate;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt.TableName = "Datewise_Re_internal";
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
                        Response.AddHeader("content-disposition", "attachment;filename= Datewise_Re_internal_Master.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Transaction_Register", "TransactionRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Get_daywise_external_data(Returnable_Internal_outward L)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        string response = string.Empty;
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = L.QueryType;
                        cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = L.FromDate;
                        cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = L.ToDate;                       
                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);

                        var result = new List<Models.Returnable_internal_pending>();
                        
                        foreach (DataRow row in ds.Tables[0].Rows)
                        {

                           
                            result.Add(item: new Models.Returnable_internal_pending
                            {
                                id = Convert.ToString(row["Id"]),
                                editid = Convert.ToString(row["editid"]),
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
                                ReceivedQty1 = Convert.ToString(row["ReceivedQty1"]),
                                PendingQty = Convert.ToString(row["PendingQty"]),
                                Qty = Convert.ToString(row["Qty"]),
                                Remarks = Convert.ToString(row["Remarks1"]),
                                RemainingQty = Convert.ToString(row["RemainingQty"]),
                                Timein = Convert.ToString(row["Timein"]),
                                ReturnDCNo = Convert.ToString(row["ReturnDCNo"]),
                                Inwardedby = Convert.ToString(row["Inwardedby"]),
                                Remarks2 = Convert.ToString(row["Remarks2"]),
                                Dept = Convert.ToString(row["Dept"]),
                                Lastupdated = Convert.ToString(row["Lastupdated"])
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
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Export_daywise_externalmaster(Models.Datewise L)
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Getdaywise_Externaldownload";
                    cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = L.Fromdate;
                    cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = L.Todate;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt.TableName = "Datewise_Re_external";
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
                        Response.AddHeader("content-disposition", "attachment;filename= Datewise_Re_external.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Transaction_Register", "TransactionRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Get_daywise_Non_return_internal_data(Returnable_Internal_outward L)
        {
            if (Session["UserName"] != null)
            {
                try
                {
                    using (SqlConnection con = new SqlConnection(connectionstring))
                    {
                        string response = string.Empty;
                        con.Open();
                        DataSet ds = new DataSet();
                        SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = L.QueryType;
                        cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = L.FromDate;
                        cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = L.ToDate;

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);

                        var result = new List<Models.Returnable_internal_pending>();

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            result.Add(item: new Models.Returnable_internal_pending
                            {
                                id = Convert.ToString(row["Id"]),
                                Transaction_id = Convert.ToString(row["Transaction_id"]),
                                M_From = Convert.ToString(row["M_From"]),
                                M_To = Convert.ToString(row["M_To"]),                              
                                Material_name = Convert.ToString(row["Material_name"]),
                                UOM = Convert.ToString(row["UOM"]),
                                RefDCNo = Convert.ToString(row["Ref_DCNo"]),
                                Timeout = Convert.ToString(row["Timeout"]),
                                Outwardedby = Convert.ToString(row["Outwardedby"]),
                                VechicleNo = Convert.ToString(row["VechicleNo"]),
                                Qty = Convert.ToString(row["Qty"]),
                                Remarks = Convert.ToString(row["Remarks"]),
                                Dept = Convert.ToString(row["Dept"])
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
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Export_daywise_nonreint(Models.Datewise L)
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Getdaywise_noninternal";
                    cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = L.Fromdate;
                    cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = L.Todate;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt.TableName = "Datewise_NonRe_internal";
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
                        Response.AddHeader("content-disposition", "attachment;filename= Datewise_NonRe_internal.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Transaction_Register", "TransactionRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Get_daywise_Non_return_external_data(Returnable_Internal_outward L)
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
                        cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = L.QueryType;
                        cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = L.FromDate;
                        cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = L.ToDate;                       

                        SqlDataAdapter da = new SqlDataAdapter(cmd);
                        da.Fill(ds);

                        var result = new List<Models.Returnable_internal_pending>();

                        foreach (DataRow row in ds.Tables[0].Rows)
                        {
                            result.Add(item: new Models.Returnable_internal_pending
                            {
                                id = Convert.ToString(row["Id"]),
                                Transaction_id = Convert.ToString(row["Transaction_id"]),
                                M_From = Convert.ToString(row["M_From"]),
                                M_To = Convert.ToString(row["M_To"]),                                
                                Material_name = Convert.ToString(row["Material_name"]),
                                UOM = Convert.ToString(row["UOM"]),
                                RefDCNo = Convert.ToString(row["Ref_DCNo"]),                                
                                VechicleNo = Convert.ToString(row["VechicleNo"]),
                                Qty = Convert.ToString(row["Qty"]),
                                Remarks = Convert.ToString(row["Remarks"]),
                                Timein = Convert.ToString(row["Timein"]),                               
                                Inwardedby = Convert.ToString(row["Inwardedby"]),                              
                                Dept = Convert.ToString(row["Dept"])
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
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

        public ActionResult Export_daywise_nonreext(Models.Datewise L)
        {
            if (Session["UserName"] != null)
            {
                using (SqlConnection con = new SqlConnection(connectionstring))
                {
                    con.Open();
                    DataTable dt = new DataTable();
                    SqlCommand cmd = new SqlCommand("SP_GetSettings_data", con);
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.Parameters.Add("@QueryType", SqlDbType.VarChar, 50).Value = "Getdaywise_nonexternal";
                    cmd.Parameters.Add("@Parameter", SqlDbType.VarChar, 50).Value = L.Fromdate;
                    cmd.Parameters.Add("@Parameter1", SqlDbType.VarChar, 50).Value = L.Todate;
                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    dt.TableName = "Datewise_NonRe_external";
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
                        Response.AddHeader("content-disposition", "attachment;filename= Datewise_NonRe_external.xlsx");

                        using (MemoryStream MyMemoryStream = new MemoryStream())
                        {
                            wb.SaveAs(MyMemoryStream);
                            MyMemoryStream.WriteTo(Response.OutputStream);
                            Response.Flush();
                            Response.End();
                        }
                    }
                }
                return RedirectToAction("Transaction_Register", "TransactionRegister");
            }
            else
            {
                return RedirectToAction("Index", "Login");
            }
        }

    }
}