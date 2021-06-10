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

        private List<DirectoryInView> directoriesToCopy = new List<DirectoryInView>();
        private List<FileInView> filesToCopy = new List<FileInView>();
        
        private List<DirectoryInView> directoriesToMove = new List<DirectoryInView>();
        private List<FileInView> filesToMove = new List<FileInView>();

        private string currentDirectory = @"C:\Users\pshen\playground";

        private List<FileInView> selectedFiles = new List<FileInView>();
        private FileInView selectedFile = null;

        private List<DirectoryInView> selectedDirectories = new List<DirectoryInView>();
        private DirectoryInView selectedDirectory = null;

        private ArchiveForm archiveForm;
        private WinRarPathForm winRarForm;

        private ContextMenuStrip menuStrip;
        ToolStripMenuItem createFolderMenuItem;
        private ToolStripMenuItem pasteFileMenuItem;
        public Form1()
        {
            InitializeComponent();
            winRarForm = new WinRarPathForm();

            winRarForm.ShowDialog();
            menuStrip = new ContextMenuStrip();

            createFolderMenuItem = new ToolStripMenuItem("Создать папку");
            pasteFileMenuItem = new ToolStripMenuItem("Вставить");
            pasteFileMenuItem.Enabled = false;
            
            menuStrip.Items.Add(createFolderMenuItem);
            menuStrip.Items.Add(pasteFileMenuItem);

            createFolderMenuItem.Click += CreateFolderMenyItem_Click;
            pasteFileMenuItem.Click += PasteFileMenuItemOnClick;

            fileViewer.ContextMenuStrip = menuStrip;

            /*foreach (var i in Directory.GetFiles(@"c:\"))
                MessageBox.Show(i);*/
            RewatchDirectory();
            UpdateView();
        }

        private void PasteFileMenuItemOnClick(object sender, EventArgs e)
        {
            if(filesToCopy.Count > 0 || directoriesToCopy.Count > 0)
            {
                //MessageBox.Show("Copy");
                foreach (var i in filesToCopy)
                {
                    File.Copy(i.FullName, currentDirectory + "\\" + i.ViewName, true);
                }

                foreach (var i in directoriesToCopy)
                {
                    // MessageBox.Show(i.FullName);
                    // MessageBox.Show(currentDirectory + "\\" + i.ViewName);
                    CopyFilesRecursively(i.FullName, currentDirectory + "\\" + i.ViewName);
                }
            }
            
            else if(filesToMove.Count > 0 || directoriesToMove.Count > 0)
            {
                //MessageBox.Show("Move");
                foreach (var i in filesToMove)
                {
                    File.Copy(i.FullName, currentDirectory + "\\" + i.ViewName, true);
                    File.Delete(i.FullName);
                }

                foreach (var i in directoriesToMove)
                {
                    CopyFilesRecursively(i.FullName, currentDirectory + "\\" + i.ViewName);
                    Directory.Delete(i.FullName, true);
                }
                
                filesToMove.Clear();
                directoriesToMove.Clear();
            }
            
            RewatchDirectory();
        }

        private static void CopyFilesRecursively(string sourcePath, string targetPath)
        {
            Directory.CreateDirectory(targetPath);
            //Now Create all of the directories
            foreach (string dirPath in Directory.GetDirectories(sourcePath, "*", SearchOption.AllDirectories))
            {
                MessageBox.Show(dirPath.Replace(sourcePath, targetPath));
                Directory.CreateDirectory(dirPath.Replace(sourcePath, targetPath));
            }

            //Copy all the files & Replaces any files with the same name
            foreach (string newPath in Directory.GetFiles(sourcePath, "*.*",SearchOption.AllDirectories))
            {
                File.Copy(newPath, newPath.Replace(sourcePath, targetPath), true);
            }
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
        
        private void CreateFolderMenyItem_Click(object sender, EventArgs e)
        {
            CreateFolderDialog cf = new CreateFolderDialog();
            
            cf.ShowDialog();
            if (cf.DialogResult == DialogResult.OK)
            {
                Directory.CreateDirectory(currentDirectory + "\\" + cf.folderNameRes);
                //MessageBox.Show(currentDirectory + "\\" + cf.folderNameRes);
                RewatchDirectory();
            }
        }

        public void SetFilesToCopy()
        {
            if(selectedFiles.Count == 0 && selectedDirectories.Count == 0)
                return;
            
            filesToCopy.Clear();
            directoriesToCopy.Clear();
            filesToMove.Clear();
            directoriesToMove.Clear();
            
            string filesList = "";
            
            foreach (var i in selectedFiles)
            {
                filesToCopy.Add(i);
                filesList += i.FullName + "\n";
            }

            foreach (var i in selectedDirectories)
            {
                directoriesToCopy.Add(i);
                filesList += i.FullName + "\n";
            }

            pasteFileMenuItem.Enabled = true;
            //MessageBox.Show(filesList);
        }
        
        public void SetFilesToMove()
        {
            if (selectedFiles.Count == 0 && selectedDirectories.Count == 0)
                return;

            filesToCopy.Clear();
            directoriesToCopy.Clear();
            filesToMove.Clear();
            directoriesToMove.Clear();
            
            string filesList = "";
            
            foreach (var i in selectedFiles)
            {
                filesToMove.Add(i);
                filesList += i.FullName + "\n";
            }

            foreach (var i in selectedDirectories)
            {
                directoriesToMove.Add(i);
                filesList += i.FullName + "\n";
            }
            pasteFileMenuItem.Enabled = true;
            //MessageBox.Show(filesList);
        }
    }
}
