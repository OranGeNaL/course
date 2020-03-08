using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafBuilder
{
    public static class Main
    {

        public static int dotCount = 0;
        public static int dotMax = 10;
        public static int line_count = 0;
        public static int activeDotInd = 0;
        public static int mouse_x;
        public static int mouse_y;

        public static List<Dot> dots = new List<Dot>();
        public static List<Line> lines = new List<Line>();
    }
}
