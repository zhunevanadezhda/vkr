using System;
using System.Windows.Forms;

namespace Diplom
{
    public partial class SignUp : Form
    {
        private bool _isEmailValid = false;
        //private bool _isImageLoaded = false;
        private bool _isPasswordValid = false;
        private bool _isPassword2Valid = false;
        //private bool _isSurnameValid = false;
        //private bool _isNameValid = false;
        private bool _isCompany = false;
        //private bool _isNumberSROO = false;
        //private bool _isNameSROO = false;
        private bool _isNameCom = false;
        private bool _isFormCom = false;
        private bool _isOGRN = false;
        private bool _isDateOGRN = false;
        private bool _isPasport = true;
        private bool _isPasportNumber = false;
        private bool _isPasportDate = false;
        private bool _isPasportWhere = false;
        private ErrorProvider epMain;
        private ErrorProvider error;
        BD dataBase = new BD();
        public SignUp()
        {
            InitializeComponent();
            epMain = new System.Windows.Forms.ErrorProvider();
            epMain.SetIconAlignment(email_tb, ErrorIconAlignment.MiddleRight);
            epMain.SetIconPadding(email_tb, 2);
            epMain.BlinkRate = 1000;
            epMain.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;

            error = new System.Windows.Forms.ErrorProvider();
            error.SetIconAlignment(password2_tb, ErrorIconAlignment.MiddleRight);
            error.SetIconPadding(password2_tb, 2);
            error.BlinkRate = 1000;
            error.BlinkStyle = System.Windows.Forms.ErrorBlinkStyle.AlwaysBlink;
            signUp_bt.Enabled = false;
            password_tb.UseSystemPasswordChar = true;
            password2_tb.UseSystemPasswordChar = true;

            cancel_bt.FlatStyle = FlatStyle.Popup;
            signUp_bt.FlatStyle = FlatStyle.Popup;
        }

        private void Sync()
        {
            signUp_bt.Enabled = _isEmailValid && _isPasswordValid && _isPassword2Valid && (company_rb.Checked && _isCompany || independently_rb.Checked && _isPasport);
        }
        private void SyncCom()
        {
            _isCompany = _isOGRN && _isFormCom && _isNameCom && _isDateOGRN;
            Sync();
        }
        private void SyncPas()
        {
            _isPasport = (_isPasportNumber && _isPasportDate && _isPasportWhere) || !(_isPasportNumber || _isPasportDate || _isPasportWhere);
            Sync();
        }

        private void email_tb_TextChanged(object sender, EventArgs e)
        {
            if (email_tb.Text != "") {
                bool b=dataBase.isUser(email_tb.Text);
                if (b)
                {
                    epMain.SetError(email_tb, "Данный Email уже занят");
                    _isEmailValid = false;
                }
                else
                {
                    epMain.SetError(email_tb, "");
                    _isEmailValid = true;
                }
            }
            else _isEmailValid = false;
            Sync();
        }
        private void password_tb_TextChanged(object sender, EventArgs e)
        {
            if (password_tb.Text != "") _isPasswordValid = true;
            else _isPasswordValid = false;
            Sync();
        }
        private void password2_tb_TextChanged(object sender, EventArgs e)
        {
            if (password2_tb.Text != "") _isPassword2Valid = true;
            else _isPassword2Valid = false;
            Sync();
        }
        
        private void signUp_bt_Click(object sender, EventArgs e)
        {
            if (password_tb.Text != password2_tb.Text)
            {
                error.SetError(password2_tb, "Пароль веден неверно");
                _isPassword2Valid = false;
            }
            else
            {
                error.SetError(password2_tb, "");
                _isPassword2Valid = true;
                int experience = -1;
                if (experience_tb.Text!="") experience = Int32.Parse(experience_tb.Text);
                User newUser = new User(name_tb.Text,surname_tb.Text, patronym_tb.Text,telephone_tb.Text, email_tb.Text, password_tb.Text,  numberSROO_tb.Text, nameSROO_tb.Text, insuranceCom_tb.Text, experience, education_tb.Text, membership_tb.Text);
                if (_isCompany)
                {
                    Company company = dataBase.GetCompanyByOGRN(ogrnCom_tb.Text);
                    if (company != null)
                        newUser.Company = company;
                    else
                    {
                        company = new Company(nameCom_tb.Text, adresCom_tb.Text, ogrnCom_tb.Text, DateTime.Parse(dateogrnCom_tb.Text), formCom_tb.Text, emailCom_tb.Text, telephoneCom_tb.Text, insuranceCom_tb.Text);
                        int id = dataBase.AddCompany(company);
                        company.Id = id;
                        newUser.Company = company;
                    }
                }
                else if (_isPasportNumber)
                {
                    newUser.PasportDate = DateTime.Parse(pasportDate_tb.Text);
                    newUser.PasportNumber = pasportNumber_tb.Text;
                    newUser.PasportWhere = pasportWhere_tb.Text;
                }
                dataBase.AddUser(newUser);
                this.Close();
            }
        }

        private void company_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (company_rb.Checked == true) {
                independently_rb.Checked = false;
                nameCom_lb.Visible = nameCom_tb.Visible = adresCom_lb.Visible = adresCom_tb.Visible = ogrnCom_lb.Visible = ogrnCom_tb.Visible = dateogrnCom_lb.Visible = dateogrnCom_tb.Visible =
                        insuranceCom_lb.Visible = insuranceCom_tb.Visible = telephoneCom_lb.Visible = telephoneCom_tb.Visible = emailCom_lb.Visible = emailCom_tb.Visible = formCom_lb.Visible = 
                        formCom_tb.Visible = true;
                pasportNumber_lb.Visible = pasportNumber_tb.Visible = pasportDate_lb.Visible = pasportDate_tb.Visible = pasportWhere_lb.Visible = pasportWhere_tb.Visible = false;
                SyncCom();
            }
            Sync();
            //else independently_rb.Checked = true;
        }

        private void independently_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (independently_rb.Checked == true) { company_rb.Checked = false;
                nameCom_lb.Visible = nameCom_tb.Visible = adresCom_lb.Visible = adresCom_tb.Visible = ogrnCom_lb.Visible = ogrnCom_tb.Visible = dateogrnCom_lb.Visible = dateogrnCom_tb.Visible =
                        insuranceCom_lb.Visible = insuranceCom_tb.Visible = telephoneCom_lb.Visible = telephoneCom_tb.Visible = emailCom_lb.Visible = emailCom_tb.Visible = formCom_lb.Visible =
                        formCom_tb.Visible = false;
                pasportNumber_lb.Visible = pasportNumber_tb.Visible = pasportDate_lb.Visible = pasportDate_tb.Visible = pasportWhere_lb.Visible = pasportWhere_tb.Visible = true;
                _isCompany = false;
            }
            Sync();
            //else company_rb.Checked = true;
        }

        private void cancel_bt_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void nameCom_tb_TextChanged(object sender, EventArgs e)
        {
            if (nameCom_tb.Text != "") _isNameCom = true;
            else _isNameCom = false;
            SyncCom();
        }

        /*private void numberSROO_tb_TextChanged(object sender, EventArgs e)
        {
            if (numberSROO_tb.Text != "") _isNumberSROO = true;
            else _isNumberSROO = false;
            Sync();
        }

        private void nameSROO_tb_TextChanged(object sender, EventArgs e)
        {
            if (nameSROO_tb.Text != "") _isNameSROO = true;
            else _isNameSROO = false;
            Sync();
        }*/

        private void formCom_tb_TextChanged(object sender, EventArgs e)
        {
            if (formCom_tb.Text != "") _isFormCom = true;
            else _isFormCom = false;
            SyncCom();
        }

        private void ogrnCom_tb_TextChanged(object sender, EventArgs e)
        {
            if (ogrnCom_tb.Text != "") _isOGRN = true;
            else _isOGRN = false;
            SyncCom();
        }

        private void dateogrnCom_tb_TextChanged(object sender, EventArgs e)
        {
            if (dateogrnCom_tb.Text != "") _isDateOGRN = true;
            else _isDateOGRN = false;
            SyncCom();
        }

        private void pasportNumber_tb_TextChanged(object sender, EventArgs e)
        {
            if (pasportNumber_tb.Text != "") _isPasportNumber = true;
            else _isPasportNumber = false;
            SyncPas();
        }

        private void pasportDate_tb_TextChanged(object sender, EventArgs e)
        {
            if (pasportDate_tb.Text != "") _isPasportDate = true;
            else _isPasportDate = false;
            SyncPas();
        }

        private void pasportWhere_tb_TextChanged(object sender, EventArgs e)
        {
            if (pasportWhere_tb.Text != "") _isPasportWhere = true;
            else _isPasportWhere = false;
            SyncPas();
        }
    }
}
