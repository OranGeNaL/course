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
        private static string archive;
        private static Process rarProc;
        private static string tempListFile;

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

        public static void ArchiveSelected(List<FileInView> files, List<DirectoryInView> directories)
        {
            string enumeration = "";
            
            foreach (var i in files)
            {
                enumeration += i.FullName + '\n';
            }
            
            foreach (var i in directories)
            {
                enumeration += i.FullName + '\n';
            }

            CreateTempListFile(enumeration);
            
            rarProc.StartInfo.UseShellExecute = false;
            rarProc.StartInfo.RedirectStandardOutput = true;
            
            //CreateTempListFile(listContent);
            rarProc.StartInfo.FileName = Settings.winrarPath;
            rarProc.StartInfo.Arguments = " A " + archive + " " + " @" + tempListFile;
            //MessageBox.Show(rarProc.StartInfo.Arguments);
            rarProc.Start();

            MessageBox.Show(rarProc.StandardOutput.ReadToEnd());

            DeleteTempListFile();
            //MessageBox.Show(enumeration);
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
        
        public static void SetArchiveToCreate(string name)
        {
            DirectoryInfo info = new DirectoryInfo(name);
            archive = name + "\\" + info.Name + ".rar";
            
            rarProc = new Process();
            rarProc.StartInfo.FileName = Settings.winrarPath;

            tempListFile = "temp_" + info.Name + ".txt";

            //GetArchiveContent();
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

        
        private static void CreateTempListFile(string content)
        {
            var file = File.OpenWrite(tempListFile);

            Byte[] info = new UTF8Encoding(true).GetBytes(content);

            file.Write(info, 0, info.Length);
            file.Close();
        }

        private static void DeleteTempListFile()
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

            //MessageBox.Show(rarProc.StandardOutput.ReadToEnd());
            DeleteTempListFile();
            
            Close();
        }

        private void button2_Click(object sender, EventArgs e) //Извлечь всё
        {
            FileInfo info = new FileInfo(archive);

            rarProc.StartInfo.Arguments = " x " + archive + " " + archive.Replace(info.Extension, "\\");// + " -n@" + tempListFile;
            //MessageBox.Show(rarProc.StartInfo.Arguments);
            rarProc.Start();

            //MessageBox.Show(rarProc.StandardOutput.ReadToEnd());
            
            Close();
        }
    }
}
