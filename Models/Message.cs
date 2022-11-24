using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MMIv3.Models
{
    public class Message
    {
        public int id { get; set; }
        public int friendid { get; set; }
        public string content { get; set; }
        public DateTime sendtime { get; set; }
    }
}
