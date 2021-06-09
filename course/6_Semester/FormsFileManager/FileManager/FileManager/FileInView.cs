using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;
using System.IO;

namespace FileManager
{
    public class FileInView
    {
        public string FullName { get; set; }
        public string ViewName { get; set; }

        private  bool selected = false;
        public bool Selected { get { return selected; } set
            {
                if(value == false)
                    filePanel.BackColor = Color.Transparent;
                selected = value;
            } 
        }

        /*private delegate void DirStateChangedHandler(string message);
        event DirStateChangedHandler DirStateChanged;*/

        private delegate void DeleteFileHandler();
        event DeleteFileHandler Deleted;

        private delegate void SelectFileHandler(FileInView file);
        event SelectFileHandler Select;

        private delegate void SelectFilesHandler(FileInView file);
        event SelectFilesHandler SelectAdditional;

        private delegate void OpenArchiveHandler(string name);
        event OpenArchiveHandler OpenArchive;

        Form1 mainForm;

        Panel filePanel;
        PictureBox fileIcon;
        Label fileName;

        ContextMenuStrip menuStrip;

        public FileInView() { }
        public FileInView(string fullname, Form1 parent)
        {
            FullName = fullname;
            mainForm = parent;
            ViewName = FullName.Substring(FullName.LastIndexOf('\\') + 1);

            Deleted += mainForm.DeleteFile;
            Select += mainForm.SelectFile;
            SelectAdditional += mainForm.SelectFiles;
            OpenArchive += mainForm.OpenArchive;


            filePanel = new Panel();
            fileIcon = new PictureBox();
            fileName = new Label();

            fileIcon.Image = Image.FromFile("file-icon.png");
            fileIcon.SizeMode = PictureBoxSizeMode.Zoom;

            filePanel.Size = new Size(100, 115);
            filePanel.Controls.Add(fileIcon);
            filePanel.Controls.Add(fileName);
            filePanel.Anchor = AnchorStyles.Top;

            fileName.Dock = DockStyle.Bottom;
            fileName.AutoSize = false;
            fileName.Size = new Size(100, 40);
            fileName.Text = ViewName;
            fileName.TextAlign = ContentAlignment.MiddleCenter;

            fileIcon.Dock = DockStyle.Fill;

            menuStrip = new ContextMenuStrip();

            ToolStripMenuItem openMenyItem = new ToolStripMenuItem("Открыть");
            ToolStripMenuItem copyMenyItem = new ToolStripMenuItem("Копировать");
            ToolStripMenuItem cutMenyItem = new ToolStripMenuItem("Вырезать");
            ToolStripMenuItem deleteMenyItem = new ToolStripMenuItem("Удалить");
            ToolStripMenuItem addToArchiveMenuItem = new ToolStripMenuItem("Добавить в архив");
            ToolStripMenuItem openArchMenyItem = new ToolStripMenuItem("Открыть как архив");

            if(Path.GetExtension(ViewName) == ".rar" ||
                Path.GetExtension(ViewName) == ".zip" ||
                Path.GetExtension(ViewName) == ".7zip")
            {
                menuStrip.Items.Add(openArchMenyItem);
                openArchMenyItem.Click += OpenArchMenyItem_Click;
                fileIcon.Image = Image.FromFile("archive-icon.png");
            }

            openMenyItem.Click += OpenMenyItem_Click;
            deleteMenyItem.Click += DeleteMenyItem_Click;
            copyMenyItem.Click += CopyMenyItemOnClick;
            addToArchiveMenuItem.Click += AddToArchiveMenuItem_Click;
            cutMenyItem.Click += CutMenyItemOnClick;

            menuStrip.Items.AddRange(new[] {openMenyItem, copyMenyItem, cutMenyItem, deleteMenyItem, addToArchiveMenuItem});

            fileIcon.ContextMenuStrip = menuStrip;
            fileName.ContextMenuStrip = menuStrip;

            mainForm.Controls.Find("fileViewer", true)[0].Controls.Add(filePanel);


            fileIcon.MouseEnter += File_MouseEnter;
            fileName.MouseEnter += File_MouseEnter;
            filePanel.MouseEnter += File_MouseEnter;

            fileIcon.MouseLeave += File_MouseLeave;
            fileName.MouseLeave += File_MouseLeave;
            filePanel.MouseLeave += File_MouseLeave;

            fileIcon.MouseClick += File_MouseClick;
            fileName.MouseClick += File_MouseClick;
            filePanel.MouseClick += File_MouseClick;


            fileIcon.MouseDoubleClick += File_MouseDoubleClick;
            fileName.MouseDoubleClick += File_MouseDoubleClick;
        }

        private void CutMenyItemOnClick(object sender, EventArgs e)
        {
            mainForm.SetFilesToMove();
        }

        private void CopyMenyItemOnClick(object sender, EventArgs e)
        {
            mainForm.SetFilesToCopy();
        }

        private void OpenArchMenyItem_Click(object sender, EventArgs e)
        {
            OpenArchive(FullName);
        }

        private void File_MouseClick(object sender, MouseEventArgs e)
        {
            Selected = true;
            if ((Control.ModifierKeys & Keys.Control) == Keys.Control)
                SelectAdditional(this);
            else
                Select(this);
        }
        private void AddToArchiveMenuItem_Click(object sender, EventArgs e)
        {
            //DirStateChanged("DELETE");
            Selected = true;
            Select(this);
            mainForm.ArchiveSelected();
        }
        private void DeleteMenyItem_Click(object sender, EventArgs e)
        {
            //DirStateChanged("DELETE");
            Selected = true;
            Select(this);
            Deleted();
        }

        private void OpenMenyItem_Click(object sender, EventArgs e)
        {
            Process.Start(FullName);
        }

        private void File_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            Process.Start(FullName);
        }

        private void File_MouseLeave(object sender, EventArgs e)
        {
            if(!Selected)
                filePanel.BackColor = Color.Transparent;
        }

        private void File_MouseEnter(object sender, EventArgs e)
        {
            filePanel.BackColor = Color.LightSkyBlue;
        }

        public void RemoveFromView()
        {
            mainForm.Controls.Find("fileViewer", true)[0].Controls.Remove(filePanel);
        }
    }
}
