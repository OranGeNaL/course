using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon
{
    class Wall:IStatic
    {
        public static char[] sprites = { '/', '\\', '|', '_', '-' };


        public string ID { get; set; }
        public Pos position { get; set; }
        public char sprite { get; set; }
        public bool Passable { get; set; }

        public Wall(Pos pos, char spr)
        {
            position = pos;
            sprite = spr;
            Passable = false;
        }

        public override string ToString()
        {
            return sprite.ToString();
        }

        public static bool IsWall(char val)
        {
            foreach (var i in sprites)
                if (i == val)
                    return true;
            return false;
        }
    }
}
