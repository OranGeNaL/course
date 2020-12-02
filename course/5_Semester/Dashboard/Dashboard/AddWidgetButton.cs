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
    class AddWidgetButton : IWidget
    {
        Control form;
        Thread process;
        Point location;

        public string Name { get; set; } = "button";

        public Guid ID { get; set; }

        int defaultWidth;

        Panel panel = new Panel();

        bool clicked = false;

        Panel timePanel = new Panel();
        Panel weatherPanel = new Panel();
        Panel currencyPanel = new Panel();

        Panel settingsPanel = new Panel();

        Label timeLabel = new Label();
        Label weatherLabel = new Label();
        Label currencyLabel = new Label();

        public AddWidgetButton() { }
        public AddWidgetButton(Control _form, Point _location)
        {
            form = _form;
            location = _location;
            CreateComponents();

            ID = Guid.NewGuid();

            process = new Thread(new ThreadStart(Process));
            process.Start();
        }

        ~AddWidgetButton()
        {
            Stop();
        }

        public void Initialize()
        {
            panel.Location = new Point(location.X, location.Y + 10);

            panel.BackgroundImage = Image.FromFile("button-pictures/add-button.png");
            panel.BackgroundImageLayout = ImageLayout.Zoom;

            settingsPanel.Size = new Size(50, 50);
            settingsPanel.Location = new Point(0, form.Height);
            settingsPanel.BackgroundImage = Image.FromFile("button-pictures/setting-button.png");
            settingsPanel.BackColor = Color.Transparent;
            settingsPanel.BackgroundImageLayout = ImageLayout.Zoom;

            weatherPanel.Size = new Size(75, 30);
            weatherPanel.Location = new Point(panel.Location.X, panel.Location.Y + panel.Height + 10);
            weatherPanel.BackgroundImage = Image.FromFile("button-pictures/add-weather-button.png");
            weatherPanel.BackColor = Color.Transparent;
            weatherPanel.BackgroundImageLayout = ImageLayout.Zoom;

            weatherLabel.AutoSize = false;
            weatherLabel.Size = new Size(75, 30);
            weatherLabel.Location = new Point(0, 0);
            weatherLabel.Text = "Погода";
            weatherLabel.Font = new Font("Rubik", 12, FontStyle.Regular);
            weatherLabel.TextAlign = ContentAlignment.MiddleCenter;
            weatherLabel.ForeColor = Color.Black;
            weatherLabel.BackColor = Color.Transparent;
            weatherPanel.Controls.Add(weatherLabel);

            currencyPanel.Size = new Size(125, 30);
            currencyPanel.Location = new Point(weatherPanel.Location.X, weatherPanel.Location.Y + weatherPanel.Height + 10);
            currencyPanel.BackgroundImage = Image.FromFile("button-pictures/add-currency-button.png");
            currencyPanel.BackColor = Color.Transparent;
            currencyPanel.BackgroundImageLayout = ImageLayout.Zoom;

            currencyLabel.AutoSize = false;
            currencyLabel.Size = new Size(125, 30);
            currencyLabel.Location = new Point(0, 0);
            currencyLabel.Text = "Курсы валют";
            currencyLabel.Font = new Font("Rubik", 12, FontStyle.Regular);
            currencyLabel.TextAlign = ContentAlignment.MiddleCenter;
            currencyLabel.ForeColor = Color.Black;
            currencyLabel.BackColor = Color.Transparent;
            currencyPanel.Controls.Add(currencyLabel);

            timePanel.Size = new Size(70, 30);
            timePanel.Location = new Point(currencyPanel.Location.X, currencyPanel.Location.Y + currencyPanel.Height + 10);
            timePanel.BackgroundImage = Image.FromFile("button-pictures/add-time-button.png");
            timePanel.BackColor = Color.Transparent;
            timePanel.BackgroundImageLayout = ImageLayout.Zoom;

            timeLabel.AutoSize = false;
            timeLabel.Size = new Size(70, 30);
            timeLabel.Location = new Point(0, 0);
            timeLabel.Text = "Время";
            timeLabel.Font = new Font("Rubik", 12, FontStyle.Regular);
            timeLabel.TextAlign = ContentAlignment.MiddleCenter;
            timeLabel.ForeColor = Color.Black;
            timeLabel.BackColor = Color.Transparent;
            timePanel.Controls.Add(timeLabel);

            settingsPanel.MouseEnter += SettingsPanel_MouseEnter;
            settingsPanel.MouseLeave += SettingsPanel_MouseLeave;
            settingsPanel.MouseDown += SettingsPanel_MouseDown;
            settingsPanel.MouseUp += SettingsPanel_MouseUp;

            panel.MouseEnter += Panel_MouseEnter;
            panel.MouseLeave += Panel_MouseLeave;
            panel.MouseDown += Panel_MouseDown;
            panel.MouseUp += Panel_MouseUp;

            weatherLabel.MouseEnter += WeatherPanel_MouseEnter;
            weatherLabel.MouseLeave += WeatherPanel_MouseLeave;
            weatherLabel.MouseDown += WeatherPanel_MouseDown;
            weatherLabel.MouseUp += WeatherPanel_MouseUp;

            currencyLabel.MouseEnter += CurrencyPanel_MouseEnter;
            currencyLabel.MouseLeave += CurrencyPanel_MouseLeave;
            currencyLabel.MouseDown += CurrencyPanel_MouseDown;
            currencyLabel.MouseUp += CurrencyPanel_MouseUp;

            timeLabel.MouseEnter += TimePanel_MouseEnter;
            timeLabel.MouseLeave += TimePanel_MouseLeave;
            timeLabel.MouseDown += TimePanel_MouseDown;
            timeLabel.MouseUp += TimePanel_MouseUp;

            form.Controls.Add(settingsPanel);
            form.Controls.Add(timePanel);
            form.Controls.Add(currencyPanel);
            form.Controls.Add(weatherPanel);
        }

        public void CreateComponents()
        {
            panel.Size = new Size(100, 100);
            defaultWidth = form.Width;
            Initialize();

            form.Controls.Add(panel);
        }

        public void UpdateAppearance()
        {
            panel.Size = new Size((int)Math.Round(Animator.Scale(100, defaultWidth, form.Width)), (int)Math.Round(Animator.Scale(100, defaultWidth, form.Width)));
            panel.Location = new Point(form.Width / 2 - panel.Width / 2, (int)Math.Round(Animator.Scale(Settings.widgets.Count * 100, defaultWidth, form.Width)));

            settingsPanel.Size = new Size((int)Math.Round(Animator.Scale(50, defaultWidth, form.Width)), (int)Math.Round(Animator.Scale(50, defaultWidth, form.Width)));
            settingsPanel.Location = new Point(0, (int)(form.Height - settingsPanel.Height * 1.8));

            weatherPanel.Size = new Size((int)Math.Round(Animator.Scale(75, defaultWidth, form.Width)), (int)Math.Round(Animator.Scale(30, defaultWidth, form.Width)));
            if (clicked)
                weatherPanel.Location = new Point(panel.Location.X, panel.Location.Y + panel.Height + (int)Math.Round(Animator.Scale(10, defaultWidth, form.Width)));
            else
                weatherPanel.Location = new Point(0 - weatherPanel.Width, panel.Location.Y + panel.Height + (int)Math.Round(Animator.Scale(10, defaultWidth, form.Width)));

            weatherLabel.Size = new Size((int)Math.Round(Animator.Scale(75, defaultWidth, form.Width)), (int)Math.Round(Animator.Scale(30, defaultWidth, form.Width)));
            weatherLabel.Font = new Font("Rubik", (int)Math.Round(Animator.Scale(12, defaultWidth, form.Width)), FontStyle.Regular);

            currencyPanel.Size = new Size((int)Math.Round(Animator.Scale(125, defaultWidth, form.Width)), (int)Math.Round(Animator.Scale(30, defaultWidth, form.Width)));
            if(clicked)
                currencyPanel.Location = new Point(weatherPanel.Location.X, weatherPanel.Location.Y + weatherPanel.Height + (int)Math.Round(Animator.Scale(10, defaultWidth, form.Width)));
            else
                currencyPanel.Location = new Point(0 - currencyPanel.Width, weatherPanel.Location.Y + weatherPanel.Height + (int)Math.Round(Animator.Scale(10, defaultWidth, form.Width)));

            currencyLabel.Size = new Size((int)Math.Round(Animator.Scale(125, defaultWidth, form.Width)), (int)Math.Round(Animator.Scale(30, defaultWidth, form.Width)));
            currencyLabel.Font = new Font("Rubik", (int)Math.Round(Animator.Scale(12, defaultWidth, form.Width)), FontStyle.Regular);

            timePanel.Size = new Size((int)Math.Round(Animator.Scale(70, defaultWidth, form.Width)), (int)Math.Round(Animator.Scale(30, defaultWidth, form.Width)));
            if(clicked)
                timePanel.Location = new Point(currencyPanel.Location.X, currencyPanel.Location.Y + currencyPanel.Height + (int)Math.Round(Animator.Scale(10, defaultWidth, form.Width)));
            else
                timePanel.Location = new Point(0 - timePanel.Width, currencyPanel.Location.Y + currencyPanel.Height + (int)Math.Round(Animator.Scale(10, defaultWidth, form.Width)));

            timeLabel.Size = new Size((int)Math.Round(Animator.Scale(70, defaultWidth, form.Width)), (int)Math.Round(Animator.Scale(30, defaultWidth, form.Width)));
            timeLabel.Font = new Font("Rubik", (int)Math.Round(Animator.Scale(12, defaultWidth, form.Width)), FontStyle.Regular);
        }

        public void Process()
        {
            /*while (true)
            {
                SafeWriter.WriteTextSafe(DateTime.Now.ToShortTimeString(), time);
                SafeWriter.WriteTextSafe(DateTime.Now.Day.ToString() + " / " + DateTime.Now.Month.ToString(), date);
                SafeWriter.WriteTextSafe(DateTime.Now.DayOfWeek.ToString(), day);
                Thread.Sleep(1000);
            }*/
        }

        public void Stop()
        {
            process.Abort();
        }

        private void Panel_MouseEnter(object sender, EventArgs e)
        {
            panel.BackgroundImage = Image.FromFile("button-pictures/add-button-hovered.png");
        }

        private void Panel_MouseLeave(object sender, EventArgs e)
        {
            panel.BackgroundImage = Image.FromFile("button-pictures/add-button.png");
        }

        private void Panel_MouseDown(object sender, EventArgs e)
        {
            panel.BackgroundImage = Image.FromFile("button-pictures/add-button-pressed.png");

            /*if (!clicked)
            {
                //MessageBox.Show((panel.Location.X - weatherPanel.Location.X).ToString());
                Animator.Translate(weatherPanel, panel.Location.X - weatherPanel.Location.X, 0, 0.5, Animator.easeOut);
                Animator.Translate(currencyPanel, panel.Location.X - currencyPanel.Location.X, 0, 0.5, Animator.easeOut);
                Animator.Translate(timePanel, panel.Location.X - timePanel.Location.X, 0, 0.5, Animator.easeOut);
            }
            else
            {
                Animator.Translate(weatherPanel, 0 - weatherPanel.Width - weatherPanel.Location.X, 0, 0.5, Animator.easeOut);
                Animator.Translate(currencyPanel, 0 - currencyPanel.Width - currencyPanel.Location.X, 0, 0.5, Animator.easeOut);
                Animator.Translate(timePanel, 0 - currencyPanel.Width - timePanel.Location.X, 0, 0.5, Animator.easeOut);
            }*/

            clicked = !clicked;
            UpdateAppearance();
        }

        private void Panel_MouseUp(object sender, EventArgs e)
        {
            panel.BackgroundImage = Image.FromFile("button-pictures/add-button-hovered.png");
        }

        ///
        /// Обработка кнопки погоды
        /// 
        private void WeatherPanel_MouseEnter(object sender, EventArgs e)
        {
            weatherPanel.BackgroundImage = Image.FromFile("button-pictures/add-weather-button-hovered.png");
        }

        private void WeatherPanel_MouseLeave(object sender, EventArgs e)
        {
            weatherPanel.BackgroundImage = Image.FromFile("button-pictures/add-weather-button.png");
        }

        private void WeatherPanel_MouseDown(object sender, EventArgs e)
        {
            weatherPanel.BackgroundImage = Image.FromFile("button-pictures/add-weather-button-pressed.png");
            Settings.widgets.Add(new WeatherWidget(form, new Point(0, Settings.widgets.Count * 100)));
            clicked = false;
            UpdateAppearance();
            Settings.UpdateWidgets();
        }

        private void WeatherPanel_MouseUp(object sender, EventArgs e)
        {
            weatherPanel.BackgroundImage = Image.FromFile("button-pictures/add-weather-button-hovered.png");
        }

        ///
        /// Обработка кнопки валют
        ///
        private void CurrencyPanel_MouseEnter(object sender, EventArgs e)
        {
            currencyPanel.BackgroundImage = Image.FromFile("button-pictures/add-currency-button-hovered.png");
        }

        private void CurrencyPanel_MouseLeave(object sender, EventArgs e)
        {
            currencyPanel.BackgroundImage = Image.FromFile("button-pictures/add-currency-button.png");
        }

        private void CurrencyPanel_MouseDown(object sender, EventArgs e)
        {
            currencyPanel.BackgroundImage = Image.FromFile("button-pictures/add-currency-button-pressed.png");
            Settings.widgets.Add(new CurrencyWidget(form, new Point(0, Settings.widgets.Count * 100)));
            clicked = false;
            UpdateAppearance();
            Settings.UpdateWidgets();
        }

        private void CurrencyPanel_MouseUp(object sender, EventArgs e)
        {
            currencyPanel.BackgroundImage = Image.FromFile("button-pictures/add-currency-button-hovered.png");
        }

        ///
        /// Обработка кнопки времени
        ///
        private void TimePanel_MouseEnter(object sender, EventArgs e)
        {
            timePanel.BackgroundImage = Image.FromFile("button-pictures/add-time-button-hovered.png");
        }

        private void TimePanel_MouseLeave(object sender, EventArgs e)
        {
            timePanel.BackgroundImage = Image.FromFile("button-pictures/add-time-button.png");
        }

        private void TimePanel_MouseDown(object sender, EventArgs e)
        {
            timePanel.BackgroundImage = Image.FromFile("button-pictures/add-time-button-pressed.png");
            Settings.widgets.Add(new ClockTypeOne(form, new Point(0, Settings.widgets.Count * 100)));
            clicked = false;
            UpdateAppearance();
            Settings.UpdateWidgets();
        }

        private void TimePanel_MouseUp(object sender, EventArgs e)
        {
            timePanel.BackgroundImage = Image.FromFile("button-pictures/add-time-button-hovered.png");
        }

        ///
        ///Обработка кнопки настроек
        ///
        private void SettingsPanel_MouseEnter(object sender, EventArgs e)
        {
            settingsPanel.BackgroundImage = Image.FromFile("button-pictures/setting-button-hovered.png");
        }

        private void SettingsPanel_MouseLeave(object sender, EventArgs e)
        {
            settingsPanel.BackgroundImage = Image.FromFile("button-pictures/setting-button.png");
        }

        private void SettingsPanel_MouseDown(object sender, EventArgs e)
        {
            settingsPanel.BackgroundImage = Image.FromFile("button-pictures/setting-button-pressed.png");
            SettingsForm settingsForm = new SettingsForm();
            settingsForm.Show();
        }

        private void SettingsPanel_MouseUp(object sender, EventArgs e)
        {
            settingsPanel.BackgroundImage = Image.FromFile("button-pictures/setting-button-hovered.png");
        }
    }
}
