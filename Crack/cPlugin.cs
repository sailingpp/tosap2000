using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SAP2000v15;
namespace Crack
{
    // enable class interface for COM
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class cPlugin
    {
         // main method for sap2000 plugin
        public void Main(ref SAP2000v15.cSapModel SapModel, ref SAP2000v15.cSapPlugin ISapPlugin)
        {
            GetInfo(SapModel);
            ISapPlugin.Finish(0);
        }

        public  void GetInfo(SAP2000v15.cSapModel SapModel)
        {
            int Num = 0;
            int[] objTypes = null;
            string[] objNames = null;
            SapModel.SelectObj.GetSelected(ref Num, ref objTypes, ref objNames);
            MessageBox.Show(string.Format("Selected {0} objects.", Num));
            foreach (var item in objNames)
            {
                MessageBox.Show("你选择的构件编号为：" + item);
            }
            int ret;
            int NumberResults = 0;
            string[] Obj = null;
            string[] Elm = null;
            string[] LoadCase = null;
            string[] StepType = null;
            double[] ObjSta = null;
            double[] ElmSta = null;
            double[] StepNum = null;
            double[] M3 = null;
            double[] M2 = null;
            double[] V2 = null;
            double[] V3 = null;
            double[] P = null;
            double[] T = null;
            Dictionary<string, List<double>> mydic = new Dictionary<string, List<double>>();

            string FileName=null;
            string MatProp=null;
            double t3=0.0;
            double t2=0.0;
            int color=0;
            string notes=null;
            string guid=null;
            SapModel.PropFrame.GetRectangle("b1x0.3", ref FileName, ref MatProp, ref t3, ref t2, ref color, ref notes, ref guid);
            foreach (var item in objNames)
            {
                for (int i = 0; i < 1; i++)
                {
                    List<double> sapResList = new List<double>();
                    ret = SapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                    ret = SapModel.Results.Setup.SetCaseSelectedForOutput(System.Convert.ToString(i + 1), System.Convert.ToBoolean(-1));
                    ret = SapModel.Results.FrameForce(item, SAP2000v15.eItemTypeElm.ObjectElm, ref NumberResults, ref Obj, ref ObjSta, ref Elm, ref ElmSta, ref LoadCase, ref StepType, ref  StepNum, ref P, ref V2, ref V3, ref T, ref M2, ref M3);
                    sapResList.AddRange(M3);
                    mydic.Add(item, sapResList);
                }
            }
            double temp=0;
            foreach (KeyValuePair<string, List<double>> item in mydic)
            {
                double moment = Math.Max(Math.Abs(item.Value.Max()), Math.Abs(item.Value.Min()));
                temp = moment;
                MessageBox.Show("构件编号：" + item.Key + ",最大弯矩：" + Math.Round(item.Value.Max(), 3) + ",最小弯矩：" + Math.Round(item.Value.Min(), 3));
            }

            FormMain form = new FormMain();
            form.PassText(t2*1000,t3*1000,temp*1000000);
            form.Show();

        }

    }
}
