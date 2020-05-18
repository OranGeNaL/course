using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace practice
{
    class Dot
    {
        public int X, Y;

        public double doubX = 0;
        public double doubY = 0;

        public Dot(int _X, int _Y)
        {
            X = _X;
            Y = _Y;
        }

        public Dot(double _X, double _Y)
        {
            doubX = _X;
            doubY = _Y;
            X =  (int)(_X * Painter.SCREEN_DIV_VALUE * Painter.scale / Painter.divisionValue);
            Y = Painter.SCREEN_HEIGHT - (int)(_Y * Painter.SCREEN_DIV_VALUE * Painter.scale / Painter.divisionValue);
        }

        public void UpdatePosition()
        {
            X = (int)(doubX * Painter.SCREEN_DIV_VALUE * Painter.scale / Painter.divisionValue);
            Y = Painter.SCREEN_HEIGHT - (int)(doubY * Painter.SCREEN_DIV_VALUE * Painter.scale / Painter.divisionValue);
        }

        public Point ConvertToPoint()
        {
            return new Point(X, Y);
        }
    }
}
