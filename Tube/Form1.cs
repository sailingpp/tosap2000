using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using SAP2000v15;

namespace Tube
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void btnDulTubeToSap2000_Click(object sender, EventArgs e)
        {
            #region 初始化数据
            string path = textBoxPath.Text;
            double a = Convert.ToDouble(textBoxA.Text);//侧壁厚度
            double b = Convert.ToDouble(textBoxB.Text);//中板壁厚
            double b1 = Convert.ToDouble(textBoxB1.Text);//左舱长度
            double b2 = Convert.ToDouble(textBoxB2.Text);//右舱长度
            double c = Convert.ToDouble(textBoxC.Text);//顶板厚度
            double d = Convert.ToDouble(textBoxD.Text);//底板厚度
            double h = Convert.ToDouble(textBoxH.Text);//高度
            double futuh = Convert.ToDouble(textBoxFuTuH.Text);//覆土高度
            double vel = Convert.ToDouble(textBoxVel.Text);
            string concrete = comboBoxCon.Text;
            RecDulTube tube = new RecDulTube(a, b, b1, b2, c, d, h, vel, futuh);
            #endregion

            #region sap2000过程
            //dimension variables
            #region 1定义变量
            SAP2000v15.SapObject mySapObject;
            SAP2000v15.cSapModel mySapModel;
            int ret;
            double[] ModValue;
            bool[] Restraint;//定义约束
            string[] FrameName;//框架编号
            string[] PointName;//节点点编号

            string temp_string1;//定义中间变量
            string temp_string2;//定义中间变量
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
            #endregion

            #region 2初始化sap2000模型
            //create Sap2000 object

            mySapObject = new SAP2000v15.SapObject();//创建一个sap2000模型

            //start Sap2000 application开启应用程序

            mySapObject.ApplicationStart(SAP2000v15.eUnits.kN_m_C, true, "");//创建一个新的模型

            //create SapModel object

            mySapModel = mySapObject.SapModel;

            //initialize model

            ret = mySapModel.InitializeNewModel((SAP2000v15.eUnits.kN_m_C));//初始化模型

            //create new blank model

            ret = mySapModel.File.NewBlank();//创建新的空模型
            #endregion

            #region 3定义材料属性
            //define material property 创建材料属性

            ret = mySapModel.PropMaterial.SetMaterial("CONC", SAP2000v15.eMatType.MATERIAL_CONCRETE, -1, "", "");

            //assign isotropic mechanical properties to material

            ret = mySapModel.PropMaterial.SetMPIsotropic("CONC", 3600, 0.2, 0.0000055, 0);
            #endregion

            #region 4定义截面属性
            //define rectangular frame section property 定义截面属性

            ret = mySapModel.PropFrame.SetRectangle("b1x" + tube.WallThick, "CONC", tube.WallThick, 1, -1, "", "");
            ret = mySapModel.PropFrame.SetRectangle("b1x" + tube.MiddelWallThick, "CONC", tube.MiddelWallThick, 1, -1, "", "");
            ret = mySapModel.PropFrame.SetRectangle("b1x" + tube.TopThick, "CONC", tube.TopThick, 1, -1, "", "");
            ret = mySapModel.PropFrame.SetRectangle("b1x" + tube.BotThick, "CONC", tube.BotThick, 1, -1, "", "");

            //define frame section property modifiers定义截面修正

            ModValue = new double[8];

            for (int i = 0; i <= 7; i++) { ModValue[i] = 1; }

            ModValue[0] = 1000;

            ModValue[1] = 0;

            ModValue[2] = 0;

            double[] temp_SystemArray = ModValue;

            ret = mySapModel.PropFrame.SetModifiers("b1x" + tube.WallThick, ref temp_SystemArray);

            //switch to k-ft units 切换单位

            ret = mySapModel.SetPresentUnits(SAP2000v15.eUnits.kN_m_C);
            #endregion

            #region 5add frame object by coordinates 按坐标添加构件

            FrameName = new string[7];//新建7个构件的编号名

            temp_string1 = FrameName[0];

            temp_string2 = FrameName[0];

            ret = mySapModel.FrameObj.AddByCoord(0, 0, 0, 0, 0, tube.Height, ref temp_string1, "b1x" + tube.WallThick, "1", "Global");

            FrameName[0] = temp_string1;

            ret = mySapModel.FrameObj.AddByCoord(tube.LeftLength, 0, 0, tube.LeftLength, 0, tube.Height, ref temp_string1, "b1x" + tube.MiddelWallThick, "2", "Global");

            FrameName[1] = temp_string1;

            ret = mySapModel.FrameObj.AddByCoord(tube.LeftLength + tube.RightLength, 0, 0, tube.LeftLength + tube.RightLength, 0, tube.Height, ref temp_string1, "b1x" + tube.WallThick, "3", "Global");

            FrameName[2] = temp_string1;

            ret = mySapModel.FrameObj.AddByCoord(0, 0, tube.Height, tube.LeftLength, 0, tube.Height, ref temp_string1, "b1x" + tube.TopThick, "4", "Global");

            FrameName[3] = temp_string1;

            ret = mySapModel.FrameObj.AddByCoord(0, 0, 0, tube.LeftLength, 0, 0, ref temp_string1, "b1x" + tube.BotThick, "5", "Global");

            FrameName[4] = temp_string1;

            ret = mySapModel.FrameObj.AddByCoord(tube.LeftLength, 0, 0, tube.LeftLength + tube.RightLength, 0, 0, ref temp_string1, "b1x" + tube.BotThick, "6", "Global");

            FrameName[5] = temp_string1;

            ret = mySapModel.FrameObj.AddByCoord(tube.LeftLength, 0, tube.Height, tube.LeftLength + tube.RightLength, 0, tube.Height, ref temp_string1, "b1x" + tube.TopThick, "7", "Global");

            FrameName[6] = temp_string1;
            #endregion

            #region 6设置边界条件
            //assign point object restraint at base 设置底部的固定约束

            PointName = new string[6];

            Restraint = new bool[6];
            //限制U1,U2,U3
            for (int i = 0; i <= 2; i++) { Restraint[i] = true; }
            //限制R1,R2,R3
            for (int i = 3; i <= 5; i++) { Restraint[i] = true; }

            ret = mySapModel.FrameObj.GetPoints(FrameName[0], ref temp_string1, ref temp_string2);
            PointName[0] = temp_string1;
            PointName[1] = temp_string2;
            ret = mySapModel.PointObj.SetRestraint(PointName[0], ref Restraint, 0);

            ret = mySapModel.FrameObj.GetPoints(FrameName[1], ref temp_string1, ref temp_string2);
            PointName[2] = temp_string1;
            PointName[3] = temp_string2;
            ret = mySapModel.PointObj.SetRestraint(PointName[2], ref Restraint, 0);

            ret = mySapModel.FrameObj.GetPoints(FrameName[2], ref temp_string1, ref temp_string2);//设置3号构件的底点固接
            PointName[4] = temp_string1;
            PointName[5] = temp_string2;
            ret = mySapModel.PointObj.SetRestraint(PointName[4], ref Restraint, 0);

            //assign point object restraint at top 设置顶部的铰接约束

            for (int i = 0; i <= 2; i++) { Restraint[i] = true; }

            for (int i = 3; i <= 5; i++) { Restraint[i] = false; }

            ret = mySapModel.FrameObj.GetPoints(FrameName[0], ref temp_string1, ref temp_string2);
            PointName[0] = temp_string1;
            PointName[1] = temp_string2;
            ret = mySapModel.PointObj.SetRestraint(PointName[1], ref Restraint, 0);

            ret = mySapModel.FrameObj.GetPoints(FrameName[1], ref temp_string1, ref temp_string2);
            PointName[2] = temp_string1;
            PointName[3] = temp_string2;
            ret = mySapModel.PointObj.SetRestraint(PointName[3], ref Restraint, 0);

            ret = mySapModel.FrameObj.GetPoints(FrameName[2], ref temp_string1, ref temp_string2);//设置3号构件的顶点铰接
            PointName[4] = temp_string1;
            PointName[5] = temp_string2;
            ret = mySapModel.PointObj.SetRestraint(PointName[5], ref Restraint, 0);
            #endregion

            #region 7设置自动框架细分及指定拉压弹簧
            double[] vec = new double[] { 0, 0, 1 };
            for (int f = 0; f < 7; f++)
            {
                ret = mySapModel.FrameObj.SetAutoMesh(FrameName[f], true, true, true, 10, 0.1, 0);
            }
            //设置线弹簧 参数4： 1是拉压弹簧，2是受压弹簧，3是受拉弹簧
            //设置线弹簧 参数7： 弹簧的方向
            ret = mySapModel.FrameObj.SetSpring(FrameName[0], 1, 1, 2, "Simple", 1, 2, 0, ref  vec, 0, true, "Local", 0);
            ret = mySapModel.FrameObj.SetSpring(FrameName[2], 1, 1, 2, "Simple", 1, -2, 0, ref  vec, 0, true, "Local", 0);
            ret = mySapModel.FrameObj.SetSpring(FrameName[4], 1, 1, 2, "Simple", 1, 2, 0, ref  vec, 0, true, "Local", 0);
            ret = mySapModel.FrameObj.SetSpring(FrameName[5], 1, 1, 2, "Simple", 1, 2, 0, ref  vec, 0, true, "Local", 0);
            #endregion

            ret = mySapModel.View.RefreshView(0, false);   //refresh view, update (initialize) zoom 刷新界面

            #region 8添加荷载模式
            //add load patterns 添加荷载模式
            ret = mySapModel.LoadPatterns.Add("1", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, true);
            ret = mySapModel.LoadPatterns.Add("2", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, true);
            ret = mySapModel.LoadPatterns.Add("3", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, true);
            #endregion

            //assign loading for load pattern 
            #region 9设置均布荷载
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[0], "1", 1, 2, 0, 1, tube.VelWallBotLoad, tube.VelWallTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[2], "1", 1, 2, 0, 1, -tube.VelWallBotLoad, -tube.VelWallTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[3], "1", 1, 2, 0, 1, -tube.VelPlateTopLoad, -tube.VelPlateTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[6], "1", 1, 2, 0, 1, -tube.VelPlateTopLoad, -tube.VelPlateTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[0], "2", 1, 2, 0, 1, tube.SoilWallBotLoad, tube.SoilWallTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[2], "2", 1, 2, 0, 1, -tube.SoilWallBotLoad, -tube.SoilWallTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[3], "2", 1, 2, 0, 1, -tube.SoilPlateTop, -tube.SoilPlateTop, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[6], "2", 1, 2, 0, 1, -tube.SoilPlateTop, -tube.SoilPlateTop, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[0], "3", 1, 2, 0, 1, tube.WaterWallBotLoad, tube.WaterWallTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[2], "3", 1, 2, 0, 1, -tube.WaterWallBotLoad, -tube.WaterWallTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[3], "3", 1, 2, 0, 1, -tube.WaterPlateTopLoad, -tube.WaterPlateTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[6], "3", 1, 2, 0, 1, -tube.WaterPlateTopLoad, -tube.WaterPlateTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[4], "3", 1, 2, 0, 1, tube.WaterPlateBotLoad, tube.WaterPlateBotLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[5], "3", 1, 2, 0, 1, tube.WaterPlateBotLoad, tube.WaterPlateBotLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            #endregion

            ret = mySapModel.SetPresentUnits(SAP2000v15.eUnits.kN_m_C);  //switch to k-in units 转换单位

            ret = mySapModel.File.Save(@path); //save model

            #region 10分析模块
            //run model (this will create the analysis model)
            ret = mySapModel.Analyze.RunAnalysis();
            for (int i = 0; i < 3; i++)
            {
                ret = mySapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                ret = mySapModel.Results.Setup.SetCaseSelectedForOutput(System.Convert.ToString(i + 1), System.Convert.ToBoolean(-1));
                ret = mySapModel.Results.FrameForce(FrameName[0], SAP2000v15.eItemTypeElm.ObjectElm, ref NumberResults, ref Obj, ref ObjSta, ref Elm, ref ElmSta, ref LoadCase, ref StepType, ref  StepNum, ref P, ref V2, ref V3, ref T, ref M2, ref M3);
                sapResList.AddRange(M3);
            }

            MessageBox.Show("输出成功，关闭模型");  //close Sap2000
            mySapObject.ApplicationExit(false);
            mySapModel = null;
            mySapObject = null;

            //fill Sap2000 result strings
            string SapResultString = null;
            for (int i = 0; i < sapResList.Count; i++)
            {
                SapResultString += string.Format("{0:0.00000}", sapResList[i]) + "\r\n";
            }
            MessageBox.Show(SapResultString);
            #endregion
            #endregion
        }

        private void btnToSingleTubeSap2000_Click(object sender, EventArgs e)
        {
            #region 初始化数据
            string path = textBoxSPath.Text;
            double a = Convert.ToDouble(textBoxSA.Text);//侧壁厚度
            double b1 = Convert.ToDouble(textBoxB1.Text);//左舱长度
            double c = Convert.ToDouble(textBoxC.Text);//顶板厚度
            double d = Convert.ToDouble(textBoxD.Text);//底板厚度
            double h = Convert.ToDouble(textBoxH.Text);//高度
            double futuh = Convert.ToDouble(textBoxFuTuH.Text);//覆土高度
            double vel = Convert.ToDouble(textBoxVel.Text);
            string concrete = comboBoxCon.Text;
            RecSingleTube tube = new RecSingleTube(a, b1, c, d, h, vel, futuh);
            #endregion

            #region sap2000过程
            //dimension variables
            #region 1定义变量
            SAP2000v15.SapObject mySapObject;

            SAP2000v15.cSapModel mySapModel;

            int ret;

            double[] ModValue;

            bool[] Restraint;//定义约束

            string[] FrameName = new string[4];//4个框架编号

            string[] PointName;//节点点编号

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
            #endregion
            #region 定义中间变量
            string temp_string1;//定义中间变量

            string temp_string2;//定义中间变量

            #endregion

            #region 2初始化sap2000模型
            //create Sap2000 object

            mySapObject = new SAP2000v15.SapObject();//创建一个sap2000模型

            mySapObject.ApplicationStart(SAP2000v15.eUnits.kN_m_C, true, "");//创建一个新的模型

            //create SapModel object

            mySapModel = mySapObject.SapModel;

            //initialize model

            ret = mySapModel.InitializeNewModel((SAP2000v15.eUnits.kN_m_C));//初始化模型

            //create new blank model

            ret = mySapModel.File.NewBlank();//创建新的空模型
            #endregion

            #region 3定义材料属性
            //define material property 创建材料属性

            ret = mySapModel.PropMaterial.SetMaterial("CONC", SAP2000v15.eMatType.MATERIAL_CONCRETE, -1, "", "");

            //assign isotropic mechanical properties to material

            ret = mySapModel.PropMaterial.SetMPIsotropic("CONC", 3600, 0.2, 0.0000055, 0);
            #endregion

            #region 4定义截面属性
            //define rectangular frame section property 定义截面属性

            ret = mySapModel.PropFrame.SetRectangle("b1x" + tube.WallThick, "CONC", tube.WallThick, 1, -1, "", "");
            ret = mySapModel.PropFrame.SetRectangle("b1x" + tube.TopThick, "CONC", tube.TopThick, 1, -1, "", "");
            ret = mySapModel.PropFrame.SetRectangle("b1x" + tube.BotThick, "CONC", tube.BotThick, 1, -1, "", "");

            //define frame section property modifiers定义截面修正

            ModValue = new double[8];

            for (int i = 0; i < 8; i++) { ModValue[i] = 1; }

            ModValue[0] = 1000;

            ModValue[1] = 0;

            ModValue[2] = 0;

            double[] temp_SystemArray = ModValue;

            ret = mySapModel.PropFrame.SetModifiers("b1x" + tube.WallThick, ref temp_SystemArray);

            //switch to k-ft units 切换单位

            ret = mySapModel.SetPresentUnits(SAP2000v15.eUnits.kN_m_C);
            #endregion

            #region 5add frame object by coordinates 按坐标添加构件

            temp_string1 = FrameName[0];

            temp_string2 = FrameName[0];

            ret = mySapModel.FrameObj.AddByCoord(0, 0, 0, 0, 0, tube.Height, ref temp_string1, "b1x" + tube.WallThick, "1", "Global");

            FrameName[0] = temp_string1;

            ret = mySapModel.FrameObj.AddByCoord(tube.LeftLength, 0, 0, tube.LeftLength, 0, tube.Height, ref temp_string1, "b1x" + tube.WallThick, "2", "Global");

            FrameName[1] = temp_string1;

            ret = mySapModel.FrameObj.AddByCoord(0, 0, tube.Height, tube.LeftLength, 0, tube.Height, ref temp_string1, "b1x" + tube.TopThick, "3", "Global");

            FrameName[2] = temp_string1;

            ret = mySapModel.FrameObj.AddByCoord(0, 0, 0, tube.LeftLength, 0, 0, ref temp_string1, "b1x" + tube.BotThick, "4", "Global");

            FrameName[3] = temp_string1;


            #endregion

            #region 6设置边界条件
            //assign point object restraint at base 设置底部的固定约束

            PointName = new string[6];

            Restraint = new bool[6];
            //限制U1,U2,U3
            for (int i = 0; i < 3; i++) { Restraint[i] = true; }
            //限制R1,R2,R3
            for (int i = 3; i < 6; i++) { Restraint[i] = true; }

            ret = mySapModel.FrameObj.GetPoints(FrameName[0], ref temp_string1, ref temp_string2);
            PointName[0] = temp_string1;
            PointName[1] = temp_string2;
            ret = mySapModel.PointObj.SetRestraint(PointName[0], ref Restraint, 0);

            ret = mySapModel.FrameObj.GetPoints(FrameName[1], ref temp_string1, ref temp_string2);
            PointName[2] = temp_string1;
            PointName[3] = temp_string2;
            ret = mySapModel.PointObj.SetRestraint(PointName[2], ref Restraint, 0);

            //assign point object restraint at top 设置顶部的铰接约束

            for (int i = 0; i < 3; i++) { Restraint[i] = true; }

            for (int i = 3; i < 6; i++) { Restraint[i] = false; }

            ret = mySapModel.FrameObj.GetPoints(FrameName[0], ref temp_string1, ref temp_string2);
            PointName[0] = temp_string1;
            PointName[1] = temp_string2;
            ret = mySapModel.PointObj.SetRestraint(PointName[1], ref Restraint, 0);

            ret = mySapModel.FrameObj.GetPoints(FrameName[1], ref temp_string1, ref temp_string2);
            PointName[2] = temp_string1;
            PointName[3] = temp_string2;
            ret = mySapModel.PointObj.SetRestraint(PointName[3], ref Restraint, 0);

            #endregion

            #region 7设置自动框架细分及指定拉压弹簧
            double[] vec = new double[] { 0, 0, 1 };
            for (int f = 0; f < 4; f++)
            {
                ret = mySapModel.FrameObj.SetAutoMesh(FrameName[f], true, true, true, 10, 0.1, 0);
            }
            //设置线弹簧 参数4： 1是拉压弹簧，2是受压弹簧，3是受拉弹簧
            //设置线弹簧 参数7： 弹簧的方向
            ret = mySapModel.FrameObj.SetSpring(FrameName[0], 1, 1, 2, "Simple", 1, 2, 0, ref  vec, 0, true, "Local", 0);
            ret = mySapModel.FrameObj.SetSpring(FrameName[1], 1, 1, 2, "Simple", 1, -2, 0, ref  vec, 0, true, "Local", 0);
            ret = mySapModel.FrameObj.SetSpring(FrameName[3], 1, 1, 2, "Simple", 1, 2, 0, ref  vec, 0, true, "Local", 0);
            #endregion

            ret = mySapModel.View.RefreshView(0, false);  //refresh view, update (initialize) zoom 刷新界面

            #region 8添加荷载模式
            //add load patterns 添加荷载模式
            ret = mySapModel.LoadPatterns.Add("1", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, true);
            ret = mySapModel.LoadPatterns.Add("2", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, true);
            ret = mySapModel.LoadPatterns.Add("3", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, true);
            #endregion

            //assign loading for load pattern 

            #region 9设置均布荷载
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[0], "1", 1, 2, 0, 1, tube.VelWallBotLoad, tube.VelWallTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[1], "1", 1, 2, 0, 1, -tube.VelWallBotLoad, -tube.VelWallTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[2], "1", 1, 2, 0, 1, -tube.VelPlateTopLoad, -tube.VelPlateTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[0], "2", 1, 2, 0, 1, tube.SoilWallBotLoad, tube.SoilWallTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[1], "2", 1, 2, 0, 1, -tube.SoilWallBotLoad, -tube.SoilWallTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[2], "2", 1, 2, 0, 1, -tube.SoilPlateTop, -tube.SoilPlateTop, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[0], "3", 1, 2, 0, 1, tube.WaterWallBotLoad, tube.WaterWallTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[1], "3", 1, 2, 0, 1, -tube.WaterWallBotLoad, -tube.WaterWallTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[2], "3", 1, 2, 0, 1, -tube.WaterPlateTopLoad, -tube.WaterPlateTopLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[3], "3", 1, 2, 0, 1, tube.WaterPlateBotLoad, tube.WaterPlateBotLoad, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            #endregion

            ret = mySapModel.SetPresentUnits(SAP2000v15.eUnits.kN_m_C);  //switch to k-in units 转换单位

            ret = mySapModel.File.Save(@path); //save model

            #region 10分析模块
            //run model (this will create the analysis model)
            ret = mySapModel.Analyze.RunAnalysis();
            for (int i = 0; i < 3; i++)
            {
                ret = mySapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                ret = mySapModel.Results.Setup.SetCaseSelectedForOutput(System.Convert.ToString(i + 1), System.Convert.ToBoolean(-1));
                ret = mySapModel.Results.FrameForce(FrameName[0], SAP2000v15.eItemTypeElm.ObjectElm, ref NumberResults, ref Obj, ref ObjSta, ref Elm, ref ElmSta, ref LoadCase, ref StepType, ref  StepNum, ref P, ref V2, ref V3, ref T, ref M2, ref M3);
                sapResList.AddRange(M3);
            }

            MessageBox.Show("关闭模型");  //close Sap2000
            mySapObject.ApplicationExit(false);
            mySapModel = null;
            mySapObject = null;

            //fill Sap2000 result strings
            string SapResultString = null;
            for (int i = 0; i < sapResList.Count; i++)
            {
                SapResultString += string.Format("{0:0.00000}", sapResList[i]) + "\r\n";
            }
            MessageBox.Show(SapResultString);
            #endregion
            #endregion
        }

        private void btnWallToSap2000_Click(object sender, EventArgs e)
        {
            #region 初始化参数
            double k0 = Convert.ToDouble(textBox_K0.Text);
            double height_Soil = Convert.ToDouble(textBoxWFuTuAtt.Text);
            double height_Water = Convert.ToDouble(textBoxWaterAtt.Text);
            double height_plan_top = Convert.ToDouble(textBoxTopAtt.Text);
            double height_plan_bot = Convert.ToDouble(textBoxBotAtt.Text);
            double section_b = Convert.ToDouble(textBoxWb.Text);
            double section_h = Convert.ToDouble(textBoxWHeight.Text);
            string path = textBoxWallPath.Text;
            Wall wall = new Wall(section_b, section_h, height_plan_top, height_plan_bot, height_Soil, height_Water, 5);
            #endregion

            #region sap2000完整过程
            //dimension variables
            #region 1.定义建模变量
            SAP2000v15.SapObject mySapObject;
            SAP2000v15.cSapModel mySapModel;
            int ret;
            double[] ModValue = new double[8];//定义截面修正
            bool[] Restraint = new bool[6];//定义6个约束
            string[] FrameName = new string[1];//只有一个构件;
            string[] PointName = new string[2];//只有2个端点;
            #endregion

            #region 2.定义输出结果变量
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

            #endregion

            #region 3.初始化建模

            mySapObject = new SAP2000v15.SapObject();//create Sap2000 object

            mySapObject.ApplicationStart(SAP2000v15.eUnits.kN_m_C, true, ""); //start Sap2000 application

            mySapModel = mySapObject.SapModel; //create SapModel object

            ret = mySapModel.InitializeNewModel((SAP2000v15.eUnits.kN_m_C)); //initialize model

            ret = mySapModel.File.NewBlank(); //create new blank model

            ret = mySapModel.PropMaterial.SetMaterial("CONC", SAP2000v15.eMatType.MATERIAL_CONCRETE, -1, "", "");//define material property

            ret = mySapModel.PropMaterial.SetMPIsotropic("CONC", 3600, 0.2, 0.0000055, 0); //assign isotropic mechanical properties to material

            ret = mySapModel.PropFrame.SetRectangle("b1x0.3", "CONC", wall.Section_height, wall.Section_width, -1, "", "");//define rectangular frame section property

            //define frame section property modifiers 定义截面修正

            for (int i = 0; i <= 7; i++)
            {
                ModValue[i] = 1;
            }
            ModValue[0] = 1000;
            ModValue[1] = 0;
            ModValue[2] = 0;

            ret = mySapModel.PropFrame.SetModifiers("B1X0.3", ref ModValue);

            ret = mySapModel.SetPresentUnits(SAP2000v15.eUnits.kN_m_C);    //switch to k-ft units

            ret = mySapModel.FrameObj.AddByCoord(0, 0, 0, 0, 0, wall.Wall_length, ref FrameName[0], "B1X0.3", "1", "Global"); //add frame object by coordinates
            #endregion

            #region 4.设置边界条件
            //assign point object restraint at base
            for (int i = 0; i < 3; i++) //限制U1,U2,U3
            {
                Restraint[i] = true;
            }
            for (int i = 3; i < 6; i++)    //限制R1,R2,R3
            {
                Restraint[i] = true;
            }


            ret = mySapModel.FrameObj.GetPoints(FrameName[0], ref PointName[0], ref PointName[1]);

            ret = mySapModel.PointObj.SetRestraint(PointName[0], ref Restraint, 0);


            //assign point object restraint at top

            for (int i = 0; i < 3; i++)
            {
                Restraint[i] = true;
            }
            for (int i = 3; i < 6; i++)
            {
                Restraint[i] = false;
            }

            ret = mySapModel.PointObj.SetRestraint(PointName[1], ref Restraint, 0);
            #endregion

            ret = mySapModel.View.RefreshView(0, false); //refresh view, update (initialize) zoom

            #region 5.设置荷载
            //add load patterns

            ret = mySapModel.LoadPatterns.Add("1", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, true);//1 vel荷载
            ret = mySapModel.LoadPatterns.Add("2", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, true);//2 soil荷载
            ret = mySapModel.LoadPatterns.Add("3", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, true);//3 water荷载

            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[0], "1", 1, 2, 0, 1, wall.Vel_load_bot, wall.Vel_load_top, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[0], "2", 1, 2, 0, 1, wall.Soil_load_bot, wall.Soil_load_top, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);
            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[0], "3", 1, 2, 0, 1, wall.Water_load_bot, wall.Water_load_top, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            //switch to k-in units
            ret = mySapModel.SetPresentUnits(SAP2000v15.eUnits.kN_m_C);
            #endregion

            ret = mySapModel.File.Save(@path); //save model
            #region 6.计算分析
            ret = mySapModel.Analyze.RunAnalysis(); //run model (this will create the analysis model)

            //initialize for Sap2000 results

            //get Sap2000 results for load patterns 1 through 3
            for (int i = 0; i < 3; i++)
            {
                ret = mySapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();
                ret = mySapModel.Results.Setup.SetCaseSelectedForOutput(System.Convert.ToString(i + 1), System.Convert.ToBoolean(-1));
                ret = mySapModel.Results.FrameForce(FrameName[0], SAP2000v15.eItemTypeElm.ObjectElm, ref NumberResults, ref Obj, ref ObjSta, ref Elm, ref ElmSta, ref LoadCase, ref StepType, ref  StepNum, ref P, ref V2, ref V3, ref T, ref M2, ref M3);
                sapResList.AddRange(M3);
            }
            //close Sap2000
            mySapObject.ApplicationExit(false);
            mySapModel = null;
            mySapObject = null;
            #endregion

            #region 7.获取计算结果
            //fill Sap2000 result strings
            string SapResultString = null;
            for (int i = 0; i < sapResList.Count; i++)
            {
                SapResultString += string.Format("{0:0.00000}", sapResList[i]) + "\r\n";
            }

            MessageBox.Show(SapResultString);
            #endregion

            #endregion
        }

        private void buttonTest_Click(object sender, EventArgs e)
        {
            //dimension variables
            SAP2000v15.SapObject mySapObject;

            SAP2000v15.cSapModel mySapModel;

            int ret;

            int i;

            double[] ModValue;

            double[] PointLoadValue;

            bool[] Restraint;

            string[] FrameName;

            string[] PointName;

            double[] SapResult;

            string temp_string1;

            string temp_string2;

            bool temp_bool;

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

            //create Sap2000 object

            mySapObject = new SAP2000v15.SapObject();

            //start Sap2000 application

            temp_bool = true;

            mySapObject.ApplicationStart(SAP2000v15.eUnits.kip_in_F, temp_bool, "");

            //create SapModel object

            mySapModel = mySapObject.SapModel;

            //initialize model

            ret = mySapModel.InitializeNewModel((SAP2000v15.eUnits.kip_in_F));

            //create new blank model

            ret = mySapModel.File.NewBlank();

            //define material property

            ret = mySapModel.PropMaterial.SetMaterial("CONC", SAP2000v15.eMatType.MATERIAL_CONCRETE, -1, "", "");

            //assign isotropic mechanical properties to material

            ret = mySapModel.PropMaterial.SetMPIsotropic("CONC", 3600, 0.2, 0.0000055, 0);

            //define rectangular frame section property

            ret = mySapModel.PropFrame.SetRectangle("R1", "CONC", 12, 12, -1, "", "");

            //define frame section property modifiers

            ModValue = new double[8];

            for (i = 0; i <= 7; i++)
            {

                ModValue[i] = 1;

            }

            ModValue[0] = 1000;

            ModValue[1] = 0;

            ModValue[2] = 0;

            double[] temp_SystemArray = ModValue;

            ret = mySapModel.PropFrame.SetModifiers("R1", ref temp_SystemArray);

            //switch to k-ft units

            ret = mySapModel.SetPresentUnits(SAP2000v15.eUnits.kip_ft_F);

            //add frame object by coordinates

            FrameName = new string[3];

            temp_string1 = FrameName[0];

            temp_string2 = FrameName[0];

            ret = mySapModel.FrameObj.AddByCoord(0, 0, 0, 0, 0, 10, ref temp_string1, "R1", "1", "Global");

            FrameName[0] = temp_string1;

            ret = mySapModel.FrameObj.AddByCoord(0, 0, 10, 8, 0, 16, ref temp_string1, "R1", "2", "Global");

            FrameName[1] = temp_string1;

            ret = mySapModel.FrameObj.AddByCoord(-4, 0, 10, 0, 0, 10, ref temp_string1, "R1", "3", "Global");

            FrameName[2] = temp_string1;

            //assign point object restraint at base

            PointName = new string[2];

            Restraint = new bool[6];

            for (i = 0; i <= 3; i++)
            {

                Restraint[i] = true;

            }

            for (i = 4; i <= 5; i++)
            {

                Restraint[i] = false;

            }

            ret = mySapModel.FrameObj.GetPoints(FrameName[0], ref temp_string1, ref temp_string2);

            PointName[0] = temp_string1;

            PointName[1] = temp_string2;

            bool[] temp_SystemArray1 = Restraint;

            ret = mySapModel.PointObj.SetRestraint(PointName[0], ref temp_SystemArray1, 0);

            //assign point object restraint at top

            for (i = 0; i <= 1; i++)
            {

                Restraint[i] = true;

            }

            for (i = 2; i <= 5; i++)
            {

                Restraint[i] = false;

            }

            ret = mySapModel.FrameObj.GetPoints(FrameName[1], ref temp_string1, ref temp_string2);

            PointName[0] = temp_string1;

            PointName[1] = temp_string2;

            temp_SystemArray1 = Restraint;

            ret = mySapModel.PointObj.SetRestraint(PointName[1], ref temp_SystemArray1, 0);

            //refresh view, update (initialize) zoom


            ret = mySapModel.View.RefreshView(0, false);

            //add load patterns

            ret = mySapModel.LoadPatterns.Add("1", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 1, true);

            ret = mySapModel.LoadPatterns.Add("2", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, true);

            ret = mySapModel.LoadPatterns.Add("3", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, true);

            ret = mySapModel.LoadPatterns.Add("4", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, true);

            ret = mySapModel.LoadPatterns.Add("5", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, true);

            ret = mySapModel.LoadPatterns.Add("6", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, true);

            ret = mySapModel.LoadPatterns.Add("7", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, true);

            //assign loading for load pattern 2

            ret = mySapModel.FrameObj.GetPoints(FrameName[2], ref temp_string1, ref temp_string2);

            PointName[0] = temp_string1;

            PointName[1] = temp_string2;

            PointLoadValue = new double[6];

            PointLoadValue[2] = -10;

            temp_SystemArray = PointLoadValue;

            ret = mySapModel.PointObj.SetLoadForce(PointName[0], "2", ref temp_SystemArray, false, "Global", 0);

            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[2], "2", 1, 10, 0, 1, 1.8, 1.8, "Global", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            //assign loading for load pattern 3

            ret = mySapModel.FrameObj.GetPoints(FrameName[2], ref temp_string1, ref temp_string2);

            PointName[0] = temp_string1;

            PointName[1] = temp_string2;

            PointLoadValue = new double[6];

            PointLoadValue[2] = -17.2;

            PointLoadValue[4] = -54.4;

            temp_SystemArray = PointLoadValue;

            ret = mySapModel.PointObj.SetLoadForce(PointName[1], "3", ref temp_SystemArray, false, "Global", 0);

            //assign loading for load pattern 4

            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[1], "4", 1, 11, 0, 1, 2, 2, "Global", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            //assign loading for load pattern 5

            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[0], "5", 1, 2, 0, 1, 2, 2, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[1], "5", 1, 2, 0, 1, -2, -2, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            //assign loading for load pattern 6

            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[0], "6", 1, 2, 0, 1, 0.9984, 0.3744, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[1], "6", 1, 2, 0, 1, -0.3744, 0, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            //assign loading for load pattern 7

            ret = mySapModel.FrameObj.SetLoadPoint(FrameName[1], "7", 1, 2, 0.5, -15, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            //switch to k-in units

            ret = mySapModel.SetPresentUnits(SAP2000v15.eUnits.kip_in_F);

            //save model

            ret = mySapModel.File.Save(@"E:\sapmodel\demo\API_1-test01.sdb");

            //run model (this will create the analysis model)

            ret = mySapModel.Analyze.RunAnalysis();

            //initialize for Sap2000 results

            SapResult = new double[7];
            //获取杆件的节点
            ret = mySapModel.FrameObj.GetPoints(FrameName[1], ref temp_string1, ref temp_string2);

            PointName[0] = temp_string1;

            PointName[1] = temp_string2;

            //get Sap2000 results for load patterns 1 through 7

            NumberResults = 5;//决定了啥？


            for (i = 0; i <= 6; i++)
            {
                ret = mySapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

                ret = mySapModel.Results.Setup.SetCaseSelectedForOutput(System.Convert.ToString(i + 1), System.Convert.ToBoolean(-1));

                ret = mySapModel.Results.FrameForce(FrameName[0], SAP2000v15.eItemTypeElm.ObjectElm, ref NumberResults, ref Obj, ref ObjSta, ref Elm, ref ElmSta, ref LoadCase, ref StepType, ref  StepNum, ref P, ref V2, ref V3, ref T, ref M2, ref M3);

                sapResList.AddRange(M3);
            }

            //close Sap2000

            mySapObject.ApplicationExit(false);

            mySapModel = null;

            mySapObject = null;

            //fill Sap2000 result strings

            string res = null;
            for (i = 0; i < sapResList.Count; i++)
            {
                res = res + string.Format("{0:0.00000}", sapResList[i]) + "\r\n";
            }
            MessageBox.Show(res);
        }

        private void buttonTest2_Click(object sender, EventArgs e)
        {
            //dimension variables
            SAP2000v15.SapObject mySapObject;

            SAP2000v15.cSapModel mySapModel;

            int ret;

            int i;

            double[] ModValue;

            double[] PointLoadValue;

            bool[] Restraint;

            string[] FrameName;

            string[] PointName;

            int NumberResults;

            string[] Obj;
            string[] Elm;
            string[] LoadCase;

            string[] StepType;

            double[] StepNum;

            double[] U1;

            double[] U2;

            double[] U3;

            double[] R1;

            double[] R2;

            double[] R3;

            double[] SapResult;

            string temp_string1;

            string temp_string2;

            bool temp_bool;

            //create Sap2000 object

            mySapObject = new SAP2000v15.SapObject();

            //start Sap2000 application

            temp_bool = true;

            mySapObject.ApplicationStart(SAP2000v15.eUnits.kip_in_F, temp_bool, "");

            //create SapModel object

            mySapModel = mySapObject.SapModel;

            //initialize model

            ret = mySapModel.InitializeNewModel((SAP2000v15.eUnits.kip_in_F));

            //create new blank model

            ret = mySapModel.File.NewBlank();

            //define material property

            ret = mySapModel.PropMaterial.SetMaterial("CONC", SAP2000v15.eMatType.MATERIAL_CONCRETE, -1, "", "");

            //assign isotropic mechanical properties to material

            ret = mySapModel.PropMaterial.SetMPIsotropic("CONC", 3600, 0.2, 0.0000055, 0);

            //define rectangular frame section property

            ret = mySapModel.PropFrame.SetRectangle("R1", "CONC", 12, 12, -1, "", "");

            //define frame section property modifiers

            ModValue = new double[8];

            for (i = 0; i <= 7; i++)
            {

                ModValue[i] = 1;

            }

            ModValue[0] = 1000;

            ModValue[1] = 0;

            ModValue[2] = 0;

            double[] temp_SystemArray = ModValue;

            ret = mySapModel.PropFrame.SetModifiers("R1", ref temp_SystemArray);

            //switch to k-ft units

            ret = mySapModel.SetPresentUnits(SAP2000v15.eUnits.kip_ft_F);

            //add frame object by coordinates

            FrameName = new string[3];

            temp_string1 = FrameName[0];

            temp_string2 = FrameName[0];

            ret = mySapModel.FrameObj.AddByCoord(0, 0, 0, 0, 0, 10, ref temp_string1, "R1", "1", "Global");

            FrameName[0] = temp_string1;

            ret = mySapModel.FrameObj.AddByCoord(0, 0, 10, 8, 0, 16, ref temp_string1, "R1", "2", "Global");

            FrameName[1] = temp_string1;

            ret = mySapModel.FrameObj.AddByCoord(-4, 0, 10, 0, 0, 10, ref temp_string1, "R1", "3", "Global");

            FrameName[2] = temp_string1;

            //assign point object restraint at base

            PointName = new string[2];

            Restraint = new bool[6];

            for (i = 0; i <= 3; i++)
            {

                Restraint[i] = true;

            }

            for (i = 4; i <= 5; i++)
            {

                Restraint[i] = false;

            }

            ret = mySapModel.FrameObj.GetPoints(FrameName[0], ref temp_string1, ref temp_string2);

            PointName[0] = temp_string1;

            PointName[1] = temp_string2;

            bool[] temp_SystemArray1 = Restraint;

            ret = mySapModel.PointObj.SetRestraint(PointName[0], ref temp_SystemArray1, 0);

            //assign point object restraint at top

            for (i = 0; i <= 1; i++)
            {

                Restraint[i] = true;

            }

            for (i = 2; i <= 5; i++)
            {

                Restraint[i] = false;

            }

            ret = mySapModel.FrameObj.GetPoints(FrameName[1], ref temp_string1, ref temp_string2);

            PointName[0] = temp_string1;

            PointName[1] = temp_string2;

            temp_SystemArray1 = Restraint;

            ret = mySapModel.PointObj.SetRestraint(PointName[1], ref temp_SystemArray1, 0);

            //refresh view, update (initialize) zoom

            temp_bool = false;

            ret = mySapModel.View.RefreshView(0, temp_bool);

            //add load patterns

            temp_bool = true;

            ret = mySapModel.LoadPatterns.Add("1", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 1, temp_bool);

            ret = mySapModel.LoadPatterns.Add("2", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, temp_bool);

            ret = mySapModel.LoadPatterns.Add("3", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, temp_bool);

            ret = mySapModel.LoadPatterns.Add("4", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, temp_bool);

            ret = mySapModel.LoadPatterns.Add("5", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, temp_bool);

            ret = mySapModel.LoadPatterns.Add("6", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, temp_bool);

            ret = mySapModel.LoadPatterns.Add("7", SAP2000v15.eLoadPatternType.LTYPE_OTHER, 0, temp_bool);

            //assign loading for load pattern 2

            ret = mySapModel.FrameObj.GetPoints(FrameName[2], ref temp_string1, ref temp_string2);

            PointName[0] = temp_string1;

            PointName[1] = temp_string2;

            PointLoadValue = new double[6];

            PointLoadValue[2] = -10;

            temp_SystemArray = PointLoadValue;

            ret = mySapModel.PointObj.SetLoadForce(PointName[0], "2", ref temp_SystemArray, false, "Global", 0);

            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[2], "2", 1, 10, 0, 1, 1.8, 1.8, "Global", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            //assign loading for load pattern 3

            ret = mySapModel.FrameObj.GetPoints(FrameName[2], ref temp_string1, ref temp_string2);

            PointName[0] = temp_string1;

            PointName[1] = temp_string2;

            PointLoadValue = new double[6];

            PointLoadValue[2] = -17.2;

            PointLoadValue[4] = -54.4;

            temp_SystemArray = PointLoadValue;

            ret = mySapModel.PointObj.SetLoadForce(PointName[1], "3", ref temp_SystemArray, false, "Global", 0);

            //assign loading for load pattern 4

            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[1], "4", 1, 11, 0, 1, 2, 2, "Global", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            //assign loading for load pattern 5

            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[0], "5", 1, 2, 0, 1, 2, 2, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[1], "5", 1, 2, 0, 1, -2, -2, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            //assign loading for load pattern 6

            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[0], "6", 1, 2, 0, 1, 0.9984, 0.3744, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            ret = mySapModel.FrameObj.SetLoadDistributed(FrameName[1], "6", 1, 2, 0, 1, -0.3744, 0, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            //assign loading for load pattern 7

            ret = mySapModel.FrameObj.SetLoadPoint(FrameName[1], "7", 1, 2, 0.5, -15, "Local", System.Convert.ToBoolean(-1), System.Convert.ToBoolean(-1), 0);

            //switch to k-in units

            ret = mySapModel.SetPresentUnits(SAP2000v15.eUnits.kip_in_F);

            //save model

            ret = mySapModel.File.Save(@"E:\sapmodel\demo\API_1-test01.sdb");

            //run model (this will create the analysis model)

            ret = mySapModel.Analyze.RunAnalysis();

            //initialize for Sap2000 results

            SapResult = new double[7];
            //获取杆件的节点
            ret = mySapModel.FrameObj.GetPoints(FrameName[1], ref temp_string1, ref temp_string2);

            PointName[0] = temp_string1;

            PointName[1] = temp_string2;

            //get Sap2000 results for load patterns 1 through 7

            NumberResults = 5;//决定了啥？

            Obj = new string[1];

            Elm = new string[1];

            LoadCase = new string[1];

            StepType = new string[1];

            StepNum = new double[1];

            U1 = new double[1];

            U2 = new double[1];

            U3 = new double[1];

            R1 = new double[1];

            R2 = new double[1];

            R3 = new double[1];
            #region 输出节点位移
            for (i = 0; i <= 6; i++)
            {

                ret = mySapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

                ret = mySapModel.Results.Setup.SetCaseSelectedForOutput(System.Convert.ToString(i + 1), System.Convert.ToBoolean(-1));

                if (i <= 3)
                {

                    string[] temp_SystemArray20 = Obj;

                    string[] temp_SystemArray3 = Elm;

                    string[] temp_SystemArray4 = LoadCase;

                    string[] temp_SystemArray5 = StepType;

                    double[] temp_SystemArray6 = StepNum;

                    double[] temp_SystemArray7 = U1;

                    double[] temp_SystemArray8 = U2;

                    double[] temp_SystemArray9 = U3;

                    double[] temp_SystemArray10 = R1;

                    double[] temp_SystemArray11 = R2;

                    double[] temp_SystemArray12 = R3;

                    //获取节点位移
                    ret = mySapModel.Results.JointDispl(PointName[1], SAP2000v15.eItemTypeElm.ObjectElm, ref NumberResults, ref temp_SystemArray20, ref temp_SystemArray3, ref temp_SystemArray4, ref temp_SystemArray5, ref temp_SystemArray6, ref temp_SystemArray7, ref temp_SystemArray8, ref temp_SystemArray9, ref temp_SystemArray10, ref temp_SystemArray11, ref temp_SystemArray12);

                    temp_SystemArray9.CopyTo(U3, 0);

                    SapResult[i] = U3[0];

                }

                else
                {

                    string[] temp_SystemArray20 = Obj;

                    string[] temp_SystemArray3 = Elm;

                    string[] temp_SystemArray4 = LoadCase;

                    string[] temp_SystemArray5 = StepType;

                    double[] temp_SystemArray6 = StepNum;

                    double[] temp_SystemArray7 = U1;

                    double[] temp_SystemArray8 = U2;

                    double[] temp_SystemArray9 = U3;

                    double[] temp_SystemArray10 = R1;

                    double[] temp_SystemArray11 = R2;

                    double[] temp_SystemArray12 = R3;

                    ret = mySapModel.Results.JointDispl(PointName[0], SAP2000v15.eItemTypeElm.ObjectElm, ref NumberResults, ref temp_SystemArray20, ref temp_SystemArray3, ref temp_SystemArray4, ref temp_SystemArray5, ref temp_SystemArray6, ref temp_SystemArray7, ref temp_SystemArray8, ref temp_SystemArray9, ref temp_SystemArray10, ref temp_SystemArray11, ref temp_SystemArray12);
                    temp_SystemArray7.CopyTo(U1, 0);

                    SapResult[i] = U1[0];

                }

            }
            #endregion



            List<double> m = new List<double>();
            for (i = 0; i <= 6; i++)
            {
                ret = mySapModel.Results.Setup.DeselectAllCasesAndCombosForOutput();

                ret = mySapModel.Results.Setup.SetCaseSelectedForOutput(System.Convert.ToString(i + 1), System.Convert.ToBoolean(-1));

                string[] temp_SystemArray20 = Obj;

                string[] temp_SystemArray3 = Elm;

                string[] temp_SystemArray4 = LoadCase;

                string[] temp_SystemArray5 = StepType;

                double[] temp_SystemArray6 = StepNum;

                double[] temp_SystemArray7 = U1;

                double[] temp_SystemArray8 = U2;

                double[] temp_SystemArray9 = U3;

                double[] temp_SystemArray10 = R1;

                double[] temp_SystemArray11 = R2;

                double[] temp_SystemArray12 = R3;


                //获取节点位移
                ret = mySapModel.Results.JointDispl(PointName[1], SAP2000v15.eItemTypeElm.ObjectElm, ref NumberResults, ref temp_SystemArray20, ref temp_SystemArray3, ref temp_SystemArray4, ref temp_SystemArray5, ref temp_SystemArray6, ref temp_SystemArray7, ref temp_SystemArray8, ref temp_SystemArray9, ref temp_SystemArray10, ref temp_SystemArray11, ref temp_SystemArray12);

                temp_SystemArray9.CopyTo(U3, 0);

                SapResult[i] = U3[0];

            }

            //close Sap2000

            mySapObject.ApplicationExit(false);

            mySapModel = null;

            mySapObject = null;

            //fill Sap2000 result strings

            string res = null;
            for (i = 0; i < SapResult.Length; i++)
            {
                res = res + string.Format("{0:0.00000}", SapResult[i]) + "\r\n";
            }
            MessageBox.Show(res);
        }

        private void btnToDwg_Click(object sender, EventArgs e)
        {
            string path = textBoxDwgPath.Text;
            DrawTube drawTube = new DrawTube();
            drawTube.FilePath = path;
            double a = Convert.ToDouble(textBoxA.Text);
            double b = Convert.ToDouble(textBoxB.Text);
            double b1 = Convert.ToDouble(textBoxB1.Text);
            double b2 = Convert.ToDouble(textBoxB2.Text);
            double c = Convert.ToDouble(textBoxC.Text);
            double d = Convert.ToDouble(textBoxD.Text);
            double h = Convert.ToDouble(textBoxH.Text);
            drawTube.test(a, b, a, c, d, b1, b2, h);
            MessageBox.Show("生成成功");
        }

    }

}
