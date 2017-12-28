using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEEIPro.Controllers
{
    public class AboutController : Controller
    {

        /// GET: /About/简介
        public ActionResult ProfileInfo()
        {
            return View();
        }

        /// GET: /About/发展目标
        public ActionResult Goals()
        {
            return View();
        }

        /// GET: /About/研究院风采
        public ActionResult Landscape()
        {
            return View();
        }

        /// GET: /About/组织架构
        public ActionResult Organization()
        {
            return View();
        }


        /// GET: /About/profile子页面
        public ActionResult ProfileView()
        {
            return View();
        }

        /// GET: /About/LandscapeShow子页面
        public ActionResult LandscapeShow()
        {
            return View();
        }

        /// GET: /About/ Organization子页面
        public ActionResult OrganizationInfo()
        {
            return View();
        }

        /// GET: /About/ Goals 子页面
        public ActionResult GoalsInfo()
        {
            return View();
        }
    }
}
