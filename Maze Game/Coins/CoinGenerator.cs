using Maze_Game.GameWorlds;
using Maze_Game.Math;
using Maze_Game.Physics;
using Maze_Game.Rendering;

namespace Maze_Game.Coins
{
    public class CoinGenerator
    {
        private GameWorld _gameWorld;
        private List<IntVector2> _emptyCellPositions;
        private char _coinChar;
        private int _coinNumber;

        public CoinGenerator(GameWorld gameWorld, List<IntVector2> emptyCellPositions, char coinChar, int coinNumber) 
        {
            _gameWorld = gameWorld;
            _emptyCellPositions = emptyCellPositions;
            _coinChar = coinChar;
            _coinNumber = coinNumber;
        }

        public List<Coin> SpawnCoins()
        {
            Random random = new Random();
            List<Coin> coins = new List<Coin>(_coinNumber);

            for (int i = 0; i < _coinNumber; i++)
            {
                int index = random.Next(0, _emptyCellPositions.Count);
                Coin coin = SpawnCoinGameObject(_emptyCellPositions[index]);
                coins.Add(coin);
            }

            return coins;
        }

        private Coin SpawnCoinGameObject(IntVector2 position)
        {
            GameObject coinGameObject = new GameObject(_gameWorld, position);
            coinGameObject.AddComponent(new CharRenderer(coinGameObject, _coinChar));
            Collider coinCollider = coinGameObject.AddComponent(new CharCollider(coinGameObject, false, true));
            Coin coinComponent = coinGameObject.AddComponent(new Coin(coinGameObject, coinCollider));

            coinGameObject.Create();

            return coinComponent;
        }
    }
}