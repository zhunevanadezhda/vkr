namespace Diplom
{
    class Parameter
    {
        private string podhod;
        private string metod;
        private string name;
        private double value;

        public Parameter(string name, double value)
        {
            this.name = name;
        //    this.unit = unit;
            this.value = value;
        }
        public Parameter(string podhod, string metod, string name, double value)
        {
            this.podhod = podhod;
            this.metod = metod;
            this.name = name;
         //   this.unit = unit;
            this.value = value;
        }
        public Parameter()
        {
        }

        public string Podhod { get => podhod; set => podhod = value; }
        public string Metod { get => metod; set => metod = value; }
        public string Name { get => name; set => name = value; }
       // public string Unit { get => unit; set => unit = value; }
        public double Value { get => value; set => this.value = value; }
    }
}
