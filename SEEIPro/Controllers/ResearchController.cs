using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEEIPro.Controllers
{
    public class ResearchController : Controller
    {
        //
        // GET: /Research/课题研究

        public ActionResult Reform() { return View(); }

        public ActionResult Educationreform() { return View(); }

        //
        // GET: /Research/Create

        public ActionResult Printing3D() { return View(); }

        public ActionResult Printing3DArea() { return View(); }
    }
}
