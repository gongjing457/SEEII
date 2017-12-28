using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEEIPro.Controllers
{
    public class ContactController : Controller
    {
        //
        // GET: /Contact/Cooperate details
        public ActionResult Cooperate()
        {
            return View();
        }

        //
        // GET: /Contact/Cooperation
        public ActionResult Cooperation()
        {
            return View();
        }

        //
        // GET: /Contact/download

        public ActionResult Download()
        {
            return View();
        }

        //
        // GET: /Contact/Create

        public ActionResult Email()
        {
            return View();
        }

        //
        // GET: /Contact/mailbox
        public ActionResult Mailbox()
        {
            return View();
        }


        //
        // GET: /Contact/FeedBack
        public ActionResult FeedBack()
        {
            return View();
        }



        // POST: /Contact/Create

        [HttpPost]
        public ActionResult Create(FormCollection collection)
        {
            try
            {
                // TODO: Add insert logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Contact/Recruitment
        public ActionResult Recruitment()
        {
            return View();
        }

        //
        // GET: /Contact/zhaopin
        public ActionResult Employ()
        {
            return View();
        }


    }
}
