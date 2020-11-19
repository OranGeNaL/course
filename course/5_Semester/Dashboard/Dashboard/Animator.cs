using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Threading;
using System.Drawing;

namespace Dashboard
{
    static class Animator
    {
        public static BezierCurve linear = new BezierCurve(0, 0, 1, 1);
        public static BezierCurve ease = new BezierCurve(0.26, 0.12, 0.23, 0.98);
        public static BezierCurve easeIn = new BezierCurve(0.42, 0.01, 1, 1);
        public static BezierCurve easeOut = new BezierCurve(0, 0, 0.58, 1);
        public static BezierCurve easeInOut = new BezierCurve(0.43, 0.01, 0.58, 1);

        public static void Translate(Control obj, int toX, int toY, double time, BezierCurve curve)
        {
            /*toX += obj.Location.X;
            toY += obj.Location.Y;*/
            //MessageBox.Show(string.Format("toX: {0}\ntoY: {1}", toX, toY));
            Thread thread = new Thread(new ParameterizedThreadStart(TranslateAsync));
            thread.Start(new TranslateObj(obj, toX, toY, time, curve));
        }

        private static void TranslateAsync(object wannaCast)
        {
            TranslateObj translateObj = (TranslateObj)wannaCast;
            Control control = translateObj.obj;
            int toX = translateObj.toX;
            int toY = translateObj.toY;
            double time = translateObj.time;
            BezierCurve curve = translateObj.curve;
            //Point dest = new Point(control.Location.X + toX, control.Location.Y + toY);
            double timePassed = 0;

            while(timePassed < time)
            {

                double coeff = BezierCurveCount(curve, timePassed / time, 0.016 / time) * time * 10;
                SafeWriter.WriteLocationSafe(new Point((int)Math.Round(toX * coeff / (time / 0.016)) + control.Location.X, (int)Math.Round(toY * coeff / (time / 0.016)) + control.Location.Y), control);

                /*if(coeff < 0)
                    MessageBox.Show(coeff.ToString());*/
                timePassed += 0.016;
                Thread.Sleep(16);
            }
        }


        private static double BezierCurveCount(BezierCurve curve, double t, double step)
        {
            Couple firstPoint = new Couple(curve.x1, curve.y1);
            Couple secondPoint = new Couple(curve.x2, curve.y2);
            //MessageBox.Show(t.ToString());
            Couple result = (1 - t) * 3 * new Couple(0, 0) + 3 * t * (1 - t) * firstPoint + 3 * t * 2 * (1 - t) * secondPoint + t * 3 * new Couple(1, 1);
            Couple prevResult;
            if (t != 0)
            {
                t -= step;
                prevResult = (1 - t) * 3 * new Couple(0, 0) + 3 * t * (1 - t) * firstPoint + 3 * t * 2 * (1 - t) * secondPoint + t * 3 * new Couple(1, 1);
            
                //MessageBox.Show(string.Format("res: {0}\nprevRes: {1}", result.Y, prevResult.Y));
                return Math.Abs(result.Y - prevResult.Y);
            }
            return Math.Abs(result.Y);
        }

        public static float Scale(int defaultValue, float defaultWidth, float realWidth)
        {
            return defaultValue * (realWidth / (defaultWidth));
        }
    }

    class BezierCurve
    {
        public double x1;
        public double y1;
        public double x2;
        public double y2;

        public BezierCurve(double _x1, double _y1, double _x2, double _y2)
        {
            x1 = _x1;
            y1 = _y1;
            x2 = _x2;
            y2 = _y2;
        }
    }

    class Couple
    {
        public double X { get; set; }
        public double Y { get; set; }

        public Couple() { }
        public Couple(double x, double y)
        {
            X = x;
            Y = y;
        }

        public static Couple operator +(Couple couple1, Couple couple2)
        {
            return new Couple(couple1.X + couple2.X, couple1.Y + couple2.Y);
        }

        public static Couple operator -(Couple couple1, Couple couple2)
        {
            return new Couple(couple1.X - couple2.X, couple1.Y - couple2.Y);
        }

        public static Couple operator *(Couple couple, double num)
        {
            return new Couple(couple.X * num, couple.Y * num);
        }

        public static Couple operator *(double num, Couple couple)
        {
            return new Couple(couple.X * num, couple.Y * num);
        }
    }

    class TranslateObj
    {
        public Control obj;
        public int toX;
        public int toY;
        public double time;
        public BezierCurve curve;

        public TranslateObj() { }
        public TranslateObj(Control control, int X, int Y, double _time, BezierCurve bezierCurve)
        {
            obj = control;
            toX = X;
            toY = Y;
            time = _time;
            curve = bezierCurve;
        }
    }
}
