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
        Control form;
        Thread process;
        Point location;

        int defaultWidth;

        Panel panel = new Panel();
        Label time = new Label();
        Label date = new Label();
        Label day = new Label();

        public ClockTypeOne() { }
        public ClockTypeOne(Control _form, Point _location)
        {
            form = _form;
            location = _location;
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
            panel.Location = location;

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
            defaultWidth = panel.Size.Width;
            Initialize();

            form.Controls.Add(panel);
        }

        public void UpdateAppearance()
        {
            panel.Location = new Point(0, (int)Math.Round(Settings.Scale(location.Y, defaultWidth, form.Width)));
            panel.Size = new Size(form.Size.Width - 15, (int)Math.Round(Settings.Scale(100, defaultWidth, panel.Size.Width)));

            date.Font = new Font("Rubik", Settings.Scale(16, defaultWidth, panel.Size.Width), FontStyle.Regular);
            date.Size = new Size(panel.Width / 2, panel.Height / 2);
            date.Location = new Point(0, 0);

            day.Font = new Font("Rubik", Settings.Scale(16, defaultWidth, panel.Size.Width), FontStyle.Regular);
            day.Size = new Size(panel.Width / 2, panel.Height / 2);
            day.Location = new Point(0, date.Size.Height);

            time.Font = new Font("Rubik", Settings.Scale(36, defaultWidth, panel.Size.Width), FontStyle.Regular);
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
