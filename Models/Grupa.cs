using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMIv3.Models
{
    public class Grupa
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string about { get; set; }
        public string url { get; set; }

        public Grupa(int id, SqlConnection conn) {
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = conn;
            cmd.CommandText = "select name,about,url from groups where id =" + id;
            cmd.CommandType=System.Data.CommandType.Text;
            SqlDataAdapter adapter = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            adapter.Fill(dt);
            foreach (DataRow row in dt.Rows)
            {
                Name = row[0].ToString();
                about = row[1].ToString();
                url = row[2].ToString();

            
            
            }
        
        }

    }
}
