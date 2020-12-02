using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Dashboard
{
    class CurrencyWidget : IWidget
    {

        Control form;
        Thread process;
        Point location;

        public string Name { get; set; } = "currency";
        public Guid ID { get; set; }

        int defaultWidth;

        Panel panel = new Panel();
        Label usdLabel = new Label();
        Label eurLabel = new Label();
        PictureBox usdTendency = new PictureBox();
        PictureBox eurTendency = new PictureBox();

        Panel removeButton = new Panel();

        string currencyRequest = "https://openexchangerates.org/api/latest.json?app_id=837b5e828db04d0dbb2a01367acfad5c&symbols=RUB,EUR";
        string currentCurrencyResponse;
        string yesterdayCurrencyRequest;
        string yesterdayCurrencyResponse;

        public CurrencyWidget() { }
        public CurrencyWidget(Control _form, Point _location)
        {
            form = _form;
            location = _location;
            CreateComponents();
            ID = Guid.NewGuid();

            process = new Thread(new ThreadStart(Process));
            process.Start();
        }

        ~CurrencyWidget()
        {
            //Stop();
        }

        public void Initialize()
        {
            panel.Location = location;

            usdLabel.Text = "USD: 76";
            usdLabel.Font = new Font("Rubik", 24, FontStyle.Regular);
            usdLabel.ForeColor = Color.Black;
            usdLabel.BackColor = Color.Transparent;
            usdLabel.AutoSize = false;
            usdLabel.Size = new Size((int)Math.Round(panel.Width * 0.4), panel.Height / 2);
            usdLabel.Location = new Point(usdTendency.Location.X - usdLabel.Width, panel.Height / 2);
            usdLabel.TextAlign = ContentAlignment.MiddleRight;

            usdTendency.BackColor = Color.Transparent;
            usdTendency.Size = new Size((int)Math.Round(panel.Width * 0.06), panel.Height / 2);
            usdTendency.Location = new Point(panel.Width / 2 - usdTendency.Width, panel.Height / 2);
            usdTendency.SizeMode = PictureBoxSizeMode.Zoom;
            eurTendency.BackColor = Color.Transparent;
            usdTendency.ImageLocation = "currency-pictures/down-tendency.png";

            eurLabel.Text = "EUR: 76";
            eurLabel.Font = new Font("Rubik", 24, FontStyle.Regular);
            eurLabel.ForeColor = Color.Black;
            eurLabel.BackColor = Color.Transparent;
            eurLabel.AutoSize = false;
            eurLabel.Size = new Size((int)Math.Round(panel.Width * 0.4), panel.Height / 2);
            eurLabel.Location = new Point(panel.Width / 2, panel.Height / 2);
            eurLabel.TextAlign = ContentAlignment.MiddleRight;

            eurTendency.BackColor = Color.Transparent;
            eurTendency.Location = new Point(eurLabel.Location.X + eurLabel.Size.Width, panel.Height / 2);
            eurTendency.Size = new Size((int)Math.Round(panel.Width * 0.06), panel.Height / 2);
            eurTendency.SizeMode = PictureBoxSizeMode.Zoom;
            eurTendency.BackColor = Color.Transparent;
            eurTendency.ImageLocation = "currency-pictures/up-tendency.png";

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
            panel.Controls.Add(usdTendency);
            panel.Controls.Add(usdLabel);
            panel.Controls.Add(eurTendency);
            panel.Controls.Add(eurLabel);

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
                    WebClient webClient = new WebClient();

                    currentCurrencyResponse = webClient.DownloadString(currencyRequest).Replace("base", "based");
                    //MessageBox.Show(currentCurrencyResponse);
                    CurrencyResponse currencyResponse = JsonSerializer.Deserialize<CurrencyResponse>(currentCurrencyResponse);

                    yesterdayCurrencyRequest = "https://openexchangerates.org/api/historical/" + DateTime.Now.AddDays(-1).Year + "-" + DateTime.Now.AddDays(-1).ToString("MM") + "-" + DateTime.Now.AddDays(-1).ToString("dd") + ".json?app_id=837b5e828db04d0dbb2a01367acfad5c&symbols=RUB,EUR";
                    //MessageBox.Show(yesterdayCurrencyRequest);
                    
                    yesterdayCurrencyResponse = webClient.DownloadString(yesterdayCurrencyRequest).Replace("base", "based");
                    //MessageBox.Show(yesterdayCurrencyResponse);
                    CurrencyResponse currencyResponseYesterday = JsonSerializer.Deserialize<CurrencyResponse>(yesterdayCurrencyResponse);

                    if (currencyResponse.rates["RUB"] > currencyResponseYesterday.rates["RUB"])
                    {
                        usdTendency.ImageLocation = "currency-pictures/up-tendency.png";
                    }
                    else
                    {
                        usdTendency.ImageLocation = "currency-pictures/down-tendency.png";
                    }

                    if (currencyResponse.rates["RUB"] / currencyResponse.rates["EUR"] > currencyResponseYesterday.rates["RUB"] / currencyResponseYesterday.rates["EUR"])
                    {
                        eurTendency.ImageLocation = "currency-pictures/up-tendency.png";
                    }
                    else
                    {
                        eurTendency.ImageLocation = "currency-pictures/down-tendency.png";
                    }

                    SafeWriter.WriteTextSafe("USD: " + Math.Round(currencyResponse.rates["RUB"], 2).ToString(), usdLabel);
                    SafeWriter.WriteTextSafe("EUR: " + Math.Round(currencyResponse.rates["RUB"] / currencyResponse.rates["EUR"], 2).ToString(), eurLabel);
                }
                catch (WebException ex)
                {
                    MessageBox.Show(ex.Message + ex.Data);
                }
                Thread.Sleep(60000);
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

            usdLabel.Font = new Font("Rubik", Animator.Scale(24, defaultWidth, panel.Size.Width), FontStyle.Regular);
            usdLabel.Size = new Size((int)Math.Round(panel.Width * 0.4), panel.Height / 2);
            usdLabel.Location = new Point(usdTendency.Location.X - usdLabel.Width, panel.Height / 2);

            eurLabel.Font = new Font("Rubik", Animator.Scale(24, defaultWidth, panel.Size.Width), FontStyle.Regular);
            eurLabel.Size = new Size((int)Math.Round(panel.Width * 0.4), panel.Height / 2);
            eurLabel.Location = new Point(panel.Width / 2, panel.Height / 2);

            usdTendency.Size = new Size((int)Math.Round(panel.Width * 0.06), panel.Height / 2);
            usdTendency.Location = new Point(panel.Width / 2 - usdTendency.Width, panel.Height / 2);

            eurTendency.Location = new Point(eurLabel.Location.X + eurLabel.Size.Width, panel.Height / 2);
            eurTendency.Size = new Size((int)Math.Round(panel.Width * 0.06), panel.Height / 2);
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
