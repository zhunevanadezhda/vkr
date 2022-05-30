using System;
using System.Windows.Forms;

namespace Diplom
{
    public partial class UpdatePassword : Form
    {
        private int UserId;
        private BD dataBase=new BD();
        private bool oldParole = false;
        private bool parole = false;
        private ErrorProvider error;
        public UpdatePassword(int id_u)
        {
            InitializeComponent();
            UserId = id_u;
            save_bt.FlatStyle = FlatStyle.Popup;
            cancel_bt.FlatStyle = FlatStyle.Popup;
            error = new System.Windows.Forms.ErrorProvider();
            error.SetIconAlignment(password2_tb, ErrorIconAlignment.MiddleRight);
            error.SetIconPadding(password2_tb, 2);
            error.BlinkRate = 1000;
            error.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            oldParole_tb.UseSystemPasswordChar = true;
            password_tb.UseSystemPasswordChar = true;
            password2_tb.UseSystemPasswordChar = true;
        }

        private void Sync()
        {
            save_bt.Enabled = oldParole && parole;
        }

        private void password_tb_TextChanged(object sender, EventArgs e)
        {
            if (password_tb.Text != "" && password2_tb.Text == password_tb.Text) parole = true;
            else parole = false;
            Sync();
        }

        private void password2_tb_TextChanged(object sender, EventArgs e)
        {
            if (password2_tb.Text != "" && password2_tb.Text == password_tb.Text) parole = true;
            else parole = false;
            Sync();
        }

        private void cancel_bt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void save_bt_Click(object sender, EventArgs e)
        {
            if (dataBase.CheckUser(UserId, oldParole_tb.Text))
            {
                dataBase.UpdatePassword(UserId, password_tb.Text);
                this.Close();
            }
            else MessageBox.Show("Пароль был введен не верно", "Внимание", MessageBoxButtons.OK);
        }

        private void oldParole_tb_TextChanged(object sender, EventArgs e)
        {
            if (oldParole_tb.Text != "") oldParole = true;
            else oldParole = false;
            Sync();
        }
    }
}