using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpellLand
{
    internal class Unit
    {
        internal float X, Y;
        internal string Name { get; set; }
        public Unit(string name = "Noname Unit")
        {
            Name = name;
        }
    }
}
