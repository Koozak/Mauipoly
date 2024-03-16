using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mauipoly
{
    class Player
    { 
        public int id;
        public string Nickname;
        public int Money;
        public bool Turn;

        public Player(int id, string nickname, bool turn)
        {
            this.id = id;
            Nickname = nickname;
            Money = 2000;
            Turn = turn;
        }
    }

}
