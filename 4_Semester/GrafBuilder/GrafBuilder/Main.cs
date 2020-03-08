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

        public static int[,] CreateIncMatrix()
        {
            int[,] resultMatrix = new int[dots.Count, lines.Count];

            for(int i = 0; i < dots.Count; i++)
            {
                for(int j = 0; j < lines.Count; j++)
                {
                    resultMatrix[i, j] = 0;
                }
            }

            for(int j = 0; j < lines.Count; j++)
            {
                resultMatrix[lines[j].dot1.DotInd - 1, j] = 1;
                resultMatrix[lines[j].dot2.DotInd - 1, j] = -1;
            }

            return resultMatrix;
        }
    }
}
