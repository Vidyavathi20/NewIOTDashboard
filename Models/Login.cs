using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MaterialGateRegister.Models
{
    public class Login
    {
        public string id { get; set; }
        public string Employeename { get; set; }
        public string Password { get; set; }
        public string Oldpassword { get; set; }
        public string Emailid { get; set; }
        public string Userrole { get; set; }
        public string Role_id { get; set; }
        public string QueryType { get; set; }
        public string Superadmin { get; set; }
    }
    public class Role
    {
        public string id { get; set; }
        public string Userrole { get; set; }
        public string QueryType { get; set; }
    }


    public class Vendor
    {
        public string QueryType { get; set; }
        public string Id { get; set; }
        public string Customer_name { get; set; }
    }

    public class UOM_list
    {
        public string QueryType { get; set; }
        public string Id { get; set; }
        public string UOM { get; set; }
    }

        public class Uom
        {
            public string QueryType { get; set; }
            public string Id { get; set; }
            public string UOM { get; set; }

        }
    
}