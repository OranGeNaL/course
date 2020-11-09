using System;
using System.IO;
using System.Threading;

namespace Dungeon
{
    class Program
    {
        static void Main(string[] args)
        {
            ////////////////////////////////////////////
            //Первичная настройка экрана
            ////////////////////////////////////////////
            Console.SetWindowSize(ScreenManager.c_ScreenWidth, ScreenManager.c_ScreenHeight);
            ScreenManager.Initialize();
            GameOverlord.hero = new Hero(new Pos(5, 5), 'H');

            Map map = new Map();

            string mapStr;

            using (FileStream fstream = File.OpenRead("map.txt"))
            {
                // преобразуем строку в байты
                byte[] array = new byte[fstream.Length];
                // считываем данные
                fstream.Read(array, 0, array.Length);
                // декодируем байты в строку
                mapStr = System.Text.Encoding.Default.GetString(array);
            }

            map.ImportFromStr(mapStr);
            ScreenManager.CurMap = map;
            ScreenManager.UpdateScreen();
            //Console.WriteLine(map.PrintMap());

            Thread game = new Thread(new ThreadStart(StartGame));
            game.Start();
            
        }

        private static void StartGame()
        {
            while (true)
            {
                ConsoleKey consoleKey = Console.ReadKey().Key;
                Console.WriteLine(consoleKey);
                if (consoleKey == ConsoleKey.UpArrow)
                {
                    GameOverlord.hero.position.posY--;
                }
                else if (consoleKey == ConsoleKey.DownArrow)
                {
                    GameOverlord.hero.position.posY++;
                }
                else if (consoleKey == ConsoleKey.LeftArrow)
                {
                    GameOverlord.hero.position.posX--;
                }
                else if (consoleKey == ConsoleKey.RightArrow)
                {
                    GameOverlord.hero.position.posX++;
                }
                ScreenManager.UpdateScreen();
            }
        }
    }
}
