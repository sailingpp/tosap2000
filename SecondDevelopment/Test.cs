using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Autodesk.AutoCAD.ApplicationServices;
using Autodesk.AutoCAD.Colors;
using Autodesk.AutoCAD.DatabaseServices;
using Autodesk.AutoCAD.EditorInput;
using Autodesk.AutoCAD.Geometry;
using Autodesk.AutoCAD.Runtime;
using Autodesk.AutoCAD.Windows;
using AcadApp = Autodesk.AutoCAD.ApplicationServices.Application;

namespace SecondDevelopment
{
    public partial class Test : Form
    {
        public Test()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            panel1.Visible = !panel1.Visible;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            panel2.Visible = !panel2.Visible;
        }

        private void button3_Click(object sender, EventArgs e)
        {
            panel3.Visible = !panel3.Visible;
        }

        private void button4_Click(object sender, EventArgs e)
        {
            panel4.Visible = !panel4.Visible;
        }

        private void button5_Click(object sender, EventArgs e)
        {
            Document doc = Autodesk.AutoCAD.ApplicationServices.Application.DocumentManager.MdiActiveDocument;
            Database db = doc.Database;
            Circle cirl = new Circle();
            cirl.Center = new Point3d(100, 100, 0);
            using (Transaction trans = db.TransactionManager.StartTransaction())
            {
                BlockTable bt = trans.GetObject(db.BlockTableId, OpenMode.ForRead) as BlockTable;
                BlockTableRecord btr = trans.GetObject(bt[BlockTableRecord.ModelSpace], OpenMode.ForWrite) as BlockTableRecord;
                btr.AppendEntity(cirl);
                trans.Commit();
            }
            db.SaveAs(@"E:\sapmodel\demo\test.dwg", DwgVersion.Current);
        }

        private void button6_Click(object sender, EventArgs e)
        {
            int ret;
            //(@"E:\sapmodel\demo\API_1-tube001.sdb");
            SAP2000v15.SapObject mySapObject = new SAP2000v15.SapObject();
           
            SAP2000v15.cSapModel mySapModel = new SAP2000v15.cSapModel();
            int n = mySapModel.File.OpenFile(@"E:\sapmodel\demo\API_1-tube001.sdb");
            MessageBox.Show(mySapModel.FrameObj.GetSelected("1", true).ToString());//选不到
            MessageBox.Show(mySapModel.FrameObj.GetSelected("0", true).ToString());
            MessageBox.Show(mySapModel.FrameObj.GetSelected("b1x0.3", true).ToString());
            MessageBox.Show( mySapModel.GetDatabaseUnits().ToString());
       
            MessageBox.Show(mySapModel.FrameObj.Count().ToString());
            int el=1;
             string[] elm=new  string[10];
             double[] rdi=new  double[10];
            double[] rdj=new double[10];
            int[] objint=new  int[10];
            string[] objname=new string[10];
            mySapModel.SelectObj.GetSelected(ref el,ref objint,ref objname);
            mySapModel.FrameObj.GetElm("b1x0.3", ref el, ref elm, ref rdi, ref rdj);
            foreach (var item in objname)
            {
                MessageBox.Show(item);
            }
            
            string[] names = new string[10];
            int t=10;
            mySapModel.FrameObj.GetNameList(ref t,  ref names);
  
            string[] FrameName=new string[2];
            int NumberResults = 2;
            string[] Obj = new string[1];
            string[] Elm = new string[1];
            string[] LoadCase = new string[1];
            string[] StepType = new string[1];
            double[] ObjSta = new double[1]; ;
            double[] ElmSta = new double[1]; ;
            double[] StepNum = new double[1];
            double[] M3 = new double[1];
            double[] M2 = new double[1];
            double[] V2 = new double[1];
            double[] V3 = new double[1];
            double[] P = new double[1];
            double[] T = new double[1];
            List<double> sapResList = new List<double>();
            ret = mySapModel.FrameObj.GetSelected(FrameName[0], true);
            bool ft=true;
            int m= mySapModel.FrameObj.GetSelected("0",ref ft);
            string path = mySapModel.GetModelFilename(true);
            for (int i = 0; i <= 6; i++)
            {
                ret = mySapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                ret = mySapModel.Results.Setup.SetCaseSelectedForOutput(System.Convert.ToString(i + 1), System.Convert.ToBoolean(-1));
                ret = mySapModel.Results.FrameForce("1", SAP2000v15.eItemTypeElm.ObjectElm, ref NumberResults, ref Obj, ref ObjSta, ref Elm, ref ElmSta, ref LoadCase, ref StepType, ref  StepNum, ref P, ref V2, ref V3, ref T, ref M2, ref M3);
                sapResList.AddRange(M3);
            }
            string res = null;
            for (int i = 0; i < sapResList.Count; i++)
            {
                res = res + string.Format("{0:0.00000}", sapResList[i]) + "\r\n";
            }
            MessageBox.Show(n.ToString());
            MessageBox.Show(res);
            MessageBox.Show(path);
            for (int i = 0; i < names.Length; i++)
            {
                MessageBox.Show(names[i].ToString());
            }
          
        }
    }
}
