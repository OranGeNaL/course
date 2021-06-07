using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace FileManager
{
    public partial class ArchiveForm : Form
    {
        private string archive;
        private Process rarProc;
        private string tempListFile;

        public ArchiveForm()
        {
            InitializeComponent();
            rarProc = new Process();
        }

        private void ArchiveForm_Load(object sender, EventArgs e)
        {

        }

        private void GetArchiveContent()
        {
            rarProc.StartInfo.Arguments = "/C lb " + archive;
            //rarProc.StartInfo.StandardOutputEncoding = Encoding.UTF32;
            rarProc.StartInfo.UseShellExecute = false;
            rarProc.StartInfo.RedirectStandardOutput = true;

            rarProc.Start();
            foreach(var i in ParseAnswer(rarProc.StandardOutput.ReadToEnd()))
            {
                listBox1.Items.Add(i);
            }
        }

        private List<string> ParseAnswer(string answer)
        {
            List<string> result = new List<string>();

            foreach(var i in answer.Split(new char[] { '\n' }, StringSplitOptions.RemoveEmptyEntries))
            {
                result.Add(i.Trim());
            }

            return result;
        }

        public void SetArchive(string name)
        {
            archive = name;
            Text = name;

            rarProc.StartInfo.FileName = Settings.winrarPath;

            FileInfo info = new FileInfo(name);
            tempListFile = "temp_" + info.Name.Replace(info.Extension, ".txt");

            GetArchiveContent();
        }

        private List<string> GetSelectedNames()
        {
            List<string> elems = new List<string>();
            foreach(var i in listBox1.SelectedItems)
            {
                elems.Add(i.ToString());
            }

            return elems;
        }

        
        private void CreateTempListFile(string content)
        {
            var file = File.OpenWrite(tempListFile);

            Byte[] info = new UTF8Encoding(true).GetBytes(content);

            file.Write(info, 0, info.Length);
            file.Close();
        }

        private void DeleteTempListFile()
        {
            File.Delete(tempListFile);
        }

        private void button1_Click(object sender, EventArgs e) //Извлечь выбранное
        {
            string listContent = "";
            foreach (var i in GetSelectedNames())
                listContent += i + '\n';

            FileInfo info = new FileInfo(archive);

            CreateTempListFile(listContent);
            rarProc.StartInfo.Arguments = " x " + archive + " " + archive.Replace(info.Extension, "\\") + " -n@" + tempListFile;
            rarProc.Start();

            MessageBox.Show(rarProc.StandardOutput.ReadToEnd());
            DeleteTempListFile();
        }

        private void button2_Click(object sender, EventArgs e) //Извлечь всё
        {
            FileInfo info = new FileInfo(archive);

            rarProc.StartInfo.Arguments = " x " + archive + " " + archive.Replace(info.Extension, "\\");// + " -n@" + tempListFile;
            //MessageBox.Show(rarProc.StartInfo.Arguments);
            rarProc.Start();

            //MessageBox.Show(rarProc.StandardOutput.ReadToEnd());

            //DeleteTempListFile();
        }
    }
}
