using SEEIPro.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEEIPro.Controllers
{
    public class HatchingController : Controller
    {
        seeiExpertsDBEntities seeiDb = new seeiExpertsDBEntities();
        //
        // GET: /Hatching/教装学院

        public ActionResult eCollege()
        {
            return View();
        }

        public ActionResult eCollegeInstute()
        {
            return View();
        }


        //
        // GET: /Hatching/Details/培训中心

        public ActionResult Training()
        {
            return View();
        }

        public ActionResult TrainCenter()
        {
            return View();
        }


        //
        // GET: /Hatching/高校联合培养

        public ActionResult Cocultivation()
        {
            return View();
        }

        public ActionResult Cocultivate()
        {
            return View();
        }

        //
        // GET: /Hatching/专家智库
        public ActionResult Consultant()
        {
            return View();
        }
        public ActionResult DevExpert()
        {
            List<Expert> exp_list = seeiDb.Experts.ToList();
            ViewBag.Experts = exp_list;
            return View();
        }

    }
}
