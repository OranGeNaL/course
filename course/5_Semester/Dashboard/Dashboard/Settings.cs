using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dashboard
{
    static class Settings
    {
        public static string City { get; set; }

        public static float Scale(int defaultValue, float defaultWidth, float realWidth)
        {
            return defaultValue * (realWidth / (defaultWidth));
        }
    }
}
