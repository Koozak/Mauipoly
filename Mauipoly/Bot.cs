using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mauipoly
{
    class Bot : Player
    {
        public Bot(string nickname, bool turn, string image) : base(nickname, turn, image)
        {
            Money = 1000;
            BoardFieldList = new BoardField[24];
        }
    }
}
