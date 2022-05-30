using System;
using System.Windows.Forms;

namespace Diplom
{
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            SignIn singIn = new SignIn();
            Application.Run(singIn);
            int id = singIn.Autorization;
            if (id>0)
            {
                Application.Run(new Form1(id));
            }
            //Application.Run(new Form1(1));
            /*  List<string> Metods = new List<string>();
              Metods.Add("сравнительный|метод сравнения продаж");
              //Metods.Add("доходный|метод капитализации");
              Valuation form = new Valuation(9, "участок", Metods, null);
              Application.Run(form);*/
            //SampleForm form = new SampleForm(1,9);
            //Application.Run(form);
          //  ReportForm form = new ReportForm(1, 9, "участок");
            //Application.Run(form);
            // Application.Run(new Reference());
        }
    }
}
