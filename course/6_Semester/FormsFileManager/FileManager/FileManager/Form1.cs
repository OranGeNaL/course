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
    public partial class Form1 : Form
    {
        private List<FileInView> filesInView = new List<FileInView>();
        private string currentDirectory = @"c:\Windows";


        public Form1()
        {
            InitializeComponent();
            /*foreach (var i in Directory.GetFiles(@"c:\"))
                MessageBox.Show(i);*/
            RewatchDirectory();
            UpdateView();
        }

        public void UpdateView()
        {
            fileViewer.ColumnCount = fileViewer.Width / 115;
            //MessageBox.Show(fileViewer.ColumnCount.ToString());
            //foreach(var i in fileViewer.Col)
        }

        private void RewatchDirectory()
        {
            textBox1.Text = currentDirectory;

            filesInView.Clear();
            foreach (var i in Directory.GetFiles(currentDirectory))
            {
                filesInView.Add(new FileInView(i, this));
            }
        }

        private void fileViewer_Resize(object sender, EventArgs e)
        {
            UpdateView();
        }
    }
}
