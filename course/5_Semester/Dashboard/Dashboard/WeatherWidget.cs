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
    class WeatherWidget : IWidget
    {
        Control form;
        Thread process;
        Point location;

        public string Name { get; set; } = "weather";
        public Guid ID { get; set; }

        int defaultWidth;

        Panel panel = new Panel();
        Label temperature = new Label();
        Label date = new Label();
        Label city = new Label();
        Label condition = new Label();

        Panel removeButton = new Panel();
        //PictureBox image = new PictureBox();
        //PictureBox backImage = new PictureBox();

        string weatherRequest = "https://api.openweathermap.org/data/2.5/weather?q=" + Settings.City + "&appid=da5bd0105833e7d94fbfcb03212a19b6&units=metric";
        string weatherResponse;

        public WeatherWidget() { }
        public WeatherWidget(Control _form, Point _location)
        {
            form = _form;
            location = _location;
            CreateComponents();
            ID = Guid.NewGuid();

            process = new Thread(new ThreadStart(Process));
            process.Start();
        }

        ~WeatherWidget()
        {
            //Stop();
        }

        public void Initialize()
        {
            panel.Location = location;

            /*backImage.Location = location;
            backImage.Size = panel.Size;
            backImage.SizeMode = PictureBoxSizeMode.Zoom;
            backImage.ImageLocation = "sunny.png";*/

            //panel.BackgroundImage = Image.FromFile("sunny.png");
            panel.BackgroundImageLayout = ImageLayout.Zoom;

            removeButton.BackgroundImage = Image.FromFile("button-pictures/remove-button.png");
            removeButton.BackgroundImageLayout = ImageLayout.Zoom;
            removeButton.Size = new Size(panel.Width / 15, panel.Width / 15);
            removeButton.Location = new Point(panel.Width - removeButton.Width, 0);
            removeButton.BackColor = Color.Transparent;

            removeButton.MouseEnter += RemovePanel_MouseEnter;
            removeButton.MouseLeave += RemovePanel_MouseLeave;
            removeButton.MouseDown += RemovePanel_MouseDown;
            removeButton.MouseUp += RemovePanel_MouseUp;
            //MessageBox.Show(string.Format("SizeX: {0}\nSizeY: {1}\nLocX: {2}", removeButton.Width, removeButton.Height, removeButton.Location.X));


            temperature.Text = "27°";
            temperature.Font = new Font("Rubik", 28, FontStyle.Regular);
            temperature.ForeColor = Color.White;
            temperature.BackColor = Color.Transparent;
            temperature.AutoSize = false;
            temperature.Size = new Size(panel.Width / 4, panel.Height / 2);
            temperature.Location = new Point(0, panel.Height / 2);
            temperature.TextAlign = ContentAlignment.TopCenter;

            date.Text = "Friday, 6 September";
            date.Font = new Font("Rubik", 8, FontStyle.Regular);
            date.ForeColor = Color.White;
            date.BackColor = Color.Transparent;
            date.AutoSize = false;
            date.Size = new Size(panel.Width / 2, panel.Height / 4);
            date.Location = new Point(temperature.Size.Width, panel.Height / 2);
            date.TextAlign = ContentAlignment.BottomLeft;

            city.Text = Settings.City;
            city.Font = new Font("Rubik", 8, FontStyle.Regular);
            city.ForeColor = Color.White;
            city.BackColor = Color.Transparent;
            city.AutoSize = false;
            city.Size = new Size(panel.Width / 2, panel.Height / 4);
            city.Location = new Point(temperature.Size.Width, panel.Height / 2 + date.Size.Height);
            city.TextAlign = ContentAlignment.TopLeft;

            condition.Text = "Sunny";
            condition.Font = new Font("Rubik", 10, FontStyle.Regular);
            condition.ForeColor = Color.White;
            condition.BackColor = Color.Transparent;
            condition.AutoSize = false;
            condition.Size = new Size(panel.Width / 4, panel.Height / 4);
            condition.Location = new Point(date.Location.X + date.Size.Width, (panel.Height / 4) * 3);
            condition.TextAlign = ContentAlignment.TopCenter;

            /*image.Size = new Size(panel.Width / 4, (panel.Height / 4) * 3);
            image.BackColor = Color.Transparent;
            image.Location = new Point(date.Location.X + date.Size.Width, 0);
            image.SizeMode = PictureBoxSizeMode.Zoom;
            image.ImageLocation = "01d.png";*/

            panel.Controls.Add(removeButton);
            panel.Controls.Add(temperature);
            panel.Controls.Add(date);
            panel.Controls.Add(city);
            panel.Controls.Add(condition);
            //panel.Controls.Add(image);
            //panel.Controls.Add(backImage);
        }

        public void CreateComponents()
        {
            panel.Size = new Size(form.Size.Width, 100);
            defaultWidth = Settings.MinimimWidth;

            Initialize();

            form.Controls.Add(panel);
        }

        public void Process()
        {
            while (true)
            {
                try
                {
                    weatherRequest = "https://api.openweathermap.org/data/2.5/weather?q=" + Settings.City + "&appid=da5bd0105833e7d94fbfcb03212a19b6&units=metric";
                    WebClient webClient = new WebClient();
                    weatherResponse = webClient.DownloadString(weatherRequest).Replace("base", "based");//замена из-за резервации base языком
                    WeatherResponse responseObject = JsonSerializer.Deserialize<WeatherResponse>(weatherResponse);

                    //image.ImageLocation = "http://openweathermap.org/img/wn/" + responseObject.weather[0].icon + "@2x.png";
                    if (panel.BackgroundImage != Image.FromFile("weather-pictures/" + responseObject.weather[0].icon + ".png"))
                        panel.BackgroundImage = Image.FromFile("weather-pictures/" + responseObject.weather[0].icon + ".png");

                    SafeWriter.WriteTextSafe(Math.Round(responseObject.main.temp).ToString() + "°", temperature);
                    SafeWriter.WriteTextSafe(DateTime.Now.DayOfWeek.ToString() + ", " + DateTime.Now.Date.Day.ToString() + " " + DateTime.Now.ToString("MMMM"), date);
                    SafeWriter.WriteTextSafe(responseObject.weather[0].description, condition);
                    SafeWriter.WriteTextSafe(Settings.City, city);

                    //MessageBox.Show(weatherResponse);
                    //MessageBox.Show(responseObject.weather[0].description.ToString());
                }
                catch (WebException ex)
                {
                    MessageBox.Show(ex.Message + ex.Data);
                }
                Thread.Sleep(5000);
            }
        }

        public void Stop()
        {
            process.Abort();
        }

        public void UpdateAppearance()
        {
            location.Y = Settings.widgets.IndexOf(this) * 100;

            panel.Location = new Point(0, (int)Math.Round(Animator.Scale(location.Y, defaultWidth, form.Width)));
            panel.Size = new Size(form.Size.Width - 15, (int)Math.Round(Animator.Scale(100, defaultWidth, panel.Size.Width)));

            removeButton.Size = new Size(panel.Width / 15, panel.Width / 15);
            removeButton.Location = new Point(panel.Width - removeButton.Width, 0);

            /*backImage.Location = panel.Location;
            backImage.Size = panel.Size;*/

            temperature.Font = new Font("Rubik", Animator.Scale(28, defaultWidth, panel.Width), FontStyle.Regular);
            temperature.Size = new Size(panel.Width / 4, panel.Height / 2);
            temperature.Location = new Point(0, panel.Height / 2);

            date.Font = new Font("Rubik", Animator.Scale(8, defaultWidth, panel.Size.Width), FontStyle.Regular);
            date.Size = new Size(panel.Width / 2, panel.Height / 4);
            date.Location = new Point(temperature.Size.Width, panel.Height / 2);

            city.Font = new Font("Rubik", Animator.Scale(8, defaultWidth, panel.Size.Width), FontStyle.Regular);
            city.Size = new Size(panel.Width / 2, panel.Height / 4);
            city.Location = new Point(temperature.Size.Width, panel.Height / 2 + date.Size.Height);

            condition.Font = new Font("Rubik", Animator.Scale(10, defaultWidth, panel.Size.Width), FontStyle.Regular);
            condition.Size = new Size(panel.Width / 4, panel.Height / 4);
            condition.Location = new Point(date.Location.X + date.Size.Width, (panel.Height / 4) * 3);

            /*image.Size = new Size(panel.Width / 4, (panel.Height / 4) * 3);
            image.Location = new Point(date.Location.X + date.Size.Width, 0);*/
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
