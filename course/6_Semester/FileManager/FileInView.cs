using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileManager
{
    public class FileInView
    {
        public string Name { get; private set; }
        public string Type { get; private set; }
        public string Size { get; private set; }

        public FileInView() { }
        public FileInView(string name)
        {
            Name = name;
            Type = "file";
            Size = "0 B";
        }
        public FileInView(string name, string type, string size)
        {
            Name = name;
            Type = type;
            Size = size;
        }
    }
}
