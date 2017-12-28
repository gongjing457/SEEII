using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SEEIPro.Models;
using System.Data.SqlClient;
using System.Configuration;
using SEEIPro.Utils;
using System.Data;

namespace SEEIPro.Controllers
{
    public class BackstageController : Controller
    {

        //获取Web.config文件中数据库连接的配置信息
        public static readonly string connstr = ConfigurationManager.ConnectionStrings["seeiExpertsDB"].ConnectionString;
        // @"Data Source=.;Initial Catalog=seeiExpertsDB;Persist Security Info=True;User ID=sa;Password=888457;MultipleActiveResultSets=True;Application Name=EntityFramework";

        LoginModel loginmodel = new LoginModel();


        //
        // GET: /Home/

        public ActionResult Login()
        {
            return View();
        }

        //
        // POST: /Home/Handler

        [HttpPost]
        public ActionResult Handler()
        {
            string username = Request["name"].ToString();
            string password = Request["pwd"].ToString();
            int res = CheckUser(username, password);
            if (res > 0)
            {
                getLoginUser(res);
                Session["loginModel"] = loginmodel;
                Session["name"] = loginmodel.name;
                return Content("yes");
            }
            else
            {
                return Content("no");
            }


        }
        /// <summary>
        /// 登录用户
        /// </summary>
        private void getLoginUser(int res)
        {

            string sqlStr = "SELECT sId,name,gender,birthdate,position,department,limit,entryDate FROM Staff WHERE sId=@id";
            SqlParameter sqlParas = new SqlParameter("@id", res);
            DataTable dt = DBHelper.ExecuteDataTable(sqlStr, sqlParas);
            if (dt.Rows.Count > 0)
            {
                loginmodel.sId = res;
                loginmodel.name = dt.Rows[0][1].ToString();
                loginmodel.gender = dt.Rows[0][2].ToString();
                loginmodel.birthdate = Convert.ToDateTime(dt.Rows[0][3].ToString());
                loginmodel.position = dt.Rows[0][4].ToString();
                loginmodel.department = dt.Rows[0][5].ToString();
                loginmodel.limit = Convert.ToInt32(dt.Rows[0][6]);
                if (dt.Rows[0][7].ToString() != null && dt.Rows[0][7].ToString().Length > 0)
                {
                    loginmodel.entryDate = Convert.ToDateTime(dt.Rows[0][7].ToString());
                }
                else
                {
                    loginmodel.entryDate = DateTime.Now;
                }
            }
        }


        public ActionResult loadMain()
        {
            return Redirect("/ExpertsManage/Index");
        }

        private int CheckUser(string username, string password)
        {
            string sql = "SELECT sId,name FROM  [Staff] WHERE userName=@username and passWord=@userpwd";
            SqlParameter[] paras = { new SqlParameter("@username", username), new SqlParameter("@userpwd", password) };
            return Convert.ToInt32(ExecuteScalar(sql, paras));
        }

        private object ExecuteScalar(string sql, SqlParameter[] paras)
        {
            using (SqlConnection conn = new SqlConnection(connstr))
            {
                conn.Open();
                return ExecuteScalar(conn, sql, paras);
            }
        }


        /// 执行查询语句 返回查询结果
        public static object ExecuteScalar(SqlConnection conn, string cmdText,
            params SqlParameter[] parameters)
        {
            using (SqlCommand cmd = conn.CreateCommand())
            {
                cmd.CommandText = cmdText;
                cmd.Parameters.AddRange(parameters);
                return cmd.ExecuteScalar();
            }
        }

        protected override void HandleUnknownAction(string actionName)
        {

            try
            {
                this.View(actionName).ExecuteResult(this.ControllerContext);
            }
            catch (InvalidOperationException ieox)
            {

                ViewData["error"] = "Unknown Action: \"" + Server.HtmlEncode(actionName) + "\"";

                ViewData["exMessage"] = ieox.Message;

                this.View("Error").ExecuteResult(this.ControllerContext);

            }
        }
    }
}
