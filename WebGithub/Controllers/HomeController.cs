using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Data.SqlClient;
using System.Configuration;

namespace WebGithub.Controllers
{
    public class HomeController : Controller
    {
        SqlConnection Conn = new SqlConnection(ConfigurationManager.ConnectionStrings["project7ConnectionString"].ConnectionString);
        // GET: Default
        public ActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public ActionResult Index(string id, string pwd)
        {
            string sql = "select * from MemberAccount where mbrAccount = @mbrAccount and mbrPwd=@mbrPwd";
            SqlCommand cmd = new SqlCommand(sql, Conn);
            cmd.Parameters.AddWithValue("@mbrAccount", id);
            cmd.Parameters.AddWithValue("@mbrPwd", pwd);
            SqlDataReader rd;

            Conn.Open();
            rd = cmd.ExecuteReader();
            if (rd.Read())
            {
                Session["id"] = rd["mbrPwd"].ToString();


                Conn.Close();
                return RedirectToAction("Indexmain", "Home");
            }
            Conn.Close();
            ViewBag.LoginErr = "帳號或密碼有誤";
            return View();

        }

        public ActionResult Logout()
        {
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
        public ActionResult Indexmain()
        {
            return View();
        }
    }
}