using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using Template10.Mvvm;

namespace HF.Models
{
    [XmlRootAttribute]
    public class ItemGroup : BindableBase
    {
        [XmlAttribute]
        public string Id { get; set; }
        [XmlAttribute]
        private string title;
        public string Title
        {
            get { return title; }
            set { Set(ref title, value, nameof(Title)); }
        }

        [XmlArray]
        public List<Item> itemList { get; set; }
        
        public ItemGroup(string title)
        {
            Title = title;
            itemList = new List<Item>();
        }
        public ItemGroup(string title, List<Item> list)
        {
            Title = title;
            itemList = list;
        }
        public ItemGroup()
        {

        }
    }
}
