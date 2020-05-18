using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice
{
    class Curve
    {
        public List<Dot> dots = new List<Dot>();

        public Curve(List<Dot> _dots)
        {
            dots = _dots;
        }

        public Point[] ConvertToPoint()
        {
            Point[] res = new Point[dots.Count];
            int k = 0;

            foreach(Dot i in dots)
            {
                res[k] = i.ConvertToPoint();
                k++;
            }

            return res;
        }
    }
}
