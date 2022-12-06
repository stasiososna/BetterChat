using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMIv3.Models
{
    public class Posty
    {
        public int Id { get; set; }
        public string content { get; set; }
        public int sender { get; set; }
        public Posty(int id,string value) {
        content = value;
            sender = id;
            
        
        
        }
    }
}
