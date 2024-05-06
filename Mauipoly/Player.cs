namespace Mauipoly
{
    class Player
    { 
        public string Nickname;
        public int Money;
        public bool isTurn;
        public string Image;
        public BoardField[] BoardFieldList;

        public Player(string nickname, bool turn, string image)
        {
            Nickname = nickname;
            Money = 1000;
            isTurn = turn;
            Image = image;
            BoardFieldList = new BoardField[24];
        }
    }

}
