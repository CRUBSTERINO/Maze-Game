using Maze_Game.Coins;
using Maze_Game.GameWorlds;
using Maze_Game.Math;
using Maze_Game.MazeGeneration;
using Maze_Game.Physics;
using Maze_Game.Rendering;

namespace Maze_Game
{
    public class GameManager
    {
        private GameWorld _gameWorld;
        private MazeGenerator? _mazeGenerator;
        private GameObject? _playerGameObject;
        private CoinsManager? _coinsManager;
        private IntVector2 _finishGlobalPosition;

        public GameManager(GameWorld gameWorld) 
        {
            _gameWorld = gameWorld;
        }

        public void CreatePlayer(Rect areaOfMovement, char playerChar)
        {
            _playerGameObject = new GameObject(_gameWorld, new IntVector2(2, 1));
            _playerGameObject.AddComponent(new CharRenderer(_playerGameObject, playerChar));
            _playerGameObject.AddComponent(new PlayerController(_playerGameObject, 1, areaOfMovement));
            _playerGameObject.AddComponent(new CharCollider(_playerGameObject, false, false));

            _playerGameObject.Create();
        }

        public void GenerateMaze(Rect mazeArea, char wallChar)
        {
            Random random = new Random();
            IntVector2 startLocalPosition = new IntVector2(random.Next(1, mazeArea.Size.X - 1), 0);
            IntVector2 finishLocalPosition = new IntVector2(random.Next(1, mazeArea.Size.X - 1), mazeArea.Size.Y - 1);
            _finishGlobalPosition = finishLocalPosition + mazeArea.Position;

            _mazeGenerator = new MazeGenerator(mazeArea, wallChar, _gameWorld, startLocalPosition, finishLocalPosition);
            _mazeGenerator.GenerateMaze();
        }

        public void SpawnCoins(char coinChar, int coinNumber)
        {
            if (_mazeGenerator == null)
            {
                throw new Exception("Maze Generator is not initialized");
            }

            CoinGenerator coinGenerator = new CoinGenerator(_gameWorld, _mazeGenerator.GetAllEmptyCellGlobalPositions(), coinChar, coinNumber);
            List<Coin> coins = coinGenerator.SpawnCoins();

            _coinsManager = new CoinsManager(coins);
        }

        public void CreateCoinsCounter(IntVector2 position)
        {
            if (_coinsManager == null)
            {
                throw new Exception("Coin Manager is not initialized");
            }

            GameObject coinsCounterGameObject = new GameObject(_gameWorld, position);
            TextFieldRenderer textRenderer = coinsCounterGameObject.AddComponent(new TextFieldRenderer(coinsCounterGameObject, ""));
            coinsCounterGameObject.AddComponent(new CoinsCounter(coinsCounterGameObject, _coinsManager, textRenderer));

            coinsCounterGameObject.Create();
        }

        public void SetupWinConditions()
        {
            if (_playerGameObject == null)
            {
                throw new Exception("Player GameObject is not initialized");
            }

            GameObject winConditionsManagerGameObject = new GameObject(_gameWorld, IntVector2.Zero);
            WinConditionsManager winConditionsManager = winConditionsManagerGameObject.AddComponent(new WinConditionsManager(winConditionsManagerGameObject, _playerGameObject, _finishGlobalPosition));
            winConditionsManager.OnMazeCompleted += () => Environment.Exit(0);

            winConditionsManagerGameObject.Create();
        }
    }
}