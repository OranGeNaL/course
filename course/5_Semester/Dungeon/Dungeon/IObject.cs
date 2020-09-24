using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

namespace Dungeon
{
    interface IObject
    {
        public string ID { get; set; }
        public Pos position { get; set; }
        public char sprite { get; set; }


        public string ToString();
    }
}
