using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Template10.Mvvm;

namespace HF.Models
{
    [XmlRootAttribute]
    public class HistroyPoint: BindableBase
    {
        [XmlAttribute]
        public DateTime TimeStamp { get; set; }
        [XmlAttribute]
        public int Difference { get; set; }
        [XmlElement]
        public User Seller { get; set; }
        
        public HistroyPoint(DateTime time, int diff, User user)
        {
            TimeStamp = time;
            Difference = diff;
            Seller = user;
        }
        public HistroyPoint()
        {
        }
    }
}
