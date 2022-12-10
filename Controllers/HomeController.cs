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
        static SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-07OANPP;Initial Catalog=mmi1;Integrated Security=True");
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
            if (userAction.id != 0)
            {
                if (userAction.action != 1 && userAction.action!=14)
                {
                    userAction.SetUser(userAction.id);
                    conn.Close();
                    userAction.GetFriends();
                    userAction.GetRequests();
                    userAction.SetProfPic();

                }
                
                
                if (userAction.action == 1 && userAction.url != "null")
                {
                    
                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;
                    
                    cmd.CommandText = "update User1 set avatar = '" + userAction.url + "' where id = '" + userAction.id+"'";
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }
                if (userAction.action == 14){
                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "update User1 set username = '" + userAction.username + "',password = '"+userAction.password+"', email = '"+userAction.email+"' where id = '" + userAction.id + "'";
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
                if (userAction.action == 12)
                {
                    conn.Open();
                    Increment increment = new Increment();
                    var result = increment.oblicz("groups");
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = conn;
                    cmd.CommandType=CommandType.Text;
                    cmd.CommandText = "Insert into groups(id,name,about,ownerid,url) values('"+result+"','"+userAction.grname+"','"+userAction.grabout+"','"+userAction.id+"','basic.jpg');";
                    cmd.ExecuteNonQuery();
                    userAction.focusedgroupid = result;
                    conn.Close();
                
                }
                if (userAction.action == 16)
                {
                    SqlCommand cmd = new SqlCommand();
                    conn.Open();
                    cmd.Connection = conn;
                    cmd.CommandType = System.Data.CommandType.Text;

                    cmd.CommandText = "update groups set url = '" + userAction.url + "' where id = '" + userAction.focusedgroupid + "'";
                    cmd.ExecuteNonQuery();
                    conn.Close();

                }




                if (userAction.action == 4 || userAction.action == 7 || userAction.action == 9 || userAction.action == 8)
                {
                    if (userAction.action == 9)
                    {
                        Increment increment = new Increment();
                        int result = increment.oblicz("posts");
                        SqlCommand cmd = new SqlCommand();
                        conn.Open();
                        cmd.CommandText = "insert into posts(id,adderid,content) values('" + result + "','" + userAction.id + "','" + userAction.postcontent + "');";
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = conn;
                        cmd.ExecuteNonQuery();



                    }
                    if (userAction.action == 4 || userAction.action == 9 || userAction.action == 8)
                    {
                        if (userAction.focusedfriendid == userAction.id)
                        {
                            userAction.focusedfriend = new Friend(userAction.focusedfriendid, conn);
                            userAction.profile = new Profile(userAction.focusedfriend, userAction.id, conn, userAction.username);
                            userAction.profile.getposts(conn);


                        }
                        else
                        {
                            userAction.focusedfriend = new Friend(userAction.focusedfriendid, conn);
                            userAction.profile = new Profile(userAction.focusedfriend, conn, userAction.focusedfriend.Name);
                            userAction.profile.getposts(conn);

                        }
                    }
                    if (userAction.action == 7)
                    {
                        SqlCommand cmd = new SqlCommand();
                        conn.Open();
                        cmd.Connection = conn;
                        cmd.CommandText = "update profil set aboutme = '" + userAction.changeaboutme + "' where id='" + userAction.focusedfriendid + "';";
                        cmd.CommandType = CommandType.Text;
                        cmd.ExecuteNonQuery();
                        conn.Close();
                        if (userAction.focusedfriendid == userAction.id)
                        {
                            userAction.focusedfriend = new Friend(userAction.focusedfriendid, conn);
                            userAction.profile = new Profile(userAction.focusedfriend, userAction.id, conn, userAction.username);
                            userAction.profile.getposts(conn);


                        }
                        else
                        {
                            userAction.focusedfriend = new Friend(userAction.focusedfriendid, conn);
                            userAction.profile = new Profile(userAction.focusedfriend, conn, userAction.focusedfriend.Name);
                            userAction.profile.getposts(conn);

                        }
                    }



                    userAction.setfriend();

                }
                if (userAction.action == 5 || userAction.action == 6)
                {
                    conn.Open();
                    userAction.setfriend();
                    if (userAction.action == 6)
                    {
                        if (userAction.sendmessage != "")
                        {
                            Increment inc = new Increment();
                            int result = inc.oblicz("messagesb");
                            SqlCommand cmd = new SqlCommand();
                            cmd.Connection = conn;
                            cmd.CommandType = CommandType.Text;
                            cmd.CommandText = "insert into messagesb(id,user1_id,user2_id,content,senddate,wsend) " +
                                "values ('" + result + "','" + userAction.id + "','" + userAction.focusedfriendid + "','" + userAction.sendmessage + "',GETDATE(),'" + userAction.id + "') ";
                            cmd.ExecuteNonQuery();
                            userAction.sendmessage = "";
                            conn.Close();
                        }
                    }
                    userAction.setlastmessages();


                }

                conn.Close();
                userAction.getgroups();
                userAction.GetFriends();
                userAction.GetRequests();
                userAction.SetProfPic();
                userAction.getlastthree();
                return View(userAction);
            }
            else if (userAction.action == 10)
            { 
            return View(userAction);
            }
            else
            {

                return View("Blad");
            }
        }
        public ActionResult LogInto(User user)
        {



            result = getid(user.username, user.password, conn);
            UserAction userAction = new UserAction(user, 0, result);
            conn.Close();
            userAction.GetFriends();
            userAction.GetRequests();
            userAction.getlastthree();
            userAction.getgroups();
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
            userex.createprof(temp1,conn);
            
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