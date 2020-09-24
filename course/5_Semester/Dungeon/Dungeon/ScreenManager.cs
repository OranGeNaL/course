using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;

namespace Dungeon
{
    public static class ScreenManager
    {
        public const int c_ScreenWidth = 100;
        public const int c_ScreenHeight = 30;

        

        public static Pixel[,] screenMesh;
        private static Map curMap;

        internal static Map CurMap { get => curMap; set => curMap = value; }

        public static void Initialize()
        {
            screenMesh = new Pixel[c_ScreenHeight, c_ScreenWidth];
            for (int i = 0; i < c_ScreenHeight; i++)
                for (int j = 0; j < c_ScreenWidth; j++)
                    screenMesh[i, j] = new Pixel();
        }

        public static void UpdateScreen()
        {
            Console.Clear();
            Console.WriteLine(CurMap.MapPrint());
        }

        public static void PrintWithColor(object obj, ConsoleColor foreColor, ConsoleColor backColor)
        {
            Console.ForegroundColor = foreColor;
            Console.BackgroundColor = backColor;

            Console.Write(obj.ToString());
        }
    }
}
