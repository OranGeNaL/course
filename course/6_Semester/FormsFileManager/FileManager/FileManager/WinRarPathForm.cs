using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public partial class WinRarPathForm : Form
    {
        public WinRarPathForm()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.ShowDialog();
            string path = fileDialog.FileName;

            textBox1.Text = path;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (File.Exists(textBox1.Text))
            {
                FileInfo info = new FileInfo(textBox1.Text);
                if (info.Name.ToUpper() == "Rar.exe".ToUpper())
                { 
                    Settings.winrarPath = textBox1.Text;
                    this.Close(); 
                }
                else
                {
                    MessageBox.Show("Это не rar.exe");
                }
            }

            else
                MessageBox.Show("Файла не существует");
        }
    }
}
