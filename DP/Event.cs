using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DP
{
    internal class Event
    {
        public int id { get; set; }
        public string name { get; set; }
        public string date { get; set; }
        public string time { get; set; }
        public string other { get; set; }


        public Event(int id, string name, string date, string time, string other)
        {
            this.id = id;
            this.name = name;
            this.date = date;
            this.time = time;
            this.other = other;
        }
    }
}
