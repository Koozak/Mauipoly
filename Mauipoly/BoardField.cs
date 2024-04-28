using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mauipoly
{
    class BoardField
    {
        public string Name;
        public int HowMuchForField;
        public bool Occupied;
        public int x;
        public int y;

        public BoardField(string name, int howMuchForField, int x, int y)
        {
            Name = name;
            HowMuchForField = howMuchForField;
            Occupied = false;
            this.x = x;
            this.y = y;
        }
    }
}
