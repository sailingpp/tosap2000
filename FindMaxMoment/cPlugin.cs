using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SAP2000v15;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace FindMaxMoment
{
    // enable class interface for COM
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class cPlugin
    {
        List<ConcreteFrame> frameList = new List<ConcreteFrame>();
        public void Main(ref SAP2000v15.cSapModel SapModel, ref SAP2000v15.cSapPlugin ISapPlugin)
        {
            GetMax(SapModel);
          
            GetFrameInfomation(SapModel);
            Form1 form = new Form1();
            form.SetConcreteList(frameList);
            form.Show();
            //foreach (var item in frameList)
            //{
            //    foreach (var jtem in item.MaxMomentList)
            //    {
            //        MessageBox.Show("构件" + item.Name + ",maxMoment" + jtem);
            //    }
            //}
            ISapPlugin.Finish(0);
        }
        public void GetMax(SAP2000v15.cSapModel SapModel)
        {
            int Num = 0;
            int[] objTypes = null;
            string[] objNames = null;
            SapModel.SelectObj.GetSelected(ref Num, ref objTypes, ref objNames);
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
            int loadcaseNumber = 0;
            string[] loadCase = null;
            SapModel.LoadCases.GetNameList(ref loadcaseNumber, ref loadCase, 0);
            foreach (var item in objNames)
            {
                foreach (var ktem in loadCase)
                {
                    Dictionary<string, List<double>> mydic = new Dictionary<string, List<double>>();
                    List<double> sapResList = new List<double>();
                    SapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                    SapModel.Results.Setup.SetCaseSelectedForOutput(ktem, true);
                    SapModel.Results.FrameForce(item, SAP2000v15.eItemTypeElm.ObjectElm, ref NumberResults, ref Obj, ref ObjSta, ref Elm, ref ElmSta, ref LoadCase, ref StepType, ref  StepNum, ref P, ref V2, ref V3, ref T, ref M2, ref M3);
                    sapResList.AddRange(M3);
                    mydic.Add(item, sapResList);
                    foreach (KeyValuePair<string, List<double>> jtem in mydic)
                    {
                        MessageBox.Show("工况:" + ktem + ",构件编号：" + jtem.Key + ",最大弯矩：" + Math.Round(jtem.Value.Max(), 3) + ",最小弯矩：" + Math.Round(jtem.Value.Min(), 3));
                    }
                }

            }

        }
        public void GetFrameInfomation(SAP2000v15.cSapModel SapModel)
        {
            int frameNameNumber = 0;
            string[] frameNames = null;
            SapModel.FrameObj.GetNameList(ref frameNameNumber, ref frameNames);
            
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
            int loadcaseNumber = 0;
            string[] loadCase = null;
            SapModel.LoadCases.GetNameList(ref loadcaseNumber, ref loadCase, 0);
            foreach (var item in frameNames)
            {
                ConcreteFrame frame = new ConcreteFrame();
                string proname = null;
                string sauto = null;
                SapModel.FrameObj.GetSection(item, ref proname, ref sauto);
                string matname=null;
                string filename = null;
                double t3=0;
                double t2=0;
                int color=0;
                string notes = null;
                string guid = null;
                SapModel.PropFrame.GetRectangle(proname,ref filename, ref matname, ref t3, ref t2, ref color, ref notes, ref guid);
                frame.B = t2;
                frame.H = t3;
                frame.ConcreteLabel = matname;
                frame.Name = item;

                foreach (var ktem in loadCase)
                {
                    Dictionary<string, List<double>> mydic = new Dictionary<string, List<double>>();
                    List<double> sapResList = new List<double>();
                    SapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                    SapModel.Results.Setup.SetCaseSelectedForOutput(ktem, true);
                    SapModel.Results.FrameForce(item, SAP2000v15.eItemTypeElm.ObjectElm, ref NumberResults, ref Obj, ref ObjSta, ref Elm, ref ElmSta, ref LoadCase, ref StepType, ref  StepNum, ref P, ref V2, ref V3, ref T, ref M2, ref M3);
                    sapResList.AddRange(M3);
                    mydic.Add(item, sapResList);
                    foreach (KeyValuePair<string, List<double>> jtem in mydic)
                    {
                        frame.MaxMomentList.Add(jtem.Value.Max());
                        frame.MinMomentList.Add(jtem.Value.Min()); 
                    }
                }
                frameList.Add(frame);
            }
           
            
            
        }

    }
}
