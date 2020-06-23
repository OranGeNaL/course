using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing.Drawing2D;
using System.Runtime.CompilerServices;

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

        void RefreshListBox()
        {
            listBox1.Items.Clear();

            foreach (Curve i in Painter.curves)
            {
                listBox1.Items.Add(i.name);
            }
        }

        private void firstPagePaint_Paint(object sender, PaintEventArgs e)
        {
            DoubleBuffered = true;
            Graphics graphics = firstPagePaint.CreateGraphics();
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Pen blackPen = new Pen(Color.Black, 1);
            Pen greenPen = new Pen(Color.Green, 2);

            graphics.DrawLine(blackPen, new Point(Painter.cursorX, 0), new Point(Painter.cursorX, Painter.SCREEN_HEIGHT));
            graphics.DrawLine(blackPen, new Point(0, Painter.cursorY), new Point(Painter.SCREEN_WIDTH, Painter.cursorY));

            if(Painter.leftDownX <= 0)
                graphics.DrawLine(blackPen, new Point(0, 0), new Point(0, Painter.SCREEN_HEIGHT));

            else if (Painter.leftDownX >= Painter.SCREEN_WIDTH)
                graphics.DrawLine(blackPen, new Point(Painter.SCREEN_WIDTH, 0), new Point(Painter.SCREEN_WIDTH, Painter.SCREEN_HEIGHT));

            else
                graphics.DrawLine(blackPen, new Point(Painter.leftDownX, 0), new Point(Painter.leftDownX, Painter.SCREEN_HEIGHT));

            if (Painter.leftDownY <= 0)
                graphics.DrawLine(blackPen, new Point(0, 0), new Point(Painter.SCREEN_WIDTH, 0));

            else if (Painter.leftDownY >= Painter.SCREEN_WIDTH)
                graphics.DrawLine(blackPen, new Point(0, Painter.SCREEN_HEIGHT), new Point(Painter.SCREEN_WIDTH, Painter.SCREEN_HEIGHT));

            else
                graphics.DrawLine(blackPen, new Point(0, Painter.leftDownY), new Point(Painter.SCREEN_WIDTH, Painter.leftDownY));


            for (int i = -(int)(Painter.SCREEN_WIDTH - Painter.SCREEN_WIDTH % (Painter.SCREEN_DIV_VALUE * Painter.scale)); i < Painter.SCREEN_WIDTH; i += (int)(Painter.SCREEN_DIV_VALUE * Painter.scale))
            {
                if (Painter.leftDownX <= 0)
                    graphics.DrawLine(blackPen, new Point(0 - 10, Painter.leftDownY - i), new Point(0 + 10, Painter.leftDownY - i));

                else if (Painter.leftDownX >= Painter.SCREEN_WIDTH)
                    graphics.DrawLine(blackPen, new Point(Painter.SCREEN_WIDTH - 10, Painter.leftDownY - i), new Point(Painter.SCREEN_WIDTH + 10, Painter.leftDownY - i));

                else
                    graphics.DrawLine(blackPen, new Point(Painter.leftDownX - 10, Painter.leftDownY - i), new Point(Painter.leftDownX + 10, Painter.leftDownY - i));

                if (Painter.leftDownY <= 0)
                    graphics.DrawLine(blackPen, new Point(Painter.leftDownX - i, 0 - 10), new Point(Painter.leftDownX - i, 0 + 10));

                else if (Painter.leftDownY >= Painter.SCREEN_WIDTH)
                    graphics.DrawLine(blackPen, new Point(Painter.leftDownX - i, Painter.SCREEN_HEIGHT - 10), new Point(Painter.leftDownX - i, Painter.SCREEN_HEIGHT + 10));

                else
                    graphics.DrawLine(blackPen, new Point(Painter.leftDownX - i, Painter.leftDownY - 10), new Point(Painter.leftDownX - i, Painter.leftDownY + 10));
            }

            try
            {
            foreach (Curve i in Painter.curves)
            {
                if(i.drawCurve)
                    graphics.DrawCurve(new Pen(i.color, 2), i.ConvertToPoint());

                if(i.drawDots)
                    foreach(Dot j in i.dots)
                    {
                        graphics.DrawRectangle(new Pen(i.dotsColor, 2), j.X - 1, j.Y - 1, 3, 3);
                    }
            }

            foreach (Dot i in Painter.dots)
            {
                graphics.DrawRectangle(greenPen, i.X - 1, i.Y - 1, 3, 3);
            }
            }
            catch
            {

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

        private void divisionValueScroll_Scroll(object sender, EventArgs e)
        {
            Painter.divisionValue = divisionValueScroll.Value / 10000.0;
            RefreshForm();
        }

        private void scaleScrollValueLable_Click(object sender, EventArgs e)
        {

        }

        private void verticalScroll_Scroll(object sender, EventArgs e)
        {
            Painter.leftDownY = Painter.SCREEN_HEIGHT + verticalScroll.Value;
            RefreshForm();
        }

        private void horizontalScroll_Scroll(object sender, EventArgs e)
        {
            Painter.leftDownX = 0 - horizontalScroll.Value;
            RefreshForm();
        }

        private void firstPagePaint_MouseHover(object sender, EventArgs e)
        {
            
        }

        private void firstPagePaint_MouseMove(object sender, MouseEventArgs e)
        {
            Painter.UpdateCursorInfo(e);

            cursorXLable.Location = new Point(58 + Painter.cursorX, cursorXLable.Location.Y);
            cursorYLable.Location = new Point(cursorYLable.Location.X, 18 + Painter.cursorY);

            cursorXLable.Text = ((double)(Painter.cursorX - Painter.leftDownX) / Painter.SCREEN_DIV_VALUE / Painter.scale * Painter.divisionValue).ToString();

            cursorYLable.Text = ((double)(Painter.leftDownY - Painter.cursorY) / Painter.SCREEN_DIV_VALUE / Painter.scale * Painter.divisionValue).ToString();

            if(Painter.mouseDown)
            {
                Painter.leftDownX = Painter.leftDownXOld + (Painter.cursorX - Painter.cursorXOld);
                Painter.leftDownY = Painter.leftDownYOld + (Painter.cursorY - Painter.cursorYOld);
            }

            RefreshForm();
        }

        private void testTaskButton_CheckedChanged(object sender, EventArgs e)
        {
            if(testTaskButton.Checked)
            {
                try
                {
                    Painter.Clear();
                    double a = double.Parse(aTextBox.Text);
                    double b = double.Parse(bTextBox.Text);
                    double h = double.Parse(hTextBox.Text);
                    

                    Painter.ImportFromMatrix(Counter.AccurateCount(Counter.TestAccurate, 0, a, b, h), "Test1" , Color.Red, Color.Green);
                    Painter.ImportFromMatrix(Counter.AccurateCount(Counter.TestAccurate, 1, a, b, h), "Test2" , Color.Purple, Color.Blue);
                    RefreshForm();
                    RefreshListBox();
                }
                catch
                {
                    MessageBox.Show("Введены неверные значения!!!");
                }
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Curve i in Painter.curves)
            {
                if (i.name.Contains("Test"))
                    i.drawDots = checkBox1.Checked;
            }
            RefreshForm();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            Painter.Clear();
            RefreshForm();
        }

        private void rungeKuttaTest_CheckedChanged(object sender, EventArgs e)
        {
            if (rungeKuttaTest.Checked)
            {
                try
                {
                    Painter.Clear();
                    double a = double.Parse(aTextBox.Text);
                    double b = double.Parse(bTextBox.Text);
                    double h = double.Parse(hTextBox.Text);


                    Painter.ImportFromMatrix(Counter.RungeKutta(Counter.TestODU, 0, a, b, h, a, Counter.GetFirstValues(Counter.TestAccurate, a, 2)),"Runge1", Color.Red, Color.Cyan);
                    Painter.ImportFromMatrix(Counter.RungeKutta(Counter.TestODU, 1, a, b, h, a, Counter.GetFirstValues(Counter.TestAccurate, a, 2)), "Runge2", Color.Red, Color.Yellow);
                    RefreshForm();
                    RefreshListBox();
                }
                catch
                {
                    MessageBox.Show("Введены неверные значения!!!");
                }
            }
        }

        private void checkBox2_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Curve i in Painter.curves)
            {
                if(i.name.Contains("Test"))
                    i.drawCurve = checkBox2.Checked;
            }
            RefreshForm();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Curve i in Painter.curves)
            {
                if (i.name.Contains("Runge"))
                    i.drawDots = checkBox3.Checked;
            }
            RefreshForm();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Curve i in Painter.curves)
            {
                if (i.name.Contains("Runge"))
                    i.drawCurve = checkBox4.Checked;
            }
            RefreshForm();
        }

        private void togetherRadioButton_CheckedChanged(object sender, EventArgs e)
        {
            if (togetherRadioButton.Checked)
            {
                try
                {
                    Painter.Clear();
                    double a = double.Parse(aTextBox.Text);
                    double b = double.Parse(bTextBox.Text);
                    double h = double.Parse(hTextBox.Text);

                    Painter.ImportFromMatrix(Counter.AccurateCount(Counter.TestAccurate, 0, a, b, h), "Test1", Color.Red, Color.Green);
                    Painter.ImportFromMatrix(Counter.AccurateCount(Counter.TestAccurate, 1, a, b, h), "Test2", Color.Purple, Color.Blue);
                    Painter.ImportFromMatrix(Counter.RungeKutta(Counter.TestODU, 0, a, b, h, a, Counter.GetFirstValues(Counter.TestAccurate, a, 2)), "Runge1", Color.Red, Color.Cyan);
                    Painter.ImportFromMatrix(Counter.RungeKutta(Counter.TestODU, 1, a, b, h, a, Counter.GetFirstValues(Counter.TestAccurate, a, 2)), "Runge2", Color.Red, Color.Yellow);
                    RefreshForm();
                    RefreshListBox();
                }
                catch
                {
                    MessageBox.Show("Введены неверные значения!!!");
                }
            }
        }

        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton2.Checked)
            {
                try
                {
                    Painter.Clear();
                    double a = double.Parse(aTextBox.Text);
                    double b = double.Parse(bTextBox.Text);
                    double h = double.Parse(hTextBox.Text);
                    Painter.ImportFromMatrix(Counter.CountErr(Counter.RungeKutta, Counter.TestODU, Counter.TestAccurate, a, b, a, Counter.GetFirstValues(Counter.TestAccurate, a, 2), 0), "Err1", Color.Red, Color.Yellow);
                    RefreshForm();
                    RefreshListBox();
                }
                catch
                {
                    MessageBox.Show("Введены неверные значения!!!");
                }
            }
        }

        private void firstPagePaint_MouseDown(object sender, MouseEventArgs e)
        {
            Painter.mouseDown = true;
            Painter.cursorXOld = Painter.cursorX;
            Painter.cursorYOld = Painter.cursorY;
            Painter.leftDownXOld = Painter.leftDownX;
            Painter.leftDownYOld = Painter.leftDownY;
        }

        private void firstPagePaint_MouseUp(object sender, MouseEventArgs e)
        {
            Painter.mouseDown = false;
            Painter.cursorXOld = 0;
            Painter.cursorYOld = 0;
            Painter.leftDownXOld = 0;
            Painter.leftDownYOld = 0;
        }

        private void radioButton1_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton1.Checked)
            {
                try
                {
                    Painter.Clear();
                    double a = double.Parse(aTextBox.Text);
                    double b = double.Parse(bTextBox.Text);
                    double h = double.Parse(hTextBox.Text);
                    Painter.ImportFromMatrix(Counter.CountErr(Counter.RungeKutta, Counter.TestODU, Counter.TestAccurate, a, b, a, Counter.GetFirstValues(Counter.TestAccurate, a, 2), 1), "Err1", Color.Red, Color.Yellow);
                    RefreshForm();
                    RefreshListBox();
                }
                catch
                {
                    MessageBox.Show("Введены неверные значения!!!");
                }
            }
        }

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            foreach(Curve i in Painter.curves)
            {
                if(i.name.Contains(listBox1.SelectedItem.ToString()))
                {
                    showDots.Checked = i.drawDots;
                    showCurve.Checked = i.drawCurve;
                }
            }
        }

        private void showDots_CheckedChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                foreach (Curve i in Painter.curves)
                {
                    if (i.name.Contains(listBox1.SelectedItem.ToString()))
                    {
                        i.drawDots = showDots.Checked;
                        RefreshForm();
                    }
                }
            }
        }

        private void showCurve_CheckedChanged(object sender, EventArgs e)
        {
            if (listBox1.SelectedItem != null)
            {
                foreach (Curve i in Painter.curves)
                {
                    if (i.name.Contains(listBox1.SelectedItem.ToString()))
                    {
                        i.drawCurve = showCurve.Checked;
                        RefreshForm();
                    }
                }
            }
        }

        private void trackBar1_Scroll(object sender, EventArgs e)
        {
            yLable.Text = "y: " + (trackBar1.Value / 10.0).ToString();
            TaskGraphic(trackBar2.Value / 10.0, trackBar1.Value / 10.0);
        }

        private void radioButton3_CheckedChanged(object sender, EventArgs e)
        {
            if(radioButton3.Checked)
            {
                Counter.beta2 = 3;
                TaskGraphic(trackBar2.Value / 10.0, trackBar1.Value / 10.0);
            }
        }

        private void radioButton4_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton4.Checked)
            {
                Counter.beta2 = 3.48;
                TaskGraphic(trackBar2.Value / 10.0, trackBar1.Value / 10.0);
            }
        }

        private void radioButton5_CheckedChanged(object sender, EventArgs e)
        {
            if (radioButton5.Checked)
            {
                Counter.beta2 = 5;
                TaskGraphic(trackBar2.Value / 10.0, trackBar1.Value / 10.0);
            }
        }

        private void TaskGraphic (double x0, double y0)
        {
            Painter.Clear();
            if (twoFuncButton.Checked)
            {
                Painter.ImportFromMatrix(Counter.RungeKutta(Counter.TaskODU, 0, 0, 10, 0.1, 0, new double[2] { x0, y0 }), "Leiko", Color.Purple, Color.Yellow);
                Painter.ImportFromMatrix(Counter.RungeKutta(Counter.TaskODU, 1, 0, 10, 0.1, 0, new double[2] { x0, y0 }), "Cancer", Color.Crimson, Color.Blue);
            }
            else if (xySystemButton.Checked)
            {
                Painter.ImportFromMatrix(Counter.ConvertToXY(Counter.RungeKutta(Counter.TaskODU, 0, 0, 10, 0.1, 0, new double[2] { x0, y0 }), Counter.RungeKutta(Counter.TaskODU, 1, 0, 10, 0.1, 0, new double[2] { x0, y0 })), "XY", Color.Purple, Color.Yellow);
            }
            RefreshListBox();
            RefreshForm();
        }

        private void trackBar2_Scroll(object sender, EventArgs e)
        {
            xLable.Text = "x: " + (trackBar2.Value / 10.0).ToString();
                TaskGraphic(trackBar2.Value / 10.0, trackBar1.Value / 10.0);
        }

        private void twoFuncButton_CheckedChanged(object sender, EventArgs e)
        {
            TaskGraphic(trackBar2.Value / 10.0, trackBar1.Value / 10.0);
        }

        private void xySystemButton_CheckedChanged(object sender, EventArgs e)
        {
            TaskGraphic(trackBar2.Value / 10.0, trackBar1.Value / 10.0);
        }
    }
}

