using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Teigha.DatabaseServices;
using Teigha.Geometry;
using Teigha.Runtime;

namespace WindowsFormsApplication1
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (Services svcs = new Services())
            {
                Database db = new Database();
                Circle cirl = new Circle();
                cirl.Center = new Point3d(100, 100, 0);
                cirl.Radius = 14;
                using (BlockTable bt=db.BlockTableId.GetObject(OpenMode.ForRead) as BlockTable)
                {
                    BlockTableRecord btr = bt[BlockTableRecord.ModelSpace].GetObject(OpenMode.ForWrite) as BlockTableRecord;
                    btr.AppendEntity(cirl);
                }
                db.SaveAs(@"C:\Users\Administrator\Desktop\test.dwg", DwgVersion.Current);
                MessageBox.Show("输出成功");
            }
        }
    }
}
