namespace Maze_Game.Coins
{
    public struct CoinsGenerationsSettings
    {
        private int _coinNumber;
        private char _coinChar;

        public int CoinNumber => _coinNumber;
        public char CoinChar => _coinChar;

        public CoinsGenerationsSettings(int coinNumber, char coinChar)
        {
            _coinNumber = coinNumber;
            _coinChar = coinChar;
        }
    }
}