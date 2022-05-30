using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Diplom
{
    public partial class Form1 : Form
    {
        private BD dataBase = new BD();
        private int UserId;
        //List<Report> reports;
        public Form1(int id_u)
        {
            InitializeComponent();
            UserId = id_u;
            updateUser_bt.FlatStyle = FlatStyle.Popup;
            add_bt.FlatStyle = FlatStyle.Popup;
            update_bt.FlatStyle = FlatStyle.Popup;
            delete_bt.FlatStyle = FlatStyle.Popup;
            load_reports();            
        }

        private string GetCellValue(string Column)
        {
            int selectedrowindex = dataGridView1.SelectedCells[0].RowIndex;
            DataGridViewRow selectedRow = dataGridView1.Rows[selectedrowindex];
            return Convert.ToString(selectedRow.Cells[Column].Value);
        }

        private string GetString(string s)
        {
            return string.IsNullOrEmpty(s) ? "" : s;
        }

        private void load_reports()
        {
            dataGridView1.Rows.Clear();
            List <Report> reports = dataBase.GetReports(UserId);
            for (int i=0;i<reports.Count;i++)
            {
                string adres = dataBase.GetAdres(reports[i].Id);
                if (reports[i].Costumer != null)
                {
                    if (reports[i].Costumer.Type)//юр
                        dataGridView1.Rows.Add(reports[i].Id, reports[i].TypeObject, GetString(reports[i].Costumer.Ur.Form) + " " + GetString(reports[i].Costumer.Ur.Name), GetString(reports[i].Costumer.Ur.Telephone), adres, reports[i].IsReady, "");
                    else
                        dataGridView1.Rows.Add(reports[i].Id, reports[i].TypeObject, reports[i].Costumer.Fiz.Surname + " " + reports[i].Costumer.Fiz.Name + " " + reports[i].Costumer.Fiz.Patronym, reports[i].Costumer.Fiz.Telephone, adres, reports[i].IsReady, "");
                }
                else dataGridView1.Rows.Add(reports[i].Id, reports[i].TypeObject, "", "", adres, reports[i].IsReady, "");                 
            }
        }

        /*private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked == true)
            {
                radioButton2.Checked = radioButton3.Checked= false;
            }
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked == true)
            {
                radioButton3.Checked = radioButton1.Checked = false;
            }
        }
        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton3.Checked == true)
            {
                radioButton1.Checked = radioButton2.Checked = false;
            }
        }
        */
        private void dataGridView1_CellContent(object sender, DataGridViewCellEventArgs e)
        {
            delete_bt.Enabled = update_bt.Enabled = true;
        }

        private void add_bt_Click(object sender, EventArgs e)
        {
            TypeObjectForm form = new TypeObjectForm();
            this.Hide();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
            {
                ReportForm form2 = new ReportForm(UserId, 0, form.TypeObject);
                form2.ShowDialog();
                if (form2.DialogResult == DialogResult.Yes) load_reports();
            }
            this.Show();
        }

        private void update_bt_Click(object sender, EventArgs e)
        {
            int cellValue = Convert.ToInt32(GetCellValue("Column1"));
            string cellValue2 = Convert.ToString(GetCellValue("Column2"));
            ReportForm form = new ReportForm(UserId, cellValue,cellValue2);
            this.Hide();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.Yes)
            {
                load_reports();
            }
            this.Show();
        }

        private void delete_bt_Click(object sender, EventArgs e)
        {
            int cellValue = Convert.ToInt32(GetCellValue("Column1"));
            dataBase.DeleteReport(cellValue);
            int index = dataGridView1.SelectedRows[0].Index;
            dataGridView1.Rows.RemoveAt(index);
        }

        private void updateUser_bt_Click(object sender, EventArgs e)
        {
            UpdateUser form = new UpdateUser(UserId);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        private void dataGridView1_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            bool b = Convert.ToBoolean(dataGridView1.Rows[e.RowIndex].Cells[e.ColumnIndex].Value);
            int id = Convert.ToInt32(dataGridView1.Rows[e.RowIndex].Cells["Column1"].Value);
            dataBase.SetIsReady(id,b);
        }

        private void updatePassword_bt_Click(object sender, EventArgs e)
        {
            UpdatePassword form = new UpdatePassword(UserId);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }
    }
}
