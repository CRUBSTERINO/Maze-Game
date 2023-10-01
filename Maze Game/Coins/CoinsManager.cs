namespace Maze_Game.Coins
{
    public class CoinsManager
    {
        private int _collectedCoinsCount;
        private List<Coin> _coins;

        public Action<int>? OnCoinsCountChanged;

        public CoinsManager(List<Coin> coins) 
        {
            _collectedCoinsCount = 0;
            _coins = coins;

            foreach (Coin coin in _coins) 
            {
                coin.OnCoinCollected += CoinCollectedHandler;
            }
        }

        private void CoinCollectedHandler(Coin coin)
        {
            _collectedCoinsCount++;
            coin.OnCoinCollected -= CoinCollectedHandler;

            OnCoinsCountChanged?.Invoke(_collectedCoinsCount);
        }
    }
}