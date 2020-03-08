using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GrafBuilder
{
    public partial class Form1 : Form
    {

        Pen[] pen = new Pen[] { new Pen(Color.Black, 2), new Pen(Color.Red, 2), new Pen(Color.Blue, 2), new Pen(Color.Green, 2), new Pen(Color.Purple, 2), new Pen(Color.Yellow, 2), new Pen(Color.Pink, 2), new Pen(Color.Violet, 2), new Pen(Color.Brown, 2), new Pen(Color.Magenta, 2) };

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void Add_dot_Click(object sender, EventArgs e)
        {
            try
            {
                int x = int.Parse(textBox1.Text);
                int y = int.Parse(textBox2.Text);
                Main.dotCount++;
                Main.dots.Add(new Dot(x, y, Main.dotCount));
            }
            catch
            {
                MessageBox.Show("Введены неверные значения!!!");
            }

            RefreshForm();
        }

        void RefreshForm()
        {
            panel1.Invalidate();

            if (Main.dotCount == Main.dotMax)
                add_dot.Enabled = !(Main.dotCount == Main.dotMax);

            string dotList = "";
            for (int i = 0; i < Main.dots.Count(); i++)
            {
                Dot temp = Main.dots[i];
                dotList += temp.DotInd.ToString() + ".    x:" + temp.DotX.ToString() + "    y:" + temp.DotY.ToString() + "\n";
            }

            string lineList = "";
            for (int i = 0; i < Main.lines.Count(); i++)
            {
                Line temp = Main.lines[i];
                lineList += temp.line_ind.ToString() + ".    " + temp.dot1.DotInd.ToString() + "   ->   " + temp.dot2.DotInd.ToString() + "\n";
            }

            label4.Text = lineList;
            label1.Text = dotList;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphics = panel1.CreateGraphics();

            for (int i = 0; i < Main.dots.Count(); i++) // Отрисовка точек
            {
                Dot temp = Main.dots[i];
                graphics.DrawRectangle(pen[i], temp.DotX, temp.DotY, 2, 2);
            }

            for (int i = 0; i < Main.lines.Count(); i++) // Отрисовка линий
            {
                Line temp = Main.lines[i];
                graphics.DrawLine(new Pen(Color.Black, 1), temp.dot1.DotX, temp.dot1.DotY, temp.dot2.DotX, temp.dot2.DotY);
            }
        }

        private void Add_line_Click(object sender, EventArgs e)
        {
            try
            {
                int first = int.Parse(textBox3.Text);
                int second = int.Parse(textBox4.Text);

                if (first > 0 && first <= Main.dotCount && second > 0 && second <= Main.dotCount && first != second)
                {
                    Main.line_count++;
                    Main.lines.Add(new Line(FindByInd(first), FindByInd(second), Main.line_count));
                }
                else
                    MessageBox.Show("Введены неверные значения!!!");
            }
            catch
            {
                MessageBox.Show("Введены неверные значения!!!");
            }

            RefreshForm();
        }

        private Dot FindByInd(int ind)
        {
            Dot temp = new Dot(0, 0, 0);
            for (int i = 0; i < Main.dots.Count(); i++)
            {
                if (Main.dots[i].DotInd == ind)
                {
                    temp = Main.dots[i];
                }
            }
            return temp;
        }

        private void Panel1_MouseDown(object sender, MouseEventArgs e)
        {
            for (int i = 0; i < Main.dots.Count(); i++)
            {
                Dot temp = Main.dots[i];
                if (e.X <= temp.DotX + 4 && e.X >= temp.DotX - 4 && e.Y <= temp.DotY + 4 && e.Y >= temp.DotY - 4)
                {
                    Main.activeDotInd = temp.DotInd;
                    RefreshForm();
                    break;
                }
            }

        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Main.mouse_x = e.X;
            Main.mouse_y = e.Y;
            if (Main.activeDotInd != 0)
            {
                Dot temp = FindByInd(Main.activeDotInd);
                temp.DotX = e.X;
                temp.DotY = e.Y;
                RefreshForm();
            }
        }

        private void Panel1_MouseUp(object sender, MouseEventArgs e)
        {
            if (Main.activeDotInd != 0)
            {
                Main.activeDotInd = 0;
                RefreshForm();
            }
        }

        private void Panel1_MouseDoubleClick(object sender, MouseEventArgs e)
        {
              if (add_dot.Enabled)
              {
                  bool b = true;
                  for (int i = 0; i < Main.dots.Count(); i++)
                  {
                      Dot temp = Main.dots[i];
                      if (e.X <= temp.DotX + 4 && e.X >= temp.DotX - 4 && e.Y <= temp.DotY + 4 && e.Y >= temp.DotY - 4)
                      {
                          b = false;
                          break;
                      }
                  }
                  if (b)
                  {
                      Main.dotCount++;
                      Main.dots.Add(new Dot(e.X, e.Y, Main.dotCount));
                      RefreshForm();
                  }
              }

        }

        private void Clear_button_Click(object sender, EventArgs e)
        {
            Main.dots.Clear();
            Main.lines.Clear();
            Main.dotCount = 0;
            Main.line_count = 0;
            RefreshForm();
        }

        private void Remove_button_Click(object sender, EventArgs e)
        {
            try
            {
                int ind = int.Parse(textBox5.Text);
                RemoveADot(ind);
            }
            catch
            {
                MessageBox.Show("Введено неверное значение!!!");
            }
        }

        private void RemoveADot(int ind)
        {
            for(int i = 0; i < Main.lines.Count(); i++)
            {
                Line temp = Main.lines[i];
                if (temp.dot1.DotInd == ind || temp.dot2.DotInd == ind)
                {
                    Main.lines.Remove(Main.lines[i]);
                    Main.line_count--;
                    i--;
                }
            }
            for (int i = 0; i < Main.lines.Count(); i++)
            {
                Main.lines[i].line_ind = i + 1;
            }

            for (int i = 0; i < Main.dots.Count(); i++)
            {
                Dot temp = Main.dots[i];
                if (temp.DotInd == ind)
                {
                    Main.dotCount--;
                    Main.dots.Remove(Main.dots[i]);
                    break;
                }
            }
            for (int i = 0; i < Main.dots.Count(); i++)
            {
                Main.dots[i].DotInd = i + 1;
            }

            RefreshForm();
        }
    }
}
