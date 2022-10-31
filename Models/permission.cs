using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace MaterialGateRegister.Models
{
    public class permission
    {
            [Key]
            public int Permission_id { get; set; }
            public string Role_id { get; set; }

            [Required]
            [StringLength(50)]
            public string Module_name { get; set; }

            public string Add_form { get; set; }
            public string View_form { get; set; }
            public string Delete_form { get; set; }
            public string Edit_form { get; set; }
            
            public string QueryType { get; set; }

            public string UniqueId { get; set; }



        }
    
}