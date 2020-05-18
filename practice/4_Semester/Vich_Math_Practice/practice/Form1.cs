using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        void RefreshForm()
        {
            scaleScrollValueLable.Text = Painter.scale.ToString();
            divisionValueValueLable.Text = Painter.divisionValue.ToString();
            Painter.UpdatePosition();
            firstPagePaint.Invalidate();
        }

        private void firstPagePaint_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = firstPagePaint.CreateGraphics();
            Pen blackPen = new Pen(Color.Black, 1);
            Pen greenPen = new Pen(Color.Green, 2);
            Pen redPen = new Pen(Color.Red, 2);

            graphics.DrawLine(blackPen, new Point(0, 0), new Point(0, Painter.SCREEN_HEIGHT));
            graphics.DrawLine(blackPen, new Point(0, Painter.SCREEN_HEIGHT), new Point(Painter.SCREEN_WIDTH, Painter.SCREEN_HEIGHT));

            for(int i = 0; i < Painter.SCREEN_WIDTH; i += (int)(Painter.SCREEN_DIV_VALUE * Painter.scale))
            {
                graphics.DrawLine(blackPen, new Point(0, Painter.SCREEN_HEIGHT - i), new Point(10, Painter.SCREEN_HEIGHT - i));
                graphics.DrawLine(blackPen, new Point(i, Painter.SCREEN_HEIGHT), new Point(i, Painter.SCREEN_HEIGHT - 10));
            }

            foreach (Curve i in Painter.curves)
            {
                graphics.DrawCurve(redPen, i.ConvertToPoint());
            }

            foreach (Dot i in Painter.dots)
            {
                graphics.DrawRectangle(greenPen, i.X - 1, i.Y - 1, 3, 3);
            }
        }

        private void tabPage1_Click(object sender, EventArgs e)
        {

        }

        private void scaleScroll_Scroll(object sender, EventArgs e)
        {
            Painter.scale = scaleScroll.Value / 100.0;
            RefreshForm();
        }

        private void drawFunc_Click(object sender, EventArgs e)
        {
            Painter.ImportFromMatrix(Counter.CountFunc(0, 5, 0.1));
        }

        private void divisionValueScroll_Scroll(object sender, EventArgs e)
        {
            Painter.divisionValue = divisionValueScroll.Value / 100.0;
            RefreshForm();
        }

        private void scaleScrollValueLable_Click(object sender, EventArgs e)
        {

        }
    }
}
