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

        public string name = "";

        public bool enabled = true;
        public bool drawDots = true;
        public bool drawCurve = true;

        public Color color = Color.Red;
        public Color dotsColor = Color.Green;

        public Curve(List<Dot> _dots, string _name)
        {
            dots = _dots;
            name = _name;
        }

        public Curve(List<Dot> _dots, string _name, Color _curveColor, Color _dotColor)
        {
            dots = _dots;
            name = _name;
            color = _curveColor;
            dotsColor = _dotColor;
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
