using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace practice
{
    static class Painter
    {
        public const int SCREEN_WIDTH = 600;
        public const int SCREEN_HEIGHT = 600;
        public const int SCREEN_DIV_VALUE = 100;

        public static int leftDownX = 0;
        public static int leftDownY = SCREEN_HEIGHT;

        public static double scale = 0.1;
        public static double divisionValue = 1;

        public static List<Dot> dots = new List<Dot>();
        public static List<Curve> curves = new List<Curve>();


        public static void UpdatePosition()
        {
            foreach (Dot i in dots)
            {
                i.UpdatePosition();
            }
        }

        public static void ImportFromMatrix(double[,] matrix)
        {
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                dots.Add(new Dot(matrix[i, 0], matrix[i, 1]));
            }

            curves.Add(new Curve(dots));
        }
    }
}
