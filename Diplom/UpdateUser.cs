using System;
using System.Windows.Forms;

namespace Diplom
{
    public partial class UpdateUser : Form
    {
        private bool _isCompany = false;
        private bool _isNameCom = false;
        private bool _isFormCom = false;
        private bool _isOGRN = false;
        private bool _isDateOGRN = false;
        private bool _isPasport = true;
        private bool _isPasportNumber = false;
        private bool _isPasportDate = false;
        private bool _isPasportWhere = false;
        private BD dataBase = new BD();
        private int Id;
        public UpdateUser(int id)
        {
            InitializeComponent();
            User user = dataBase.GetUserById(id);
            cancel_bt.FlatStyle = FlatStyle.Popup;
            save_bt.FlatStyle = FlatStyle.Popup;
            load(user);
        }

        private void load(User user)
        {
            surname_tb.Text = user.Surname;
            name_tb.Text = user.Name;
            patronym_tb.Text = user.Patronym;
            telephone_tb.Text = user.Telephone;
            if (user.Experience > -1)
                experience_tb.Text = user.Experience.ToString();
            numberSROO_tb.Text = user.NumberSROO;
            nameSROO_tb.Text = user.NameSROO;
            membership_tb.Text = user.Membership;
            insurance_tb.Text = user.Insurance;
            education_tb.Text = user.Education;
            if (user.Company!=null)
            {
                company_rb.Checked = true;
                independently_rb.Checked = false;
                nameCom_lb.Visible = nameCom_tb.Visible = adresCom_lb.Visible = adresCom_tb.Visible = ogrnCom_lb.Visible = ogrnCom_tb.Visible = dateogrnCom_lb.Visible = dateogrnCom_tb.Visible =
                        insuranceCom_lb.Visible = insuranceCom_tb.Visible = telephoneCom_lb.Visible = telephoneCom_tb.Visible = emailCom_lb.Visible = emailCom_tb.Visible = formCom_lb.Visible =
                        formCom_tb.Visible = true;
                pasportNumber_lb.Visible = pasportNumber_tb.Visible = pasportDate_lb.Visible = pasportDate_tb.Visible = pasportWhere_lb.Visible = pasportWhere_tb.Visible = false;
                nameCom_tb.Text = user.Company.Name;
                formCom_tb.Text = user.Company.Form;
                ogrnCom_tb.Text = user.Company.OGRN;
                if (user.Company.DateOGRN!=DateTime.MinValue)
                    dateogrnCom_tb.Text = Convert.ToString(user.Company.DateOGRN);
                adresCom_tb.Text = user.Company.Adres;
                telephone_tb.Text = user.Company.Telephone;
                emailCom_tb.Text = user.Company.Email;
                insuranceCom_tb.Text = user.Company.Insurance;
            }
            else
            {
                independently_rb.Checked = true;
                company_rb.Checked = false;
                nameCom_lb.Visible = nameCom_tb.Visible = adresCom_lb.Visible = adresCom_tb.Visible = ogrnCom_lb.Visible = ogrnCom_tb.Visible = dateogrnCom_lb.Visible = dateogrnCom_tb.Visible =
                        insuranceCom_lb.Visible = insuranceCom_tb.Visible = telephoneCom_lb.Visible = telephoneCom_tb.Visible = emailCom_lb.Visible = emailCom_tb.Visible = formCom_lb.Visible =
                        formCom_tb.Visible = false;
                pasportNumber_lb.Visible = pasportNumber_tb.Visible = pasportDate_lb.Visible = pasportDate_tb.Visible = pasportWhere_lb.Visible = pasportWhere_tb.Visible = true;
                pasportNumber_tb.Text = user.PasportNumber;
                if (user.PasportDate != DateTime.MinValue)
                    pasportDate_tb.Text = Convert.ToString(user.PasportDate);
                pasportWhere_tb.Text = Convert.ToString(user.PasportWhere);
            }
        }

        private void Sync()
        {
            save_bt.Enabled = company_rb.Checked && _isCompany || independently_rb.Checked && _isPasport;
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

        private void company_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (company_rb.Checked == true)
            {
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
            if (independently_rb.Checked == true)
            {
                company_rb.Checked = false;
                nameCom_lb.Visible = nameCom_tb.Visible = adresCom_lb.Visible = adresCom_tb.Visible = ogrnCom_lb.Visible = ogrnCom_tb.Visible = dateogrnCom_lb.Visible = dateogrnCom_tb.Visible =
                        insuranceCom_lb.Visible = insuranceCom_tb.Visible = telephoneCom_lb.Visible = telephoneCom_tb.Visible = emailCom_lb.Visible = emailCom_tb.Visible = formCom_lb.Visible =
                        formCom_tb.Visible = false;
                pasportNumber_lb.Visible = pasportNumber_tb.Visible = pasportDate_lb.Visible = pasportDate_tb.Visible = pasportWhere_lb.Visible = pasportWhere_tb.Visible = true;
                _isCompany = false;
            }
            Sync();
            //else company_rb.Checked = true;
        }

        private void nameCom_tb_TextChanged(object sender, EventArgs e)
        {
            if (nameCom_tb.Text != "") _isNameCom = true;
            else _isNameCom = false;
            SyncCom();
        }
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

        private void cancel_bt_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        private void save_bt_Click(object sender, EventArgs e)
        {
            int experience = -1;
            if (experience_tb.Text != "") experience = Int32.Parse(experience_tb.Text);
            User newUser = new User(Id,name_tb.Text, surname_tb.Text, patronym_tb.Text,telephone_tb.Text, "", "", numberSROO_tb.Text, nameSROO_tb.Text, insuranceCom_tb.Text, experience, education_tb.Text, membership_tb.Text);
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
            if (dataBase.UpdateUser(newUser))
                this.Close();
            else MessageBox.Show("");
        }
    }
}
