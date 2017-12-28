using SEEIPro.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Validation;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
using NPOI.HSSF.UserModel;
using System.Data;
using System.Reflection;
using System.IO;
using NPOI.SS.UserModel;
using SEEIPro.Utils;
using System.Text;

namespace SEEIPro.Controllers
{
    public class ExpertsManageController : Controller
    {

        static seeiExpertsDBEntities seeiDb = new seeiExpertsDBEntities();
        List<UnitProperty> unitList = seeiDb.UnitProperties.ToList<UnitProperty>();
        List<StorageStatu> statusList = seeiDb.StorageStatus.ToList<StorageStatu>();

        /// <summary>
        /// GET: /ExpertsManage/查看专家列表
        /// </summary>
        /// <returns></returns>
        /// 
        [HttpGet]
        public ActionResult Index(int? page)
        {
            int PageNum = page ?? 1;
            int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);

            var experts = from expert in seeiDb.Experts select expert;
            experts = experts.OrderBy(e => e.SerialNum);

            //通过ToPagedList扩展方法进行分页  
            IPagedList<Expert> pagedlist = experts.ToPagedList(PageNum, PageSize);
            ViewBag.StatusList = statusList;
            //将分页处理后的列表传给View  
            return View(pagedlist);

        }


        /// <summary>
        /// GET: /ExpertsManage/Create添加新专家页面
        /// </summary>
        /// <returns></returns>
        public ActionResult Add()
        {
            //unitList = seeiDb.UnitProperties.ToList<UnitProperty>();
            //statusList = seeiDb.StorageStatus.ToList<StorageStatu>();
            ViewBag.UnitList = unitList;
            ViewBag.StatusList = statusList;
            return View();

        }

        //
        // POST: /ExpertsManage/Create

        //[HttpPost]
        //public ActionResult Create(FormCollection collection)
        //{
        //    try
        //    {
        //        // TODO: Add insert logic here

        //        return RedirectToAction("Index");
        //    }
        //    catch
        //    {
        //        return View();
        //    }
        //}

        //
        // GET: /ExpertsManage/Edit/5

        //public ActionResult Load(int id)
        //{
        //    return View();
        //}

        //
        // POST: /ExpertsManage/保存专家图像
        [HttpPost]
        public ActionResult SaveExpertImg()
        {
            /***********************************************************/
            if (Request.Files.Count > 0)
            {
                HttpPostedFileBase hpf = Request.Files["imgFile"];
                if (hpf != null)
                {
                    string s = hpf.FileName;
                    string path = "/Content/xiaoyicun/" + s;
                    hpf.SaveAs(Server.MapPath(path));
                }
                return Content("ok");
            }
            else
            {
                return Content("fail");
            }

        }

        // POST: /ExpertsManage/保存专家信息
        [HttpPost]
        public ActionResult SaveExpert()
        {
            /***********************************************************/
            string eid = Request["eId"];
            string eName = Request["eName"];
            string gender = Request["gender"];
            string birthDay = Request["birthDay"];
            string imgName = Request["imgName"];
            string identityNumber = Request["identityNumber"];
            string unitProperty = Request["unitProperty"];
            //string unitName = Request["unitName"];
            string unitOne = Request["unitOne"];
            string unitTwo = Request["unitTwo"];
            string unitThree = Request["unitThree"];
            string academicTitles = Request["academicTitles"];
            string field = Request["field"];
            string email = Request["email"];
            string officePhone = Request["officePhone"];
            string cellPphone = Request["cellPphone"];
            string postalAddress = Request["postalAddress"];
            string expertSources = Request["expertSources"];
            string beStatus = Request["beStatus"];
            string personalUrl = Request["personalUrl"];
            string Categories = Request["Categories"];
            string expertIntroduction = Request["expertIntroduction"];
            string expertworkingExperience = Request["expertworkingExperience"];
            string expertAchievement = Request["expertAchievement"];
            string remark = Request["remark"];

            Expert expert = new Expert();

            expert.eId = eid;
            expert.eName = eName;
            expert.gender = gender;
            expert.birthDay = Convert.ToDateTime(birthDay);
            expert.identityNumber = identityNumber;
            expert.unitProperty = Convert.ToInt32(unitProperty);
            expert.UnitDetailsOne = unitOne;
            expert.UnitDetailsTwo = unitTwo;
            expert.UnitDetailsThree = unitThree;
            expert.academicTitles = academicTitles;
            expert.field = field;
            expert.email = email;
            expert.officePhone = officePhone;
            expert.cellPphone = cellPphone;
            expert.postalAddress = postalAddress;
            expert.expertSources = expertSources;
            expert.beStatus = Convert.ToInt32(beStatus);
            expert.img = "/Content/xiaoyicun/" + imgName;
            expert.personalUrl = personalUrl;
            expert.Categories = Categories;
            expert.SerialNum = eid.Substring(eid.Length - 4);
            expert.expertIntroduction = expertIntroduction;
            expert.expertworkingExperience = expertworkingExperience;
            expert.expertAchievement = expertAchievement;
            expert.remark = remark;

            seeiDb.Experts.Add(expert);
            if (seeiDb.SaveChanges() > 0)
            {
                return Content("success");
            }
            else
            {
                return Content("fail");
            }

        }

        /// <summary>
        /// 获取单位性质代码
        /// </summary>
        /// <param name="unitproperty"></param>
        /// <returns></returns>
        private int unitpropertyInt(string unitproperty)
        {
            int unitInt = 0;
            for (int i = 0; i < unitList.Count; i++)
            {
                if (unitList[i].unitProperties.Equals(unitproperty))
                {
                    unitInt = unitList[i].id;
                }
            }
            return unitInt;
        }

        /// <summary>
        /// 获取入库状态码
        /// </summary>
        /// <param name="bestatus"></param>
        /// <returns></returns>
        private int statusInt(string bestatus)
        {
            int statusInt = 0;
            for (int i = 0; i < statusList.Count; i++)
            {
                if (statusList[i].beStatus.Equals(bestatus))
                {
                    statusInt = statusList[i].sid;
                }
            } return statusInt;
        }



        /// <summary>
        /// 显示编辑页面 编辑
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Edit(int id)
        {
            //1.检查id
            //2.查询数据
            Expert exp = (from expt in seeiDb.Experts where expt.sId == id select expt).FirstOrDefault();
            //if (exp != null)
            //{
            //    ViewData["exptinfo"] = exp;
            //}
            ViewBag.UnitList = unitList;
            ViewBag.StatusList = statusList;
            return View(exp);
        }

        /// <summary>
        /// 保存编辑信息
        /// </summary>
        /// <param name="exp"></param>
        /// <returns></returns>
        [HttpPost]
        public void Edit(Expert expt)
        {
            string unitproperity = Request["eUnitProperty"];
            string status = Request["eStatus"];
            //1.将实体对象加入EF对象容器中，并获取伪包装类对象  
            Expert exp = seeiDb.Experts.Find(expt.sId);
            DbEntityEntry<Expert> entry = seeiDb.Entry<Expert>(exp);
            //2.将伪包装类对象的状态设置为Detached
            entry.State = System.Data.EntityState.Detached;
            //3.修改
            exp.eId = expt.eId;
            exp.eName = expt.eName;
            exp.gender = expt.gender;
            exp.birthDay = expt.birthDay;
            exp.identityNumber = expt.identityNumber;
            exp.unitProperty = expt.unitProperty;
            exp.UnitDetailsOne = expt.UnitDetailsOne;
            exp.UnitDetailsTwo = expt.UnitDetailsTwo;
            exp.UnitDetailsThree = expt.UnitDetailsThree;
            exp.academicTitles = expt.academicTitles;
            exp.email = expt.email;
            exp.officePhone = expt.officePhone;
            exp.cellPphone = expt.cellPphone;
            exp.postalAddress = expt.postalAddress;
            exp.expertSources = expt.expertSources;
            exp.field = expt.field;
            exp.personalUrl = expt.personalUrl;
            exp.beStatus = expt.beStatus;
            exp.Categories = expt.Categories;
            exp.remark = expt.remark;
            exp.SerialNum = expt.eId.Substring(expt.eId.Length - 4);
            //4.设置属性状态 

            seeiDb.Entry(exp).State = System.Data.EntityState.Modified;
            //5.保存
            try
            {
                if (seeiDb.SaveChanges() > 0)
                {
                    // return this.JavaScript("alert('专家信息修改成功！');");return RedirectToAction("Index");
                    Response.Write("<script>alert('该专家信息已修改!');window.location='/ExpertsManage/Index';</script>");
                }
                //else
                //{
                //    return Content("修改失败，请重试！");
                //}
            }
            catch (DbEntityValidationException ex)
            {
                //foreach (var item in e.EntityValidationErrors)
                //{
                //    foreach (var item2 in item.ValidationErrors)
                //    {
                //        string error = string.Format("{0}:{1}\r\n", item2.PropertyName, item2.ErrorMessage);
                //    }
                //}   定位到错误属性
                throw ex.GetBaseException();

            }
            catch (Exception except)
            {
                throw except.GetBaseException();
            }

        }
        /// <summary>
        /// POST: /ExpertsManage/Delete/5
        /// </summary>
        /// <param name="sid"></param>
        /// <returns></returns>
        [HttpGet]
        public void Delete(int id)
        {
            //1.检查id是否存在
            //2.执行删除
            var entry = seeiDb.Set<Expert>().Find(id);
            seeiDb.Entry<Expert>(entry).State = System.Data.EntityState.Deleted;

            // TODO: Add delete logic here
            int res = seeiDb.SaveChanges();
            if (res > 0)
            {
                Response.Write("<script>alert('已成功删除该专家的信息!');window.location='/ExpertsManage/Index';</script>");
            }
            else
            {
                Response.Write("<script>alert('该专家的信息不存在!');window.location='/ExpertsManage/Index';</script>");
            }
        }

        /// <summary>
        /// 专家列表导出到Excel
        /// </summary>
        /// <returns></returns>
        [HttpGet]
        public FileResult OutToExcel()
        {
            string sheetname = ConfigurationManager.AppSettings["Sheet"];
            List<Expert> list = seeiDb.Experts.ToList();
            string filename = DateTime.Now.ToString("yyyyMMddhhmmss") + ".xls";//获取当前时间

            DataTable dt = ExcelUtil.ConvertToDataTable(list);
            string[] headers = {"序号","专家编号","姓名","性别","单位性质","单位信息","技术职称","从事领域","邮箱","办公电话","手机号码",
                                   "通信地址","入库状态","相关主页","业务类别","备注","添加时间"};
            string[] cellKes = {"sId","eId","eName","gender","unitProperty","UnitDetailsOne","academicTitles","field","email","officePhone","cellPphone",
                                   "postalAddress","beStatus","personalUrl","Categories","remark","addTime"};
            MemoryStream ms = ExcelUtil.Export(dt, sheetname, headers, cellKes);

            #region 将excel文件保存到服务器指定路径
            //string xlsname = ConfigurationManager.AppSettings["Excel"];
            // byte[] data = ms.ToArray();//Encoding.UTF8.GetBytes();
            // string filePath = Server.MapPath("~/Content/xiaoyicun/" + xlsname + ".xls");
            // FileManager.WriteBuffToFile(data, filePath);  
            #endregion

            ms.Seek(0, SeekOrigin.Begin);
            return File(ms, "application/vnd .ms-excel", Url.Encode(filename));
        }

        /// <summary>
        /// GET: /ExpertsManage/模糊查询专家信息
        /// </summary>
        /// <param name="page"></param>
        /// <param name="keywords"></param>
        /// <returns></returns>
        [HttpGet]
        public ActionResult Query(int? page)
        {
            string str = Request["kwd"];
            //string str = "龚";
            int PageNum = page ?? 1;
            int PageSize = int.Parse(ConfigurationManager.AppSettings["PageSize"]);
            List<Expert> exps = seeiDb.Experts.Where(e => e.eName.Contains(str) || e.academicTitles.Contains(str) || e.UnitDetailsOne.Contains(str) || e.field.Contains(str)).OrderBy(e => e.SerialNum).ToList();
            exps = exps.OrderBy(e => e.SerialNum).ToList();
            if (exps.Count == 0 || exps == null)
            {
                Response.Write("<script>alert('没有查询到任何相关数据!');window.location='/ExpertsManage/Index';</script>");
                return null;
            }
            else
            {
                //通过ToPagedList扩展方法进行分页  
                IPagedList<Expert> pagedlist = exps.ToPagedList(PageNum, PageSize);
                ViewBag.StatusList = statusList;
                //将分页处理后的列表传给View  
                return View(pagedlist);
            }
        }



    }
}
