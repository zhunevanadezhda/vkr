using System;
using System.Windows.Forms;

namespace Diplom
{
    public partial class AddOwner : Form
    {
        private int OwnerId;
        private int ReportId;
        private string Type;
        BD dataBase = new BD();
        public AddOwner(int reportId, int ownerId, string type)
        {
            InitializeComponent();
            save_bt.FlatStyle = FlatStyle.Popup;
            cancel_bt.FlatStyle = FlatStyle.Popup;
            OwnerId = ownerId;
            ReportId = reportId;
            Type = type;
            if (OwnerId > 0)
                load();
            else
            {
                fiz_rb.Checked = true;
                ur_rb.Checked = false;
                nameCom_lb.Visible = nameCom_tb.Visible = adresCom_lb.Visible = adresCom_tb.Visible = ogrnCom_lb.Visible = ogrnCom_tb.Visible = dateogrnCom_lb.Visible = dateogrnCom_mtb.Visible =
                        telephoneCom_lb.Visible = telephoneCom_mtb.Visible = emailCom_lb.Visible = emailCom_tb.Visible = formCom_lb.Visible = formCom_tb.Visible = false;
                pasportNumber_lb.Visible = pasportNumber_tb.Visible = pasportDate_lb.Visible = pasportDate_mtb.Visible = pasportWhere_lb.Visible = pasportWhere_tb.Visible =
                        surname_lb.Visible = surname_tb.Visible = name_lb.Visible = name_tb.Visible = patronym_lb.Visible = patronym_tb.Visible = true;
                save_bt.Enabled = false;
            }
        }

        private void OwnerChanged(object sender, EventArgs e)
        {
            save_bt.Enabled = true;
        }

        private void load()
        {
            ur_rb.Visible = false;
            fiz_rb.Visible = false;
            if (Type == "ur")
            {
                ur_rb.Checked = true;
                fiz_rb.Checked = false;
                Company company = dataBase.GetCompanyById(OwnerId);
                nameCom_tb.Text = company.Name;
                adresCom_tb.Text = company.Adres;
                ogrnCom_tb.Text = company.OGRN;
                dateogrnCom_mtb.Text = Convert.ToString(company.DateOGRN);
                telephoneCom_mtb.Text = company.Telephone;
                emailCom_tb.Text = company.Email;
                formCom_tb.Text = company.Form;
            }
            else
            {
                ur_rb.Checked = false;
                fiz_rb.Checked = true;
                Human human = dataBase.GetHuman(OwnerId);
                pasportNumber_tb.Text = human.PasportNumber;
                pasportDate_mtb.Text = Convert.ToString(human.PasportDate);
                pasportWhere_tb.Text = human.PasportWhere;
                surname_tb.Text = human.Surname;
                name_tb.Text = human.Name;
                patronym_tb.Text = human.Patronym;
                nameCom_lb.Visible = nameCom_tb.Visible = adresCom_lb.Visible = adresCom_tb.Visible = ogrnCom_lb.Visible = ogrnCom_tb.Visible = dateogrnCom_lb.Visible = dateogrnCom_mtb.Visible =
                        telephoneCom_lb.Visible = telephoneCom_mtb.Visible = emailCom_lb.Visible = emailCom_tb.Visible = formCom_lb.Visible = formCom_tb.Visible = false;
                pasportNumber_lb.Visible = pasportNumber_tb.Visible = pasportDate_lb.Visible = pasportDate_mtb.Visible = pasportWhere_lb.Visible = pasportWhere_tb.Visible =
                        surname_lb.Visible = surname_tb.Visible = name_lb.Visible = name_tb.Visible = patronym_lb.Visible = patronym_tb.Visible = true;
            }
        }
        private void fiz_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (fiz_rb.Checked == true)
            {
                ur_rb.Checked = false;
                nameCom_lb.Visible = nameCom_tb.Visible = adresCom_lb.Visible = adresCom_tb.Visible = ogrnCom_lb.Visible = ogrnCom_tb.Visible = dateogrnCom_lb.Visible = dateogrnCom_mtb.Visible =
                        telephoneCom_lb.Visible = telephoneCom_mtb.Visible = emailCom_lb.Visible = emailCom_tb.Visible = formCom_lb.Visible = formCom_tb.Visible = false;
                pasportNumber_lb.Visible = pasportNumber_tb.Visible = pasportDate_lb.Visible = pasportDate_mtb.Visible = pasportWhere_lb.Visible = pasportWhere_tb.Visible =
                        surname_lb.Visible = surname_tb.Visible = name_lb.Visible = name_tb.Visible = patronym_lb.Visible = patronym_tb.Visible = true;
            }
            save_bt.Enabled = true;
        }
        private void ur_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (ur_rb.Checked == true)
            {
                fiz_rb.Checked = false;
                nameCom_lb.Visible = nameCom_tb.Visible = adresCom_lb.Visible = adresCom_tb.Visible = ogrnCom_lb.Visible = ogrnCom_tb.Visible = dateogrnCom_lb.Visible = dateogrnCom_mtb.Visible =
                        telephoneCom_lb.Visible = telephoneCom_mtb.Visible = emailCom_lb.Visible = emailCom_tb.Visible = formCom_lb.Visible = formCom_tb.Visible = true;
                pasportNumber_lb.Visible = pasportNumber_tb.Visible = pasportDate_lb.Visible = pasportDate_mtb.Visible = pasportWhere_lb.Visible = pasportWhere_tb.Visible = false;
                surname_lb.Visible = surname_tb.Visible = name_lb.Visible = name_tb.Visible = patronym_lb.Visible = patronym_tb.Visible = false;
            }
            save_bt.Enabled = true;
        }

        private void save_bt_Click(object sender, EventArgs e)
        {
            if (ur_rb.Checked)
            {
                Company company;
                DateTime date;
                if (!DateTime.TryParse(dateogrnCom_mtb.Text, out date))
                    date = DateTime.MinValue;
                if (OwnerId > 0)
                {
                    company = new Company(OwnerId, nameCom_tb.Text, adresCom_tb.Text, ogrnCom_tb.Text, date, formCom_tb.Text, emailCom_tb.Text, telephoneCom_mtb.Text, true);
                    company.Insurance = "";
                    dataBase.UpdateCompany(company);
                    dataBase.UpdateComRep(ReportId, company.Id, company.IsOwner, false);
                }
                else
                {
                    company = new Company(nameCom_tb.Text, adresCom_tb.Text, ogrnCom_tb.Text, date, formCom_tb.Text, emailCom_tb.Text, telephoneCom_mtb.Text, true);
                    company.Insurance = "";
                    int id = dataBase.AddCompany(company);
                    dataBase.AddComRep(ReportId, id, true, false);
                }
            }
            else
            {
                DateTime date;
                Human human;
                if (!DateTime.TryParse(pasportDate_mtb.Text, out date))
                    date = DateTime.MinValue;
                if (OwnerId > 0)
                {
                    human = new Human(OwnerId, name_tb.Text, surname_tb.Text, patronym_tb.Text, "", pasportNumber_tb.Text, date, pasportWhere_tb.Text, true);
                    dataBase.UpdateHuman(human);
                    dataBase.UpdateHuRep(ReportId, human.Id, human.IsOwner, false);
                }
                else
                {
                    human = new Human(name_tb.Text, surname_tb.Text, patronym_tb.Text, "", pasportNumber_tb.Text, DateTime.MinValue, pasportWhere_tb.Text, true);
                    int id = dataBase.AddHuman(human);
                    dataBase.AddHuRep(ReportId, id, true, false);
                }
            }
            this.DialogResult = DialogResult.OK;
            this.Close();
        }

        private void cancel_bt_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }
    }
}