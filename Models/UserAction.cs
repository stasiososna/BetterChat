using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMIv3.Models
{
    public class UserAction : User
    {
        public int action { get; set; }
        public int id { get; set; }
        public string url { get; set; }
        public List<Friend> Friendlist;
        public List<Friend> WaitingReqlist;
        public string sendreqname { get; set; }
        public int accepted { get; set; }
        public int denied { get; set; }



        public UserAction(User user, int action2, int id2)
        {

            action = action2;
            username = user.username;
            password = user.password;
            id = id2;

        }

        public UserAction() { }
        public void GetFriends()
        {
            Friendlist = new List<Friend>();
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HLJB7UO\SQLEXPRESS01;Initial Catalog=DDB2;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select user1_id, user2_id from Friends where currstate = 1 and (user1_id=" + id + " or user2_id= " + id + ")";
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                int id3 = Convert.ToInt32(row[0]);
                int id4 = Convert.ToInt32(row[1]);
                if (id3 == id)
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandText = "Select username, avatar from User1 where id = " + id4;
                    SqlDataAdapter sda2 = new SqlDataAdapter(sqlCommand);
                    DataTable dt2 = new DataTable();
                    sda2.Fill(dt2);
                    string name;
                    string ava;
                    foreach (DataRow row2 in dt2.Rows)
                    {
                        name = Convert.ToString(row2[0]);
                        ava = Convert.ToString(row2[1]);
                        Friend friend = new Friend(id4, name, ava);
                        Friendlist.Add(friend);

                    }

                }
                else
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandText = "Select username, avatar from User1 where id = " + id3;
                    SqlDataAdapter sda2 = new SqlDataAdapter(sqlCommand);
                    DataTable dt2 = new DataTable();
                    sda2.Fill(dt2);
                    string name;
                    string ava;
                    foreach (DataRow row2 in dt2.Rows)
                    {
                        name = Convert.ToString(row2[0]);
                        ava = Convert.ToString(row2[1]);
                        Friendlist.Add(new Friend(id3, name, ava));

                    }

                }


            }

            conn.Close();


        }
        public void SetProfPic()
        {
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HLJB7UO\SQLEXPRESS01;Initial Catalog=DDB2;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select avatar from User1 where id = " + id;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                url = row[0].ToString();

            }


        }
        public void GetRequests()
        {
            WaitingReqlist = new List<Friend>();
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HLJB7UO\SQLEXPRESS01;Initial Catalog=DDB2;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "select user1_id, user2_id from Friends where currstate = 0 and Wsend != '" + id + "' and (user1_id=" + id + " or user2_id= " + id + ")";
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                int id3 = Convert.ToInt32(row[0]);
                int id4 = Convert.ToInt32(row[1]);
                if (id3 == id)
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandText = "Select username, avatar from User1 where id = " + id4;
                    SqlDataAdapter sda2 = new SqlDataAdapter(sqlCommand);
                    DataTable dt2 = new DataTable();
                    sda2.Fill(dt2);
                    string name;
                    string ava;
                    foreach (DataRow row2 in dt2.Rows)
                    {
                        name = Convert.ToString(row2[0]);
                        ava = Convert.ToString(row2[1]);
                        Friend friend = new Friend(id4, name, ava);
                        WaitingReqlist.Add(friend);

                    }

                }
                else
                {
                    SqlCommand sqlCommand = new SqlCommand();
                    sqlCommand.Connection = conn;
                    sqlCommand.CommandType = System.Data.CommandType.Text;
                    sqlCommand.CommandText = "Select username, avatar from User1 where id = " + id3;
                    SqlDataAdapter sda2 = new SqlDataAdapter(sqlCommand);
                    DataTable dt2 = new DataTable();
                    sda2.Fill(dt2);
                    string name;
                    string ava;
                    foreach (DataRow row2 in dt2.Rows)
                    {
                        name = Convert.ToString(row2[0]);
                        ava = Convert.ToString(row2[1]);
                        WaitingReqlist.Add(new Friend(id3, name, ava));

                    }

                }


            }

            conn.Close();


        }
        public void SetUser(int id2)
        {
            id = id2;
            SqlConnection conn = new SqlConnection(@"Data Source=DESKTOP-HLJB7UO\SQLEXPRESS01;Initial Catalog=DDB2;Integrated Security=True");
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select username,password from User1 where id= " + id2;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                password = row[1].ToString();
                username = row[0].ToString();

            }


        }


    }
}
