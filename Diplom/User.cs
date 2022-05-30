using System;
using System.Collections.Generic;

namespace Diplom
{
    class User : Human
    {
        private string email;
        private string password;
        private string numberSROO;
        private string nameSROO;
        private string insurance;
        private int experience;
        private string education;//номер и дата выдачи документов потверждающих получение проф навков в области оцен деят
        private string membership;//свидетельство о челнстве
        private Company company;

        public User(int id, string name, string surname, string patronym, string telephone, string pasportNumber, DateTime pasportDate, string pasportWhere, string email, string password, string numberSROO,
            string nameSROO, string insurance, int experience, string education, string membership) : base(id, name, surname, patronym, telephone, pasportNumber, pasportDate, pasportWhere)
        {
            this.email = email;
            this.password = password;
            this.numberSROO = numberSROO;
            this.nameSROO = nameSROO;
            this.insurance = insurance;
            this.experience = experience;
            this.education = education;
            this.membership = membership;
        }
        public User(string name, string surname, string patronym, string telephone, string email, string password, string numberSROO, string nameSROO, string insurance, int experience, string education, string membership)
            : base(name, surname, patronym, telephone)
        {
            this.email = email;
            this.password = password;
            this.numberSROO = numberSROO;
            this.nameSROO = nameSROO;
            this.insurance = insurance;
            this.experience = experience;
            this.education = education;
            this.membership = membership;
        }
        public User(int id, string name, string surname, string patronym, string email) : base(id, name, surname, patronym)
        {
            this.email = email;
        }
        public User(int id, string name, string surname, string patronym, string telephone, string email, string password, string numberSROO, string nameSROO, string insurance, int experience, string education, string membership)
            : base(id, name, surname, patronym, telephone)
        {
            this.email = email;
            this.password = password;
            this.numberSROO = numberSROO;
            this.nameSROO = nameSROO;
            this.insurance = insurance;
            this.experience = experience;
            this.education = education;
            this.membership = membership;
        }

        public string Email { get => email; set => email = value; }
        public string Password { get => password; set => password = value; }
        public string NumberSROO { get => numberSROO; set => numberSROO = value; }
        public string NameSROO { get => nameSROO; set => nameSROO = value; }
        public string Insurance { get => insurance; set => insurance = value; }
        public int Experience { get => experience; set => experience = value; }
        public string Education { get => education; set => education = value; }
        public string Membership { get => membership; set => membership = value; }
        internal Company Company { get => company; set => company = value; }

        public Dictionary<string, string> getCode()
        {
            Dictionary<string, string> codes = new Dictionary<string, string>();
            codes.Add("$nameUser", Name);
            codes.Add("$surnameUser", Surname);
            codes.Add("$patronymUser", Patronym);
            codes.Add("$telephoneUser", Telephone);
            codes.Add("$pasportNumberUser", PasportNumber);
            codes.Add("$pasportDateUser", PasportDate.ToString());
            codes.Add("$pasportWhereUser", PasportWhere);
            codes.Add("$emailUser", email);
            codes.Add("$numberSROO", numberSROO);
            codes.Add("$nameSROO", nameSROO);
            codes.Add("$insuranceUser", insurance);
            codes.Add("$experience", experience.ToString());
            codes.Add("$education", education);
            codes.Add("$membership", membership);
            codes.Add("$Valuer", Surname + " " + Name + " " + Patronym + ", номер СРОО: " + numberSROO + ", краткое наименование СРОО: \"" + nameSROO + "\"");
            if (company != null)
            {
                codes.Add("$nameEC", company.Name);
                codes.Add("$adresEC", company.Adres);
                codes.Add("$ogrnEC", company.OGRN);
                codes.Add("$dateOGRNEC", company.DateOGRN.ToString());
                codes.Add("$formEC", company.Form);
                codes.Add("$emailEC", company.Email);
                codes.Add("$telehoneEC", company.Telephone);
                codes.Add("$insuranceEC", company.Telephone);
            }
            return codes;
        }
    }
}