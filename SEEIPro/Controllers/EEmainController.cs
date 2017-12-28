using SEEIPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEEIPro.Controllers
{
    public class EEmainController : Controller
    {
        seeiExpertsDBEntities seeiDB = new seeiExpertsDBEntities();

        //
        // GET: /MainPage/

        public ActionResult HomePage()
        {
            return View();
        }

        public ActionResult ExpertProfile()
        {
            List<Expert> expts = seeiDB.Experts.ToList<Expert>();
            ViewBag.Experts = expts;
            return View();
        }
    }
}
