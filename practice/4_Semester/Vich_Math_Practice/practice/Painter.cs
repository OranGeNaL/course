﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Reflection.Emit;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace practice
{
    static class Painter
    {
        public const int SCREEN_WIDTH = 600;
        public const int SCREEN_HEIGHT = 600;
        public const int SCREEN_DIV_VALUE = 100;

        public static int leftDownX = 0;
        public static int leftDownXOld = 0;
        public static int leftDownY = SCREEN_HEIGHT;
        public static int leftDownYOld = SCREEN_HEIGHT;

        public static Pen blackPen = new Pen(Color.Black, 1);
        public static Pen greenPen = new Pen(Color.Green, 2);
        public static Pen redPen = new Pen(Color.Red, 2);

        public static int cursorX = 0;
        public static int cursorY = 0;
        public static int cursorXOld = 0;
        public static int cursorYOld = 0;
        public static bool mouseDown = false;

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

            foreach (Curve i in curves)
            {
                foreach (Dot j in i.dots)
                {
                    j.UpdatePosition();
                }
            }
        }

        public static void Clear()
        {
            dots.Clear();
            curves.Clear();
        }

        public static void UpdateCursorInfo(MouseEventArgs eventArgs)
        {
            cursorX = eventArgs.X;
            cursorY = eventArgs.Y;
        }

        public static void ImportFromMatrix(double[,] matrix, string funcName)
        {
            List<Dot> newCurveDots = new List<Dot>();
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                newCurveDots.Add(new Dot(matrix[i, 0], matrix[i, 1]));
            }

            curves.Add(new Curve(newCurveDots, funcName));
        }

        public static void ImportFromMatrix(double[,] matrix, string funcName, Color curveColor, Color dotsColor)
        {
            List<Dot> newCurveDots = new List<Dot>();
            for (int i = 0; i < matrix.GetUpperBound(0) + 1; i++)
            {
                newCurveDots.Add(new Dot(matrix[i, 0], matrix[i, 1]));
            }

            curves.Add(new Curve(newCurveDots, funcName, curveColor, dotsColor));
        }
    }
}
