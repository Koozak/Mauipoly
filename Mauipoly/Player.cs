using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mauipoly
{
    class Player
    { 
        public string Nickname;
        public int Money;
        public bool isTurn;
        public string Location;
        public string Image;

        public Player(string nickname, bool turn, string image)
        {
            Nickname = nickname;
            Money = 2000;
            isTurn = turn;
            Location = "6:0";
            Image = image;
        }
    }

}
