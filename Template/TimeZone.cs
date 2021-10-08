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
        public int gmtOffset { get; set; }
        public string[] locations;

        public TimeZone(string inputName, int inputOffset)
        {
            this.name = inputName;
            switch (inputName)
            {
                case "gmt":
                    this.gmtOffset = inputOffset;
                    this.locations = new string[2] { "London", "Lisbon" };
                    break;
                case "utc":
                    this.gmtOffset = inputOffset - 2;
                    this.locations = new string[2] { "Where", "Where" };
                    break;
                default:
                    Console.WriteLine("Unable to find time zone");
                    break;
            }
        }
    }
}
