using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
    }
}
