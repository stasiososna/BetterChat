using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMIv3.Models;
using System.Data.SqlClient;

namespace MMIv3.Controllers
{
    public class HomeController : Controller
    {
        public int result;
        static SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-07OANPP;Initial Catalog=mmi1;Integrated Security=True");
        public ActionResult Index()
        {
            var user = new User();
            return View(user);
        }
        
        public ActionResult Register()
        {
            var userex = new UserEx();
            return View(userex);
        }
        public ActionResult LogIn(User user)
        {

            int i;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select id from User1 where username ='" + user.username + "' and password='" + user.password+"'";
            cmd.Connection = conn;


            {

            };
            try
            {
                result = (Int32)cmd.ExecuteScalar();
            }
            catch { }
            if (result > 0)
            {
                conn.Close();
                return View(user);

                
            }
            else
            {
                conn.Close();
                return View("Blad");
                
            }
        }
        public ActionResult SignIn(UserEx userex)
        {
            int temp1 = 0;
            Increment temp = new Increment();
            temp1 = temp.oblicz("User2");
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.Connection = conn;
            cmd.CommandText = "Insert Into User1 (id, username, password, email, admin, avatar) Values ('" + temp1 + "','" + userex.username + "','" + userex.password + "','" + userex.email + "','0','basic.jpg')";

            cmd.ExecuteNonQuery();
            conn.Close();

            






            return View("Index");
        }



    }
}