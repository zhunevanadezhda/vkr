using System;

namespace Diplom
{
    class Company
    {
        private int id;
        private string name;
        private string adres;
        private string ogrn;
        private DateTime dateOGRN;
        private string form;
        private string email;
        private string telephone;
        private string insurance;
        private bool isOwner;

        public Company()
        {
        }
        public Company(int id, string name, string adres, string oGRN, DateTime dateOGRN, string form, string email, string telephone, string insurance)
        {
            this.Id = id;
            this.Name = name;
            this.Adres = adres;
            this.OGRN = oGRN;
            this.DateOGRN = dateOGRN;
            this.Form = form;
            this.Email = email;
            this.Telephone = telephone;
            this.Insurance = insurance;
        }
        public Company(string name, string form, string telephone)
        {
            this.name = name;
            this.form = form;
            this.telephone = telephone;
        }
        public Company(string name, string adres, string ogrn, DateTime dateOGRN, string form, string email, string telephone, string insurance)
        {
            this.name = name;
            this.adres = adres;
            this.ogrn = ogrn;
            this.dateOGRN = dateOGRN;
            this.form = form;
            this.email = email;
            this.telephone = telephone;
            this.insurance = insurance;
        }
        public Company(int id, string name, string adres, string ogrn, DateTime dateOGRN, string form, string email, string telephone, bool isOwner)
        {
            this.id = id;
            this.name = name;
            this.adres = adres;
            this.ogrn = ogrn;
            this.dateOGRN = dateOGRN;
            this.form = form;
            this.email = email;
            this.telephone = telephone;
            this.isOwner = isOwner;
        }
        public Company(string name, string adres, string ogrn, DateTime dateOGRN, string form, string email, string telephone, bool isOwner)
        {
            this.name = name;
            this.adres = adres;
            this.ogrn = ogrn;
            this.dateOGRN = dateOGRN;
            this.form = form;
            this.email = email;
            this.telephone = telephone;
            this.isOwner = isOwner;
        }
        public Company(int id, string name, string form)
        {
            this.id = id;
            this.name = name;
            this.form = form;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Adres { get => adres; set => adres = value; }
        public string OGRN { get => ogrn; set => ogrn = value; }
        public DateTime DateOGRN { get => dateOGRN; set => dateOGRN = value; }
        public string Form { get => form; set => form = value; }
        public string Email { get => email; set => email = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string Insurance { get => insurance; set => insurance = value; }
        public bool IsOwner { get => isOwner; set => isOwner = value; }
    }
}
