using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HF.Models
{
    [XmlRootAttribute]
    public class User
    {
        [XmlAttribute]
        public string Name { get; set; }
        public string Password { get; set; }

        public User(string name,string pass)
        {
            Name = name;
            Password = pass;
        }
        public User()
        {

        }
        public override string ToString()
        {
            return Name;
        }
    }
}
