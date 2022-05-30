namespace Diplom
{
    class Sample
    {
        private int id;
        private string name;
        private string path;

        public Sample(int id, string name, string path)
        {
            this.id = id;
            this.name = name;
            this.path = path;
        }
        public Sample(string name, string path)
        {
            this.name = name;
            this.path = path;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        public string Path { get => path; set => path = value; }
    }
}
