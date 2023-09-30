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

        public void SpawnCoins()
        {
            Random random = new Random();

            for (int i = 0; i < _coinNumber; i++)
            {
                int index = random.Next(0, _emptyCellPositions.Count);
                SpawnCoinGameObject(_emptyCellPositions[index]);
            }
        }

        private void SpawnCoinGameObject(IntVector2 position)
        {
            GameObject coinGameObject = new GameObject(_gameWorld, position);
            coinGameObject.AddComponent(new CharRenderer(coinGameObject, _coinChar));
            Collider coinCollider = coinGameObject.AddComponent(new CharCollider(coinGameObject, false, true));
            coinGameObject.AddComponent(new Coin(coinGameObject, coinCollider));

            coinGameObject.Create();
        }
    }
}