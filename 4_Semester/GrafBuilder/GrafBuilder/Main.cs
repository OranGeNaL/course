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
        public static int lineCount = 0;
        public static int activeDotInd = 0;
        public static int mouseX;
        public static int mouseY;
        public static string sourcePath = "";

        public static List<Dot> dots = new List<Dot>();
        public static List<Line> lines = new List<Line>();

        public static void CreateDots(int n)
        {
            int r = 50;
            int OX = 150;
            int OY = 100;
            for(int i = 1; i <= n; i++)
            {
                double x, y;
                x = OX + r * Math.Cos(2 * i * Math.PI / n);
                y = OY + r * Math.Sin(2 * i * Math.PI / n);
                dotCount++;
                dots.Add(new Dot((int)x, (int)y, dotCount));
            }
        }
    }
}
