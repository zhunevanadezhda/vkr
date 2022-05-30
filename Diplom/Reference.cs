using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace Diplom
{
    public partial class Reference : Form
    {
        public Reference()
        {
            InitializeComponent();
            load();
        }

        public void load()
        {
            string fileName = "обозначение.txt";
            IEnumerable<string> lines = File.ReadLines(fileName);
            foreach (string s in lines)
                listBox1.Items.Add(s);
        }
    }
}
