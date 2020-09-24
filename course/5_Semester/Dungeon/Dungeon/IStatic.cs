using System;
using System.Collections.Generic;
using System.Text;

namespace Dungeon
{
    interface IStatic:IObject
    {
        public bool Passable { get; set; }
    }
}
