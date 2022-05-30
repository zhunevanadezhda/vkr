using System;
using System.Windows.Forms;

namespace Diplom
{
    public partial class SignIn : Form
    {
        private ErrorProvider error;
       // private NpgsqlConnection conn;//_sConnStr;
        private bool _isLoginValid = false;
        private bool _isPasswordValid = false;
        public int Autorization=0;
        public SignIn()
        {
            InitializeComponent();
            error = new System.Windows.Forms.ErrorProvider();
            error.SetIconAlignment(login_tb, ErrorIconAlignment.MiddleRight);
            error.SetIconPadding(login_tb, 2);
            error.BlinkRate = 1000;
            error.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            signIn_bt.Enabled = false;
            password_tb.UseSystemPasswordChar = true;
            password_tb.MaxLength = 25;
            signIn_bt.FlatStyle = FlatStyle.Popup;
            signUp_bt.FlatStyle = FlatStyle.Popup;
        }

        private void SyncVhod()
        {
            signIn_bt.Enabled = _isLoginValid && _isPasswordValid;
        }

        private void login_tb_TextChanged(object sender, EventArgs e)
        {
            if (login_tb.Text != "") _isLoginValid = true;
            else _isLoginValid = false;
            SyncVhod();
        }

        private void password_tb_TextChanged(object sender, EventArgs e)
        {
            if (password_tb.Text != "") _isPasswordValid = true;
            else _isPasswordValid = false;
            SyncVhod();
        }

        private void signIn_bt_Click(object sender, EventArgs e)
        {
            BD dataBase = new BD();
            bool b = dataBase.CheckUser(login_tb.Text, password_tb.Text);
            //List<string>list= dataBase.CheckUser(login_tb.Text, password_tb.Text);
            if (b)
            {
                /*Form1 form = new Form1(login_tb.Text);
                this.Hide();
                form.ShowDialog();*/
                Autorization = dataBase.GetUserId(login_tb.Text);
               // MessageBox.Show(Autorization+" ", "Такого аккаунта не существует", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                this.Close();
            }
            else
                MessageBox.Show("Неверно введен пароль или email", "Такого аккаунта не существует", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            //label1.Text = list[0]+" "+Equals(list[0],list[1]);
            //label2.Text = list[1];
        }

        private void signUp_bt_Click(object sender, EventArgs e)
        {
            SignUp form = new SignUp();
            this.Hide();
            form.ShowDialog();
            this.Show();
        }
    }
}
