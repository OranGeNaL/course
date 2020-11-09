using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard
{
    public partial class Form1 : Form
    {
        List<IWidget> widgets = new List<IWidget>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //widgets.Add(new ClockTypeOne(this));
            widgets.Add(new Weather(this, new Point(0, 0)));
        }

        private void Form1_SizeChanged(object sender, EventArgs e)
        {
            foreach (var i in widgets)
                i.UpdateAppearance();
        }

        private void Form1_FormClosed(object sender, FormClosedEventArgs e)
        {
            foreach (var i in widgets)
                i.Stop();
        }
    }
}
