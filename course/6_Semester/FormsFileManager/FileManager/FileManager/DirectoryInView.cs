using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    class DirectoryInView
    {
        public string FullName { get; set; }
        public string ViewName { get; set; }

        Form1 mainForm;

        Panel directoryPanel;
        PictureBox directoryIcon;
        Label directoryName;

        public DirectoryInView() { }
        public DirectoryInView(string fullname, Form1 parent)
        {
            FullName = fullname;
            mainForm = parent;
            ViewName = FullName.Substring(FullName.LastIndexOf('\\') + 1);


            directoryPanel = new Panel();
            directoryIcon = new PictureBox();
            directoryName = new Label();

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

            mainForm.Controls.Find("fileViewer", true)[0].Controls.Add(directoryPanel);


            directoryIcon.MouseEnter += Directory_MouseEnter;
            directoryName.MouseEnter += Directory_MouseEnter;
            directoryPanel.MouseEnter += Directory_MouseEnter;

            directoryIcon.MouseLeave += Directory_MouseLeave;
            directoryName.MouseLeave += Directory_MouseLeave;
            directoryPanel.MouseLeave += Directory_MouseLeave;


            directoryIcon.MouseDoubleClick += Directory_MouseDoubleClick;

        }


        private void Directory_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            mainForm.ChangeDirectoryDirectly(FullName);
        }

        private void Directory_MouseLeave(object sender, EventArgs e)
        {
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
