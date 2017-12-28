using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEEIPro.Controllers
{
    public class CommunicateController : Controller
    {
        //
        // GET: /communication/

        public ActionResult Index()
        {
            return View();
        }


        // GET: /communication/HigherEdu

        public ActionResult HigherEducate()
        {
            return View();//高教学会合作
        }


        // GET: /communication/HigherEducation

        public ActionResult HigherEducation()
        {
            return View();   //高教学会合作
        }


        // GET: /communication/ College
        public ActionResult College()
        {
            return View();     //院校合作
        }


        // GET: /communication/PartnerInstitutions 
        public ActionResult PartnerInstitutions()
        {
            return View();   //高院校合作
        }

        // GET: /communication/ Academic 
        public ActionResult Academic()
        {
            return View();     //学术平台
        }

        // GET: /communication/ Academy
        public ActionResult Academy()
        {
            return View();
        }
    }
}
