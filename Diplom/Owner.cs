using System.Collections.Generic;

namespace Diplom
{
    class Owner
    {
        List<Human> fizOwners=null;
        List<Company> urOwners = null;

        internal List<Human> FizOwners { get => fizOwners; set => fizOwners = value; }
        internal List<Company> UrOwners { get => urOwners; set => urOwners = value; }

        public Owner(List<Human> fizOwners, List<Company> urOwners)
        {
            this.fizOwners = fizOwners;
            this.urOwners = urOwners;
        }
        public Owner()
        {}
        public Owner(List<Human> fizOwners)
        {
            this.fizOwners = fizOwners;
        }
        public Owner(List<Company> urOwners)
        {
            this.urOwners = urOwners;
        }

        public Dictionary<string,string> getCodes()
        {
            Dictionary<string,string> codes = new Dictionary<string, string>();
            int k = 1;
            foreach (Company c in UrOwners)
            { codes.Add(k.ToString(), c.Form + " \"" + c.Name + "\", " + c.Adres + ", " + c.OGRN + ", " + c.DateOGRN + ", " + c.Email + ", " + c.Telephone);
                k++;
            }
            foreach (Human h in FizOwners)
            { codes.Add(k.ToString(), h.Surname + " " + h.Name + " " + h.Patronym + "; паспортные данные" + h.PasportNumber + ", выдан:" + h.PasportDate + ", " + h.PasportWhere);
                k++;
            }
            return codes;
        }
    }
}