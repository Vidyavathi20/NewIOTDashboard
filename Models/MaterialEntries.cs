using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaterialGateRegister.Models
{
    public class Returnable_Internal_outward
    {
        public string id { get; set; }
        public string M_From { get; set; }
        public string M_To { get; set; }
        public string EDR { get; set; }
        public string RefDCNo { get; set; }
        public string Timeout { get; set; }
        public string Outwardedby { get; set; }
        public string QueryType { get; set; }
        public string Send_qty { get; set; }
        public string Pending_qty { get; set; }
        public string Transaction_id { get; set; }
        public string Received_qty { get; set; }
        public string FromDate { get; set; }
           public string ToDate { get; set; }
        public string Pending_item { get; set; }
    }

    public class Returnable_Internal_outward_list
    {
        public string QueryType { get; set; }
        public string id { get; set; }
        public string Material_name { get; set; }
        public string Qty { get; set; }
        public string UOM { get; set; }
        public string Remarks { get; set; }
        
    }
    public class Returnable_External_outward
    {
        public string id { get; set; }
        public string M_From { get; set; }
        public string M_To { get; set; }
        public string EDR { get; set; }
        public string RefDCNo { get; set; }
        public string Timein { get; set; }
        public string Inwardedby { get; set; }
        public string QueryType { get; set; }
        public string Transaction_id { get; set; }
        public string Return_qty { get; set; }
        public string Pending_qty { get; set; }
        public string Pending_item { get; set; }
    }
    public class Returnable_External_outward_list
    {
        public string QueryType { get; set; }
        public string id { get; set; }
        public string Material_name { get; set; }
        public string Qty { get; set; }
        public string UOM { get; set; }
        public string Remarks { get; set; }
        public string Pending_qty { get; set; }
        public string Send_qty { get; set; }

    }
    public class NonReturnable_Internal_outward
    {
        public string id { get; set; }
        public string id2 { get; set; }
        public string VechicleNo { get; set; }
        public string M_From { get; set; }
        public string M_To { get; set; }     
        public string RefDCNo { get; set; }
        public string Timeout { get; set; }
        public string Outwardedby { get; set; }
        public string Remarks { get; set; }
        public string QueryType { get; set; }
    }
    public class NonReturnable_Internal_outward_list
    {
        public string QueryType { get; set; }
        public string id { get; set; }
        public string Material_name { get; set; }
        public string Qty { get; set; }
        public string id2 { get; set; }
        public string UOM { get; set; }
        public string Remarks { get; set; }

    }
    public class NonReturnable_external_outward
    {
        public string id { get; set; }
        public string id2 { get; set; }
        public string VechicleNo { get; set; }
        public string M_From { get; set; }
        public string M_To { get; set; }
        public string RefDCNo { get; set; }
        public string Timein { get; set; }
        public string Inwardedby { get; set; }
        public string QueryType { get; set; }
    }
    public class NonReturnable_external_outward_list
    {
        public string QueryType { get; set; }
        public string id { get; set; }
        public string Material_name { get; set; }
        public string Qty { get; set; }
        public string UOM { get; set; }
        public string Remarks { get; set; }

    }
    public class Returnable_Internal_inward
    {
        public string id { get; set; }
        public string id2 { get; set; }
        public string M_From { get; set; }
        public string M_To { get; set; }
        public string VechicleNo { get; set; }
        public string RefDCNo { get; set; }
        public string Timeout { get; set; }
        public string Outwardedby { get; set; }
        public string Inwardedby { get; set; }
        public string ReturnDC_No { get; set; }
        public string Timein { get; set; }
        public string Remarks { get; set; }
        public string QueryType { get; set; }
    }
    public class Returnable_internal_inwardlist
    {
        public string Id { get; set; }
        public string Id2 { get; set; }
        public string editid { get; set; }
        public string Transaction_id { get; set; }
        public string Transaction_id1 { get; set; }
        public string PendingQty { get; set; }
        public string ReceivedQty { get; set; }
        public string ReceivedQty1 { get; set; }
        public string RemainingQty { get; set; }
        public string Material_name { get; set; }
        public string Qty { get; set; }
        public string UOM { get; set; }
        public string Remarks { get; set; }
        public string Remarks2 { get; set; }
        public string QueryType { get; set; }
        public string RefDCNo { get; set; }


    }
    public class Returnable_external_return
    {
        public string id { get; set; }
        public string M_From { get; set; }
        public string M_To { get; set; }
        public string EDR { get; set; }
        public string RefDCNo { get; set; }
        public string Timeout { get; set; }
        public string Outwardedby { get; set; }
        public string Inwardedby { get; set; }
        public string ReturnDC_No { get; set; }
        public string Timein { get; set; }
        public string id2 { get; set; } 
        public string QueryType { get; set; }
    }
    public class Returnable_external_returnlist
    {
        public string id { get; set; }
        public string id2 { get; set; }

        public string PendingQty { get; set; }
        public string ReturnedQty { get; set; }           
        public string RemainingQty { get; set; }
        public string Material_name { get; set; }
        public string Qty { get; set; }
        public string UOM { get; set; }
        public string Remarks { get; set; }
        public string QueryType { get; set; }
        public string RefDCNo { get; set; }
        public string Transaction_id { get; set; }
        public string Remarks2 { get; set; }
    }
   

}