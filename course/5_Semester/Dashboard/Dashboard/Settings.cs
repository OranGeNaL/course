using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.Json;
using System.IO;
using System.Windows.Forms;
using System.Drawing;

namespace Dashboard
{
    static class Settings
    {
        public static string SettingsFile { get; set; } = "settings.json";

        public static string City { get; set; } = "Khabarovsk";

        public static int MinimimWidth { get; set; } = 500;

        public static List<IWidget> widgets { get; set; }

        public static AddWidgetButton AddButton { get; set; }

        public static Form1 form { get; set; }

        public static void UpdateWidgets()
        {
            AddButton.UpdateAppearance();
            foreach (var i in widgets)
                i.UpdateAppearance();
        }

        public static void LoadFromFile()
        {
            try {
            string jRes;
            using (StreamReader streamReader = new StreamReader(SettingsFile, Encoding.UTF8))
            {
                jRes = streamReader.ReadToEnd();
                //MessageBox.Show(jRes);
            }
            SettingsObject settingsObject = JsonSerializer.Deserialize<SettingsObject>(jRes);

            City = settingsObject.City;
            foreach(var i in settingsObject.strWidgets)
            {
                if (i == "weather")
                    widgets.Add(new WeatherWidget(form, new Point(0, Settings.widgets.Count * 100)));
                else if (i == "currency")
                    widgets.Add(new CurrencyWidget(form, new Point(0, Settings.widgets.Count * 100)));
                else if (i == "clock")
                    widgets.Add(new ClockTypeOne(form, new Point(0, Settings.widgets.Count * 100)));
                }
            }
            catch
            {

            }
        }

        public static void WritoToFile()
        {
            SettingsObject settingsObject = new SettingsObject(City, WriteWidgets());
            string jRes = JsonSerializer.Serialize(settingsObject);
            //MessageBox.Show(jRes);
            StreamWriter streamWriter = new StreamWriter(SettingsFile, false, Encoding.UTF8);
            streamWriter.Write(jRes);
            streamWriter.Flush();
            streamWriter.Close();
        }

        public static List<string> WriteWidgets()
        {
            List<string> list = new List<string>();
            foreach (var i in widgets)
            {
                //MessageBox.Show(i.Name);
                list.Add(i.Name);
            }
            return list;
        }

        /*public static IWidget GetWidgetByID(Guid targerID)
        {
            foreach (var i in widgets)
                if (i.ID == targerID)
                    return i;
            return null;
        }*/
    }

    class SettingsObject
    {
        public string City { get; set; }
        public List<string> strWidgets { get; set; }

        public SettingsObject() { }
        public SettingsObject(string city, List<string> widgets)
        {
            City = city;
            strWidgets = widgets;
        }
    }
}
