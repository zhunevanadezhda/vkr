using System;
using System.Collections.Generic;

namespace Diplom
{
    class Report
    {
        private int id;
        private DateTime dateInspection;
        private DateTime dateValutaion;
        private DateTime dateReport;
        private string basedOn;
        private string goal;
        private string limitation;
        private bool isReady;
        private string typeObject;//квартира, дом, дом с участком, участок
        //private bool isCostumer;//0-физ, 1-юрид не нужно
        //private Object costumer; не рентабельно
        //private List<RealEstate> estates; использовать потом
        private Costumer costumer;
        //private Owner owners;
       // private string creator;

        /*public Report(int id, DateTime dateInspection, DateTime dateValutaion, DateTime dateReport, string basedOn, string goal, string limitation, bool isReady, string typeObject, string creator)
        {
            this.id = id;
            this.dateInspection = dateInspection;
            this.dateValutaion = dateValutaion;
            this.dateReport = dateReport;
            this.basedOn = basedOn;
            this.goal = goal;
            this.limitation = limitation;
            this.isReady = isReady;
            this.typeObject = typeObject;
            this.creator = creator;
        }
        public Report(int id, DateTime dateInspection, DateTime dateValutaion, DateTime dateReport, string basedOn, string goal, string limitation, bool isReady, string typeObject, Costumer costumer, string creator)
        {
            this.id = id;
            this.dateInspection = dateInspection;
            this.dateValutaion = dateValutaion;
            this.dateReport = dateReport;
            this.basedOn = basedOn;
            this.goal = goal;
            this.limitation = limitation;
            this.isReady = isReady;
            this.costumer = costumer;
            this.typeObject = typeObject;
            this.creator = creator;
        }*/
        public Report(int id, DateTime dateInspection, DateTime dateValutaion, DateTime dateReport, string basedOn, string goal, string limitation, bool isReady,string typeObject)
        {
            this.id = id;
            this.dateInspection = dateInspection;
            this.dateValutaion = dateValutaion;
            this.dateReport = dateReport;
            this.basedOn = basedOn;
            this.goal = goal;
            this.limitation = limitation;
            this.isReady = isReady;
            this.typeObject = typeObject;
        }
        public Report(DateTime dateInspection, DateTime dateValutaion, DateTime dateReport, string basedOn, string goal, string limitation, bool isReady, string typeObject,Costumer costumer)
        {
            this.dateInspection = dateInspection;
            this.dateValutaion = dateValutaion;
            this.dateReport = dateReport;
            this.basedOn = basedOn;
            this.goal = goal;
            this.limitation = limitation;
            this.isReady = isReady;
            this.typeObject = typeObject;
            this.costumer = costumer;
        }
        public Report(int id, DateTime dateInspection, DateTime dateValutaion, DateTime dateReport, string basedOn, string goal, string limitation, bool isReady, string typeObject, Costumer costumer) : this(id, dateInspection, dateValutaion, dateReport, basedOn, goal, limitation, isReady, typeObject)
        {
            this.costumer = costumer;
        }
        /*public Report(int id, DateTime dateInspection, DateTime dateValutaion, DateTime dateReport, string basedOn, string goal, string limitation, bool isReady, Costumer costumer, List<RealEstate> estates, string creator)
        {
            this.id = id;
            this.dateInspection = dateInspection;
            this.dateValutaion = dateValutaion;
            this.dateReport = dateReport;
            this.basedOn = basedOn;
            this.goal = goal;
            this.limitation = limitation;
            this.isReady = isReady;
            this.estates = estates;
            this.creator = creator;
        }*/   
        public Report()
        {
        }
        public Report(int id)
        {
            this.id = id;
        }

        public int Id { get => id; set => id = value; }
        public DateTime DateInspection { get => dateInspection; set => dateInspection = value; }
        public DateTime DateValutaion { get => dateValutaion; set => dateValutaion = value; }
        public DateTime DateReport { get => dateReport; set => dateReport = value; }
        public string BasedOn { get => basedOn; set => basedOn = value; }
        public string Goal { get => goal; set => goal = value; }
        public string Limitation { get => limitation; set => limitation = value; }
        public bool IsReady { get => isReady; set => isReady = value; }
        public Costumer Costumer { get => costumer; set => costumer = value; }
        //internal List<RealEstate> Estates { get => estates; set => estates = value; }
       // public string Creator { get => creator; set => creator = value; }
        public string TypeObject { get => typeObject; set => typeObject = value; }

        public Dictionary<string,string> getCode()
        {
            Dictionary<string, string> codes = new Dictionary<string, string>();
            codes.Add("$dateInspection", dateInspection.ToString());
            codes.Add("$dateValutaion", dateValutaion.ToString());
            codes.Add("$dateReport", dateReport.ToString());
            codes.Add("$basedOn", basedOn);
            codes.Add("$goal", goal);
            codes.Add("$limitation", limitation);
            codes.Add("$typeObject", typeObject);
            if (costumer!=null)
            {
                if (Costumer.Type)//ur
                {
                    codes.Add("$nameCostumer", Costumer.Ur.Name);
                    codes.Add("$adresCostumer", Costumer.Ur.Adres);
                    codes.Add("$ogrnCostumer", Costumer.Ur.OGRN);
                    codes.Add("$dateOGRNCostumer", Costumer.Ur.DateOGRN.ToString());
                    codes.Add("$formCostumer", Costumer.Ur.Form);
                    codes.Add("$emailCostumer", Costumer.Ur.Email);
                    codes.Add("$telehoneCostumer", Costumer.Ur.Telephone);
                    codes.Add("$Costumer", Costumer.Ur.Form+" \""+Costumer.Ur.Name+"\"");
                }
                else
                {
                    codes.Add("$nameCostumer", Costumer.Fiz.Name);
                    codes.Add("$surnameCostumer", Costumer.Fiz.Surname);
                    codes.Add("$patronymCostumer", Costumer.Fiz.Patronym);
                    codes.Add("$telephoneCostumer", Costumer.Fiz.Telephone);
                    codes.Add("$pasportNumberCostumer", Costumer.Fiz.PasportNumber);
                    codes.Add("$pasportDateCostumer", Costumer.Fiz.PasportDate.ToString());
                    codes.Add("$pasportWhereCostumer", Costumer.Fiz.PasportWhere);
                    codes.Add("$Costumer", Costumer.Fiz.Surname + " " + Costumer.Fiz.Name + " " + Costumer.Fiz.Patronym);
                }
            }
            return codes;
        }
        //internal Owner Owners { get => owners; set => owners = value; }
    }
}
