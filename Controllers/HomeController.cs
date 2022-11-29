using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MMIv3.Models;
using System.Data.SqlClient;
using System.Data;

namespace MMIv3.Controllers
{
    public class HomeController : Controller
    {
        public int result;
        public int islogged = 0;
        static SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HLJB7UO\SQLEXPRESS01;Initial Catalog=DDB2;Integrated Security=True");
        public ActionResult Index()
        {
            var user = new User();
            if (user.password != "")
                return View(user);
            else
                return View();
        }

        public ActionResult Register()
        {
            var userex = new UserEx();
            return View(userex);
        }
        public ActionResult LogIn(UserAction userAction)
        {
            userAction.SetUser(userAction.id);
            conn.Close();
            userAction.GetFriends();
            userAction.GetRequests();
            userAction.SetProfPic();
            if (userAction.action == 1 && userAction.url != "null")
            {

                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = System.Data.CommandType.Text;
                result = getid(userAction.username, userAction.password, conn);
                cmd.CommandText = "update User1 set avatar = " + userAction.url + " where id = " + userAction.id;
                cmd.ExecuteNonQuery();
                conn.Close();

            }

            if (userAction.action == 2 && userAction.sendreqname != "")
            {

                SqlCommand cmd = new SqlCommand();
                int result = getid2(userAction.sendreqname, conn);
                Increment increment = new Increment();
                int id = increment.oblicz("Friends");

                conn.Open();
                cmd.CommandText = "Insert into Friends(id, user1_id, user2_id, currstate,Wsend) values ('" + id + "','" + userAction.id + "','" + result + "','0','" + userAction.id + "')";
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.ExecuteNonQuery();
                conn.Close();


            }
            if (userAction.action == 3)
            {
                SqlCommand cmd = new SqlCommand();
                conn.Open();
                cmd.CommandType = System.Data.CommandType.Text;
                cmd.Connection = conn;
                cmd.CommandText = "update Friends set currstate = '1' where user1_id in (" + userAction.id + "," + userAction.accepted + ") and user2_id in (" + userAction.id + "," + userAction.accepted + ")";
                cmd.ExecuteNonQuery();



                conn.Close();


            }
            conn.Close();
            userAction.GetFriends();
            userAction.GetRequests();
            userAction.SetProfPic();
            userAction.getlastthree();
            return View(userAction);
        }
        public ActionResult LogInto(User user)
        {



            result = getid(user.username, user.password, conn);
            UserAction userAction = new UserAction(user, 0, result);
            conn.Close();
            userAction.GetFriends();
            userAction.GetRequests();
            userAction.getlastthree();
            userAction.SetProfPic();

            if (result > 0)
            {

                islogged = 1;
                return View("LogIn", userAction);


            }
            else
            {

                return View("Blad");

            }

        }
        public int getid(string username, string password, SqlConnection con)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select id from User1 where username ='" + username + "' and password='" + password + "'";
            cmd.Connection = con;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                result = Int32.Parse(row[0].ToString());
            
            }
            con.Close();
            return result;
        }
        public int getid2(string username, SqlConnection con)
        {
            con.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select id from User1 where username ='" + username + "'";
            cmd.Connection = con;
            try
            {
                result = (Int32)cmd.ExecuteScalar();
            }
            catch { }
            con.Close();
            return result;
        }
        public ActionResult SignIn(UserEx userex)
        {
            int temp1 = 0;
            Increment temp = new Increment();
            temp1 = temp.oblicz("User1");

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