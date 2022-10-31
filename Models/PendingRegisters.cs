using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaterialGateRegister.Models
{
    public class Returnable_internal_pending
    {

        public string id { get; set; }
        public string editid { get; set; }
        public string id2 { get; set; }
        public string M_From { get; set; }
        public string M_To { get; set; }
        public string EDR { get; set; }
        public string RefDCNo { get; set; }
        public string Timeout { get; set; }
        public string Outwardedby { get; set; }
        public string Remarks { get; set; }
        public string PendingQty { get; set; }
        public string ReceivedQty { get; set; }
        public string Material_name { get; set; }
        public string RemainingQty { get; set; }
        public string VechicleNo { get; set; }
        public string ReturnedQty { get; set; }
        public string Timein { get; set; }
        public string Inwardedby { get; set; }
        public string Remarks2 { get; set; }
        public string Transaction_id { get; set; }
        public string Dept { get; set; }
        public string ReturnDCNo { get; set; }
        public string Qty { get; set; }
        public string Lastupdated { get; set; }
        public string ReceivedQty1 { get; set; }
        public string UOM { get; set; }

    }
    public class Returnable_External_Master
    {
        public string id { get; set; }      
        public string RefDCNo { get; set; }       
        public string Remarks { get; set; }
        public string PendingQty { get; set; }
        public string ReturnedQty1 { get; set; }
        public string Material_name { get; set; }
        public string RemainingQty { get; set; }
        public string ReturnedQty { get; set; }    
        public string Qty { get; set; }
        public string UOM { get; set; }
    }
    }