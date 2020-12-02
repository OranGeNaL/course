using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard
{
    interface IWidget
    {
        public void CreateComponents();
        public void UpdateAppearance();
        public void Initialize();

        public void Process();
        public void Stop();

        public Guid ID { get; set; }
        public string Name { get; set; }
    }
}
