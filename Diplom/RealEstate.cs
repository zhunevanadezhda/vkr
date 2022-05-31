using System.Collections.Generic;

namespace Diplom
{
    class RealEstate
    {
        private int id;
        private string adres;
        private string region;
        private int price;//рыночная стоимость
        private string kadastrNumber;
        private string rights;
        private string link;
        private string telephone;
        private List<Element> elements;

        public RealEstate(int id, string adres, string region,int price, string link, string telephone)
        {
            this.id = id;
            this.adres = adres;
            this.region = region;
            this.price = price;
            this.link = link;
            this.telephone = telephone;
        }
        public RealEstate(int id, string adres, string region,int price, string kadastrNumber, string rights, string link, string telephone)
        {
            this.id = id;
            this.adres = adres;
            this.region = region;
            this.price = price;
            this.kadastrNumber = kadastrNumber;
            this.rights = rights;
            this.link = link;
            this.telephone = telephone;
        }
        public RealEstate()
        {}
        public RealEstate(int id, string adres, string region, string kadastrNumber, string rights)
        {
            this.id = id;
            this.adres = adres;
            this.region = region;
            this.kadastrNumber = kadastrNumber;
            this.rights = rights;
        }
        public RealEstate(string adres, string region, string kadastrNumber, string rights)
        {
            this.adres = adres;
            this.region = region;
            this.kadastrNumber = kadastrNumber;
            this.rights = rights;
        }

        public int Id { get => id; set => id = value; }
        public string Adres { get => adres; set => adres = value; }
        public string Region { get => region; set => region = value; }
        public int Price { get => price; set => price = value; }
        public string KadastrNumber { get => kadastrNumber; set => kadastrNumber = value; }
        public string Rights { get => rights; set => rights = value; }
        public string Link { get => link; set => link = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        internal List<Element> Elements { get => elements; set => elements = value; }

        public bool isElementExist(string name)
        {
            return elements.Exists(x => x.Name == name);
        }
        public Dictionary<string,string> getCodes()
        {
            Dictionary<string, string> codes = new Dictionary<string, string>();
            codes.Add("$adresRE", adres);
            codes.Add("$regionRE", region);
            codes.Add("$kadastrNumberRE", kadastrNumber);
            codes.Add("$rights", rights);
            if (elements!=null)
            {
                foreach (Element e in elements)
                {
                    if (string.IsNullOrEmpty(e.Unit))
                        codes.Add("$elementName[" + e.Name + "]", e.Name);
                    else codes.Add("$elementName[" + e.Name + "]", e.Name+", "+e.Unit);
                    codes.Add("$elementValue[" + e.Name + "]", e.Value);
                }
            }
            return codes;
        }
        public string getValue(string name)
        {
            return elements.Find(x => x.Name == name).Value;
        }
        public int getCorrect(string name)
        {
            return elements.Find(x => x.Name == name).Correct;
        }
    }
}
