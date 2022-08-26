using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Windows.Forms;
using System.Runtime.InteropServices;
using SAP2000v15;
namespace SecondDevelopment
{
    [ClassInterface(ClassInterfaceType.AutoDual)]
    public class cPlugin
    {
        public void Main(ref SAP2000v15.cSapModel SapModel, ref SAP2000v15.cSapPlugin ISapPlugin)
        {
            Test t = new Test();
            t.ShowDialog();

            if (t.DialogResult == DialogResult.OK)
            {
                MessageBox.Show("welcome");
            }
            ISapPlugin.Finish(0);

        }
    }
}
