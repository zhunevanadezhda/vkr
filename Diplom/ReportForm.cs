using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace Diplom
{
    public partial class ReportForm : Form
    {
        private int UserId;
        private int ReportId;
        private string TypeObject;
        private BD dataBase = new BD();
        private Dictionary<string, int> ids = new Dictionary<string, int>();//CompanyCostumer,HumanCostumer,Object
        private bool isReportChanged = false;
        private bool isCostumerChanged = false;
        private bool isOwnerChanged = false;
        private bool isRealEstatesChanged = false;
        private bool isDocumentsChanged = false;
        private bool isDocumentsLoaded = false;
        private List<Document> documents = new List<Document>();
        private int i_pic = 0;
        // private bool isValuationChanged = false;//только то что есть в этой форме, остальное сохранятеся в другой форме (при выходе предлагается сохраниться)
        //private Owner owners; использовать не как глобальные элементы
        //private List<RealEstate> estates; использовать не как глобальные элементы
        public ReportForm(int id_u, int id_r, string typeObject)
        {
            InitializeComponent();
            tabControl1.DrawItem += new DrawItemEventHandler(tabControl1_DrawItem);
            UserId = id_u;
            ReportId = id_r;
            TypeObject = typeObject;
            save_bt.FlatStyle = FlatStyle.Popup;
            form_bt.FlatStyle = FlatStyle.Popup;
            Cancel_bt.FlatStyle = FlatStyle.Popup;
            updateUser_bt.FlatStyle = FlatStyle.Popup;
            addElement_bt.FlatStyle = FlatStyle.Popup;
            deleteElement_bt.FlatStyle = FlatStyle.Popup;
            addDoc_bt.FlatStyle = FlatStyle.Popup;
            deleteDoc_bt.FlatStyle = FlatStyle.Popup;
            renameDoc_bt.FlatStyle = FlatStyle.Popup;
            addFoto_bt.FlatStyle = FlatStyle.Popup;
            deleteFoto_bt.FlatStyle = FlatStyle.Popup;
            next_bt.FlatStyle = FlatStyle.Popup;
            pred_bt.FlatStyle = FlatStyle.Popup;
            valuation_bt.FlatStyle = FlatStyle.Popup;
            addOwner_bt.FlatStyle = FlatStyle.Popup;
            updateOwner_bt.FlatStyle = FlatStyle.Popup;
            deleteOwner_bt.FlatStyle = FlatStyle.Popup;
            load();
            Sync();
        }

        private void Sync()
        {
            save_bt.Enabled = isReportChanged || isCostumerChanged || isOwnerChanged || isRealEstatesChanged || isDocumentsChanged;
            form_bt.Enabled = !save_bt.Enabled;
        }
        public void load_user()
        {
            User user = dataBase.GetUserById(UserId);
            fio_lb.Text = "ФИО - " + user.Surname + " " + user.Name + " " + user.Patronym;
            emailExect_lb.Text = "Email - " + user.Email;
            telephoneExect_lb.Text = "Телефон - " + user.Telephone;
            nameSROO_lb.Text = "Наименование СРОО - " + user.NameSROO;
            numberSROO_lb.Text = "Номер СРОО - " + user.NumberSROO;
            if (user.Experience > -1)
                expirience_lb.Text = "Стаж работы - " + user.Experience;
            else expirience_lb.Text = "Стаж работы - ";
            if (user.Company != null)
            {
                company_lb.Text = "Компания - " + user.Company.Form + " " + user.Company.Name;
            }
            else company_lb.Visible = false;
        }
        public void load_object()
        {
            object_dgv.Rows.Add("addead","Цена за один кв.м.", "Цена за один кв.м.", "тыс.руб.");
            object_dgv.Rows[0].Visible = false;
            object_dgv.Rows.Add("added", "Площадь", "Площадь", "кв.м.");
            object_dgv.Rows.Add("added", "Дата предложения", "Дата предложения", "");            
            object_dgv.Rows.Add("added", "Время до метро/жд станции/остановки общественного транспорта", "Время до метро/жд станции/остановки общественного транспорта", "мин.");
            switch (TypeObject)
            {
                case "квартира":
                    object_dgv.Rows.Add("added", "Жилая площадь", "Жилая площадь", "кв.м.");
                    object_dgv.Rows.Add("added", "Площадь кухни", "Площадь кухни", "кв.м.");
                    object_dgv.Rows.Add("added", "Кол-во комнат", "Кол-во комнат", "");
                    object_dgv.Rows.Add("added", "Номер этажа", "Номер этажа", "");
                    object_dgv.Rows.Add("added", "Кол-во этажей", "Кол-во этажей", "");
                    object_dgv.Rows.Add("added", "Качество отделки", "Качество отделки", "");
                    object_dgv.Rows.Add("added", "Наличие мебели", "Наличие мебели", "");
                    object_dgv.Rows.Add("added", "Вид из окна", "Вид из окна", "");
                    object_dgv.Rows.Add("added", "Санузел", "Санузел", "");
                    object_dgv.Rows.Add("added", "Высота потолков", "Высота потолков", "");
                    object_dgv.Rows.Add("added", "Наличие балкона или лоджии", "Наличие балкона или лоджии", "");
                    object_dgv.Rows.Add("added", "Материал стен", "Материал стен", "");
                    object_dgv.Rows.Add("added", "Год постройки", "Год постройки", "");
                    object_dgv.Rows.Add("added", "Физический износ здания", "Физический износ здания", "%");
                    object_dgv.Rows.Add("added", "Благоустройство дома", "Благоустройство дома", "");
                    object_dgv.Rows.Add("added", "Состояние дома", "Состояние дома", "");
                    object_dgv.Rows.Add("added", "Инженерные системы", "Инженерные системы", "");
                    break;
                case "дом":
                    object_dgv.Rows.Add("added", "Кол-во этажей", "Кол-во этажей", "");
                    object_dgv.Rows.Add("added", "Материал стен", "Материал стен", "");
                    object_dgv.Rows.Add("added", "Год постройки", "Год постройки", "");
                    object_dgv.Rows.Add("added", "Физический износ здания", "Физический износ здания", "%");
                    object_dgv.Rows.Add("added", "Благоустройство дома", "Благоустройство дома", "");
                    object_dgv.Rows.Add("added", "Состояние дома", "Состояние дома", "");
                    object_dgv.Rows.Add("added", "Инженерные системы", "Инженерные системы", "");
                    break;
                case "дом с участком":
                    object_dgv.Rows.Add("added", "Кол-во этажей", "Кол-во этажей", "");
                    object_dgv.Rows.Add("added", "Материал стен", "Материал стен", "");
                    object_dgv.Rows.Add("added", "Год постройки", "Год постройки", "");
                    object_dgv.Rows.Add("added", "Физический износ здания", "Физический износ здания", "%");
                    object_dgv.Rows.Add("added", "Благоустройство дома", "Благоустройство дома", "");
                    object_dgv.Rows.Add("added", "Состояние дома", "Состояние дома", "");
                    object_dgv.Rows.Add("added", "Инженерные системы", "Инженерные системы", "");
                    break;
                case "участок":
                    break;
            }
            object_dgv.Rows.Add("added", "Условия финансирования", "Условия финансирования", "");
            object_dgv.Rows.Add("added", "Условия финансирования", "Условия продажи", "");
        }
        public void load_owners()
        {
            for (int i = 1; i < owner_dgv.RowCount;)
            {
                owner_dgv.Rows.RemoveAt(i);
            }
            Owner owners = new Owner();
            owners.FizOwners = dataBase.GetHumanOwners(ReportId);
            owners.UrOwners = dataBase.GetCompaniesOwners(ReportId);
            if (owners.UrOwners != null)
                foreach (Company c in owners.UrOwners)
                    owner_dgv.Rows.Add(c.Id, "ur", c.Form + " " + c.Name);
            if (owners.FizOwners != null)
                foreach (Human h in owners.FizOwners)
                    owner_dgv.Rows.Add(h.Id, "fiz", h.Surname + " " + h.Name + " " + h.Patronym);
        }

        //загрузка
        public void load()
        {
            //object_tp
            object_lb.Text = Convert.ToString(TypeObject[0]).ToUpper() + TypeObject.Remove(0, 1);
            adres_tb.MaxLength = 200;
            region_tb.MaxLength = 50;
            kadastrNumber_tb.MaxLength = 20;
            rights_tb.MaxLength = 30;
            //costumer_tp
            name_tb.MaxLength = 30;
            surname_tb.MaxLength = 50;
            patronym_tb.MaxLength = 30;
            pasportNumber_tb.MaxLength = 10;
            pasportWhere_tb.MaxLength = 150;
            nameCom_tb.MaxLength = 100;
            adresCom_tb.MaxLength = 200;
            ogrnCom_tb.MaxLength = 15;
            formCom_tb.MaxLength = 3;
            emailCom_tb.MaxLength = 40;
            basedOn_tb.MaxLength = 150;
            goal_tb.MaxLength = 120;
            limitation_tb.MaxLength = 100;
            owner_dgv.Rows.Add();
            //executant_tp
            load_user();
            //documents_tp
            addFoto_bt.Enabled = false;
            next_bt.Enabled = false;
            pred_bt.Enabled = false;
            deleteDoc_bt.Enabled = false;
            deleteFoto_bt.Enabled = false;
            renameDoc_bt.Enabled = false;
            //owner_tp
            deleteOwner_bt.Enabled = false;
            updateOwner_bt.Enabled = false;
            if (ReportId == 0)
            {
                //owner_dgv.Rows[0].Visible = false;
                isReportChanged = true;
                //основное окно
                save_bt.Enabled = true;
                form_bt.Enabled = false;
                //podhods_tp
                sravn_cb.Checked = true;
                dohod_cb.Checked = zatrat_cb.Checked = dohodMet1_cb.Enabled = zatratMet1_cb.Enabled = false;
                marketValue_cb.Checked = true;
                liquidationValue_cb.Checked = investmentValue_cb.Checked = false;
                //costumer_tp
                fiz_rb.Checked = true;
                isOwner_cb.Checked = false;
                isCostumerChanged = false;
                nameCom_lb.Visible = nameCom_tb.Visible = adresCom_lb.Visible = adresCom_tb.Visible = ogrnCom_lb.Visible = ogrnCom_tb.Visible = dateogrnCom_lb.Visible = dateogrnCom_mtb.Visible =
                        telephoneCom_lb.Visible = telephoneCom_mtb.Visible = emailCom_lb.Visible = emailCom_tb.Visible = formCom_lb.Visible = formCom_tb.Visible = false;
                pasportNumber_lb.Visible = pasportNumber_tb.Visible = pasportDate_lb.Visible = pasportDate_mtb.Visible = pasportWhere_lb.Visible = pasportWhere_tb.Visible =
                        surname_lb.Visible = surname_tb.Visible = name_lb.Visible = name_tb.Visible = patronym_lb.Visible = patronym_tb.Visible = telephone_lb.Visible = telephone_mtb.Visible = true;
                //owners_tp
                owner_dgv.Rows[0].Visible = false;
                //object_tp
                load_object();
            }
            else
            {

                //specification_tp
                Report report = dataBase.GetReport(ReportId);
                dateInspection_dtp.Value = report.DateInspection;
                dateValutaion_dtp.Value = report.DateValutaion;
                dateReport_dtp.Value = report.DateReport;
                basedOn_tb.Text = report.BasedOn;
                goal_tb.Text = report.Goal;
                limitation_tb.Text = report.Limitation;
                //costumer_tp
                if (report.Costumer.Type)
                {
                    fiz_rb.Checked = false;
                    ur_rb.Checked = true;
                    Company costumer = report.Costumer.Ur;
                    if (costumer != null)
                    {
                        ids.Add("CompanyCostumer", costumer.Id);
                        nameCom_tb.Text = costumer.Name;
                        adresCom_tb.Text = costumer.Adres;
                        ogrnCom_tb.Text = costumer.OGRN;
                        dateogrnCom_mtb.Text = Convert.ToString(costumer.DateOGRN);
                        formCom_tb.Text = costumer.Form;
                        emailCom_tb.Text = costumer.Email;
                        telephoneCom_mtb.Text = costumer.Telephone;
                        isOwner_cb.Checked = costumer.IsOwner;
                        if (costumer.IsOwner)
                        { owner_dgv.Rows[0].Cells[0].Value = costumer.Id;
                            owner_dgv.Rows[0].Cells[1].Value = "ur";
                            owner_dgv.Rows[0].Cells[2].Value = formCom_tb.Text + " " + nameCom_tb.Text;
                        }
                        //  sravn_cb.Checked = true;
                    }
                    else owner_dgv.Rows[0].Visible = false;
                    nameCom_lb.Visible = nameCom_tb.Visible = adresCom_lb.Visible = adresCom_tb.Visible = ogrnCom_lb.Visible = ogrnCom_tb.Visible = dateogrnCom_lb.Visible = dateogrnCom_mtb.Visible =
                        telephoneCom_lb.Visible = telephoneCom_mtb.Visible = emailCom_lb.Visible = emailCom_tb.Visible = formCom_lb.Visible = formCom_tb.Visible = true;
                    pasportNumber_lb.Visible = pasportNumber_tb.Visible = pasportDate_lb.Visible = pasportDate_mtb.Visible = pasportWhere_lb.Visible = pasportWhere_tb.Visible =
                        surname_lb.Visible = surname_tb.Visible = name_lb.Visible = name_tb.Visible = patronym_lb.Visible = patronym_tb.Visible = telephone_lb.Visible = telephone_mtb.Visible = false;
                }
                else
                {
                    fiz_rb.Checked = true;
                    ur_rb.Checked = false;
                    Human costumer = report.Costumer.Fiz;
                    if (costumer != null)
                    {
                        ids.Add("HumanCostumer", costumer.Id);
                        name_tb.Text = costumer.Name;
                        surname_tb.Text = costumer.Surname;
                        patronym_tb.Text = costumer.Patronym;
                        pasportNumber_tb.Text = costumer.PasportNumber;
                        if (costumer.PasportDate != DateTime.MinValue)
                            pasportDate_mtb.Text = Convert.ToString(costumer.PasportDate);
                        pasportWhere_tb.Text = costumer.PasportWhere;
                        telephone_mtb.Text = costumer.Telephone;
                        isOwner_cb.Checked = costumer.IsOwner;
                        if (costumer.IsOwner)
                        {
                            owner_dgv.Rows[0].Cells[0].Value = costumer.Id;
                            owner_dgv.Rows[0].Cells[1].Value = "fiz";
                            owner_dgv.Rows[0].Cells[2].Value = surname_tb.Text + " " + name_tb.Text + " " + patronym_tb.Text;
                        }
                    }
                    else owner_dgv.Rows[0].Visible = false;
                    nameCom_lb.Visible = nameCom_tb.Visible = adresCom_lb.Visible = adresCom_tb.Visible = ogrnCom_lb.Visible = ogrnCom_tb.Visible = dateogrnCom_lb.Visible = dateogrnCom_mtb.Visible =
                        telephoneCom_lb.Visible = telephoneCom_mtb.Visible = emailCom_lb.Visible = emailCom_tb.Visible = formCom_lb.Visible = formCom_tb.Visible = false;
                    pasportNumber_lb.Visible = pasportNumber_tb.Visible = pasportDate_lb.Visible = pasportDate_mtb.Visible = pasportWhere_lb.Visible = pasportWhere_tb.Visible =
                        surname_lb.Visible = surname_tb.Visible = name_lb.Visible = name_tb.Visible = patronym_lb.Visible = patronym_tb.Visible = telephone_lb.Visible = telephone_mtb.Visible = true;
                }
                //object_tp
                RealEstate realEstate = dataBase.GetRealEstate(ReportId);
                List<string> metods = new List<string>();
                if (realEstate != null)
                {
                    metods = dataBase.GetPodhodsMetods(ReportId, realEstate.Id);
                    ids["Object"] = realEstate.Id;
                    adres_tb.Text = realEstate.Adres;
                    region_tb.Text = realEstate.Region;
                    kadastrNumber_tb.Text = realEstate.KadastrNumber;
                    rights_tb.Text = realEstate.Rights;
                    if (realEstate.Elements != null)
                    {
                        foreach (Element e in realEstate.Elements)
                            if (!e.Name.Contains("Итог"))
                            object_dgv.Rows.Add("", e.Name, e.Name, e.Unit, e.Value);
                        //object_dgv.Rows[0].Visible = false;
                    }
                    else load_object();
                }
                else load_object();
                //documents_tp
                documents = dataBase.GetDocuments(ReportId);
                if (documents != null)
                {
                    isDocumentsLoaded = true;
                    foreach (Document d in documents)
                        listBox1.Items.Add(d.Name);
                }
                else documents = new List<Document>();
                //podhods_tp
                for (int i = 0; i < metods.Count; i++)
                {
                    string[] subs = metods[i].Split('|');
                    switch (subs[0])
                    {
                        case "сравнительный":
                            sravn_cb.Checked = true;
                            break;
                        case "доходный":
                            dohod_cb.Checked = true;
                            switch (subs[1])
                            {

                            }
                            break;
                        case "затратный":
                            zatrat_cb.Checked = true;
                            switch (subs[1])
                            {

                            }
                            break;
                    }
                }
                if (metods.Count == 0)
                    sravn_cb.Checked = true;
                //owners_tp
                load_owners();
                isReportChanged = isCostumerChanged = isOwnerChanged = isRealEstatesChanged = isOwnerChanged = false;
                //основное окно
                save_bt.Enabled = false;
                form_bt.Enabled = true;
            }
        }
        private void tabControl1_DrawItem(Object sender, System.Windows.Forms.DrawItemEventArgs e)
        {
            string tabName = tabControl1.TabPages[e.Index].Text;
            StringFormat stringFormat = new StringFormat();
            stringFormat.Alignment = StringAlignment.Far;
            stringFormat.LineAlignment = StringAlignment.Center;
            //Find if it is selected, this one will be hightlighted...
            if (e.Index == tabControl1.SelectedIndex)
                e.Graphics.FillRectangle(Brushes.LightBlue, e.Bounds);
            e.Graphics.DrawString(tabName, this.Font, Brushes.Black, tabControl1.GetTabRect(e.Index), stringFormat);
        }

        //основное окно
        private void save_bt_Click(object sender, EventArgs e)
        {
            if (ReportId == 0)
            {
                Costumer costumer = new Costumer(ur_rb.Checked);
                Report report = new Report(dateInspection_dtp.Value, dateValutaion_dtp.Value, dateReport_dtp.Value, basedOn_tb.Text, goal_tb.Text, limitation_tb.Text, false, TypeObject, costumer);
                ReportId = dataBase.AddReport(report, UserId);
                isReportChanged = false;
                /*DateTime dateInspection, DateTime dateValutaion, DateTime dateReport, string basedOn, string goal, string limitation, bool isReady, string typeObject,Costumer costumer*/
            }
            else if (isReportChanged)
            {
                Costumer costumer = new Costumer(ur_rb.Checked);
                Report report = new Report(ReportId, dateInspection_dtp.Value, dateValutaion_dtp.Value, dateReport_dtp.Value, basedOn_tb.Text, goal_tb.Text, limitation_tb.Text, false, TypeObject, costumer);
                isReportChanged = !dataBase.UpdateReport(report);
            }
            if (isCostumerChanged)
            {
                if (ur_rb.Checked)
                {
                    if (ids.ContainsKey("CompanyCostumer"))
                    {
                        DateTime date;
                        Company company;
                        if (DateTime.TryParse(dateogrnCom_mtb.Text, out date))
                            company = new Company(ids["CompanyCostumer"], nameCom_tb.Text, adresCom_tb.Text, ogrnCom_tb.Text, date, formCom_tb.Text, emailCom_tb.Text, telephoneCom_mtb.Text, isOwner_cb.Checked);
                        else company = new Company(ids["CompanyCostumer"], nameCom_tb.Text, adresCom_tb.Text, ogrnCom_tb.Text, DateTime.MinValue, formCom_tb.Text, emailCom_tb.Text, telephoneCom_mtb.Text, isOwner_cb.Checked);
                        isCostumerChanged = !(dataBase.UpdateCompany(company) && dataBase.UpdateComRep(ReportId, company.Id, company.IsOwner, true));
                    }
                    else
                    {
                        DateTime date;
                        Company company;
                        if (DateTime.TryParse(dateogrnCom_mtb.Text, out date))
                            company = new Company(nameCom_tb.Text, adresCom_tb.Text, ogrnCom_tb.Text, date, formCom_tb.Text, emailCom_tb.Text, telephoneCom_mtb.Text, isOwner_cb.Checked);
                        else company = new Company(nameCom_tb.Text, adresCom_tb.Text, ogrnCom_tb.Text, DateTime.MinValue, formCom_tb.Text, emailCom_tb.Text, telephoneCom_mtb.Text, isOwner_cb.Checked);
                        int id = dataBase.AddCompany(company);
                        isCostumerChanged = !dataBase.AddComRep(ReportId, id, company.IsOwner, true);
                        ids.Add("CompanyCostumer", id);
                        if (ids.ContainsKey("HumanCostumer"))
                        {
                            dataBase.DeleteHuman(ids["HumanCostumer"]);
                            ids.Remove("HumanCostumer");
                        }
                    }
                }
                else {
                    if (ids.ContainsKey("HumanCostumer"))
                    {
                        DateTime date;
                        Human human;
                        if (DateTime.TryParse(pasportDate_mtb.Text, out date))
                            human = new Human(ids["HumanCostumer"], name_tb.Text, surname_tb.Text, patronym_tb.Text, telephone_mtb.Text, pasportNumber_tb.Text, date, pasportWhere_tb.Text, isOwner_cb.Checked);
                        else human = new Human(ids["HumanCostumer"], name_tb.Text, surname_tb.Text, patronym_tb.Text, telephone_mtb.Text, pasportNumber_tb.Text, DateTime.MinValue, pasportWhere_tb.Text, isOwner_cb.Checked);
                        isCostumerChanged = !(dataBase.UpdateHuman(human) && dataBase.UpdateHuRep(ReportId, human.Id, human.IsOwner, true));
                    }
                    else
                    {
                        DateTime date;
                        Human human;
                        if (DateTime.TryParse(pasportDate_mtb.Text, out date))
                            human = new Human(name_tb.Text, surname_tb.Text, patronym_tb.Text, telephone_mtb.Text, pasportNumber_tb.Text, date, pasportWhere_tb.Text, isOwner_cb.Checked);
                        else human = new Human(name_tb.Text, surname_tb.Text, patronym_tb.Text, telephone_mtb.Text, pasportNumber_tb.Text, DateTime.MinValue, pasportWhere_tb.Text, isOwner_cb.Checked);
                        int id = dataBase.AddHuman(human);
                        isCostumerChanged = !dataBase.AddHuRep(ReportId, id, human.IsOwner, true);
                        ids.Add("HumanCostumer", id);
                        if (ids.ContainsKey("CompanyCostumer"))
                        {
                            dataBase.DeleteCompany(ids["CompanyCostumer"]);
                            ids.Remove("CompanyCostumer");
                        }
                    }
                }
            }
            if (isDocumentsChanged)
            {
                if (isDocumentsLoaded)
                {
                    isDocumentsLoaded = true;
                    dataBase.UpdateDocuments(ReportId, documents);
                }
                else
                {
                    foreach (Document d in documents)
                        dataBase.AddDocument(ReportId, d);
                }
                isDocumentsChanged = false;
            }
            if (isOwnerChanged)
            {
                for (int i = 1; i < owner_dgv.RowCount; i++)
                {
                    if (Convert.ToString(owner_dgv.Rows[i].Cells[2].Value) == "deleted")
                    {
                        if (Convert.ToString(owner_dgv.Rows[i].Cells[1].Value) == "ur")
                            dataBase.DeleteCompany(Convert.ToInt32(owner_dgv.Rows[i].Cells[0].Value));
                        else dataBase.DeleteHuman(Convert.ToInt32(owner_dgv.Rows[i].Cells[0].Value));
                        owner_dgv.Rows.RemoveAt(i);
                        i--;
                    }
                }
                isOwnerChanged = false;
            }
            if (isRealEstatesChanged)
            {
                RealEstate realEstate;
                if (ids.ContainsKey("Object")) {
                    realEstate = new RealEstate(ids["Object"], adres_tb.Text, region_tb.Text, kadastrNumber_tb.Text, rights_tb.Text);
                    dataBase.UpdateRealEstate(realEstate, true);
                }
                else { realEstate = new RealEstate(adres_tb.Text, region_tb.Text, kadastrNumber_tb.Text, rights_tb.Text);
                    ids["Object"] = dataBase.AddRealEstate(ReportId, realEstate);
                }
                for (int i = 0; i < object_dgv.RowCount; i++)
                {
                    // foreach (DataGridViewRow r in object_dgv.Rows)
                    //{

                    Element element;
                    if (!dataBase.IsElementsExist(ids["Object"]))
                    {
                        element = new Element("Цена за один кв.м.", "тыс.руб.", "", i, 0);
                        dataBase.AddElement(ids["Object"], element);
                    }
                    string name = Convert.ToString(object_dgv.Rows[i].Cells[0].Value);
                    //  MessageBox.Show(name);
                    switch (name)
                    {
                        case "added":
                            //  MessageBox.Show("added " + object_dgv.Rows[i].Cells[2].Value + " "+ object_dgv.Rows[i].Cells[3]+ "" + object_dgv.Rows[i].Cells[4]);
                            element = new Element(Convert.ToString(object_dgv.Rows[i].Cells[2].Value), Convert.ToString(object_dgv.Rows[i].Cells[3].Value), Convert.ToString(object_dgv.Rows[i].Cells[4].Value), i, 0);
                            dataBase.AddElement(ids["Object"], element);
                            //MessageBox.Show(b+"");
                            object_dgv.Rows[i].Cells[1].Value = object_dgv.Rows[i].Cells[2].Value;
                            break;
                        case "changed":
                            element = new Element(Convert.ToString(object_dgv.Rows[i].Cells[2].Value), Convert.ToString(object_dgv.Rows[i].Cells[3].Value), Convert.ToString(object_dgv.Rows[i].Cells[4].Value), i, 0);
                            dataBase.UpdateElement(ids["Object"], element, Convert.ToString(object_dgv.Rows[i].Cells[1].Value));
                            object_dgv.Rows[i].Cells[1].Value = object_dgv.Rows[i].Cells[2].Value;
                            break;
                        case "deleted":
                            dataBase.DeleteElement(ReportId,ids["Object"], Convert.ToString(object_dgv.Rows[i].Cells[1].Value), false);
                            object_dgv.Rows.RemoveAt(i);
                            // object_dgv.Rows[i].Visible = false;
                            i--;
                            break;
                    }
                    object_dgv.Rows[i].Cells[0].Value = "";
                }
                isRealEstatesChanged = false;
            }
            Sync();
        }
        private void form_bt_Click(object sender, EventArgs e)
        {
            SampleForm form = new SampleForm(UserId,ReportId);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }
        private void form_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (save_bt.Enabled)
            {
                DialogResult result = MessageBox.Show("Отчет изменен. Сохранить изменения?", "Сохранение отчета", MessageBoxButtons.YesNoCancel);
                switch (result)
                {
                    case DialogResult.Yes:
                        {
                            save_bt_Click(sender, null);
                            this.DialogResult = DialogResult.Yes;
                            break;
                        }
                    case DialogResult.Cancel:
                        {
                            e.Cancel = true;
                            break;
                        }
                    case DialogResult.No:
                        {
                            this.DialogResult = DialogResult.No;
                            break;
                        }
                }
            }
            else this.DialogResult = DialogResult.Yes;
        }
        private void Cancel_bt_Click(object sender, EventArgs e)
        {
            this.DialogResult = DialogResult.Cancel;
            this.Close();
        }

        //podhods_tp
        private void dohod_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (dohod_cb.Checked)
            {
                dohodMet1_cb.Enabled = true;
            }
            else
            {
                dohodMet1_cb.Enabled = false;
            }
            // isValuationChanged = true;
            Sync();
        }
        private void zatrat_cb_CheckedChanged(object sender, EventArgs e)
        {
            if (zatrat_cb.Checked)
            {
                zatratMet1_cb.Enabled = true;
            }
            else
            {
                zatratMet1_cb.Enabled = false;
            }
            //  isValuationChanged = true;
            Sync();
        }
        private void valuation_bt_Click(object sender, EventArgs e)
        {
            DialogResult result;
            if (ReportId == 0)
                result = MessageBox.Show("Чтобы продолжить сохраните сначала изменения", "Сохранение изменений", MessageBoxButtons.YesNo);
            else if (save_bt.Enabled)
                result = MessageBox.Show("Отчет изменен. Сохранить изменения?", "Сохранение изменений", MessageBoxButtons.YesNoCancel);
            else result = DialogResult.Yes;
            if (result == DialogResult.Yes) newValuation();
        }
        private void newValuation()
        {
            List<bool> prices = new List<bool>();
            prices.Add(investmentValue_cb.Checked);
            prices.Add(liquidationValue_cb.Checked);
            List<string> metods = new List<string>();
            if (sravn_cb.Checked)
            {
                metods.Add("сравнительный|метод сравнения продаж");
            }
            if (dohod_cb.Checked)
            {
                metods.Add("доходный|метод капитализации");
            }
            if (zatrat_cb.Checked)
            {
                metods.Add("затратный|met1");
            }
            Valuation form = new Valuation(ReportId, TypeObject, metods, prices);
            this.Hide();
            form.ShowDialog();
            this.Show();
        }

        //documents_tp
        private void EnableButtons()
        {
            if (i_pic > 0) pred_bt.Enabled = true;
            else pred_bt.Enabled = false;
            if (i_pic < documents[listBox1.SelectedIndex].ListPic.Count - 1) next_bt.Enabled = true;
            else next_bt.Enabled = false;
        }
        private void DisabledButtons()
        {
            pred_bt.Enabled = false;
            next_bt.Enabled = false;
            deleteDoc_bt.Enabled = false;
            deleteFoto_bt.Enabled = false;
            renameDoc_bt.Enabled = false;
            addFoto_bt.Enabled = false;
        }
        private void addDoc_bt_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
                //openFileDialog.Filter =  "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    RenameElementForm form = new RenameElementForm("", "Наименование документа", "Введите наименование документа");
                    form.ShowDialog();
                    string name = form.newName;
                    if (form.DialogResult == DialogResult.OK)
                    {
                        bool b = true;
                        while (b && listBox1.Items.IndexOf(name) > -1)
                        {
                            MessageBox.Show("Уже есть документ с таким названием", "Внимание", MessageBoxButtons.OK);
                            form = new RenameElementForm(name, "Наименование документа", "Введите наименование документа");
                            form.ShowDialog();
                            name = form.newName;
                            if (form.DialogResult == DialogResult.OK)
                                b = true;
                            else b = false;
                        }
                        if (b)
                        {
                            pictureBox1.ImageLocation = openFileDialog1.FileName;
                            listBox1.Items.Add(name);
                            Document document = new Document(name, openFileDialog1.FileName);
                            documents.Add(document);
                            isDocumentsChanged = true;
                            listBox1.SetSelected(listBox1.Items.Count - 1, true);
                            i_pic = 0;
                            Sync();
                        }
                    }
                }
            }
        }
        private void deleteDoc_bt_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            int id = documents[index].Id;
            dataBase.DeleteDocument(id);
            // pictureBox1.ImageLocation = null;
            listBox1.Items.RemoveAt(index);
            documents.RemoveAt(index);
            isDocumentsChanged = true;
            Sync();
        }
        private void renameDoc_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            string name = listBox1.Items[index].ToString();
            RenameElementForm form = new RenameElementForm(name, "Наименование документа", "Введите наименование документа");
            form.ShowDialog();
            name = form.newName;
            if (form.DialogResult == DialogResult.OK)
            {
                bool b = true;
                while (b && listBox1.Items.IndexOf(name) > -1)
                {
                    MessageBox.Show("Уже есть документ с таким названием", "Внимание", MessageBoxButtons.OK);
                    form = new RenameElementForm(name, "Наименование документа", "Введите наименование документа");
                    form.ShowDialog();
                    name = form.newName;
                    if (form.DialogResult == DialogResult.OK)
                        b = true;
                    else b = false;
                }
                if (b)
                {
                    listBox1.Items[index] = name;
                    isDocumentsChanged = true;
                    Sync();
                }
            }
            isDocumentsChanged = true;
            Sync();
        }
        private void addFoto_bt_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                openFileDialog1.InitialDirectory = "c:\\";
                openFileDialog1.Filter = "Image Files|*.jpg;*.jpeg;*.png;*.gif;*.tif;...";
                //openFileDialog.Filter =  "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    bool b = documents[index].isPicExist(openFileDialog1.FileName);
                    if (b)
                    {
                        pictureBox1.ImageLocation = openFileDialog1.FileName;
                        documents[index].ListPic.Add(openFileDialog1.FileName);
                        i_pic = documents[index].ListPic.Count - 1;
                        EnableButtons();
                        isDocumentsChanged = true;
                        Sync();
                    }
                    else
                    {
                        MessageBox.Show("Уже есть файл с таким же путем", "Внимание", MessageBoxButtons.OK);
                    }
                }
            }
        }
        private void LoadPhoto(string url)
        {
            pictureBox1.ImageLocation = url;
            EnableButtons();
        }
        private void pred_bt_Click(object sender, EventArgs e)
        {
            i_pic--;
            LoadPhoto(documents[listBox1.SelectedIndex].ListPic[i_pic]);
        }
        private void next_bt_Click(object sender, EventArgs e)
        {
            i_pic++;
            LoadPhoto(documents[listBox1.SelectedIndex].ListPic[i_pic]);
        }
        private void deleteFoto_bt_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            documents[index].ListPic.RemoveAt(i_pic);
            if (documents[index].ListPic.Count > 0)
            {
                if (i_pic == documents[index].ListPic.Count) i_pic--;
                LoadPhoto(documents[index].ListPic[i_pic]);
            }
            else
            {
                //pictureBox1.ImageLocation = null;
                documents.RemoveAt(index);
                listBox1.Items.RemoveAt(index);
            }
            isDocumentsChanged = true;
            Sync();
        }
        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                addFoto_bt.Enabled = true;
                next_bt.Enabled = true;
                pred_bt.Enabled = true;
                deleteDoc_bt.Enabled = true;
                deleteFoto_bt.Enabled = true;
                renameDoc_bt.Enabled = true;
                i_pic = 0;
                LoadPhoto(documents[listBox1.SelectedIndex].ListPic[i_pic]);
            }
            else
            {
                DisabledButtons();
                i_pic = 0;
                pictureBox1.ImageLocation = null;
            }
        }

        //costumer_tp
        private void fiz_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (fiz_rb.Checked == true)
            {
                ur_rb.Checked = false;
                nameCom_lb.Visible = nameCom_tb.Visible = adresCom_lb.Visible = adresCom_tb.Visible = ogrnCom_lb.Visible = ogrnCom_tb.Visible = dateogrnCom_lb.Visible = dateogrnCom_mtb.Visible =
                        telephoneCom_lb.Visible = telephoneCom_mtb.Visible = emailCom_lb.Visible = emailCom_tb.Visible = formCom_lb.Visible = formCom_tb.Visible = false;
                pasportNumber_lb.Visible = pasportNumber_tb.Visible = pasportDate_lb.Visible = pasportDate_mtb.Visible = pasportWhere_lb.Visible = pasportWhere_tb.Visible =
                        surname_lb.Visible = surname_tb.Visible = name_lb.Visible = name_tb.Visible = patronym_lb.Visible = patronym_tb.Visible = telephone_lb.Visible = telephone_mtb.Visible = true;
            }
            isCostumerChanged = true;
            isReportChanged = true;
            Sync();
        }
        private void ur_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (ur_rb.Checked == true)
            {
                fiz_rb.Checked = false;
                nameCom_lb.Visible = nameCom_tb.Visible = adresCom_lb.Visible = adresCom_tb.Visible = ogrnCom_lb.Visible = ogrnCom_tb.Visible = dateogrnCom_lb.Visible = dateogrnCom_mtb.Visible =
                        telephoneCom_lb.Visible = telephoneCom_mtb.Visible = emailCom_lb.Visible = emailCom_tb.Visible = formCom_lb.Visible = formCom_tb.Visible = true;
                pasportNumber_lb.Visible = pasportNumber_tb.Visible = pasportDate_lb.Visible = pasportDate_mtb.Visible = pasportWhere_lb.Visible = pasportWhere_tb.Visible = false;
                surname_lb.Visible = surname_tb.Visible = name_lb.Visible = name_tb.Visible = patronym_lb.Visible = patronym_tb.Visible = telephone_lb.Visible = telephone_mtb.Visible = false;
            }
            isCostumerChanged = true;
            isReportChanged = true;
            Sync();
        }
        private void isOwner_rb_CheckedChanged(object sender, EventArgs e)
        {
            if (isOwner_cb.Checked)
            {
                owner_dgv.Rows[0].Visible = true;
                if (ur_rb.Checked)
                {
                    if (ids.ContainsKey("CompanyCostumer"))
                        owner_dgv.Rows[0].Cells[0].Value = ids["CompanyCostumer"];
                    else owner_dgv.Rows[0].Cells[0].Value = "";
                    owner_dgv.Rows[0].Cells[1].Value = "ur";
                    owner_dgv.Rows[0].Cells[2].Value = formCom_tb.Text + " " + nameCom_tb.Text;
                }
                else
                {
                    if (ids.ContainsKey("HumanCostumer"))
                        owner_dgv.Rows[0].Cells[0].Value = ids["HumanCostumer"];
                    else owner_dgv.Rows[0].Cells[0].Value = "";
                    owner_dgv.Rows[0].Cells[1].Value = "fiz";
                    owner_dgv.Rows[0].Cells[2].Value = surname_tb.Text + " " + name_tb.Text + " " + patronym_tb.Text;
                }
            }
            else
                owner_dgv.Rows[0].Visible = false;
            isCostumerChanged = true;
            Sync();
        }
        private void nameCom_tb_TextChanged(object sender, EventArgs e)
        {
            isOwner_rb_CheckedChanged(null, null);
            CostumerChange(null, null);
        }
        private void formCom_tb_TextChanged(object sender, EventArgs e)
        {
            isOwner_rb_CheckedChanged(null, null);
            CostumerChange(null, null);
        }
        private void name_tb_TextChanged(object sender, EventArgs e)
        {
            isOwner_rb_CheckedChanged(null, null);
            CostumerChange(null, null);
        }
        private void surname_tb_TextChanged(object sender, EventArgs e)
        {
            isOwner_rb_CheckedChanged(null, null);
            CostumerChange(null, null);
        }
        private void patronym_tb_TextChanged(object sender, EventArgs e)
        {
            isOwner_rb_CheckedChanged(null, null);
            CostumerChange(null, null);
        }

        //executant_tp
        private void updateUser_bt_Click(object sender, EventArgs e)
        {
            UpdateUser form = new UpdateUser(UserId);
            this.Hide();
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
                load_user();
            this.Show();
        }

        //owner_tp
        private void addOwner_bt_Click(object sender, EventArgs e)
        {
            AddOwner form = new AddOwner(ReportId, 0, "");
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
                load_owners();
        }
        private void deleteOwner_bt_Click(object sender, EventArgs e)
        {
            int index = owner_dgv.SelectedRows[0].Index;
            owner_dgv.Rows[index].Visible = false;
            owner_dgv.Rows[index].Cells[2].Value = "deleted";
            isOwnerChanged = true;
            Sync();
        }
        private void updateOwner_bt_Click(object sender, EventArgs e)
        {
            int index = owner_dgv.SelectedRows[0].Index;
            AddOwner form = new AddOwner(ReportId, Convert.ToInt32(owner_dgv.Rows[index].Cells[0].Value), Convert.ToString(owner_dgv.Rows[index].Cells[1].Value));
            form.ShowDialog();
            if (form.DialogResult == DialogResult.OK)
                load_owners();
        }
        private void owner_dgv_CellClick(object sender, DataGridViewCellEventArgs e)
        {
            if (owner_dgv.SelectedRows[0].Index > 0)
            {
                deleteOwner_bt.Enabled = true;
                updateOwner_bt.Enabled = true;
            }
            else
            {
                deleteOwner_bt.Enabled = false;
                updateOwner_bt.Enabled = false;
            }
        }

        //object_tp
        private void object_dgv_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            if (e.ColumnIndex == 2)
            {
                if (isElementExist(Convert.ToString(object_dgv.Rows[e.RowIndex].Cells[2].Value), e.RowIndex))
                    object_dgv.Rows[e.RowIndex].Cells[2].Value = object_dgv.Rows[e.RowIndex].Cells[1].Value;
                else
                {
                    ObjectChange(null, null);
                    if (Convert.ToString(object_dgv.Rows[e.RowIndex].Cells[0].Value) != "added")
                        object_dgv.Rows[e.RowIndex].Cells[0].Value = "changed";
                }
            }
            else {
                ObjectChange(null, null);
                if (Convert.ToString(object_dgv.Rows[e.RowIndex].Cells[0].Value) != "added")
                    object_dgv.Rows[e.RowIndex].Cells[0].Value = "changed";
            }
            isRealEstatesChanged = true;
            Sync();
    }
        private bool isElementExist(string name, int index)
        {
            bool b = true;
            for (int i = 0; i < object_dgv.Rows.Count; i++)
                //foreach (DataGridViewRow r in object_dgv.Rows)
                if (object_dgv.Rows[i].Index != index && Convert.ToString(object_dgv.Rows[i].Cells[1].Value) == name)
                { b = true; i = object_dgv.Rows.Count; }
                else b = false;
            return b;
        }
        private void addElement_bt_Click(object sender, EventArgs e)
        {
            bool b = true;
            int i = 1;
            string name = "NewElement";
            while (b)
            {
                name += i;
                b = isElementExist(name, -1);
               // MessageBox.Show(b+"");
            }
            object_dgv.Rows.Add("added", name, name);
            isRealEstatesChanged = true;
            Sync();
        }
        private void deleteElement_bt_Click(object sender, EventArgs e)
        {
            int index = object_dgv.SelectedCells[0].RowIndex;
            if (Convert.ToString(object_dgv.Rows[index].Cells[0].Value) != "added")
            {
                object_dgv.Rows[index].Cells[0].Value = "deleted";
                object_dgv.Rows[index].Visible = false;
                ObjectChange(null, null);
            }
            else object_dgv.Rows.RemoveAt(index);
            isRealEstatesChanged = true;
            Sync();
        }

        private void ReportChange(object sender, EventArgs e)
        {
            isReportChanged = true;
            Sync();
        }
        private void CostumerChange(object sender, EventArgs e)
        {
            isCostumerChanged = true;
            Sync();
        }
        private void ObjectChange(object sender, EventArgs e)
        {
            isRealEstatesChanged = true;
            Sync();
        }
    }
}