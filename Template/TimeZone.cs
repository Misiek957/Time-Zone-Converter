using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleApp2.Template
{
    public class TimeZone
    {
        public string name { get; set; }
        public int interestOffset { get; set; }
        public string[] locations;
        public string interestHour;
        public string interestMin;

        public TimeZone(string inputName, int inputOffset, string inputHour, string inputMin)
        {
            this.name = inputName;
            this.interestOffset = inputOffset;
            this.interestHour = inputHour;
            this.interestMin = inputMin;
        }
    }
}
