using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HF.Models
{
    public class ChartData
    {
        public string Name { get; set; }
        public int Sum { get; set; }
        public DateTime DateTime { get; set; }
        public bool Summable { get; set; }

        public ChartData(string name, int sum)
        {
            Name = name;
            Sum = sum;
            Summable = false;
        }
        public ChartData(string name, DateTime dateTime)
        {
            Name = name;
            DateTime = dateTime;
            Summable = false;
        }
        public ChartData(DateTime dateTime,int sum)
        {
            Sum = sum;
            DateTime = dateTime;
            Summable = false;
        }
        public ChartData()
        {
            Summable = false;
        }
        public  void SetDateToName()
        {
            Name = DateTime.Year.ToString() + "." + DateTime.Month.ToString() + "." + DateTime.Day.ToString() + ".";
        }
    }
}
