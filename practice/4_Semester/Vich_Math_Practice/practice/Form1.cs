﻿using System;
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

namespace practice
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            //DoubleBuffered = true;
            //Painter.bitmap = new Bitmap(firstPagePaint.Width, firstPagePaint.Height);
            //Painter.panel1Graphics = firstPagePaint.CreateGraphics();
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
            DoubleBuffered = true;
            Graphics graphics = firstPagePaint.CreateGraphics();
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            Pen blackPen = new Pen(Color.Black, 1);
            Pen greenPen = new Pen(Color.Green, 2);

            graphics.DrawLine(blackPen, new Point(Painter.leftDownX, 0), new Point(Painter.leftDownX, Painter.SCREEN_HEIGHT));
            graphics.DrawLine(blackPen, new Point(0, Painter.leftDownY), new Point(Painter.SCREEN_WIDTH, Painter.leftDownY));

            graphics.DrawLine(blackPen, new Point(Painter.cursorX, 0), new Point(Painter.cursorX, Painter.SCREEN_HEIGHT));
            graphics.DrawLine(blackPen, new Point(0, Painter.cursorY), new Point(Painter.SCREEN_WIDTH, Painter.cursorY));

            //for (int i = -Painter.SCREEN_WIDTH; i < Painter.SCREEN_WIDTH; i += (int)(Painter.SCREEN_DIV_VALUE * Painter.scale))
            for (int i = -(int)(Painter.SCREEN_WIDTH - Painter.SCREEN_WIDTH % (Painter.SCREEN_DIV_VALUE * Painter.scale)); i < Painter.SCREEN_WIDTH; i += (int)(Painter.SCREEN_DIV_VALUE * Painter.scale))
            {
                graphics.DrawLine(blackPen, new Point(Painter.leftDownX - 10, Painter.leftDownY - i), new Point(Painter.leftDownX + 10, Painter.leftDownY - i));
                graphics.DrawLine(blackPen, new Point(Painter.leftDownX - i, Painter.leftDownY - 10), new Point(Painter.leftDownX - i, Painter.leftDownY + 10));
            }

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

        }

        private void divisionValueScroll_Scroll(object sender, EventArgs e)
        {
            Painter.divisionValue = divisionValueScroll.Value / 1000.0;
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
            //(_X * Painter.SCREEN_DIV_VALUE * Painter.scale / Painter.divisionValue)

            cursorYLable.Text = ((double)(Painter.leftDownY - Painter.cursorY) / Painter.SCREEN_DIV_VALUE / Painter.scale * Painter.divisionValue).ToString();

            Refresh();
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
                    

                    Painter.ImportFromMatrix(Counter.AccurateCount(Counter.TestAccurate, 0, a, b, h), /*"Первая тестовая функция"*/"Test1" , Color.Red, Color.Green);
                    Painter.ImportFromMatrix(Counter.AccurateCount(Counter.TestAccurate, 1, a, b, h), /*"Вторая тестовая функция"*/"Test2" , Color.Purple, Color.Blue);
                    Refresh();
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
            Refresh();
        }

        private void clearButton_Click(object sender, EventArgs e)
        {
            Painter.Clear();
            Refresh();
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
                    Refresh();
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
            Refresh();
        }

        private void checkBox3_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Curve i in Painter.curves)
            {
                if (i.name.Contains("Runge"))
                    i.drawDots = checkBox3.Checked;
            }
            Refresh();
        }

        private void checkBox4_CheckedChanged(object sender, EventArgs e)
        {
            foreach (Curve i in Painter.curves)
            {
                if (i.name.Contains("Runge"))
                    i.drawCurve = checkBox4.Checked;
            }
            Refresh();
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
                    Refresh();
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
                    Painter.ImportFromMatrix(Counter.CountErr(Counter.RungeKutta, Counter.TestODU, Counter.TestAccurate, a, b, a, Counter.GetFirstValues(Counter.TestAccurate, a, 2)), "Err1", Color.Red, Color.Yellow);
                    Refresh();
                }
                catch
                {
                    MessageBox.Show("Введены неверные значения!!!");
                }
            }
        }
    }
}

