using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Drawing;

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
            fileName.Text = FullName;
            fileName.TextAlign = ContentAlignment.MiddleCenter;

            fileIcon.Dock = DockStyle.Fill;

            mainForm.Controls.Find("fileViewer", true)[0].Controls.Add(filePanel);

        }
    }
}
