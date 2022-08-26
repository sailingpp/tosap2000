using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SAP2000v15;

namespace demotest
{
    // enable class interface for COM
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class cPlugin
    {
        // main method for sap2000 plugin
        public void Main(ref SAP2000v15.cSapModel SapModel, ref SAP2000v15.cSapPlugin ISapPlugin)
        {
            int Num = 0;
            int[] objTypes = null;
            string[] objNames = null;
            SapModel.SelectObj.GetSelected(ref Num, ref objTypes, ref objNames);
            MessageBox.Show(string.Format("Selected {0} objects.", Num));
            foreach (var item in objNames)
            {
                MessageBox.Show("你选择的构件编号为："+item);
            }
            int ret;
            MessageBox.Show("本项目单位制为："+SapModel.GetDatabaseUnits().ToString());
            MessageBox.Show("本项目总共有构件："+SapModel.FrameObj.Count().ToString());
            string[] names = null;
            int t=0 ;
            SapModel.FrameObj.GetNameList(ref t, ref names);//获取所有构件的编号
            string path = SapModel.GetModelFilename(true);//获取文件

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
 
            for (int i = 0; i < 1; i++)
            {
                List<double> sapResList = new List<double>();
                ret = SapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                ret = SapModel.Results.Setup.SetCaseSelectedForOutput(System.Convert.ToString(i + 1), System.Convert.ToBoolean(-1));
                foreach (var item in names)
                {
                    ret = SapModel.Results.FrameForce(item, SAP2000v15.eItemTypeElm.ObjectElm, ref NumberResults, ref Obj, ref ObjSta, ref Elm, ref ElmSta, ref LoadCase, ref StepType, ref  StepNum, ref P, ref V2, ref V3, ref T, ref M2, ref M3);
                    sapResList.AddRange(M3);
                    mydic.Add(item, sapResList);
                }
            }
            //string res = null;
            //for (int i = 0; i < sapResList.Count; i++)
            //{
            //    res = res + string.Format("{0:0.00000}", sapResList[i]) + "\r\n";
            //}
            //MessageBox.Show(res);
            //MessageBox.Show(sapResList.Max().ToString());
            foreach (KeyValuePair<string, List<double>> item in mydic)
            {
                MessageBox.Show("构件编号："+item.Key+",最大弯矩：" + item.Value.Max());
            }
            ISapPlugin.Finish(0);
        }
    }
}
