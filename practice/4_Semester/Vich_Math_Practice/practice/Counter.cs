using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice
{
    public static class Counter
    {
        public static double lambda1 = 1;
        public static double lambda2 = 1;
        public static double beta1 = 1;
        public static double beta2 = 3;
        public static double c = 3;


        public delegate double FuncDelegate(int funcInd, double x, double[] y);
        public delegate List<double[,]> RungeDelegate(FuncDelegate funcDelegate, double a, double b, double h, double t0, double[] y0);

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

        public static double TaskODU(int funcInd, double t, double[] yPrev)
        {
            double x = yPrev[0];
            double y = yPrev[1];
            switch (funcInd)
            {
                case 0:
                    return (-lambda1 + beta1 * Math.Pow(y * y, 1.0 / 3) * (1 - x / c) / (1 + x)) * x;
                case 1:
                    double ans = lambda2 * y - beta2 * x * Math.Pow(y * y, 1.0 / 3) / (1 + x);
                    return ans;
            }

            return 0;
        }

        public static double[,] RungeKutta(FuncDelegate funcDelegate, int funcInd, double a, double b, double h, double t0, double[] y0)
        {
            int N = (int)((b - a) / h);
            List<double[,]> yshtr = new List<double[,]>();

            for (int i = 0; i < y0.Length; i++)
            {
                yshtr.Add(new double[N, 2]);
                yshtr[i][0, 0] = t0;
                yshtr[i][0, 1] = y0[i];
            }

            for (int i = 1; i < N; i += 1)
            {
                double[] yPrev = new double[y0.Length];
                for(int j = 0; j < y0.Length; j++)
                {
                    yPrev[j] = yshtr[j][i - 1, 1];
                }

                double[] k1 = new double[y0.Length];
                double[] k2 = new double[y0.Length];
                double[] k3 = new double[y0.Length];

                for(int j = 0; j < y0.Length; j++)
                {
                    yshtr[j][i, 0] = a + i * h;
                    k1[j] = funcDelegate(j, yshtr[j][i, 0], yPrev);
                }

                for(int j = 0; j < y0.Length; j++)
                {
                    k2[j] = funcDelegate(j, yshtr[j][i, 0] + h / 2, Addict(/*h * k1 / 2*/ Multyplicate(h / 2, k1), yPrev));
                }

                for (int j = 0; j < y0.Length; j++)
                {
                    k3[j] = funcDelegate(j, yshtr[j][i, 0] + 3 * h / 4, Addict(/*h * k1 / 2*/ Multyplicate(3 * h / 4, k2), yPrev));
                }

                for (int j = 0; j < y0.Length; j++)
                {
                    yshtr[j][i, 1] = yshtr[j][i - 1, 1] + h * (2 * k1[j] + 3 * k2[j] + 4 * k3[j]) / 9;
                }
            }

            return yshtr[funcInd];
        }

        public static List<double[,]> RungeKutta(FuncDelegate funcDelegate, double a, double b, double h, double t0, double[] y0)
        {
            int N = (int)((b - a) / h);
            List<double[,]> yshtr = new List<double[,]>();

            for (int i = 0; i < y0.Length; i++)
            {
                yshtr.Add(new double[N, 2]);
                yshtr[i][0, 0] = t0;
                yshtr[i][0, 1] = y0[i];
            }

            for (int i = 1; i < N; i += 1)
            {
                double[] yPrev = new double[y0.Length];
                for (int j = 0; j < y0.Length; j++)
                {
                    yPrev[j] = yshtr[j][i - 1, 1];
                }

                for (int j = 0; j < y0.Length; j++)
                {
                    yshtr[j][i, 0] = a + i * h;

                    double k1 = funcDelegate(j, yshtr[j][i, 0], yPrev);
                    double k2 = funcDelegate(j, yshtr[j][i, 0] + h / 2, Addict(h * k1 / 2, yPrev));
                    double k3 = funcDelegate(j, yshtr[j][i, 0] + 3 * h / 4, Addict(3 * h * k2 / 4, yPrev));

                    yshtr[j][i, 1] = yshtr[j][i - 1, 1] + h * (2 * k1 + 3 * k2 + 4 * k3) / 9;
                }
            }

            return yshtr;
        }

        public static double[,] CountErr(RungeDelegate rungeDelegate, FuncDelegate funcDelegate, FuncDelegate trueFunc, double a, double b, double t0, double[] y0, int errInd)
        {
            int k = 0;
            double[,] res = new double[ (int)(2 / 0.005), 2];
            double[,] res2 = new double[(int)(2 / 0.005), 2];

            for (double i = 0.005; i <= 2; i += 0.005)
            {
                double maxErr = 0;
                List<double[,]> list = rungeDelegate(funcDelegate, a, b, i, t0, y0);
                int m = 0;
                foreach (double[,] j in list)
                {
                    for(int p = 0; p < j.GetLength(0); p++)
                    {
                        if(Math.Abs(j[p, 1] - trueFunc(m, j[p, 0], y0)) > maxErr)
                        {
                            maxErr = Math.Abs(j[p, 1] - trueFunc(m, j[p, 0], y0));
                        }
                    }
                    m++;
                }
                res[k, 0] = i;
                res2[k, 0] = i;
                res[k, 1] = maxErr;
                res2[k, 1] = maxErr / Math.Pow(i, 3);
                k++;
            }

            switch(errInd)
            {
                case 0:
                    return res;
                case 1:
                    return res2;
                default:
                    return new double[1,1];
            }
        }

        public static int GetNumOfEls(double b)
        {
            int i = 0;
            double h = 0.0005;
            while (h <= b)
            {
                h += h * 2;
                i++;
            }
            return i;
        }

        public static double[] Addict(double num, double[] mass)
        {
            double[] res = new double[mass.Length];
            for(int i = 0; i < mass.Length; i++)
            {
               res[i] = mass[i] + num;
            }
            return res;
        }

        public static double[] Addict(double[] mass1, double[] mass2)
        {
            double[] res = new double[mass1.Length];
            for (int i = 0; i < mass1.Length; i++)
            {
                res[i] = mass1[i] + mass2[i];
            }
            return res;
        }

        public static double[] Multyplicate(double num, double[] mass)
        {
            double[] res = new double[mass.Length];
            for (int i = 0; i < mass.Length; i++)
            {
                res[i] = mass[i] * num;
            }
            return res;
        }

        public static double[,] ConvertToXY(double[,] x, double[,] y)
        {
            double[,] result = new double[x.GetLength(0), 2];

            for(int i = 0; i < x.GetLength(0); i++)
            {
                result[i, 0] = x[i, 1];
                result[i, 1] = y[i, 1];
            }

            return result;
        }
    }
}
