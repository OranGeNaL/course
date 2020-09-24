using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon
{
    class Floor:IStatic
    {
        public string ID { get; set; }
        public Pos position { get; set; }
        public char sprite { get; set; }
        public bool Passable { get; set; }

        public Floor(Pos pos, char spr)
        {
            position = pos;
            sprite = spr;
            Passable = true;
        }

        public override string ToString()
        {
            return sprite.ToString();
        }
    }
}
