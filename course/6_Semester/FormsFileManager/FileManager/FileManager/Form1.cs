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
        private List<DirectoryInView> directoriesInView = new List<DirectoryInView>();

        private List<string> dirHistory = new List<string>();

        private string currentDirectory = @"C:\Users\pshen\Downloads";

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
            foreach( ColumnStyle i in fileViewer.ColumnStyles)
            {
                i.Width = 115;
            }    
            //MessageBox.Show(fileViewer.ColumnCount.ToString());
            //foreach(var i in fileViewer.Col)
        }

        private void RewatchDirectory()
        {
            textBox1.Text = currentDirectory;

            foreach (var i in filesInView)
                i.RemoveFromView();
            filesInView.Clear();


            foreach (var i in directoriesInView)
                i.RemoveFromView();
            directoriesInView.Clear();

            foreach (var i in Directory.GetDirectories(currentDirectory))
            {
                directoriesInView.Add(new DirectoryInView(i, this));
            }

            foreach (var i in Directory.GetFiles(currentDirectory))
            {
                filesInView.Add(new FileInView(i, this));
            }

        }

        private void fileViewer_Resize(object sender, EventArgs e)
        {
            UpdateView();
        }


        public void ChangeDirectoryDirectly(string newDName)
        {
            dirHistory.Add(currentDirectory);
            currentDirectory = newDName;

            RewatchDirectory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            currentDirectory = dirHistory.Last<string>();
            dirHistory.Remove(dirHistory.Last<string>());

            RewatchDirectory();
        }
    }
}
