using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEEIPro.Controllers
{
    public class InformationController : Controller
    {
        //
        // GET: /Information/研究院新闻

        public ActionResult InstitutionalNews()
        {
            return View();
        }

        public ActionResult Institutional()
        {
            return View();
        }

        //
        // GET: /Information/教育装备新闻

        public ActionResult IndustrialNews()
        {
            return View();
        }

        public ActionResult Industrial()
        {
            return View();
        }

    }
}
