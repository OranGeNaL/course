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
        public static string destPath = "";

        public static List<Dot> dots = new List<Dot>();
        public static List<Line> lines = new List<Line>();

        public static void CreateDots(int n)
        {
            int r = 100;
            int OX = 250;
            int OY = 200;
            for(int i = 1; i <= n; i++)
            {
                double x, y;
                x = OX + r * Math.Cos(2 * i * Math.PI / n);
                y = OY + r * Math.Sin(2 * i * Math.PI / n);
                dotCount++;
                dots.Add(new Dot((int)x, (int)y, dotCount));
            }
        }

        public static bool CheckLineAviability(Dot firstDot, Dot secondDot)
        {
            foreach(Line x in lines)
            {
                if (x.dot1 == firstDot && x.dot2 == secondDot)
                    return false;
            }

            return true;
        }

        public static Dot FindByInd(int ind)
        {
            Dot temp = new Dot(0, 0, 0);
            for (int i = 0; i < dots.Count(); i++)
            {
                if (dots[i].DotInd == ind)
                {
                    temp = dots[i];
                }
            }
            return temp;
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
