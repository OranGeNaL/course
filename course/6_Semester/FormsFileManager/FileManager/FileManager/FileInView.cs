using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

namespace FileManager
{
    class FileInView
    {
        public string FullName { get; set; }
        public string ViewName { get; set; }

        Form mainForm;

        Panel filePanel;
        PictureBox fileIcon;
        Label fileName;

        public FileInView() { }
        public FileInView(string fullname, Form parent)
        {
            FullName = fullname;
            mainForm = parent;
            ViewName = FullName.Substring(FullName.LastIndexOf('\\') + 1);


            filePanel = new Panel();
            fileIcon = new PictureBox();
            fileName = new Label();

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

            mainForm.Controls.Find("fileViewer", true)[0].Controls.Add(filePanel);


            fileIcon.MouseEnter += File_MouseEnter;
            fileName.MouseEnter += File_MouseEnter;
            filePanel.MouseEnter += File_MouseEnter;

            fileIcon.MouseLeave += File_MouseLeave;
            fileName.MouseLeave += File_MouseLeave;
            filePanel.MouseLeave += File_MouseLeave;


            fileIcon.MouseDoubleClick += File_MouseDoubleClick;

        }


        private void File_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            //throw new NotImplementedException();
            //Process.Start(FullName);
        }

        private void File_MouseLeave(object sender, EventArgs e)
        {
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
