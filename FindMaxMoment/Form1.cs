using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FindMaxMoment
{
    public delegate void myDel(List<ConcreteFrame> frameList);
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            myDel del = SetConcreteList;
        }
        public void SetConcreteList(List<ConcreteFrame> frameList)
        {
            dataGridView1.DataSource = frameList;
        }
    }
}
