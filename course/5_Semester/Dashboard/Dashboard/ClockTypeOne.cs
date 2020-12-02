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

        public string Name { get; set; } = "clock";
        public Guid ID { get; set; }

        int defaultWidth;

        Panel panel = new Panel();
        Label time = new Label();
        Label date = new Label();
        Label day = new Label();

        Panel removeButton = new Panel();

        public ClockTypeOne() { }
        public ClockTypeOne(Control _form, Point _location)
        {
            form = _form;
            location = _location;
            CreateComponents();
            ID = Guid.NewGuid();

            process = new Thread(new ThreadStart(Process));
            process.Start();
        }

        ~ClockTypeOne()
        {
            //Stop();
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

            removeButton.BackgroundImage = Image.FromFile("button-pictures/remove-button.png");
            removeButton.BackgroundImageLayout = ImageLayout.Zoom;
            removeButton.Size = new Size(panel.Width / 15, panel.Width / 15);
            removeButton.Location = new Point(panel.Width - removeButton.Width, 0);
            removeButton.BackColor = Color.Transparent;

            removeButton.MouseEnter += RemovePanel_MouseEnter;
            removeButton.MouseLeave += RemovePanel_MouseLeave;
            removeButton.MouseDown += RemovePanel_MouseDown;
            removeButton.MouseUp += RemovePanel_MouseUp;

            panel.Controls.Add(removeButton);
            panel.Controls.Add(date);
            panel.Controls.Add(day);
            panel.Controls.Add(time);
        }

        public void CreateComponents()
        {
            panel.Size = new Size(form.Size.Width - 15, 100);
            defaultWidth = Settings.MinimimWidth;
            Initialize();

            form.Controls.Add(panel);
        }

        public void UpdateAppearance()
        {
            location.Y = Settings.widgets.IndexOf(this) * 100;

            panel.Location = new Point(0, (int)Math.Round(Animator.Scale(location.Y, defaultWidth, form.Width)));
            panel.Size = new Size(form.Size.Width - 15, (int)Math.Round(Animator.Scale(100, defaultWidth, panel.Size.Width)));

            removeButton.Size = new Size(panel.Width / 15, panel.Width / 15);
            removeButton.Location = new Point(panel.Width - removeButton.Width, 0);

            date.Font = new Font("Rubik", Animator.Scale(16, defaultWidth, panel.Size.Width), FontStyle.Regular);
            date.Size = new Size(panel.Width / 2, panel.Height / 2);
            date.Location = new Point(0, 0);

            day.Font = new Font("Rubik", Animator.Scale(16, defaultWidth, panel.Size.Width), FontStyle.Regular);
            day.Size = new Size(panel.Width / 2, panel.Height / 2);
            day.Location = new Point(0, date.Size.Height);

            time.Font = new Font("Rubik", Animator.Scale(36, defaultWidth, panel.Size.Width), FontStyle.Regular);
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
                Thread.Sleep(1000);
            }
        }

        public void Stop()
        {
            process.Abort();
        }

        private void RemovePanel_MouseEnter(object sender, EventArgs e)
        {
            removeButton.BackgroundImage = Image.FromFile("button-pictures/remove-button-hovered.png");
        }

        private void RemovePanel_MouseLeave(object sender, EventArgs e)
        {
            removeButton.BackgroundImage = Image.FromFile("button-pictures/remove-button.png");
        }

        private void RemovePanel_MouseDown(object sender, EventArgs e)
        {
            removeButton.BackgroundImage = Image.FromFile("button-pictures/remove-button-pressed.png");
            Stop();
            form.Controls.Remove(panel);
            Settings.widgets.Remove(this);
            Settings.UpdateWidgets();
        }

        private void RemovePanel_MouseUp(object sender, EventArgs e)
        {
            removeButton.BackgroundImage = Image.FromFile("button-pictures/remove-button-hovered.png");
        }
    }
}
