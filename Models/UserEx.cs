using Antlr.Runtime.Tree;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace MMIv3.Models
{
    public class UserEx
    {
        public string username { get; set; }
        public string password { get; set; }
        public string email { get; set; }
        public void createprof(int id,SqlConnection conn) {
            conn.Open();
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "insert into profil(id,username,aboutme) values ('" + id + "','" + username + "','About me ...');";
            cmd.CommandType= System.Data.CommandType.Text;
            cmd.ExecuteNonQuery();
        
        
        }
    }
}