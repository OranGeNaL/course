using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon
{
    class Hero:IMovable
    {
        public string ID { get; set; }
        public Pos position { get; set; }
        public char sprite { get; set; }

        public Hero()
        {

        }

        public Hero(Pos pos, char spr)
        {
            position = pos;
            sprite = spr;
        }
    }
}
