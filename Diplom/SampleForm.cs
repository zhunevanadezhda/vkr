using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Windows.Forms;
using Microsoft.Office.Interop.Word;
using System.Reflection;
using Word = Microsoft.Office.Interop.Word;
using DataTable = System.Data.DataTable;
using System.Drawing.Drawing2D;

namespace Diplom
{
    public partial class SampleForm : Form
    {
        private int UserId;
        private int ReportId;
        private BD dataBase = new BD();
        private List<Sample> samples;
        public SampleForm(int id_u, int id_r)
        {
            InitializeComponent();
            reference_bt.FlatStyle = FlatStyle.Popup;
            add_bt.FlatStyle = FlatStyle.Popup;
            open_bt.FlatStyle = FlatStyle.Popup;
            copy_bt.FlatStyle = FlatStyle.Popup;
            rename_bt.FlatStyle = FlatStyle.Popup;
            delete_bt.FlatStyle = FlatStyle.Popup;
            form_bt.FlatStyle = FlatStyle.Popup;
            UserId = id_u;
            ReportId = id_r;
            load();
            openFileDialog1.InitialDirectory = "c:\\";
            openFileDialog1.Filter = "Word File (.docx ,.doc)|*.docx;*.doc";
            openFileDialog1.FilterIndex = 2;
            saveFileDialog1.InitialDirectory = "c:\\";
            saveFileDialog1.Filter = "Word File (.docx ,.doc)|*.docx;*.doc";
            saveFileDialog1.FilterIndex = 2;
        }

        private void load()
        {
            samples = dataBase.GetSamples(UserId);
            if (samples != null)
                foreach (Sample s in samples)
                    listBox1.Items.Add(s.Name);
        }

        private void reference_bt_Click(object sender, EventArgs e)
        {
            Reference form = new Reference();
            form.ShowDialog();
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedIndex > -1)
            {
                delete_bt.Enabled = true;
                open_bt.Enabled = true;
                copy_bt.Enabled = true;
                form_bt.Enabled = true;
                rename_bt.Enabled = true;
            }
            else
            {
                delete_bt.Enabled = false;
                open_bt.Enabled = false;
                copy_bt.Enabled = false;
                form_bt.Enabled = false;
                rename_bt.Enabled = false;
            }
        }
        private void add_bt_Click(object sender, EventArgs e)
        {
            using (OpenFileDialog openFileDialog = new OpenFileDialog())
            {
                //openFileDialog1.InitialDirectory = "c:\\";
                //openFileDialog1.Filter = "Word File (.docx ,.doc)|*.docx;*.doc";
                //openFileDialog.Filter =  "Image files (*.jpg, *.jpeg, *.jpe, *.jfif, *.png) | *.jpg; *.jpeg; *.jpe; *.jfif; *.png";
                //  openFileDialog1.FilterIndex = 2;
                openFileDialog1.RestoreDirectory = true;
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    Sample sample = new Sample(openFileDialog1.SafeFileName, openFileDialog1.FileName);
                    sample.Id = dataBase.AddSample(UserId, sample);
                    samples.Add(sample);
                    listBox1.Items.Add(sample.Name);
                }
            }
        }
        private void open_bt_Click(object sender, EventArgs e)
        {
            string path = samples[listBox1.SelectedIndex].Path;
            if (File.Exists(path))
                Process.Start(path);
            else
            {
                MessageBox.Show("Данного файла не существует", "Внимание", MessageBoxButtons.OK);
                delete_bt_Click(sender, e);
            }
        }
        private void copy_bt_Click(object sender, EventArgs e)
        {
            string path = samples[listBox1.SelectedIndex].Path;
            if (File.Exists(path))
            {
                string name = listBox1.SelectedItem.ToString();
                string shortname = name.Replace(".docx", "");
                string extension = ".docx";
                if (shortname == name)
                {
                    shortname = name.Replace(".doc", "");
                    extension = ".doc";
                }
                string newname = getUniqueName(path.Replace(name, ""), shortname, extension);
                File.Copy(path, path.Replace(name, "") + newname);
                Sample sample = new Sample(newname, path.Replace(name, "") + newname);
                sample.Id = dataBase.AddSample(UserId, sample);
                samples.Add(sample);
                listBox1.Items.Add(sample.Name);
            }
            else
            {
                MessageBox.Show("Данного файла не существует", "Внимание", MessageBoxButtons.OK);
                delete_bt_Click(sender, e);
            }
        }
        private void rename_bt_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            string path = samples[index].Path;
            if (File.Exists(path))
            {
                string name = listBox1.SelectedItem.ToString();
                string shortname = name.Replace(".docx", "");
                string extension = ".docx";
                if (shortname == name)
                {
                    shortname = name.Replace(".doc", "");
                    extension = ".doc";
                }
                RenameElementForm form = new RenameElementForm(shortname, "Новое име файла", "Введите новое имя файла");
                form.ShowDialog();
                string newname = form.newName;
                if (form.DialogResult == DialogResult.OK)
                {
                    bool b = true;
                    while (b && File.Exists(path.Replace(name, "") + newname + extension))
                    {
                        MessageBox.Show("В данной директории уже существует файл с таким именем", "Внимание", MessageBoxButtons.OK);
                        form = new RenameElementForm(newname, "Новое име файла", "Введите новое имя файла");
                        newname = form.newName;
                        if (form.DialogResult == DialogResult.Cancel)
                            b = false;
                    }
                    if (b)
                    {
                        File.Move(path, path.Replace(name, "") + newname + extension);
                        samples[index].Name = newname + extension;
                        samples[index].Path = path.Replace(name, "") + newname + extension;
                        dataBase.UpdateSample(samples[index]);
                        listBox1.Items[index] = samples[index].Name;
                    }
                }
            }
            else
            {
                MessageBox.Show("Данного файла не существует", "Внимание", MessageBoxButtons.OK);
                delete_bt_Click(sender, e);
            }
        }
        private void delete_bt_Click(object sender, EventArgs e)
        {
            int index = listBox1.SelectedIndex;
            int id = samples[index].Id;
            dataBase.DeleteSample(id);
            samples.RemoveAt(index);
            listBox1.Items.RemoveAt(index);
        }
        private string getUniqueName(string path, string oldName, string extension)
        {
            int k = 1;
            string name = oldName;
            while (File.Exists(path + name + extension))
            {
                name = oldName + " (" + k + ")";
                k++;
            }
            return name + extension;
        }
        private void Disable()
        {
            reference_bt.Enabled = open_bt.Enabled = copy_bt.Enabled = delete_bt.Enabled = add_bt.Enabled = rename_bt.Enabled = listBox1.Enabled = form_bt.Enabled = false;
        }
        private void Enable()
        {
            reference_bt.Enabled = open_bt.Enabled = copy_bt.Enabled = delete_bt.Enabled = add_bt.Enabled = rename_bt.Enabled = listBox1.Enabled = form_bt.Enabled = true;
        }

        private void form_bt_Click(object sender, EventArgs e)
        {
            if (saveFileDialog1.ShowDialog() == DialogResult.Cancel)
                return;
            // получаем выбранный файл
            string filename = saveFileDialog1.FileName;
            // сохраняем текст в файл
            string path = samples[listBox1.SelectedIndex].Path;
            if (File.Exists(path))
            {
                Disable();
                File.Copy(path, filename);
                CreateWordDocument(filename, filename);
                Enable();
                /*  File.Copy(path, filename);
                StringBuilder sbText = new StringBuilder(File.ReadAllText(filename));
                sbText.Replace("$formEC", "ООО");    
                sbText.Replace("$nameEC", "Всем привет!");
                File.WriteAllText(filename, sbText.ToString());*/
            }
            else
            {
                MessageBox.Show("Данного шаблона не существует", "Внимание", MessageBoxButtons.OK);
                delete_bt_Click(sender, e);
            }
        }

        private void FindAndReplace(Microsoft.Office.Interop.Word.Application wordApp, object findText, object replaceWithText)
        {
            object matchCase = true;
            object matchWholeWord = true;
            object matchWildCards = false;
            object matchSoundLike = false;
            object nmatchAllForms = false;
            object forward = true;
            object format = false;
            object matchKashida = false;
            object matchDiactitics = false;
            object matchAlefHamza = false;
            object matchControl = false;
            object read_only = false;
            object visible = true;
            object replace = 2;
            object wrap = 1;

            wordApp.Selection.Find.Execute(ref findText,
                        ref matchCase, ref matchWholeWord,
                        ref matchWildCards, ref matchSoundLike,
                        ref nmatchAllForms, ref forward,
                        ref wrap, ref format, ref replaceWithText,
                        ref replace, ref matchKashida,
                        ref matchDiactitics, ref matchAlefHamza,
                        ref matchControl);
        }
        private void AddTable(Microsoft.Office.Interop.Word.Document aDoc, string bookmark, DataTable dt)
        {
            int iRowCount = dt.Rows.Count;
            int iColCount = dt.Columns.Count;
            object objMissing = System.Reflection.Missing.Value;
            // object objEndOfDoc = "\\endofdoc";            
            //   Range WordRange = aDoc.Bookmarks.get_Item(ref bookmark).Range;
            Range WordRange = getRange(aDoc.Range(), bookmark);
            Microsoft.Office.Interop.Word.Table wordTable;
            if (WordRange != null)
            {
                wordTable = aDoc.Tables.Add(WordRange, iRowCount, iColCount, ref objMissing, ref objMissing);
                int iTableRow = 1;
                int iTableCol = 1;
                for (int i = 0; i < dt.Columns.Count; i++)
                {
                    wordTable.Cell(iTableRow, iTableCol).Range.Text = dt.Columns[i].ColumnName;
                    iTableCol++;
                }
                iTableRow++;
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    iTableCol = 1;
                    for (int j = 0; j < dt.Columns.Count; j++)
                    {
                        wordTable.Cell(iTableRow, iTableCol).Range.Text = dt.Rows[i][j].ToString();
                        //wordApp.Write(dt.Rows[i][j].ToString());
                        iTableCol++;
                    }
                    iTableRow++;
                }
                wordTable.Borders.Enable = 1;
            }
            else MessageBox.Show(bookmark);
            //Wdc.SaveAs("E:\\test.docx");
        }
        public List<int> getRunningProcesses()
        {
            List<int> ProcessIDs = new List<int>();
            //here we're going to get a list of all running processes on
            //the computer
            foreach (Process clsProcess in Process.GetProcesses())
            {
                if (Process.GetCurrentProcess().Id == clsProcess.Id)
                    continue;
                if (clsProcess.ProcessName.Contains("WINWORD"))
                {
                    ProcessIDs.Add(clsProcess.Id);
                }
            }
            return ProcessIDs;
        }
        private void killProcesses(List<int> processesbeforegen, List<int> processesaftergen)
        {
            foreach (int pidafter in processesaftergen)
            {
                bool processfound = false;
                foreach (int pidbefore in processesbeforegen)
                {
                    if (pidafter == pidbefore)
                    {
                        processfound = true;
                    }
                }

                if (processfound == false)
                {
                    Process clsProcess = Process.GetProcessById(pidafter);
                    clsProcess.Kill();
                }
            }
        }
        private void CreateWordDocument(object filename, object savaAs)
        {
            // List<int> processesbeforegen = getRunningProcesses();
            object missing = Missing.Value;
            // string tempPath = null;
            Word.Application wordApp = new Word.Application();
            Word.Document aDoc = null;
            if (File.Exists((string)filename))
            {
                DateTime today = DateTime.Now;
                object readOnly = false; //default
                object isVisible = false;
                wordApp.Visible = false;
                aDoc = wordApp.Documents.Open(ref filename, ref missing, ref readOnly,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing,
                                            ref missing, ref missing, ref missing, ref missing);
                aDoc.Activate();
                //Find and replace:
                FindAndReplace(wordApp, "$yearReport", DateTime.Now.Year);
                User user = dataBase.GetUserById(UserId);
                Dictionary<string, string> codes = user.getCode();
                foreach (KeyValuePair<string, string> kvp in codes)
                    FindAndReplace(wordApp, kvp.Key, kvp.Value);
                //Report and Costumer
                Report report = dataBase.GetReport(ReportId);
                codes = report.getCode();
                foreach (KeyValuePair<string, string> kvp in codes)
                    FindAndReplace(wordApp, kvp.Key, kvp.Value);
                //Owners
                Owner owners = new Owner();
                owners.FizOwners = dataBase.GetHumanOwners(ReportId);
                owners.UrOwners = dataBase.GetCompaniesOwners(ReportId);
                if (owners.FizOwners.Count > 0 || owners.UrOwners.Count > 0)
                {
                    codes = owners.getCodes();
                    DataTable dt = new DataTable();
                    dt.Columns.Add("№");
                    dt.Columns.Add("Данные владельца");
                    foreach (KeyValuePair<string, string> kvp in codes)
                        dt.Rows.Add(kvp.Key, kvp.Value);
                    AddTable(aDoc, "$tableOwners", dt);
                }
                //Object
                RealEstate realEstate = dataBase.GetRealEstate(ReportId);
                List<string> podhods = null;
                if (realEstate != null)
                {
                    podhods = dataBase.GetPodhodsMetods(ReportId, realEstate.Id);
                    codes = realEstate.getCodes();
                    foreach (KeyValuePair<string, string> kvp in codes)
                        FindAndReplace(wordApp, kvp.Key, kvp.Value);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Характеристика");
                    dt.Columns.Add("Значение владельца");
                    foreach (Element e in realEstate.Elements)
                    {
                        if (string.IsNullOrEmpty(e.Unit))
                            dt.Rows.Add(e.Name, e.Value);
                        else dt.Rows.Add(e.Name + ", " + e.Unit, e.Value);
                    }
                    AddTable(aDoc, "$tableRE", dt);
                }
                //Documents
                List<Document> documents = dataBase.GetDocuments(ReportId);
                if (documents != null)
                {
                    var range = getRange(aDoc.Range(), "$documents");
                    if (range != null)
                    {
                        foreach (Document d in documents)
                        {
                            // var range = getRange(aDoc.Range(), "$documents");
                            // if (range != null)
                            //{
                            aDoc.Range(range.Start, range.End).Text = d.Name;
                            range.Start += d.Name.Length;
                          //  range.End += d.Name.Length;
                            foreach (string pathImage in d.ListPic)
                            {
                                /* string tempPath;
                                 using (Stream s = new FileStream(t, FileMode.Create, FileAccess.Write))
                                 {
                                     try
                                     {
                                         Image img = resizeImage(pathImage, new Size(200, 90));
                                         tempPath = System.Windows.Forms.Application.StartupPath + "\\Images\\~Temp\\temp.jpg";
                                         img.Save(s, System.Drawing.Imaging.ImageFormat.Png);
                                     }
                                     catch (Exception ex)
                                     {
                                         // обработка
                                     }
                                 }*/
                                if (File.Exists(pathImage))
                                {
                                    Object oMissed = aDoc.Range(range.Start, range.End); //the position you want to insert
                                    Object oLinkToFile = false;  //default
                                    Object oSaveWithDocument = true;//default
                                    aDoc.InlineShapes.AddPicture(pathImage, ref oLinkToFile, ref oSaveWithDocument, ref oMissed);
                                    range.Start++;
                                }
                                //  File.Delete(tempPath);
                            }
                            //}
                            // else break;
                        }
                    }
                   // FindAndReplace(wordApp, "$documents", "");
                }
                //Valution
                if (podhods != null && podhods.Count > 0 && podhods[0] == "сравнительный|метод сравнения продаж")
                {
                    List<RealEstate> realEstates = dataBase.GetRealEstates(ReportId);
                    DataTable dt = new DataTable();
                    dt.Columns.Add("Характеристика");
                    dt.Columns.Add("Оцениваемый объект");
                    for (int i = 1; i < realEstates.Count; i++)
                        dt.Columns.Add("Аналог " + i);
                    int n = dt.Columns.Count;
                    string[] items = new string[n];
                    items[0] = "Адрес";
                    for (int i = 1; i < n; i++)
                        items[i] = realEstates[i - 1].Adres;
                    dt.Rows.Add(items);
                    items[0] = "Регион";
                    for (int i = 1; i < n; i++)
                        items[i] = realEstates[i - 1].Region;
                    dt.Rows.Add(items);
                    items[0] = "Ссылка на источник";
                    for (int i = 1; i < n; i++)
                        items[i] = realEstates[i - 1].Link;
                    items[0] = "Телефон владельцев или риелторов";
                    for (int i = 1; i < n; i++)
                        items[i] = realEstates[i - 1].Telephone;
                    items[0] = "Цена, тыс.руб.";
                    items[1] = "-";
                    for (int i = 2; i < n; i++)
                        items[i] = realEstates[i - 1].Price.ToString();
                    items[0] = "Цена за один кв.м., тыс.руб.";
                    items[1] = "-";
                    for (int i = 2; i < n; i++)
                        items[i] = realEstates[i - 1].Elements[0].Value;
                    var range = getRange(aDoc.Range(), "$tableSravnValuationWithCorrect");
                    if (range != null)
                    {
                        int correct = 1;
                        foreach (Element e in realEstates[0].Elements)
                        {
                            if (string.IsNullOrEmpty(e.Unit))
                                items[0] = e.Name;
                            else items[0] = e.Name + ", " + e.Unit;
                            items[1] = e.Value;
                            if (e.Name != "Итог")
                                for (int i = 2; i < n; i++)
                                    items[i] = realEstates[i - 1].getValue(e.Name);
                            else
                                for (int i = 2; i < n; i++)
                                    items[i] = "";
                           // MessageBox.Show(items[0]);
                            dt.Rows.Add(items);
                            if (!e.Name.Contains("Итог"))
                            {
                                if (correct == realEstates[0].Elements.Count)
                                    dt.Rows.Add(items);
                                items[0] = "Корректировка " + correct+", %";
                                correct++;
                                items[1] = "-";
                                for (int i = 2; i < n; i++)
                                    items[i] = realEstates[i - 1].getCorrect(e.Name).ToString();
                                dt.Rows.Add(items);
                            }
                            else if(e.Name=="Итоговая корректировка")
                                dt.Rows.Add(items);
                        }
                        AddTable(aDoc, "$tableSravnValuationWithCorrect", dt);
                    }
                    else
                    {
                        range = getRange(aDoc.Range(), "$tableSravnValuationWithoutCorrect");
                        foreach (Element e in realEstates[0].Elements)
                        {
                            if (string.IsNullOrEmpty(e.Unit))
                                items[0] = e.Name;
                            else items[0] = e.Name + ", " + e.Unit;
                            items[1] = e.Value;
                            if (e.Name != "Итог")
                                for (int i = 2; i < n; i++)
                                    items[i] = realEstates[i - 1].getValue(e.Name);
                            else
                                for (int i = 2; i < n; i++)
                                    items[i] = "";
                            dt.Rows.Add(items);
                        }
                        AddTable(aDoc, "$tableSravnValuationWithoutCorrect", dt);
                        FindAndReplace(wordApp, "$tableSravnValuationWithoutCorrect", "");
                    }
                }
            }
            else
            {
                MessageBox.Show("Файл не существует", "Внимане", MessageBoxButtons.OK);
                return;
            }

            //Save as: filename
            aDoc.SaveAs2(ref savaAs, ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing,
                    ref missing, ref missing, ref missing);
            //Close Document:
            aDoc.Close(ref missing, ref missing, ref missing);
            // File.Delete(tempPath);
            if (MessageBox.Show("Отчет был создан. Открыть его?", "Создание отчета", MessageBoxButtons.YesNo) == DialogResult.Yes)
                Process.Start(savaAs.ToString());
            // List<int> processesaftergen = getRunningProcesses();
            //killProcesses(processesbeforegen, processesaftergen);
        }
        private static Image resizeImage(string filename, Size size)
        {
            Image imgToResize = Image.FromFile(@filename);
            int sourceWidth = imgToResize.Width;
            int sourceHeight = imgToResize.Height;

            float nPercent = 0;
            float nPercentW = 0;
            float nPercentH = 0;

            nPercentW = ((float)size.Width / (float)sourceWidth);
            nPercentH = ((float)size.Height / (float)sourceHeight);

            if (nPercentH < nPercentW)
                nPercent = nPercentH;
            else
                nPercent = nPercentW;

            int destWidth = (int)(sourceWidth * nPercent);
            int destHeight = (int)(sourceHeight * nPercent);

            Bitmap b = new Bitmap(destWidth, destHeight);
            Graphics g = Graphics.FromImage((Image)b);
            g.InterpolationMode = InterpolationMode.HighQualityBicubic;

            g.DrawImage(imgToResize, 0, 0, destWidth, destHeight);
            g.Dispose();

            return (Image)b;
        }
        public static Range getRange(Range range, string findText)
        {
            int start = range.Start;
            int end = range.End;
            bool isOk = range.Find.Execute(FindText: findText, MatchCase: true);
            if (isOk)
            {
                var newRange = range.Document.Range(range.Start, range.End);
                range.SetRange(start, end);
                return newRange;
            }
            else
                return null;
        }
    }
}
