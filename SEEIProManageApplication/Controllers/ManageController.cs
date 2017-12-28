using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEEIProManageApplication.Controllers
{
    public class ManageController : Controller
    {
        //
        // GET: /Manage/mainpage

        public ActionResult ManageInfo()
        {
            return View();
        }

        //
        // GET: /Manage/login view

        public ActionResult Login()
        {
            return View();
        }


        //
        // POST: /Manage/check user
        [HttpPost]
        public ActionResult Handle()
        {
            string username = Request["username"].ToString();
            string password = Request["pwd"].ToString();
            int res = CheckUser(username, password);
            //yes
            res = 1;
            if (res > 0)
            {
                //save login model
                //    getLoginUser(res);
                //    Session["loginModel"] = loginmodel;
                //    Session["name"] = loginmodel.name;
                return Content("yes");
            }
            else
            {
                return Content("no");
            }
        }

        private int CheckUser(string username, string password)
        {
            return 0;
        }

        //
        // GET: /Manage/Edit/5

        public ActionResult Edit(int id)
        {
            return View();
        }

        //
        // POST: /Manage/validate password
        [HttpPost]
        public ActionResult ValiPwd()
        {

            // TODO: Add update logic here
            //Session["loginMoel"] =()loginmodel
            if (Session["loginMoel"] == null)
            {
                return Content("no");
            }
            else
            {
                return Content("yes");
            }

        }

        //
        // GET: /Manage/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }


        public ActionResult Changepwd()
        {
            //try
            //{
            //    // TODO: Add delete logic here

            //    return RedirectToAction("Index");
            //}
            //catch
            //{
            //   
            //}
            return View();
        }
    }
}
