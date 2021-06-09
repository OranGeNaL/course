using System;
using System.Windows.Forms;

namespace FileManager
{
    public partial class CreateFolderDialog : Form
    {
        public string folderNameRes;
        public CreateFolderDialog()
        {
            InitializeComponent();
            button1.Select();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (textBox1.Text != "")
            {
                folderNameRes = textBox1.Text;
                DialogResult = DialogResult.OK;
                Close();
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.Cancel;
            Close();
        }
    }
}