using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;

namespace MMIv3.Models
{
    public class Increment
    {
        static SqlConnection conn = new SqlConnection(@"Data Source=ST14\SQLEXPRESS;Initial Catalog=mmi1;Integrated Security=True");
        public int oblicz(string db)
        {
            int result=1;
            conn.Open();
            SqlCommand cmd = conn.CreateCommand();
            cmd.CommandType = System.Data.CommandType.Text;
            
            cmd.CommandText = "select max(id) from "+db+";";
            try
            {
                result = (Int32)cmd.ExecuteScalar();
            }
            catch { }
            result++;
            
            conn.Close();
            if (result <= 0)
                result = 1;

            return result;
        }
    }
}