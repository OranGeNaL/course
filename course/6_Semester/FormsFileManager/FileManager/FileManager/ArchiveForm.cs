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
        private static string _archive;
        private static Process _rarProc;
        private static string _tempListFile;

        public ArchiveForm()
        {
            InitializeComponent();
            _rarProc = new Process();
        }

        private void ArchiveForm_Load(object sender, EventArgs e)
        {

        }

        private void GetArchiveContent()
        {
            string content = "";

            ProcessStartInfo si = new ProcessStartInfo();
            _rarProc.StartInfo.Arguments = " /C lb \"" + _archive + "\"";
            //MessageBox.Show(_rarProc.StartInfo.FileName + _rarProc.StartInfo.Arguments);
            //_rarProc.StartInfo.StandartInputEncoding = Encoding.UTF32;
            _rarProc.StartInfo.UseShellExecute = false;
            _rarProc.StartInfo.RedirectStandardOutput = true;

            _rarProc.Start();
            foreach(var i in ParseAnswer(_rarProc.StandardOutput.ReadToEnd()))
            {
                listBox1.Items.Add(i);
                content += i + "\n";
            }

            //MessageBox.Show(content);
        }

        public static void ArchiveSelected(List<FileInView> files, List<DirectoryInView> directories)
        {
            string enumeration = "";
            
            foreach (var i in files)
            {
                enumeration += "\"" + i.FullName + "\"" + '\n';
            }
            
            foreach (var i in directories)
            {
                enumeration += "\"" + i.FullName + "\"" + '\n';
            }

            CreateTempListFile(enumeration);
            
            _rarProc.StartInfo.UseShellExecute = false;
            _rarProc.StartInfo.RedirectStandardOutput = true;
            
            //CreateTempListFile(listContent);
            _rarProc.StartInfo.FileName = Settings.WinrarPath;
            _rarProc.StartInfo.Arguments = " A " + _archive + " " + " @" + _tempListFile;
            //MessageBox.Show(rarProc.StartInfo.Arguments);
            _rarProc.Start();

            //MessageBox.Show(_rarProc.StandardOutput.ReadToEnd());
            string shit = _rarProc.StandardOutput.ReadToEnd();

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
            _archive = name;
            Text = name;

            _rarProc.StartInfo.FileName = Settings.WinrarPath;

            FileInfo info = new FileInfo(name);
            _tempListFile = "temp_" + info.Name.Replace(info.Extension, ".txt");

            GetArchiveContent();
        }
        
        public static void SetArchiveToCreate(string name)
        {
            DirectoryInfo info = new DirectoryInfo(name);
            _archive = name + "\\" + info.Name + ".rar";
            
            _rarProc = new Process();
            _rarProc.StartInfo.FileName = Settings.WinrarPath;

            _tempListFile = "temp_" + info.Name + ".txt";

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
            var file = File.OpenWrite(_tempListFile);

            Byte[] info = new UTF8Encoding().GetBytes(content);

            file.Write(info, 0, info.Length);
            file.Close();
        }

        private static void DeleteTempListFile()
        {
            File.Delete(_tempListFile);
        }

        private void button1_Click(object sender, EventArgs e) //Извлечь выбранное
        {
            string listContent = "";
            foreach (var i in GetSelectedNames())
                listContent += i + '\n';

            FileInfo info = new FileInfo(_archive);

            CreateTempListFile(listContent);
            _rarProc.StartInfo.Arguments = " x \"" + _archive + "\" \"" + _archive.Replace(info.Extension, "\"\\") + " -n@" + _tempListFile;
            _rarProc.Start();

            //MessageBox.Show(rarProc.StandardOutput.ReadToEnd());
            string shit = _rarProc.StandardOutput.ReadToEnd();
            DeleteTempListFile();
            
            Close();
        }

        private void button2_Click(object sender, EventArgs e) //Извлечь всё
        {
            FileInfo info = new FileInfo(_archive);

            _rarProc.StartInfo.Arguments = " x \"" + _archive + "\" \"" + _archive.Replace(info.Extension, "\"\\");// + " -n@" + tempListFile;
            //MessageBox.Show(rarProc.StartInfo.Arguments);
            _rarProc.Start();

            //MessageBox.Show(rarProc.StandardOutput.ReadToEnd());
            string shit = _rarProc.StandardOutput.ReadToEnd();
            
            Close();
        }
    }
}
