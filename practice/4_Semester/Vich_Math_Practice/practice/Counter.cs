using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice
{
    public static class Counter
    {
        public static double[,] CountFunc(double a, double b, double h)
        {
            int N = (int)((b - a) / h);
            double[,] result = new double[N, 2];
            int k = 0;

            for(double i = a; k < N; i += h)
            {
                result[k, 0] = i;
                //result[k, 1] = Math.Sin(Math.Pow(i + 3, 2) / 2) / 2 + Math.Log(i + 2) / 2 - 1;
                result[k, 1] = Math.Pow(Math.E, i);
                k++;
            }

            return result;
        }
    }
}
