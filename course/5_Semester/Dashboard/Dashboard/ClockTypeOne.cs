using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard
{
    class ClockTypeOne : IWidget
    {
        Form form;
        Thread process;
        //Point location;

        Panel panel = new Panel();
        Label time = new Label();
        Label date = new Label();
        Label day = new Label();

        public ClockTypeOne() { }
        public ClockTypeOne(Form _form)
        {
            form = _form;
            CreateComponents();

            process = new Thread(new ThreadStart(Process));
            process.Start();
        }

        ~ClockTypeOne()
        {
            Stop();
        }

        public void Initialize()
        {
            panel.Location = new Point(0, 0);

            date.Text = "11 ноября";
            date.Font = new Font("Rubik", 16, FontStyle.Regular);
            date.AutoSize = false;
            date.Size = new Size(panel.Width / 2, panel.Height / 2);
            date.Location = new Point(0, 0);
            date.TextAlign = ContentAlignment.BottomCenter;

            day.Text = "среда";
            day.Font = new Font("Rubik", 16, FontStyle.Regular);
            day.AutoSize = false;
            day.Size = new Size(panel.Width / 2, panel.Height / 2);
            day.Location = new Point(0, date.Size.Height);
            day.TextAlign = ContentAlignment.TopCenter;

            time.Text = "14:15";
            time.Font = new Font("Rubik", 36, FontStyle.Regular);
            time.AutoSize = false;
            time.Size = new Size(panel.Width / 2, panel.Height);
            time.Location = new Point(date.Width, 0);
            time.TextAlign = ContentAlignment.MiddleCenter;

            panel.Controls.Add(date);
            panel.Controls.Add(day);
            panel.Controls.Add(time);
        }

        public void CreateComponents()
        {
            panel.Size = new Size(form.Size.Width - 15, 100);
            Initialize();

            form.Controls.Add(panel);
        }

        public void UpdateAppearance()
        {
            panel.Size = new Size(form.Size.Width - 15, 100);

            date.Size = new Size(panel.Width / 2, panel.Height / 2);
            date.Location = new Point(0, 0);

            day.Size = new Size(panel.Width / 2, panel.Height / 2);
            day.Location = new Point(0, date.Size.Height);

            time.Size = new Size(panel.Width / 2, panel.Height);
            time.Location = new Point(date.Width, 0);
        }

        public void Process()
        {
            while (true)
            {
                SafeWriter.WriteTextSafe(DateTime.Now.ToShortTimeString(), time);
                SafeWriter.WriteTextSafe(DateTime.Now.Day.ToString() + " / " + DateTime.Now.Month.ToString(), date);
                SafeWriter.WriteTextSafe(DateTime.Now.DayOfWeek.ToString(), day);
                Thread.Sleep(3000);
            }
        }

        public void Stop()
        {
            process.Abort();
        }
    }
}
