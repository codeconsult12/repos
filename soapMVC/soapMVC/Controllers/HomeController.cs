using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace soapMVC.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            ServiceReference1.RunCodeUnitBody runCodeUnitBody = new ServiceReference1.RunCodeUnitBody("a");
            ServiceReference1.RunCodeUnit runCodeUnit = new ServiceReference1.RunCodeUnit(runCodeUnitBody);
            var s = runCodeUnit.Body;
            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}