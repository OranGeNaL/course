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
        private List<string> dirHistoryBack = new List<string>();

        private string currentDirectory = @"C:\Users\pshen\Downloads\testing";

        private List<FileInView> selectedFiles = new List<FileInView>();
        private FileInView selectedFile = null;

        private List<DirectoryInView> selectedDirectories = new List<DirectoryInView>();
        private DirectoryInView selectedDirectory = null;

        private ArchiveForm archiveForm;
        private WinRarPathForm winRarForm;

        public Form1()
        {
            InitializeComponent();
            winRarForm = new WinRarPathForm();

            winRarForm.ShowDialog();

            /*foreach (var i in Directory.GetFiles(@"c:\"))
                MessageBox.Show(i);*/
            RewatchDirectory();
            UpdateView();
        }

        public void UpdateView()
        {
            fileViewer.ColumnCount = fileViewer.Width / 115;
            //MessageBox.Show(fileViewer.ColumnCount.ToString());
            //fileViewer.Style
            //MessageBox.Show(fileViewer.ColumnStyles.Count.ToString());

            foreach( ColumnStyle i in fileViewer.ColumnStyles)
            {
                i.SizeType = SizeType.Absolute;
                i.Width = 115;
            }    
            //MessageBox.Show(fileViewer.ColumnCount.ToString());
            //foreach(var i in fileViewer.Col)
        }

        private void RewatchDirectory()
        {
            textBox1.Text = currentDirectory;
            this.Text = currentDirectory;

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
            dirHistoryBack.Clear();

            RewatchDirectory();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if(dirHistory.Count > 0)
            {
                dirHistoryBack.Add(currentDirectory);
                currentDirectory = dirHistory.Last<string>();
                dirHistory.Remove(dirHistory.Last<string>());

                RewatchDirectory();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if(dirHistoryBack.Count > 0)
            {
                dirHistory.Add(currentDirectory);
                currentDirectory = dirHistoryBack.Last<string>();
                dirHistoryBack.Remove(dirHistoryBack.Last<string>());

                RewatchDirectory();
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {

        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (Directory.Exists(textBox1.Text) && e.KeyCode == Keys.Enter)
            {
                ChangeDirectoryDirectly(textBox1.Text.Substring(0, textBox1.Text.Length));
            }
        }

        public void DirStateChanged(string message)
        {
            RewatchDirectory();
        }
        
        public void DeleteFile()
        {
            if (selectedFiles.Count > 1)
                foreach (var i in selectedFiles)
                    File.Delete(i.FullName);
            else
                File.Delete(selectedFile.FullName);
            RewatchDirectory();
        }
        
        //Выбор файлов
        public void SelectFile(FileInView file)
        {
            if (!selectedFiles.Contains(file))
            {
                foreach (var i in selectedFiles)
                    i.Selected = false;
                selectedFiles.Clear();

                if (selectedFile != null)
                    selectedFile.Selected = false;
                
                foreach (var i in selectedDirectories)
                    i.Selected = false;
                selectedDirectories.Clear();

                if (selectedDirectory != null)
                    selectedDirectory.Selected = false;

                selectedFile = file;
                selectedFiles.Add(file);
            }
        }

        public void SelectFiles(FileInView file)
        {
            selectedFile = file;
            selectedFiles.Add(file);
        }

        //Выбор папок
        public void SelectDirectory(DirectoryInView directory)
        {
            if (!selectedDirectories.Contains(directory))
            {
                foreach (var i in selectedDirectories)
                    i.Selected = false;
                selectedDirectories.Clear();

                if (selectedDirectory != null)
                    selectedDirectory.Selected = false;
                
                foreach (var i in selectedFiles)
                    i.Selected = false;
                selectedFiles.Clear();

                if (selectedFile != null)
                    selectedFile.Selected = false;

                selectedDirectory = directory;
                selectedDirectories.Add(directory);
            }
        }

        public void SelectDirectories(DirectoryInView directory)
        {
            selectedDirectory = directory;
            selectedDirectories.Add(directory);
        }
        
        public void OpenArchive(string name)
        {
            if (Settings.WinrarPath == "")
                winRarForm.ShowDialog();

            archiveForm = new ArchiveForm();
            archiveForm.SetArchive(name);
            archiveForm.ShowDialog();

            RewatchDirectory();
            RewatchDirectory();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            ChangeDirectoryDirectly(currentDirectory.Substring(0, currentDirectory.LastIndexOf('\\')));
            //MessageBox.Show(currentDirectory.Substring(0, currentDirectory.LastIndexOf('\\')));
        }

        public void ArchiveSelected()
        {
            ArchiveForm.SetArchiveToCreate(currentDirectory);
            ArchiveForm.ArchiveSelected(selectedFiles, selectedDirectories);
            
            RewatchDirectory();
            RewatchDirectory();
        }
    }
}
