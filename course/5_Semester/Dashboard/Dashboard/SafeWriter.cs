using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard
{
    static class SafeWriter
    {
        private delegate void SafeTextWriteDelegate(string text, Label label);
        private delegate void SafeChangeLocationDelegate(Point location, Control control);

        public static void WriteTextSafe(string text, Label label)
        {
            if (label.InvokeRequired)
            {
                var d = new SafeTextWriteDelegate(WriteTextSafe);
                label.Invoke(d, new object[] { text, label });
            }
            else
            {
                label.Text = text;
            }
        }

        public static void WriteLocationSafe(Point location, Control control)
        {
            if (control.InvokeRequired)
            {
                var d = new SafeChangeLocationDelegate(WriteLocationSafe);
                control.Invoke(d, new object[] { location, control });
            }
            else
            {
                control.Location = location;
            }
        }
    }
}
