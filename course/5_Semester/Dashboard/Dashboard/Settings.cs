using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard
{
    static class Settings
    {
        public static string City { get; set; } = "Khabarovsk";

        public static int MinimimWidth { get; set; } = 500;

        public static List<IWidget> widgets { get; set; }

        public static AddWidgetButton AddButton { get; set; }

        public static void UpdateWidgets()
        {
            AddButton.UpdateAppearance();
            foreach (var i in widgets)
                i.UpdateAppearance();
        }

        /*public static IWidget GetWidgetByID(Guid targerID)
        {
            foreach (var i in widgets)
                if (i.ID == targerID)
                    return i;
            return null;
        }*/
    }
}
