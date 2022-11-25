using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMIv3.Models
{
    public class Message
    {

        public string content { get; set; }
        public string wsend { get; set; }
        public Message(string a) { 
        content = a;
        
        
        }
    }
}
