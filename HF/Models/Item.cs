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
    public class Item : BindableBase
    {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        private string title;
        public string Title
        {
            get { return title; }
            set { Set(ref title, value, nameof(Title)); }
        }
        [XmlAttribute]
        private string position;
        public string Position
        {
            get { return position; }
            set { Set(ref position, value, nameof(Position)); }
        }
        [XmlAttribute]
        private string description;
        public string Descreption
        {
            get { return description; }
            set { Set(ref description, value, nameof(Descreption)); }
        }
        [XmlAttribute]
        private int quantity;
        public int Quantity
        {
            get { return quantity; }
            set { Set(ref quantity, value, nameof(Quantity)); }
        }
        [XmlAttribute]
        private int criticalQuantity;
        public int CriticalQuantity
        {
            get { return criticalQuantity; }
            set { Set(ref criticalQuantity, value, nameof(CriticalQuantity)); }
        }
        [XmlAttribute]
        private int price;
        public int Price
        {
            get { return price; }
            set { Set(ref price, value, nameof(Price)); }
        }
        [XmlArray]
        public List<string> ExtraImages { get; set; }

        [XmlAttribute]
        public string Image { get; set; }

        [XmlArray]
        public List<HistroyPoint> ItemHistory { get; set; }


        public Item(string name){
            Title = name;
            Image = "ms-appx:///Assets/avatar.png";
            Position = "8/21/B";
            Descreption = "A képek mindent elmondanak, de ha kérdése van, vagy szeretné megnézni a gépkocsit hívjon bizalommal!Opel Magyarország által forgalomba helyezett garantált kilométer futás.Innovation felszereltség Barna Prémium Kárpit!+Innovation csomag Láblendítéssel nyitható csomagtérajtó(csak Sports Tourer esetén), Kulcs nélküli nyitás és indítás PEPS, Lopásgátló riasztórendszer, Vezeték nélküli, induktív telefontöltő, Szélvédőre vetített kijelző 8 színes fedélzeti számítógép kijelző(iO6 kötelező a csomaghoz), Tolatókamera, Hangszigetelés csomag, Hangszigetelt ablaküvegek, Navigáció csomag";
            Quantity = 12;
            CriticalQuantity = 10;
            Price = 1200;
            ItemHistory = new List<HistroyPoint>();
            ItemHistory.Add(new HistroyPoint(DateTime.Now.AddDays(-3), 20, new User("Admin", "0")));
            ItemHistory.Add(new HistroyPoint(DateTime.Now.AddDays(-2), -2, new User("Zotyika", "0")));
            ItemHistory.Add(new HistroyPoint(DateTime.Now.AddDays(-1), -2, new User("Zotyika", "0")));
            ItemHistory.Add(new HistroyPoint(DateTime.Now,-2,new User("Adam", "0")));
            ItemHistory.Add(new HistroyPoint(DateTime.Now,-2, new User("Zotyika", "0")));
           
            ExtraImages = new List<string>();
            ExtraImages.Add("ms-appx:///Assets/car1.jpg");
            ExtraImages.Add("ms-appx:///Assets/car2.jpg");
        }
        public Item()
        {
            Title = "";
            Image = "ms-appx:///Assets/avatar.png";
            Position = "";
            Descreption = "";
            Quantity = 0;
            CriticalQuantity = 0;
            Price = 0;
            ItemHistory = new List<HistroyPoint>();
            ExtraImages = new List<string>();
            ExtraImages.Add("ms-appx:///Assets/car1.jpg");
            ExtraImages.Add("ms-appx:///Assets/car2.jpg");
        }
    }
}
