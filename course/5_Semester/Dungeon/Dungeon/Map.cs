using System;
using System.Collections.Generic;
using System.Data;
using System.Text;

namespace Dungeon
{
    class Map
    {
        char[,] map/* = new char[ScreenManager.c_ScreenHeight, ScreenManager.c_ScreenWidth]*/;

        public Pos mapSize;

        public List<IObject> objects = new List<IObject>();

        public void ImportFromStr(string newMap)
        {
            string[] splNewMap = newMap.Split("\r\n");

            mapSize = new Pos(splNewMap[0].Length, splNewMap.Length);

            map = new char[splNewMap.Length, splNewMap[0].Length];

            for (int i = 0; i < splNewMap.Length; i++)
                for(int j = 0; j < splNewMap[0].Length; j++)
                {
                    //map[i, j] = splNewMap[i][j];

                    if (splNewMap[i][j] == ' ')
                    {
                        objects.Add(new Floor(new Pos(j, i), splNewMap[i][j]));
                    }
                    else if (Wall.IsWall(splNewMap[i][j]))
                    {
                        objects.Add(new Wall(new Pos(j, i), splNewMap[i][j]));
                    }
                    else if(splNewMap[i][j] == '0')
                    {
                        objects.Add(new Barrel(new Pos(j, i), splNewMap[i][j]));
                    }
                }
        }

        public string MapPrint()
        {
            string res = "\n";
            foreach(var i in objects)
            {
                map[i.position.posY, i.position.posX] = i.sprite;
            }
            map[GameOverlord.hero.position.posY, GameOverlord.hero.position.posX] = GameOverlord.hero.sprite;

            for (int i = 0; i < mapSize.posY; i++)
            {
                for(int j = 0; j < mapSize.posX; j++)
                {
                    res += map[i, j];
                }
                res += "\n";
            }

            return res;
        }

        public void PrintObjects()
        {
            foreach(var i in objects)
            {
                Console.Write(i.ToString() + " ");
            }
        }

        public IObject FindByPos(Pos pos)
        {
            foreach (var i in objects)
            {
                if (i.position == pos)
                    return i;
            }
            return null;
        }
    }
}
