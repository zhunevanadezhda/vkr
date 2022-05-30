namespace Diplom
{
    class Element
    {
        private string name;
        private string unit;
        private string value;
        private int number;
        private int correct;

        public Element()
        {
        }

        public Element(string name, string unit, string value,int number, int correct)
        {
            this.name = name;
            this.unit = unit;
            this.value = value;
            this.number = number;
            this.correct = correct;
        }

        public string Name { get => name; set => name = value; }
        public string Unit { get => unit; set => unit = value; }
        public string Value { get => value; set => this.value = value; }
        public int Correct { get => correct; set => correct = value; }
        public int Number { get => number; set => number = value; }        
    }
}
