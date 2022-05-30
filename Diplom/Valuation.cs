using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Diplom
{
    public partial class Valuation : Form
    {//пока только рыночная стоимость
        //может стоит выделить встроенные и добавленные элементы цветом
        //может код и не нужен будет
        //просто оставим код для всех остальных элементов и вставим
        private int ReportId;
        private int EstateId;
        // private object ids;//objects id
        private bool isSravn = true;
        private Dictionary<int, int> sravnChange;//elements -1, realestate-3, analogue1-4, analogue2-5...
        //0-не изменен, -1-добавлен/изменен, id-удален  
        private bool isDohod = true;
        private bool isZatrat = true;
        private BD dataBase = new BD();
        private List<bool> prices;//инвестиционная, ликвидационная
        private int correct = 1;
        private int KolColumn;
        private HashSet<string> metodsList;
        private HashSet<string> deletedElements = new HashSet<string>();
        private bool itog = false;
        public Valuation(int id_r, string typeObject, List<string> Metods, List<bool> Prices)
        {
            InitializeComponent();
            addAnalogue_bt.FlatStyle = FlatStyle.Popup;
            addElement_bt.FlatStyle = FlatStyle.Popup;
            deleteAnalogue_bt.FlatStyle = FlatStyle.Popup;
            deleteElement_bt.FlatStyle = FlatStyle.Popup;
            renameElement_bt.FlatStyle = FlatStyle.Popup;
            calculatePrice_bt.FlatStyle = FlatStyle.Popup;
            save_bt.FlatStyle = FlatStyle.Popup;
            cancel_bt.FlatStyle = FlatStyle.Popup;
            ReportId = id_r;
            save_bt.Enabled = false;
            prices = Prices;
            Column1.ReadOnly = true;
            metods(Metods, typeObject);
            metodsList = new HashSet<string>(Metods);
        }

        public void Sync()
        {
            save_bt.Enabled = isSravn || isDohod || isZatrat;
            //CHECK();
        }
        public void SyncDohod()
        {
            Sync();
            if (!string.IsNullOrEmpty(s_tb.Text) && !string.IsNullOrEmpty(Ca_tb.Text))
                PVD_tb.Text = Convert.ToString(setDouble(s_tb.Text) * setDouble(Ca_tb.Text));
            if (!string.IsNullOrEmpty(PVD_tb.Text) && !string.IsNullOrEmpty(poteri_tb.Text))
                DVD_tb.Text = Convert.ToString(setDouble(PVD_tb.Text) - setDouble(poteri_tb.Text));
            if (!string.IsNullOrEmpty(DVD_tb.Text) && !string.IsNullOrEmpty(rasuslpost_tb.Text) && !string.IsNullOrEmpty(rasuslper_tb.Text) && !string.IsNullOrEmpty(rasreserv_tb.Text))
                CHOD_tb.Text=Convert.ToString(setDouble(DVD_tb.Text) - setDouble(rasuslpost_tb.Text) - setDouble(rasuslper_tb.Text) - setDouble(rasreserv_tb.Text));
            if (!string.IsNullOrEmpty(ezhegod_tb.Text) && !string.IsNullOrEmpty(credit_tb.Text) && credit_tb.Text!="0")
                Rm_tb.Text = Convert.ToString(setDouble(ezhegod_tb.Text) / setDouble(credit_tb.Text));
            if (!string.IsNullOrEmpty(yearden_tb.Text) && !string.IsNullOrEmpty(capital_tb.Text) && capital_tb.Text != "0")
                Re_tb.Text = Convert.ToString(setDouble(yearden_tb.Text) / setDouble(capital_tb.Text));
            if (!string.IsNullOrEmpty(Rm_tb.Text) && !string.IsNullOrEmpty(Re_tb.Text) && !string.IsNullOrEmpty(M_tb.Text))
            {
                double Re= setDouble(Re_tb.Text);
                double Rm = setDouble(Rm_tb.Text);
                double M = setDouble(M_tb.Text);
                double R = M * Rm + (1 - M) * Re;
                R_tb.Text = Convert.ToString(R);
            }
            if (!string.IsNullOrEmpty(CHOD_tb.Text) && !string.IsNullOrEmpty(R_tb.Text) && R_tb.Text != "0")
                price_tb.Text = Convert.ToString(setDouble(CHOD_tb.Text) / setDouble(R_tb.Text));
        }
        private void CHECK()
        {
            /*label4.Text = label5.Text =label6.Text= "";
            for (int i = 0,j=1; i < sravnChange.Count; j++)
                if (sravnChange.ContainsKey(j))
                { label4.Text += j+" ";
                    label5.Text += sravnChange[j] + " ";
                    i++;
                }
            foreach (string s in deletedElements)
                label6.Text += s + " ";*/
        }

        private static double setDouble(string o)
        {
            double k;
            if (string.IsNullOrEmpty(o)) return 0;
            else if (double.TryParse(o, out k)) return k;
            else return 0;
        }
        private static string getInt(double o)
        {
            if (o == 0) return "";
            return Convert.ToString(o);
        }
        private static int setInt(string o)
        {
            int k;
            if (string.IsNullOrEmpty(o)) return 0;
            else if (int.TryParse(o, out k)) return k;
            else return 0;
        }
        private static string setString(object o)
        {
            if (o == null) return "";
            else return o.ToString();
        }

        //основное окно
        private void save_bt_Click(object sender, EventArgs e)
        {
            if (isSravn)
            {
                if (sravnChange[1] == -1)
                {
                    for (int k = 1, i = 3; k < sravnChange.Count; i++, k++)
                    {
                        int id;
                        if (sravnChange[i] > 0)
                        {
                            id = sravnChange[i];
                            dataBase.DeleteRealEstate(id);
                            sravnChange.Remove(i);
                        }
                        else
                        {
                            bool isAnalogue;
                            //MessageBox.Show("Column" + i);
                            if (i == 3)
                            {
                                if (Convert.ToString(sravn_dgv.Rows[0].Cells["Column" + i].Value).Length > 0)
                                {
                                    id = (int)sravn_dgv.Rows[0].Cells["Column" + i].Value;
                                }
                                else id = 0;
                                RealEstate estate;
                                estate = new RealEstate(id, (string)sravn_dgv.Rows[1].Cells["Column" + i].Value, (string)sravn_dgv.Rows[2].Cells["Column" + i].Value, 0, (string)sravn_dgv.Rows[3].Cells["Column" + i].Value, (string)sravn_dgv.Rows[4].Cells["Column" + i].Value);
                                //Element element
                                int n = sravn_dgv.RowCount;
                                List<Element> elements = new List<Element>();
                                Element element = new Element((string)sravn_dgv.Rows[6].Cells[0].Value, (string)sravn_dgv.Rows[6].Cells[1].Value, (string)sravn_dgv.Rows[6].Cells["Column" + i].Value, 1, 0);
                                elements.Add(element);
                                for (int j = 7, m = 1; j < n; j += 2, m++)
                                {
                                    string name = (string)sravn_dgv.Rows[j].Cells[0].Value;
                                    element = new Element(name, (string)sravn_dgv.Rows[j].Cells[1].Value, (string)sravn_dgv.Rows[j].Cells["Column" + i].Value, m, 0);
                                    elements.Add(element);
                                    if (name == "Итоговая корректировка")
                                    {
                                        element = new Element((string)sravn_dgv.Rows[j + 1].Cells[0].Value, (string)sravn_dgv.Rows[j + 1].Cells[1].Value, Convert.ToString(sravn_dgv.Rows[j + 1].Cells["Column" + i].Value), m, 0);
                                        elements.Add(element);
                                    }
                                }
                                estate.Elements = elements;
                                isAnalogue = false;
                                //                                    MessageBox.Show(estate.Id + ", " + ReportId + ", " + isAnalogue +", "+estate.Price);
                                if (estate.Id == 0)
                                    estate.Id=dataBase.AddRealEstate(ReportId, estate, isAnalogue, false, true);
                                else dataBase.UpdateRealEstate(estate, false);
                                // foreach (string s in deletedElements)
                                //   dataBase.DeleteElement(ReportId,estate.Id, s, true);
                                foreach (string s in deletedElements)
                                    if (!s.Contains("Итог")) dataBase.SetIsValuation(ReportId, id, s, false);
                                    else dataBase.DeleteElement(ReportId, estate.Id, s, true);
                                sravnChange[i] = 0;
                            }
                            else
                            {
                                if (Convert.ToString(sravn_dgv.Rows[0].Cells["Column" + i].Value).Length > 0)
                                {
                                    id = (int)sravn_dgv.Rows[0].Cells["Column" + i].Value;
                                }
                                else id = 0;
                                RealEstate estate;
                                estate = new RealEstate(id, setString(sravn_dgv.Rows[1].Cells["Column" + i].Value), setString(sravn_dgv.Rows[2].Cells["Column" + i].Value), setInt(Convert.ToString(sravn_dgv.Rows[5].Cells["Column" + i].Value)), setString(sravn_dgv.Rows[3].Cells["Column" + i].Value), setString(sravn_dgv.Rows[4].Cells["Column" + i].Value));
                                // MessageBox.Show(estate.Price + ", ");
                                //Element element
                                int n;
                                if (itog) n = sravn_dgv.RowCount - 1;
                                else n = sravn_dgv.RowCount;
                                List<Element> elements = new List<Element>();
                                Element element = new Element((string)sravn_dgv.Rows[6].Cells[0].Value, (string)sravn_dgv.Rows[6].Cells[1].Value, (string)sravn_dgv.Rows[6].Cells["Column" + i].Value, 1, 0);
                                elements.Add(element);
                                for (int j = 7, m = 1; j < n; j += 2, m++)
                                {
                                    string name = (string)sravn_dgv.Rows[j].Cells[0].Value;
                                    if (name != "Итоговая корректировка")
                                        element = new Element(name, (string)sravn_dgv.Rows[j].Cells[1].Value, (string)sravn_dgv.Rows[j].Cells["Column" + i].Value, m, setInt(sravn_dgv.Rows[j + 1].Cells["Column" + i].Value.ToString()));
                                    else element = new Element(name, Convert.ToString(sravn_dgv.Rows[j].Cells[1].Value), Convert.ToString(sravn_dgv.Rows[j].Cells["Column" + i].Value), m, 0);
                                    elements.Add(element);
                                }
                                estate.Elements = elements;
                                isAnalogue = true;
                                //  MessageBox.Show(estate.Id + ", " + ReportId + ", " + isAnalogue + ", " + estate.Price + ", "+ Convert.ToString(sravn_dgv.Rows[5].Cells["Column" + i].Value));
                                if (estate.Id == 0)
                                    estate.Id=dataBase.AddRealEstate(ReportId, estate, isAnalogue, false, true);
                                else dataBase.UpdateRealEstate(estate, false);
                                foreach (string s in deletedElements)
                                    dataBase.DeleteElement(ReportId,estate.Id, s, true);
                                sravnChange[i] = 0;
                            }
                          //  foreach (string elem in deletedElements)
                            //    dataBase.SetIsValuation(ReportId, id, elem, false);
                           // deletedElements = new HashSet<string>();
                        }
                    }
                }
                else
                {
                    for (int k = 1, i = 3; k < sravnChange.Count; i++, k++)
                    {
                        int id;
                        bool isAnalogue;
                        // MessageBox.Show(k + ", " + sravnChange.Count + ", " + i);
                        switch (sravnChange[i])
                        {
                            case 0:
                                break;
                            case -1:
                                if (i == 3)
                                {
                                    if (Convert.ToString(sravn_dgv.Rows[0].Cells["Column" + i].Value).Length > 0)
                                    {
                                        id = (int)sravn_dgv.Rows[0].Cells["Column" + i].Value;
                                    }
                                    else id = 0;
                                    RealEstate estate;
                                    estate = new RealEstate(id, (string)sravn_dgv.Rows[1].Cells["Column" + i].Value, (string)sravn_dgv.Rows[2].Cells["Column" + i].Value, 0, (string)sravn_dgv.Rows[3].Cells["Column" + i].Value, (string)sravn_dgv.Rows[4].Cells["Column" + i].Value);
                                    //Element element
                                    int n = sravn_dgv.RowCount;
                                    List<Element> elements = new List<Element>();
                                    Element element = new Element((string)sravn_dgv.Rows[6].Cells[0].Value, (string)sravn_dgv.Rows[6].Cells[1].Value, (string)sravn_dgv.Rows[6].Cells["Column" + i].Value, 1, 0);
                                    elements.Add(element);
                                    for (int j = 7, m = 1; j < n; j += 2, m++)
                                    {
                                        string name = (string)sravn_dgv.Rows[j].Cells[0].Value;
                                        element = new Element(name, (string)sravn_dgv.Rows[j].Cells[1].Value, (string)sravn_dgv.Rows[j].Cells["Column" + i].Value, m, 0);
                                        elements.Add(element);
                                        if (name == "Итоговая корректировка")
                                        {
                                            element = new Element((string)sravn_dgv.Rows[j + 1].Cells[0].Value, (string)sravn_dgv.Rows[j + 1].Cells[1].Value, Convert.ToString(sravn_dgv.Rows[j + 1].Cells["Column" + i].Value), m, 0);
                                            elements.Add(element);
                                        }
                                    }
                                    estate.Elements = elements;
                                    isAnalogue = false;
                                    //                                    MessageBox.Show(estate.Id + ", " + ReportId + ", " + isAnalogue +", "+estate.Price);
                                    if (estate.Id == 0)
                                        estate.Id=dataBase.AddRealEstate(ReportId, estate, isAnalogue, false, true);
                                    else dataBase.UpdateRealEstate(estate, false);
                                    foreach (string s in deletedElements)
                                        if (!s.Contains("Итог")) dataBase.SetIsValuation(ReportId, id, s, false);
                                        else dataBase.DeleteElement(ReportId, estate.Id, s, true);
                                    sravnChange[i] = 0;
                                }
                                else
                                {
                                    if (Convert.ToString(sravn_dgv.Rows[0].Cells["Column" + i].Value).Length > 0)
                                    {
                                        id = (int)sravn_dgv.Rows[0].Cells["Column" + i].Value;
                                    }
                                    else id = 0;
                                    RealEstate estate;
                                    estate = new RealEstate(id, setString(sravn_dgv.Rows[1].Cells["Column" + i].Value), setString(sravn_dgv.Rows[2].Cells["Column" + i].Value), setInt(Convert.ToString(sravn_dgv.Rows[5].Cells["Column" + i].Value)), setString(sravn_dgv.Rows[3].Cells["Column" + i].Value), setString(sravn_dgv.Rows[4].Cells["Column" + i].Value));
                                    // MessageBox.Show(estate.Price + ", ");
                                    //Element element
                                    int n;
                                    if (itog) n = sravn_dgv.RowCount - 1;
                                    else n = sravn_dgv.RowCount;
                                    List<Element> elements = new List<Element>();
                                    Element element = new Element((string)sravn_dgv.Rows[6].Cells[0].Value, (string)sravn_dgv.Rows[6].Cells[1].Value, (string)sravn_dgv.Rows[6].Cells["Column" + i].Value, 1, 0);
                                    elements.Add(element);
                                    for (int j = 7, m = 1; j < n; j += 2, m++)
                                    {
                                        string name = (string)sravn_dgv.Rows[j].Cells[0].Value;
                                        if (name != "Итоговая корректировка")
                                            element = new Element(name, (string)sravn_dgv.Rows[j].Cells[1].Value, (string)sravn_dgv.Rows[j].Cells["Column" + i].Value, m, setInt(sravn_dgv.Rows[j + 1].Cells["Column" + i].Value.ToString()));
                                        else element = new Element(name, Convert.ToString(sravn_dgv.Rows[j].Cells[1].Value), Convert.ToString(sravn_dgv.Rows[j].Cells["Column" + i].Value), m, 0);
                                        elements.Add(element);
                                    }
                                    estate.Elements = elements;
                                    isAnalogue = true;
                                    //  MessageBox.Show(estate.Id + ", " + ReportId + ", " + isAnalogue + ", " + estate.Price + ", "+ Convert.ToString(sravn_dgv.Rows[5].Cells["Column" + i].Value));
                                    if (estate.Id == 0)
                                        estate.Id=dataBase.AddRealEstate(ReportId, estate, isAnalogue, false, true);
                                    else dataBase.UpdateRealEstate(estate, false);
                                    foreach (string s in deletedElements)
                                        dataBase.DeleteElement(ReportId, estate.Id, s, true);
                                    sravnChange[i] = 0;
                                }
                                break;
                            default:
                                if (sravnChange[i] > 0)
                                {
                                    id = sravnChange[i];
                                    dataBase.DeleteRealEstate(id);
                                    sravnChange.Remove(i);
                                }
                                break;
                        }
                    }
                }
                isSravn = false;
                deletedElements = new HashSet<string>();
                sravnChange[1] = 0;
            }
            if (isDohod)
            {
                if (EstateId == 0)
                    EstateId = dataBase.AddRealEstate(ReportId);
                // HashSet<string> oldMetodsList = new HashSet<string>(dataBase.GetPodhodsMetods(ReportId, EstateId));
                List<Parameter> parameters = new List<Parameter>();
                parameters.Add(new Parameter("доходный", "метод капитализации", "s", setDouble(s_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "Ca", setDouble(Ca_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "ПВД", setDouble(PVD_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "Потери", setDouble(poteri_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "ДВД", setDouble(DVD_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "rasuslpost", setDouble(rasuslpost_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "rasuslper", setDouble(rasuslper_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "rasreserv", setDouble(rasreserv_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "ЧОД", setDouble(CHOD_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "ezhegod", setDouble(ezhegod_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "credit", setDouble(credit_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "Rm", setDouble(Rm_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "yearden", setDouble(yearden_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "capital", setDouble(capital_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "Re", setDouble(Re_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "M", setDouble(M_tb.Text)));
                parameters.Add(new Parameter("доходный", "метод капитализации", "R", setDouble(R_tb.Text)));
                if (dataBase.metodIsExist(EstateId, "доходный", "метод капитализации"))
                    dataBase.UpdateParameters(EstateId,parameters);
                else
                    foreach (Parameter p in parameters)
                        dataBase.AddParameter(EstateId, p);
                isDohod = false;
                save_bt.Enabled=false;
                //if (oldMetodsList)
            }
            Sync();
        }
        private void cancel_bt_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void form_closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            if (save_bt.Enabled)
            {
                DialogResult result = MessageBox.Show("Сохранить изменения?", "Сохранение", MessageBoxButtons.YesNoCancel);
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
        private void metods(List<string> list, string typeObject)
        {
            //List<string> sravnMetods = new List<string>();
            List<string> dohodMetods = new List<string>();
            //List<string> zatratMetods = new List<string>();
            List<RealEstate> realEstates = null;
            tabControl1.TabPages.Clear();
            for (int i = 0; i < list.Count; i++)
            {
                string[] subs = list[i].Split('|');
                switch (subs[0])
                {
                    case "сравнительный":
                        if (isSravn) { tabControl1.TabPages.Add(sravn_tp); isSravn = false; }
                     //   sravnMetods.Add(subs[1]);
                        break;
                    case "доходный":
                        if (isDohod) { tabControl1.TabPages.Add(dohodKap_tp); isDohod = false; }
                        dohodMetods.Add(subs[1]);
                        break;
                  /*  case "затратный":
                        if (isZatrat) { tabControl1.TabPages.Add(zatrat_tp); isZatrat = false; }
                        zatratMetods.Add(subs[1]);
                        break;*/
                }
            }
            if (isSravn)
            {
                //tabControl1.TabPages.Add(sravnObosn_tp);
                isSravn = false;
            }
            else
            {
                //  DataGridViewCell cell = new DataGridViewCell();
                //  DataGridViewRow row = new DataGridViewRow();
                sravnChange = new Dictionary<int, int>();
                realEstates = dataBase.GetRealEstates(ReportId);
                EstateId = 0;
                object[] ids = new object[realEstates.Count + 2];
                ids[0] = "";
                ids[1] = "";
                for (int i = 0; i < realEstates.Count; i++)
                    ids[i + 2] = realEstates[i].Id;
                sravn_dgv.Rows.Add(ids);
                sravn_dgv.Rows[0].Visible = false;
                sravn_dgv.Rows.Add("Адрес");
                sravn_dgv.Rows.Add("Регион");
                sravn_dgv.Rows.Add("Ссылка на источник", "", "-");
                sravn_dgv.Rows.Add("Телефон владельцев или риелторов", "", "-");
                sravn_dgv.Rows.Add("Цена", "тыс.руб.", "-");
                sravn_dgv.Rows.Add("Цена за один кв.м.", "тыс.руб.", "-");
                sravn_dgv.Rows[6].ReadOnly = true;
                sravn_dgv.Rows[5].Cells[2].ReadOnly = true;
                sravn_dgv.Rows[4].Cells[2].ReadOnly = true;
                sravn_dgv.Rows[3].Cells[2].ReadOnly = true;
                sravnChange.Add(1, 0);
                if (realEstates.Count > 0)
                {
                    EstateId = realEstates[0].Id;
                    if (realEstates.Count != 1)
                    {
                        if (realEstates.Count > 4)
                        {
                            while (realEstates.Count != (sravn_dgv.ColumnCount - 2))
                                addAnalogue_bt_Click(null, null);
                        }
                        else if (realEstates.Count < 4)
                        {
                            while (realEstates.Count != (sravn_dgv.ColumnCount - 2))
                                sravn_dgv.Columns.RemoveAt(sravn_dgv.ColumnCount - 1);
                        }
                    }
                    RealEstate realEstate = realEstates[0];
                    if (realEstate.Elements != null)
                    {
                        for (int i = 1; i < realEstate.Elements.Count; i++)
                        {
                            sravn_dgv.Rows.Add(realEstate.Elements[i].Name, realEstate.Elements[i].Unit);
                            if (!realEstate.Elements[i].Name.Contains("Итог"))
                            {
                                setCorrect(sravn_dgv.ColumnCount);
                                itog = false;
                            }
                            else itog = true;
                        }
                        for (int i = 0; i < realEstates.Count; i++)
                        {
                            //ids.Add(realEstates[i].Id);
                            load_sravn(i + 2, realEstates[i]);
                            sravnChange.Add(3 + i, 0);
                        }
                    }
                    else
                    {
                        sravn_dgv.Rows[0].Cells[2].Value = realEstate.Id;
                        sravn_dgv.Rows[1].Cells[2].Value = realEstate.Adres;
                        sravn_dgv.Rows[2].Cells[2].Value = realEstate.Region;
                        load_elements(typeObject);
                        sravnChange.Add(3, -1);
                        sravnChange.Add(4, -1);
                        sravnChange.Add(5, -1);
                        sravnChange.Add(6, -1);
                    }

                    //load_sravn(2, realEstate);

                }
                else
                {
                    //sravn_dgv.Rows.Add("Цена", "тыс.руб.");
                    //  setCorrect(6);
                    load_elements(typeObject);
                    sravnChange.Add(3, -1);
                    sravnChange.Add(4, -1);
                    sravnChange.Add(5, -1);
                    sravnChange.Add(6, -1);
                }
                KolColumn = sravn_dgv.ColumnCount+1;
            }
            if (isDohod)
            {
                //tabControl1.TabPages.Add(dohodObosn_tp);
                isDohod = false;
            }
            else
            {
                RealEstate realEstate = dataBase.GetRealEstate(ReportId);
                if (realEstate == null) EstateId = 0;
                else EstateId = realEstate.Id;
                List<Parameter> parameters = dataBase.GetParameters(EstateId, "доходный", "метод капитализации");
                if (parameters!=null)
                {
                    s_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "s").Value);
                    Ca_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "Ca").Value);
                    PVD_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "ПВД").Value);
                    poteri_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "Потери").Value);
                    DVD_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "ДВД").Value);
                    rasuslpost_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "rasuslpost").Value);
                    rasuslper_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "rasuslper").Value);
                    rasreserv_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "rasreserv").Value);
                    CHOD_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "ЧОД").Value);
                    ezhegod_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "ezhegod").Value);
                    credit_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "credit").Value);
                    Rm_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "Rm").Value);
                    yearden_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "yearden").Value);
                    capital_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "capital").Value);
                    Re_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "Re").Value);
                    M_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "M").Value);
                    R_tb.Text = Convert.ToString(parameters.Find(p => p.Name == "R").Value);
                }
            }
            if (isZatrat)
            {
                //tabControl1.TabPages.Add(zatratObosn_tp);
                isZatrat = false;
            }
            //CHECK();
        }

        //Сравнительный подход        
        private string getPriceForSquare(double price, double square)
        {
            double n = Math.Round(price / square, 1);
            return Convert.ToString(n);
        }
        private double tryDouble(string s)
        {
            double d;
            if (Double.TryParse(s, out d))
                return d;
            else return -1;
        }
        private void setCorrect(int n)
        {
            object[] row = new object[n];
            row[0] = "Корректировка " + correct;
            row[1] = "%";
            row[2] = "-";
            for (int i = 3; i < n; i++)
            {
                row[i] = 0;
            }
            sravn_dgv.Rows.Add(row);
            sravn_dgv.Rows[sravn_dgv.Rows.Count - 1].Cells[0].Style.Alignment = DataGridViewContentAlignment.MiddleRight;
            //  sravn_dgv.Rows[sravn_dgv.Rows.Count - 1].Cells[3].Style.Alignment = DataGridViewContentAlignment.MiddleCenter;
            sravn_dgv.Rows[sravn_dgv.Rows.Count - 1].Cells[0].ReadOnly = true;
            correct++;
        }
        /*   private void setCorrect2(int column)
           {
               sravn_dgv.Columns[column].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
               for (int i = 8; i < sravn_dgv.RowCount; i += 2)
                   if (!Convert.ToString(sravn_dgv.Rows[i].Cells[column].Value).Contains("Итог"))
                       sravn_dgv.Rows[i].Cells[column].Value = 0;
           }*/
        private void load_elements(string typeObject)
        {
            sravn_dgv.Rows.Add("Площадь", "кв.м.");
            setCorrect(6);
            sravn_dgv.Rows.Add("Дата предложения", "");
            setCorrect(6);
            sravn_dgv.Rows.Add("Время до метро/жд станции/остановки общественного транспорта", "мин.");
            setCorrect(6);
            switch (typeObject)
            {
                case "квартира":
                    sravn_dgv.Rows.Add("Жилая площадь", "кв.м.");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Площадь кухни", "кв.м.");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Кол-во комнат", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Номер этажа", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Кол-во этажей", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Качество отделки", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Наличие мебели", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Вид из окна", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Санузел", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Высота потолков", "м.");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Наличие балкона или лоджии", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Материал стен", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Год постройки", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Физический износ здания", "%");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Благоустройство дома", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Состояние дома", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Инженерные системы", "");
                    setCorrect(6);
                    break;
                case "дом":
                    sravn_dgv.Rows.Add("Кол-во этажей", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Материал стен", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Год постройки", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Физический износ здания", "%");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Благоустройство дома", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Состояние дома", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Инженерные системы", "");
                    setCorrect(6);
                    break;
                case "дом с участком":
                    sravn_dgv.Rows.Add("Кол-во этажей", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Материал стен", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Год постройки", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Физический износ здания", "%");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Благоустройство дома", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Состояние дома", "");
                    setCorrect(6);
                    sravn_dgv.Rows.Add("Инженерные системы", "");
                    setCorrect(6);
                    break;
                case "участок":
                    break;
            }
            sravn_dgv.Rows.Add("Условия финансирования", "");
            setCorrect(6);
            sravn_dgv.Rows.Add("Условия продажи", "");
            setCorrect(6);
        }
        private void load_sravn(int column, RealEstate realEstate)
        {
            sravn_dgv.Rows[0].Cells[column].Value = realEstate.Id;
            sravn_dgv.Rows[1].Cells[column].Value = realEstate.Adres;
            sravn_dgv.Rows[2].Cells[column].Value = realEstate.Region;
            if (column > 2)
            {
                sravn_dgv.Rows[3].Cells[column].Value = realEstate.Link;
                sravn_dgv.Rows[4].Cells[column].Value = realEstate.Telephone;
                sravn_dgv.Rows[5].Cells[column].Value = getInt(realEstate.Price);
                sravn_dgv.Rows[6].Cells[column].Value = getInt(Convert.ToDouble(realEstate.Elements[0].Value));//цена за кв. м.
                sravn_dgv.Rows[7].Cells[column].Value = realEstate.Elements[1].Value;//площадь
                sravn_dgv.Rows[8].Cells[column].Value = realEstate.Elements[1].Correct;//корректировка площади
                double price = tryDouble(Convert.ToString(sravn_dgv.Rows[5].Cells[column].Value));
                double square = tryDouble(Convert.ToString(sravn_dgv.Rows[7].Cells[column].Value));
                if (price > -1 && square > 0)
                    sravn_dgv.Rows[6].Cells[column].Value = getPriceForSquare(price, square);
                else sravn_dgv.Rows[6].Cells[column].Value = "";
            }
            else
            {
                sravn_dgv.Rows[3].Cells[column].Value = "-";
                sravn_dgv.Rows[4].Cells[column].Value = "-";
                sravn_dgv.Rows[5].Cells[column].Value = "-";
                sravn_dgv.Rows[6].Cells[column].Value = "-";//цена за кв. м.
                sravn_dgv.Rows[7].Cells[column].Value = realEstate.Elements[1].Value;//площадь
                sravn_dgv.Rows[8].Cells[column].Value = "-";//корректировка площади
            }
            int count = realEstate.Elements.Count;
            // sravn_dgv.Rows[6].Cells[column].Value = realEstate.Elements[0].Value;//цена за кв. м.
            //sravn_dgv.Rows[7].Cells[column].Value = realEstate.Elements[1].Value;//площадь
            //sravn_dgv.Rows[8].Cells[column].Value = realEstate.Elements[1].Correct;//корректировка площади            
            sravn_dgv.Rows[6].ReadOnly = true;
            int k = 9;
            if (column > 2)
                for (int i = 2; i < count; i++)
                {
                    sravn_dgv.Rows[k].Cells[column].Value = realEstate.Elements[i].Value;
                    if (!Convert.ToString(sravn_dgv.Rows[k].Cells[0].Value).Contains("Итоговая корректировка"))
                        sravn_dgv.Rows[k + 1].Cells[column].Value = realEstate.Elements[i].Correct;
                    k += 2;
                }
            else
                for (int i = 2; i < count; i++)
                {
                    sravn_dgv.Rows[k].Cells[column].Value = realEstate.Elements[i].Value;
                    if (!Convert.ToString(sravn_dgv.Rows[k].Cells[0].Value).Contains("Итог"))
                    {
                        sravn_dgv.Rows[k + 1].Cells[column].Value = "-";
                        k += 2;
                    }
                    else k++;
                }
            sravn_dgv.Columns[column].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
        }
        private void addAnalogue_bt_Click(object sender, EventArgs e)
        {
            //DataGridViewColumn column=new DataGridViewColumn("Column" + sravn_dgv.ColumnCount, "Аналог " + (sravn_dgv.ColumnCount - 3));
            sravn_dgv.Columns.Add("Column" + KolColumn, "Аналог " + (sravn_dgv.ColumnCount - 3));
            sravn_dgv.Columns["Column" + KolColumn].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            sravnChange.Add(KolColumn, -1);
            KolColumn++;
            int column = sravn_dgv.ColumnCount - 1;
            sravn_dgv.Rows[6].Cells[column].Value = "";
            if (itog)
            {
                deletedElements.Add("Итог");
                deletedElements.Add("Итоговая корректировка");
                sravn_dgv.Rows.RemoveAt(sravn_dgv.RowCount - 1);
                sravn_dgv.Rows.RemoveAt(sravn_dgv.RowCount - 1);
                sravnChange[1] = -1;
                itog = false;
            }
            for (int i = 7; i < sravn_dgv.RowCount; i += 2)
            {
                sravn_dgv.Rows[i].Cells[column].Value = "";
                sravn_dgv.Rows[i + 1].Cells[column].Value = 0;
            }
            sravn_dgv.Columns[column].DefaultCellStyle.Alignment = DataGridViewContentAlignment.MiddleCenter;
            isSravn = true;
            Sync();
        }
        private void addElement_bt_Click(object sender, EventArgs e)
        {
            if (itog)
            {
                deletedElements.Add("Итог");
                deletedElements.Add("Итоговая корректировка");
                sravn_dgv.Rows.RemoveAt(sravn_dgv.RowCount - 1);
                sravn_dgv.Rows.RemoveAt(sravn_dgv.RowCount - 1);
                sravnChange[1] = -1;
                itog = false;
            }
            sravn_dgv.Rows.Add(isExist("NewElement"));
            setCorrect(sravn_dgv.ColumnCount);
            sravnChange[1] = -1;
            isSravn = true;
            Sync();
        }
        private void delete_bt_Click(object sender, EventArgs e)
        {
            /*if (sravn_dgv.SelectedRows.Count > 0)
            {
                if (sravn_dgv.SelectedRows[0].Index > 3 && Convert.ToString(sravn_dgv.SelectedRows[0].Cells[0].Value).Contains("Корректировка "))
                {
                    for (int i = 0; i < sravn_dgv.SelectedRows.Count; i += 2)
                        sravn_dgv.SelectedRows[i].;
                }
                foreach (DataGridViewRow r in sravn_dgv.SelectedRows)
                {
                    sravn_dgv.Rows.RemoveAt(r.Index);
                    sravn_dgv.Rows.RemoveAt(r.Index + 1);
                }
                int selectedRowCount = sravn_dgv.Rows.GetRowCount(DataGridViewElementStates.Selected);
                if (selectedRowCount > 0)
                {
                    for (int i = 0; i < selectedRowCount; i++)
                    {
                        sravn_dgv.Rows.RemoveAt(sravn_dgv.SelectedRows[0].Index);
                    }
                }
                else MessageBox.Show("Данный строку удалить нельзя", "Внимание", MessageBoxButtons.OK);
            }*/
            if (sravn_dgv.SelectedCells.Count > 0)
            {
                int index = sravn_dgv.SelectedCells[0].RowIndex;
                string name = Convert.ToString(sravn_dgv.Rows[index].Cells[0].Value);
                //MessageBox.Show(name + " " + index);
                if (index > 8 && !name.Contains("Корректировка ") && !name.Contains("Итог"))
                {
                    deletedElements.Add(name);
                    // if (Convert.ToString(sravn_dgv.Rows[index].Cells[0].Value).Contains("Корректировка "))
                    //{
                    sravn_dgv.Rows.RemoveAt(index);
                    sravn_dgv.Rows.RemoveAt(index);
                    if (itog)
                    {
                        deletedElements.Add("Итог");
                        deletedElements.Add("Итоговая корректировка");
                        sravn_dgv.Rows.RemoveAt(sravn_dgv.RowCount - 1);
                        sravn_dgv.Rows.RemoveAt(sravn_dgv.RowCount - 1);                        
                        itog = false;
                    }
                    sravnChange[1] = -1;
                    isSravn = true;
                    Sync();
                    //  }
                    /*  foreach (DataGridViewRow r in sravn_dgv.SelectedRows)
                      {
                          if (!Convert.ToString(sravn_dgv.Rows[index].Cells[0].Value).Contains("Корректировка "))
                              deletedElements.Add(Convert.ToString(sravn_dgv.Rows[index].Cells[0].Value));
                      }

                      sravn_dgv.SelectedRows.Clear();
                      if (Convert.ToString(sravn_dgv.Rows[index].Cells[0].Value).Contains("Корректировка "))
                          sravn_dgv.Rows.RemoveAt(index);*/
                }
            }
        }
        private int getColumnNumber(string name)
        {
            return Convert.ToInt32(name.Remove(0, 6));
        }
        private void sravn_dgv_CellValueChanged(object sender, DataGridViewCellEventArgs e)
        {
            //если поменяли элемент ищи чтобы он был уникальным
            //если поменяли элемент уже встроенный элемент его поменять нельзя
            //if(dataGridView1.Columns[e.ColumnIndex].Name == "Reference"){
            if ((e.RowIndex == 5 || e.RowIndex == 7) && e.ColumnIndex > 2)
            {
                double price = tryDouble(Convert.ToString(sravn_dgv.Rows[5].Cells[e.ColumnIndex].Value));
                double square = tryDouble(Convert.ToString(sravn_dgv.Rows[7].Cells[e.ColumnIndex].Value));
                if (price > -1 && square > 0)
                    sravn_dgv.Rows[6].Cells[e.ColumnIndex].Value = getPriceForSquare(price, square);
                else sravn_dgv.Rows[6].Cells[e.ColumnIndex].Value = "";
            }
            switch (e.ColumnIndex)
            {
                /*case 0:
                    if (e.RowIndex < 5)
                    {
                        MessageBox.Show("Наименование данного элемента сравнения нельзя менять", "Внимание", MessageBoxButtons.OK);
                        switch (e.RowIndex)
                        {
                            case 0:
                                sravn_dgv.Rows[0].Cells[0].Value = "Адрес";
                                break;
                            case 1:
                                sravn_dgv.Rows[1].Cells[0].Value = "Регион";
                                break;
                            case 2:
                                sravn_dgv.Rows[2].Cells[0].Value = "Ссылка на источник";
                                break;
                            case 3:
                                sravn_dgv.Rows[3].Cells[0].Value = "Телефон владельцев или риелторов";
                                break;
                            case 4:
                                sravn_dgv.Rows[4].Cells[0].Value = "Цена";
                                break;
                        }
                    }
                    else {
                        string name = (string)sravn_dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value;
                        sravn_dgv.Rows[e.RowIndex].Cells[e.ColumnIndex].Value = isExist(name);
                        sravnChange[0] = 1; }
                    break;*/
                case 1:
                    sravnChange[1] = -1;
                    isSravn = true;
                    Sync();
                    break;
                default:
                    sravnChange[getColumnNumber(sravn_dgv.Columns[e.ColumnIndex].Name)] = -1;
                    isSravn = true;
                    Sync();
                    break;
            }
            if (itog)
            {
                deletedElements.Add("Итог");
                deletedElements.Add("Итоговая корректировка");
                sravn_dgv.Rows.RemoveAt(sravn_dgv.RowCount - 1);
                sravn_dgv.Rows.RemoveAt(sravn_dgv.RowCount - 1);
                sravnChange[1] = -1;
                itog = false;
            }
            //CHECK();
        }
        private string isExist(string name)
        {
            HashSet<string> elements = new HashSet<string>();
            for (int i = 0; i < sravn_dgv.RowCount - 1; i++)
                elements.Add((string)sravn_dgv.Rows[i].Cells[0].Value);
            int k = 1;
            string old_name = name;
            while (!elements.Add(name))
            { name = old_name + k;
                k++;
            }
            return name;
        }
        private void deleteAnalogue_bt_Click(object sender, EventArgs e)
        {
            if (sravn_dgv.SelectedCells.Count > 0)
            {
                HashSet<string> column = new HashSet<string>();
                foreach (DataGridViewCell cell in sravn_dgv.SelectedCells)
                {
                    int index = cell.ColumnIndex;
                    if (index > 2)
                    {
                        bool b = column.Add(sravn_dgv.Columns[index].Name);
                        if (b && Convert.ToString(sravn_dgv.Rows[0].Cells[index].Value) != "")
                            sravnChange[getColumnNumber(sravn_dgv.Columns[index].Name)] = Convert.ToInt32(sravn_dgv.Rows[0].Cells[index].Value);
                    }
                }
                foreach (string i in column)
                {
                    if (i != "Column1" && i != "Column2" && i != "Column3")
                        sravn_dgv.Columns.Remove(i);
                }
            }
            if (itog)
            {
                deletedElements.Add("Итог");
                deletedElements.Add("Итоговая корректировка");
                sravn_dgv.Rows.RemoveAt(sravn_dgv.RowCount - 1);
                sravn_dgv.Rows.RemoveAt(sravn_dgv.RowCount - 1);
                sravnChange[1] = -1;
                itog = false;
            }
            isSravn = true;
            Sync();
        }
        private void calculatePrice_bt_Click(object sender, EventArgs e)
        {
            if (sravn_dgv.ColumnCount > 3)
            {
                bool b = true;
                double[] prices = new double[sravn_dgv.ColumnCount - 2];
                for (int i = 3; i < sravn_dgv.ColumnCount; i++)
                {
                    string name = Convert.ToString(sravn_dgv.Rows[6].Cells[i].Value);
                    double price;
                    if (name != "" && Double.TryParse(name, out price))
                    {
                        b = true;
                        prices[i - 3] = price;
                    }
                    else
                    {
                        b = false;
                        i = sravn_dgv.ColumnCount;
                    }
                }
                if (b)
                {
                    object[] row = new object[sravn_dgv.ColumnCount];
                    row[0] = "Итоговая корректировка";
                    row[1] = "";
                    row[2] = "";
                    for (int i = 3; i < sravn_dgv.ColumnCount; i++)
                    {
                        int correct = 0;
                        for (int j = 8; j < sravn_dgv.RowCount; j += 2)
                        {
                            int cor = Convert.ToInt32(sravn_dgv.Rows[j].Cells[i].Value);
                            correct += cor;
                        }
                        row[i] = correct;
                        prices[i - 3] = Math.Round(prices[i - 3] + prices[i - 3] * (correct / 100.0), 1);
                        prices[prices.Length - 1] += prices[i - 3];
                    }
                    sravn_dgv.Rows.Add(row);
                    sravn_dgv.Rows[sravn_dgv.RowCount - 1].ReadOnly = true;
                    // prices[prices.Length - 1] = Math.Round(prices[prices.Length - 1] / 3 * 1000);
                    prices[prices.Length - 1] = Math.Round(prices[prices.Length - 1] / 3.0, 2);
                    row = new object[3];
                    row[0] = "Итог";
                    row[1] = "";
                    // row[2] = prices[prices.Length - 1] * Convert.ToDouble(sravn_dgv.Rows[7].Cells[2].Value);
                    row[2] = prices[prices.Length - 1] * Convert.ToDouble(sravn_dgv.Rows[7].Cells[2].Value);
                    //row[3] = prices[prices.Length - 1];
                    sravn_dgv.Rows.Add(row);
                    sravn_dgv.Rows[sravn_dgv.RowCount - 1].ReadOnly = true;
                    // sravn_dgv.Rows.Add("","","",prices[0],prices[1],prices[2]);
                    isSravn = true;
                    sravnChange[1] = -1;
                    itog = true;
                    deletedElements.Remove("Итог");
                    deletedElements.Remove("Итоговая корректировка");
                    Sync();
                }
            }
        }
        private void renameElement_bt_Click(object sender, EventArgs e)
        {
            if (sravn_dgv.SelectedCells.Count > 0)
            {
                int row = sravn_dgv.SelectedCells[0].RowIndex;
                string name = (string)sravn_dgv.Rows[row].Cells[0].Value;
                if (row > 7 && row != sravn_dgv.RowCount - 1 && (string)sravn_dgv.Rows[row + 1].Cells[0].Value != "Итог" && !name.Contains("Корректировка "))
                {
                    RenameElementForm form = new RenameElementForm(name, "Новый наименование элемента сравнения", "Введите новое наименование элемента");
                    form.ShowDialog();
                    string newname = form.newName;
                    if (form.DialogResult == DialogResult.OK)
                    {
                        int n = sravn_dgv.ColumnCount;
                        for (int i = 2; i < n; i++)
                        {
                            int id = Convert.ToInt32(sravn_dgv.Rows[0].Cells[i].Value);
                            if (Convert.ToInt32(sravn_dgv.Rows[0].Cells[i].Value) > 0)
                                dataBase.RenameElement(id, newname, name);
                        }
                        sravn_dgv.Rows[row].Cells[0].Value = newname;
                        isSravn = true;
                        Sync();
                    }
                }
                else MessageBox.Show("Нельзя переименовать данный элемент", "Внимание", MessageBoxButtons.OK);
            }
            else MessageBox.Show("Выберите элемент, который вы хотите переименовать", "Внимание", MessageBoxButtons.OK);
        }

        //Доходный подход
        //метод капитализации - Метод связанных инвестиций или техника инвестиционной группы
        //1 Этап
        private void DohodChanged(object sender, EventArgs e)
        {
            isDohod = true;
            SyncDohod();
        }        
    }
}