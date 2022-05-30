namespace Diplom
{
    class Costumer
    {
        bool type;//0-физ, 1-юрид
        Human fiz;
        Company ur;

        public Costumer(Company ur)
        {
            this.ur = ur;
            this.type = true;
        }

        public Costumer(Human fiz)
        {
            this.fiz = fiz;
            this.type = false;
        }

        public Costumer()
        {
            this.type = false;
        }

        public Costumer(bool type)
        {
            this.type = type;
        }

        public bool Type { get => type; set => type = value; }
        internal Human Fiz { get => fiz; set => fiz = value; }
        internal Company Ur { get => ur; set => ur = value; }
    }
}
