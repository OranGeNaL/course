using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard
{
    static class SafeWriter
    {
        private delegate void SafeCallDelegate(string text, Label label);

        public static void WriteTextSafe(string text, Label label)
        {
            if (label.InvokeRequired)
            {
                var d = new SafeCallDelegate(WriteTextSafe);
                label.Invoke(d, new object[] { text, label });
            }
            else
            {
                label.Text = text;
            }
        }
    }
}
