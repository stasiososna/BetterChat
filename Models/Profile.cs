using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace MMIv3.Models
{
    public class Profile
    {
        public Friend Friend { get; set; }
        public List<Posty> posty;
        public int ifmeid=0;
        public string username { get; set; }
        public string aboutme { get; set; }

        public Profile(Friend friend1, SqlConnection conn, string name)
        {
            username = name;
            posty = new List<Posty>();
            Friend = friend1;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select aboutme from profil where id = '" + friend1.Id + "'";

            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                aboutme = row[0].ToString();
            }

        
        }
        public Profile(Friend friend1,int ifmeid2, SqlConnection conn,string name)
        {
            ifmeid = ifmeid2;
            posty = new List<Posty>();
            username = name;
            Friend = friend1;
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select aboutme from profil where id = '" + ifmeid + "'";
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                aboutme = row[0].ToString();
            }


        }
        public void getposts(SqlConnection conn) {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = CommandType.Text;
            if (ifmeid != 0)
                cmd.CommandText = "select adderid,content from posts where adderid==" + ifmeid + ";";
            else 
                cmd.CommandText= "select adderid,content from posts where adderid==" + Friend.Id + ";";
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                posty.Add(new Posty(Int32.Parse(row[0].ToString()), row[1].ToString()));
            
            
            }




        }






    }
}
