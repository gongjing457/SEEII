using PagedList;
using SEEIPro.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SEEIPro.Controllers
{
    public class UtilController : Controller
    {

        static seeiExpertsDBEntities seeiExpertDb = new seeiExpertsDBEntities();
        //
        // GET: /Utils/
        public ActionResult Docs(int? page)
        {
            int PageNum = page ?? 1;
            int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            int totalCount = 0;
            var docs = GetDocuments(PageNum, PageSize, ref totalCount);

            //通过ToPagedList扩展方法进行分页  int pager,
            //IPagedList<Document> pagedlist = docs.ToPagedList(PageNum, PageSize);
            //将分页处理后的列表传给View  
            //return View(pagedlist);
            var pagedlist = new StaticPagedList<Document>(docs, PageNum, PageSize, totalCount);
            return View(pagedlist);

        }

        private List<Document> GetDocuments(int PageNum, int PageSize, ref int totalCount)
        {
            var documents = (from d in seeiExpertDb.Documents orderby d.exlId ascending select d).Skip((PageNum - 1) * PageSize).Take(PageSize);
            totalCount = seeiExpertDb.Documents.Count();
            return documents.ToList();
        }



        // POST: /Utils/下载文件

        public FileStreamResult Download(string path, string filename)
        {
            // TODO: Add insert logic here
            filename = "研究院加班申请表.pdf";
            string str = Server.MapPath("/DocFiles/研究院加班申请表.pdf");
            path = str;

            return File(new FileStream(str, FileMode.Open), "text/plain", filename);

        }

        //
        // GET: /Utils/Edit/5

        public ActionResult Evaluation()
        {
            return View();
        }

        //
        // POST: /Utils/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add update logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }

        //
        // GET: /Utils/Delete/5

        public ActionResult Delete(int id)
        {
            return View();
        }

        //
        // POST: /Utils/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, FormCollection collection)
        {
            try
            {
                // TODO: Add delete logic here

                return RedirectToAction("Index");
            }
            catch
            {
                return View();
            }
        }
    }
}
