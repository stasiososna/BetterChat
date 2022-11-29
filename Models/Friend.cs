using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Runtime.Remoting.Metadata.W3cXsd2001;
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;

namespace MMIv3.Models
{
    public class Friend
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string avatar { get; set; }
        public Friend(int id2, string namee, string ava)
        {
            Id = id2;
            Name = namee;
            avatar = "/images/" + ava;


        }
        public Friend() { }
        public Friend(int id2,SqlConnection conn)
        {
            Id = id2;
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandType = System.Data.CommandType.Text;
            cmd.CommandText = "Select username,avatar from User1 where id ='" + Id + "'";
            SqlDataAdapter sda = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            sda.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                Name = row[0].ToString();
                avatar = row[1].ToString();
            
            
            }
        
        
        }
    }
}
