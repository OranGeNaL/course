using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.Json;

namespace Dashboard
{
    class Weather : IWidget
    {
        Form form;
        Thread process;
        Point location;

        Panel panel = new Panel();
        Label temperature = new Label();
        Label date = new Label();
        Label city = new Label();
        Label condition = new Label();

        string weatherRequest = "https://api.openweathermap.org/data/2.5/weather?q=Khabarovsk&appid=da5bd0105833e7d94fbfcb03212a19b6&units=metric";
        string weatherResponse;

        public Weather() { }
        public Weather(Form _form, Point _location)
        {
            form = _form;
            location = _location;
            CreateComponents();

            process = new Thread(new ThreadStart(Process));
            process.Start();
        }

        ~Weather()
        {
            Stop();
        }

        public void Initialize()
        {
            panel.Location = location;

            temperature.Text = "27°";
            temperature.Font = new Font("Rubik", 28, FontStyle.Regular);
            temperature.AutoSize = false;
            temperature.Size = new Size(panel.Width / 4, panel.Height / 2);
            temperature.Location = new Point(0, panel.Height / 2);
            temperature.TextAlign = ContentAlignment.TopCenter;

            date.Text = "Friday, 6 September";
            date.Font = new Font("Rubik", 8, FontStyle.Regular);
            date.AutoSize = false;
            date.Size = new Size(panel.Width / 2, panel.Height / 4);
            date.Location = new Point(temperature.Size.Width, panel.Height / 2);
            date.TextAlign = ContentAlignment.BottomLeft;

            city.Text = "Khabarovsk";
            city.Font = new Font("Rubik", 8, FontStyle.Regular);
            city.AutoSize = false;
            city.Size = new Size(panel.Width / 2, panel.Height / 4);
            city.Location = new Point(temperature.Size.Width, panel.Height / 2 + date.Size.Height);
            city.TextAlign = ContentAlignment.TopLeft;

            condition.Text = "Sunny";
            condition.Font = new Font("Rubik", 10, FontStyle.Regular);
            condition.AutoSize = false;
            condition.Size = new Size(panel.Width / 4, panel.Height / 4);
            condition.Location = new Point(date.Location.X + date.Size.Width, (panel.Height / 4) * 3);
            condition.TextAlign = ContentAlignment.TopLeft;

            panel.Controls.Add(temperature);
            panel.Controls.Add(date);
            panel.Controls.Add(city);
            panel.Controls.Add(condition);
        }

        public void CreateComponents()
        {
            panel.Size = new Size(form.Size.Width - 15, 100);
            Initialize();

            form.Controls.Add(panel);
        }

        public void Process()
        {
            while (true)
            {
                try
                {
                    WebClient webClient = new WebClient();
                    weatherResponse = webClient.DownloadString(weatherRequest);
                    
                    //MessageBox.Show(weatherResponse);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + ex.Data);
                }
                Thread.Sleep(20000);
            }
        }

        public void Stop()
        {
            process.Abort();
        }

        public void UpdateAppearance()
        {
            panel.Size = new Size(form.Size.Width - 15, 100);

            temperature.Size = new Size(panel.Width / 4, panel.Height / 2);
            temperature.Location = new Point(0, panel.Height / 2);

            date.Size = new Size(panel.Width / 2, panel.Height / 4);
            date.Location = new Point(temperature.Size.Width, panel.Height / 2);

            city.Size = new Size(panel.Width / 2, panel.Height / 4);
            city.Location = new Point(temperature.Size.Width, panel.Height / 2 + date.Size.Height);

            condition.Size = new Size(panel.Width / 4, panel.Height / 4);
            condition.Location = new Point(date.Location.X + date.Size.Width, (panel.Height / 4) * 3);
        }


    }
}
