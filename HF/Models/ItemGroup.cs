using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HF.Models
{
    [XmlRootAttribute]
    public class ItemGroup
    {
        [XmlAttribute]
        public string Id { get; set; }
        [XmlAttribute]
        public string Title { get; set; }
        [XmlArray]
        public List<Item> itemList { get; set; }
        
        public ItemGroup(string title, List<Item> items)
        {
            Title = title;
            itemList = items;
        }
        public ItemGroup()
        {

        }
        public override string ToString()
        {
            return Title;
        }
    }
}
