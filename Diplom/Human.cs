using System;

namespace Diplom
{
    class Human
    {
        private int id;
        private string name;
        private string surname;
        private string patronym;
        private string telephone;
        private string pasportNumber;
        private DateTime pasportDate;
        private string pasportWhere;
        private bool isOwner;

        public Human(int id, string name, string surname, string patronym, string telephone, string pasportNumber, DateTime pasportDate, string pasportWhere)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.patronym = patronym;
            this.telephone = telephone;
            this.pasportNumber = pasportNumber;
            this.pasportDate = pasportDate;
            this.pasportWhere = pasportWhere;
        }
        public Human(string name, string surname, string patronym, string telephone, string pasportNumber, DateTime pasportDate, string pasportWhere) : this(name, surname, patronym, telephone)
        {
            this.pasportNumber = pasportNumber;
            this.pasportDate = pasportDate;
            this.pasportWhere = pasportWhere;
        }
        public Human(int id, string name, string surname, string patronym, string telephone, string pasportNumber, DateTime pasportDate, string pasportWhere, bool isOwner)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.patronym = patronym;
            this.telephone = telephone;
            this.pasportNumber = pasportNumber;
            this.pasportDate = pasportDate;
            this.pasportWhere = pasportWhere;
            this.isOwner = isOwner;
        }
        public Human(string name, string surname, string patronym, string telephone, string pasportNumber, DateTime pasportDate, string pasportWhere, bool isOwner)
        {
            this.name = name;
            this.surname = surname;
            this.patronym = patronym;
            this.telephone = telephone;
            this.pasportNumber = pasportNumber;
            this.pasportDate = pasportDate;
            this.pasportWhere = pasportWhere;
            this.isOwner = isOwner;
        }
        public Human()
        {
        }
        public Human(string name, string surname, string patronym, string telephone)
        {
            this.name = name;
            this.surname = surname;
            this.patronym = patronym;
            this.telephone = telephone;
        }
        public Human(int id, string name, string surname, string patronym)
        {
            this.id = id;
            this.name = name;
            this.surname = surname;
            this.patronym = patronym;
        }
        public Human(int id, string name, string surname, string patronym, string telephone) : this(id, name, surname, patronym)
        {
            this.telephone = telephone;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Surname { get => surname; set => surname = value; }
        public string Patronym { get => patronym; set => patronym = value; }
        public string Telephone { get => telephone; set => telephone = value; }
        public string PasportNumber { get => pasportNumber; set => pasportNumber = value; }
        public DateTime PasportDate { get => pasportDate; set => pasportDate = value; }
        public string PasportWhere { get => pasportWhere; set => pasportWhere = value; }
        public bool IsOwner { get => isOwner; set => isOwner = value; }
    }
}
