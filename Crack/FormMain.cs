using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Crack
{
    public delegate void DelPassText(double b,double h,double moment);
    public partial class FormMain : Form
    {
        public FormMain()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Concrete concrete = new Concrete();
            concrete.Ftk = Convert.ToDouble(textBoxFtk.Text);
            concrete.Cs = Convert.ToDouble(textBoxCover.Text);
            concrete.N1 = Convert.ToInt32(textBoxN1.Text);
            concrete.N2 = Convert.ToInt32(textBoxN2.Text);
            concrete.Sd1 = Convert.ToInt32(textBoxSD1.Text);
            concrete.Sd2 = Convert.ToInt32(textBoxSD2.Text);
            concrete.B = Convert.ToDouble(textBoxB.Text);
            concrete.H = Convert.ToDouble(textBoxH.Text);
            concrete.Cas = Convert.ToDouble(textBoxCas.Text);
            double Es =Convert.ToDouble(textBoxEs.Text);
            concrete.Moment = Convert.ToDouble(textBoxMoment.Text);
            double w = concrete.CalWma(1.9, Es);
            textBoxCrack.Text = Math.Round(w, 4).ToString();

        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            DelPassText delpassb=PassText;
        }
        public void PassText(double b,double h,double moment)
        {
            textBoxB.Text = b.ToString();
            textBoxH.Text = h.ToString();
            textBoxMoment.Text = moment.ToString();
        }
    }
}
