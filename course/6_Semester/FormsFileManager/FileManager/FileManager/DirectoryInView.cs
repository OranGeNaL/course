using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public class DirectoryInView
    {
        public string FullName { get; set; }
        public string ViewName { get; set; }
        
        private  bool selected = false;
        public bool Selected { get { return selected; } set
            {
                if(value == false)
                    directoryPanel.BackColor = Color.Transparent;
                selected = value;
            } 
        }

        Form1 mainForm;

        Panel directoryPanel;
        PictureBox directoryIcon;
        Label directoryName;

        ContextMenuStrip menuStrip;
        
        private delegate void SelectDirectoryHandler(DirectoryInView file);
        event SelectDirectoryHandler Select;

        private delegate void SelectAdditionalDirectoryHandler(DirectoryInView file);
        event SelectAdditionalDirectoryHandler SelectAdditional;
        
        public DirectoryInView() { }
        public DirectoryInView(string fullname, Form1 parent)
        {
            FullName = fullname;
            mainForm = parent;
            ViewName = FullName.Substring(FullName.LastIndexOf('\\') + 1);

            menuStrip = new ContextMenuStrip();

            directoryPanel = new Panel();
            directoryIcon = new PictureBox();
            directoryName = new Label();
            
            Select += mainForm.SelectDirectory;
            SelectAdditional += mainForm.SelectDirectories;

            directoryIcon.Image = Image.FromFile("folder-icon.png");
            directoryIcon.SizeMode = PictureBoxSizeMode.Zoom;

            directoryPanel.Size = new Size(100, 115);
            directoryPanel.Controls.Add(directoryIcon);
            directoryPanel.Controls.Add(directoryName);
            directoryPanel.Anchor = AnchorStyles.Top;

            directoryName.Dock = DockStyle.Bottom;
            directoryName.AutoSize = false;
            directoryName.Size = new Size(100, 40);
            directoryName.Text = ViewName;
            directoryName.TextAlign = ContentAlignment.MiddleCenter;

            directoryIcon.Dock = DockStyle.Fill;
            
            ToolStripMenuItem openMenyItem = new ToolStripMenuItem("Открыть");
            ToolStripMenuItem copyMenyItem = new ToolStripMenuItem("Копировать");
            ToolStripMenuItem cutMenyItem = new ToolStripMenuItem("Вырезать");
            ToolStripMenuItem deleteMenyItem = new ToolStripMenuItem("Удалить");
            ToolStripMenuItem addToArchiveMenuItem = new ToolStripMenuItem("Добавить в архив");
            
            menuStrip.Items.AddRange(new[] {openMenyItem, copyMenyItem, cutMenyItem, deleteMenyItem, addToArchiveMenuItem});
            
            directoryIcon.ContextMenuStrip = menuStrip;
            directoryName.ContextMenuStrip = menuStrip;
            addToArchiveMenuItem.Click += AddToArchiveMenuItem_Click;

            mainForm.Controls.Find("fileViewer", true)[0].Controls.Add(directoryPanel);


            directoryIcon.MouseEnter += Directory_MouseEnter;
            directoryName.MouseEnter += Directory_MouseEnter;
            directoryPanel.MouseEnter += Directory_MouseEnter;

            directoryIcon.MouseLeave += Directory_MouseLeave;
            directoryName.MouseLeave += Directory_MouseLeave;
            directoryPanel.MouseLeave += Directory_MouseLeave;
            
            directoryIcon.MouseClick += Directory_MouseClick;
            directoryName.MouseClick += Directory_MouseClick;
            directoryPanel.MouseClick += Directory_MouseClick;


            directoryIcon.MouseDoubleClick += Directory_MouseDoubleClick;
        }

        private void Directory_MouseClick(object sender, MouseEventArgs e)
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
        private void Directory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            mainForm.ChangeDirectoryDirectly(FullName);
        }

        private void Directory_MouseLeave(object sender, EventArgs e)
        {
            if(!Selected)
                directoryPanel.BackColor = Color.Transparent;
        }

        private void Directory_MouseEnter(object sender, EventArgs e)
        {
            directoryPanel.BackColor = Color.LightSkyBlue;
        }

        public void RemoveFromView()
        {
            mainForm.Controls.Find("fileViewer", true)[0].Controls.Remove(directoryPanel);
        }
    }
}
