using System;
using System.Windows.Forms;

namespace Diplom
{
    public partial class RenameElementForm : Form
    {
        public string newName;
        private string OldName;
        public RenameElementForm(string oldName,string title, string description)
        {
            InitializeComponent();
            cancel_bt.FlatStyle = FlatStyle.Popup;
            OK_bt.FlatStyle = FlatStyle.Popup;
            this.Text = title;
            label1.Text = description;
            textBox1.Text = oldName;
            OldName = oldName;
            OK_bt.Enabled = false;
        }

        private void OK_bt_Click(object sender, EventArgs e)
        {
            newName = textBox1.Text;
        }

        private void cancel_bt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (textBox1.Text != OldName && textBox1.Text != "")
                OK_bt.Enabled = true;
            else OK_bt.Enabled = false;
        }
    }
}
