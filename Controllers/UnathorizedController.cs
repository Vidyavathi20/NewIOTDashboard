using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MaterialGateRegister.Controllers
{
    public class UnathorizedController : Controller
    {
        // GET: Unathorized
        public ActionResult Index()
        {
            Session.Abandon();
            return View();
        }
    }
}