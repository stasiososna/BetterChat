using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Antlr.Runtime.Tree;
using System.Web.Services.Protocols;

namespace MMIv3.Models
{
    public class UserAction : User
    {
        public int action { get; set; }
        public int id { get; set; }
        public string url { get; set; }
        public List<Friend> Friendlist;
        public List<Friend> WaitingReqlist;
        public List<Friend> lastthree;
        public int[] idoffriends = new int[3];
        public string sendreqname { get; set; }
        public int accepted { get; set; }
        public int denied { get; set; }
        public SqlConnection conn;
        public int focusedfriendid { get; set; }
        public Friend focusedfriend { get; set; }
        public List<Message> lastfive;
        public string sendmessage { get; set; }
        public Profile profile { get; set; }
        public string changeaboutme { get; set; }
        public string postcontent { get; set; }
        public List<Grupa> groups { get; set; }
        public string groupimagepath { get; set; }
        public string grname { get; set; }
        public string grabout { get; set; }
        public int ifmyprofile { get; set; }
        public string email { get; set; }
        public bool disablesmt { get; set; }
        public int focusedgroupid { get; set; }



        public UserAction(User user, int action2, int id2)
        {

            action = action2;
            username = user.username;
            password = user.password;
            id = id2;
            conn = new SqlConnection(@"Data Source=DESKTOP-07OANPP;Initial Catalog=mmi1;Integrated Security=True");

        }

        public UserAction() {
            conn = new SqlConnection(@"Data Source=DESKTOP-07OANPP;Initial Catalog=mmi1;Integrated Security=True");
        }
        public void getlastthree() { 
            lastthree = new List<Friend>();
            for (int j = 0; j < 3; j++)
            {
                idoffriends[j] = 0;
            
            }
            getmessages();
            int i = 0;
            while (i<3 )
            {
                if(idoffriends[i] != 0)
                lastthree.Add(new Friend(idoffriends[i], conn));
                i++;
            }
            
        
        
        
        
        }
        public void setlastmessages() {
            lastfive = new List<Message>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select content,wsend from messagesb where user1_id in (" + id + "," + focusedfriendid + ") and  user2_id in (" + id + "," + focusedfriendid + ") order by senddate ";
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                if (row[0] != null)
                {
                    if (Int32.Parse(row[1].ToString()) != id)
                    {
                        string content = row[0].ToString();
                        lastfive.Add(new Message(content) { wsend = "You" });
                    }
                    else {
                        string content = row[0].ToString();
                        lastfive.Add(new Message(content) { wsend = "Me" });

                    }
                }
                else
                {
                    lastfive.Add(new Message("This is the beginning of conversation. Remember to be nice!"));
                }
            
            }
          





        }
        

        public void getgroups() {
            groups = new List<Grupa>();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select id from groups where ownerid=" + id;
            cmd.CommandType = CommandType.Text;
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                groups.Add(new Grupa(Int32.Parse(row[0].ToString()),conn));
            
            
            }
        
        
        
        
        }
        public void GetFriends()
        {
            Friendlist = new List<Friend>();
            
            
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
        public void setfriend() {
            focusedfriend = new Friend();
            
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = conn;
            cmd.CommandText = "select username, avatar from User1 where id = '" + focusedfriendid + "'";
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                focusedfriend.Name = row[0].ToString();
                focusedfriend.avatar = row[1].ToString();
                focusedfriend.Id = focusedfriendid;
            
            }
        
        
        
        }
        public void getmessages() {


            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select user1_id, user2_id from messagesb where  (user1_id = '" + id + "' or user2_id = '" + id + "') order by senddate";
            SqlDataAdapter sda= new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                int id1 = Int32.Parse(row[0].ToString());
                int id2 = Int32.Parse(row[1].ToString());



                if (id1 == id)
                {
                    if (id2 != idoffriends[0]&& id2 != idoffriends[1]&& id2 != idoffriends[2])
                    if (idoffriends[0] != 0)
                    {
                        if (idoffriends[1] != 0)
                        {
                            if (idoffriends[2] != 0)
                            {
                            }
                            else
                                idoffriends[2] = id2;

                        }
                        else
                            idoffriends[1] = id2;
                    }
                    else
                        idoffriends[0] = id2;


                }
                if (id2 == id)
                {
                    if (id1 != idoffriends[0] && id1 != idoffriends[1] && id1 != idoffriends[2])
                        if (idoffriends[0] != 0)
                    {
                        if (idoffriends[1] != 0)
                        {
                            if (idoffriends[2] != 0)
                            {
                            }
                            else
                                idoffriends[2] = id1;

                        }
                        else
                            idoffriends[1] = id1;
                    }
                    else
                        idoffriends[0] = id1;


                }


            }






            
        }
        
            public void SetProfPic()
        {
            
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
                url = "/images/" +  row[0].ToString();

            }


        }
        public void GetRequests()
        {
            WaitingReqlist = new List<Friend>();
            
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
            conn.Close();


        }



    }
}
