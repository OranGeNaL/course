using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GrafBuilder
{
    public class Line
    {
        public Dot dot1;
        public Dot dot2;
        public int line_ind;
        private int arrowLength = 30;
        private double arrowAngle = 30;

        public Dot firstArrow = new Dot();
        public Dot secondArrow = new Dot();

        public void UpdateLine()
        {
            Dot B = dot1.ConvertToDec();
            Dot A = dot2.ConvertToDec();

            int fullLength = (int)Math.Sqrt(Math.Pow(B.DotX - A.DotX, 2) + Math.Pow(B.DotY - A.DotY, 2));
            firstArrow.DotX = (int)(A.DotX + (B.DotX - A.DotX) * ((double)arrowLength / (double)fullLength));
            firstArrow.DotY = (int)(A.DotY + (B.DotY - A.DotY) * ((double)arrowLength / (double)fullLength));
            //firstArrow = firstArrow.ConvertToParent();
            secondArrow = firstArrow;

            firstArrow = firstArrow.Substraction(firstArrow, A);
            secondArrow = secondArrow.Substraction(secondArrow, A);

            firstArrow = firstArrow.Rotation(firstArrow, arrowAngle);
            secondArrow = secondArrow.Rotation(secondArrow, -arrowAngle);

            firstArrow = firstArrow.Addiction(firstArrow, A);
            secondArrow = secondArrow.Addiction(secondArrow, A);

            firstArrow = firstArrow.ConvertToParent();
            secondArrow = secondArrow.ConvertToParent();

        }

        public Line(Dot first_dot, Dot second_dot, int ind)
        {
            dot1 = first_dot;
            dot2 = second_dot;
            line_ind = ind;
            UpdateLine();
        }
    }
}
