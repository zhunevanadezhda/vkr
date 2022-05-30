using System;
using System.Windows.Forms;

namespace Diplom
{
    public partial class TypeObjectForm : Form
    {
        private string typeObject;

        public string TypeObject { get => typeObject; set => typeObject = value; }

        public TypeObjectForm()
        {
            InitializeComponent();
            cancel_bt.FlatStyle = FlatStyle.Popup;
            flat_pb.ImageLocation = ".//pictures//flat.png";
            house_pb.ImageLocation = ".//pictures//house.png";
            houseWithPlot_pb.ImageLocation = ".//pictures//houseWithPlot.png";
            plot_pb.ImageLocation = ".//pictures//plot2.png";
        }
        
        private void cancel_bt_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }

        private void flat_pb_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            typeObject = "квартира";
            this.Close();
        }

        private void house_pb_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            typeObject = "дом";
            this.Close();
        }

        private void houseWithPlot_pb_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            typeObject = "дом с участком";
            this.Close();
        }

        private void plot_pb_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
            typeObject = "участок";
            this.Close();
        }
    }
}
