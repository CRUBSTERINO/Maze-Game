namespace Maze_Game.Coins
{
    public class CoinsManager
    {
        private int _collectedCoinsCount;
        private int _collectedCoinsOnLevelCount;
        private List<Coin> _coins;

        public Action<int>? OnCoinsCountChanged;

        public CoinsManager() 
        {
            _collectedCoinsCount = 0;
            _coins = new List<Coin>();
        }

        public void AssignCoins(List<Coin> coins)
        {
            foreach (Coin coin in coins)
            {
                coin.OnCoinCollected += CoinCollectedHandler;
            }

            _coins.AddRange(coins);
        }

        public void RevertCollectedOnLevelCoins()
        {
            _collectedCoinsCount -= _collectedCoinsOnLevelCount;
            _collectedCoinsOnLevelCount = 0;

            OnCoinsCountChanged?.Invoke(_collectedCoinsCount);
        }

        public void ConsolidateCollectedOnLevelCoins()
        {
            _collectedCoinsOnLevelCount = 0;
        }

        public void DestroyAllSpawnedCoins()
        {
            foreach (Coin coin in _coins)
            {
                coin.OnCoinCollected -= CoinCollectedHandler;
                coin.GameObject.Destroy();
            }

            _coins.Clear();
        }

        private void CoinCollectedHandler(Coin coin)
        {
            _collectedCoinsCount++;
            _collectedCoinsOnLevelCount++;
            coin.OnCoinCollected -= CoinCollectedHandler;
            _coins.Remove(coin);

            OnCoinsCountChanged?.Invoke(_collectedCoinsCount);
        }
    }
}