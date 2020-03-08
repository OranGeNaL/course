using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace GrafBuilder
{
    public partial class Form1 : Form
    {

        Pen[] pen = new Pen[] {
            new Pen(Color.DarkGoldenrod, 2),
            new Pen(Color.Black, 2),
            new Pen(Color.Red, 2),
            new Pen(Color.Blue, 2),
            new Pen(Color.Green, 2),
            new Pen(Color.Purple, 2),
            new Pen(Color.Yellow, 2),
            new Pen(Color.Pink, 2),
            new Pen(Color.Violet, 2),
            new Pen(Color.Brown, 2),
            new Pen(Color.Magenta, 2) };

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
            foreach (Dot x in Main.dots)
            {
                dotList += x.DotInd.ToString() + ".    x:" + x.DotX.ToString() + "    y:" + x.DotY.ToString() + "\n";
            }

            string lineList = "";
            foreach(Line x in Main.lines)
            {
                lineList += x.line_ind.ToString() + ".    " + x.dot1.DotInd.ToString() + "   ->   " + x.dot2.DotInd.ToString() + "\n";
            }

            label4.Text = lineList;
            label1.Text = dotList;
        }

        private void Panel1_Paint(object sender, PaintEventArgs e)
        {

            Graphics graphics = panel1.CreateGraphics();

            foreach(Dot x in Main.dots) // Отрисовка точек
            {
                graphics.DrawRectangle(pen[x.DotInd], x.DotX, x.DotY, 2, 2);
            }

            foreach(Line x in Main.lines) // Отрисовка линий
            {
                graphics.DrawLine(pen[x.dot2.DotInd], x.dot1.DotX, x.dot1.DotY, x.dot2.DotX, x.dot2.DotY);
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
                    Main.lineCount++;
                    Main.lines.Add(new Line(FindByInd(first), FindByInd(second), Main.lineCount));
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
            foreach(Dot x in Main.dots)
            {
                if (e.X <= x.DotX + 4 && e.X >= x.DotX - 4 && e.Y <= x.DotY + 4 && e.Y >= x.DotY - 4)
                {
                    Main.activeDotInd = x.DotInd;
                    RefreshForm();
                    break;
                }
            }

        }

        private void Panel1_MouseMove(object sender, MouseEventArgs e)
        {
            Main.mouseX = e.X;
            Main.mouseY = e.Y;
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
            Clear();
        }

        private void Remove_button_Click(object sender, EventArgs e)
        {
            try
            {
                int ind = int.Parse(textBox5.Text);
                RemoveDot(ind);
                add_dot.Enabled = true;
            }
            catch
            {
                MessageBox.Show("Введено неверное значение!!!");
            }
        }

        private void Clear()
        {
            Main.dots.Clear();
            Main.lines.Clear();
            Main.dotCount = 0;
            Main.lineCount = 0;
            RefreshForm();
        }

        private void RemoveDot(int ind)
        {
            for (int i = 0; i < Main.lines.Count(); i++)
            {
                Line temp = Main.lines[i];
                if (temp.dot1.DotInd == ind || temp.dot2.DotInd == ind)
                {
                    Main.lines.Remove(Main.lines[i]);
                    Main.lineCount--;
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

        private void exportButton_Click(object sender, EventArgs e)
        {

        }

        private void importButton_Click(object sender, EventArgs e)
        {
            string fileText = "";
            Form2 importForm = new Form2();
            importForm.Owner = this;
            importForm.ShowDialog();
            if(importForm.DialogResult == DialogResult.OK)
            {
                Clear();
                StreamReader file = new StreamReader(Main.sourcePath);
                try
                {
                    fileText = file.ReadToEnd();
                }
                catch
                {
                    MessageBox.Show("Введён неверный путь к файлу!");
                    return;
                }
            }

            Main.sourcePath = "";

            int n = 0;
            int m = 0;

            string[] splittedText = fileText.Split();

            n = int.Parse(splittedText[0]);
            m = int.Parse(splittedText[1]);

            int[,] matrix = new int[n, m];

            for(int i = 0; i < n; i++)
            {
                for(int j = 0; j < m; j++)
                {
                    matrix[i, j] = int.Parse(splittedText[2 + i * m + j]);
                }
            }

            Main.CreateDots(n);
            for(int j = 0; j < m; j++)
            {
                int ind1 = 0;
                int ind2 = 0;
                for(int i = 0; i < n; i++)
                {
                    if(matrix[i,j] == 1)
                    {
                        ind1 = i + 1;
                    }
                    if(matrix[i,j] == -1)
                    {
                        ind2 = i + 1;
                    }
                }
                Main.lines.Add(new Line(FindByInd(ind1), FindByInd(ind2), Main.lineCount));
                Main.lineCount++;
            }
            RefreshForm();
        }

        private void label2_Click(object sender, EventArgs e)
        {

        }
    }
}
