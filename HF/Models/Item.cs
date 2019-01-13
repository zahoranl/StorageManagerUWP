using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace HF.Models
{
    [XmlRootAttribute]
    public class Item
    {
        [XmlAttribute]
        public int Id { get; set; }
        [XmlAttribute]
        public string Title { get; set; }
        [XmlAttribute]
        public string Position { get; set; }
        [XmlAttribute]
        public string Descreption { get; set; }
        [XmlAttribute]
        public int Quantity { get; set; }
        [XmlAttribute]
        public int CriticalQuantity { get; set; }
        [XmlAttribute]
        public int Price { get; set; }
        [XmlArray]
        public List<string> ExtraImages { get; set; }
        [XmlAttribute]
        public string Image { get; set; }
        [XmlArray]
        public List<HistroyPoint> ItemHistory
        {
            get;set;
        }


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
            
            //ItemHistory.Add(new HistroyPoint(DateTime.Now,-2, new User"Pityu"));
            //ItemHistory.Add(new HistroyPoint(DateTime.Now, -2, new User"Adam"));
            //ItemHistory.Add(new HistroyPoint(DateTime.Now, -2, "Zotyika"));
            //ItemHistory.Add(new HistroyPoint(DateTime.Now, -2, "Pityu"));
            //ItemHistory.Add(new HistroyPoint(DateTime.Now, -2, "Adam"));
            //ItemHistory.Add(new HistroyPoint(DateTime.Now, -2, "Zotyika"));
            //ItemHistory.Add(new HistroyPoint(DateTime.Now, -2, "Pityu"));
            //ItemHistory.Add(new HistroyPoint(DateTime.Now, -2, "Adam"));
            //ItemHistory.Add(new HistroyPoint(DateTime.Now, -2, "Zotyika"));
            //ItemHistory.Add(new HistroyPoint(DateTime.Now, -2, "Pityu"));
           
            ExtraImages = new List<string>();
            ExtraImages.Add("ms-appx:///Assets/car1.jpg");
            ExtraImages.Add("ms-appx:///Assets/car2.jpg");
        }
        public Item()
        {
        }
    }
}
