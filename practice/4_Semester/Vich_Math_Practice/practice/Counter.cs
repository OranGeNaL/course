using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice
{
    public static class Counter
    {
        public delegate double FuncDelegate(int funcInd, double x, double[] y);

        public static double TestAccurate(int funcInd, double x, double[] y)
        {
            double res = 0;

            switch (funcInd)
            {
                case 0:
                    res = Math.Cos(x) / Math.Sqrt(1 + Math.Pow(Math.E, 2 * x));
                    break;
                case 1:
                    res = Math.Sin(x) / Math.Sqrt(1 + Math.Pow(Math.E, 2 * x));
                    break;
            }

            return res;
        }

        public static double[,] AccurateCount(FuncDelegate funcDelegate, int funcInd, double a, double b, double h)
        {
            int N = (int)((b - a) / h);
            double[,] result = new double[N, 2];
            int k = 0;

            for (double i = a; k < N; i += h)
            {
                result[k, 0] = i;
                result[k, 1] = funcDelegate(funcInd, i, new double[1]);//TestAccurate(i, firstFunc);
                k++;
            }

            return result;
        }

        public static double[] GetFirstValues(FuncDelegate funcDelegate, double t0, int numOfValues)
        {
            double[] res = new double[numOfValues];

            for(int i = 0; i < numOfValues; i++)
            {
                res[i] = funcDelegate(i, t0, new double[1]);
            }

            return res;
        }

        public static double TestODU(int funcInd, double t, double[] y)
        {
            switch (funcInd)
            {
                case 0:
                    return -Math.Sin(t) / Math.Sqrt(1 + Math.Pow(Math.E, 2 * t)) + y[0] * (Math.Pow(y[0], 2) + Math.Pow(y[1], 2) - 1);
                case 1:
                    return Math.Cos(t) / Math.Sqrt(1 + Math.Pow(Math.E, 2 * t)) + y[1] * (Math.Pow(y[0], 2) + Math.Pow(y[1], 2) - 1);
            }

            return 0;
        }

        public static double[,] RungeKutta(FuncDelegate funcDelegate, int funcInd, double a, double b, double h, double t0, double[] y0)
        {
            int N = (int)((b - a) / h);
            List<double[,]> yshtr = new List<double[,]>();
            int k = 1;

            for(int i = 0; i < y0.Length; i++)
            {
                yshtr.Add(new double[N, 2]);
                yshtr[i][0, 0] = t0;
                yshtr[i][0, 1] = y0[i];
            }

            for (int i = 1; k < N; i += 1)
            {
                double[] yPrev = new double[y0.Length];
                for(int j = 0; j < y0.Length; j++)
                {
                    yPrev[j] = yshtr[j][i - 1, 1];
                }

                for(int j = 0; j < y0.Length; j++)
                {
                    yshtr[j][i, 0] = a + i * h;

                    double k1 = funcDelegate(j, yshtr[j][i, 0], yPrev);
                    double k2 = funcDelegate(j, yshtr[j][i, 0] + h / 2, Addict(h * k1 / 2, yPrev));
                    double k3 = funcDelegate(j, yshtr[j][i, 0] + 3 * h / 4, Addict(3 * h * k2 / 4, yPrev));

                    yshtr[j][i, 1] = yshtr[j][i - 1, 1] + h * (2 * k1 + 3 * k2 + 4 * k3) / 9;
                }
                k++;
            }


            return yshtr[funcInd];
        }

        public static double[] Addict(double num, double[] mass)
        {
            //Мб надо доп массив добавить
            for(int i = 0; i < mass.Length; i++)
            {
                mass[i] += num;
            }
            return mass;
        }
    }
}
