using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafBuilder
{
    public class Dot
    {
        public Dot()
        {
            DotX = 0;
            DotY = 0;
        }
        public Dot(int x, int y)
        {
            DotX = x;
            DotY = y;
        }
        public Dot(int x, int y, int n)
        {
            DotX = x;
            DotY = y;
            DotInd = n;
        }

        public Dot ConvertToDec()
        {
            Dot res = new Dot();
            res.dotX = DotX;
            res.dotY = 500 - DotY;
            return res;
        }
        public Dot ConvertToParent()
        {
            Dot res = new Dot();
            res.dotX = DotX;
            res.dotY = 500 - DotY;
            return res;
        }

        public Dot Substraction(Dot dot1, Dot dot2)
        {
            return new Dot(dot1.DotX - dot2.DotX, dot1.DotY - dot2.DotY);
        }

        public Dot Addiction(Dot dot1, Dot dot2)
        {
            return new Dot(dot1.DotX + dot2.DotX, dot1.DotY + dot2.DotY);
        }

        public Dot Rotation(Dot dot, double angle)
        {
            Dot dotCopy = dot;

            angle = angle * Math.PI / 180;

            dotCopy.dotX = (int)(dot.DotX * Math.Cos(angle) - dot.DotY * Math.Sin(angle));
            dotCopy.dotY = (int)(dot.DotX * Math.Sin(angle) + dot.dotY * Math.Cos(angle));

            return dotCopy;
        }

        public string ShowCoords()
        {
            return DotX.ToString() + DotY.ToString();
        }

        private int dotInd;
        public int DotInd
        {
            get { return dotInd; }
            set { dotInd = value; }
        }

        private int dotX;
        public int DotX
        {
            get { return dotX; }

            set { dotX = value; }
        }

        private int dotY;
        public int DotY
        {
            get { return dotY; }
            set { dotY = value; }
        }
    }
}
