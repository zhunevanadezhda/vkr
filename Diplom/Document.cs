using System.Collections.Generic;

namespace Diplom
{
    class Document
    {
        private int id;
        private string name;
        private List<string> listPic;

        public Document()
        {
        }

        public Document(int id, string name)
        {
            this.id = id;
            this.name = name;
        }

        public Document(string name,string pic)
        {
            this.id = 0;
            this.name = name;
            this.listPic = new List<string>();
            this.listPic.Add(pic);
        }

        public Document(int id, string name, List<string> listPic)
        {
            this.Id = id;
            this.Name = name;
            this.ListPic = listPic;
        }

        public int Id { get => id; set => id = value; }
        public string Name { get => name; set => name = value; }
        internal List<string> ListPic { get => listPic; set => listPic = value; }

        public bool isPicExist(string name)
        {
            return !this.listPic.Exists(x=>x==name);
        }
    }
}
