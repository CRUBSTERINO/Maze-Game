using Maze_Game.Coins;
using Maze_Game.GameWorlds;
using Maze_Game.Math;
using Maze_Game.MazeGeneration;
using Maze_Game.Physics;
using Maze_Game.Rendering;

namespace Maze_Game
{
    public class GameSetup
    {
        private MazeGenerator? _mazeGenerator;
        private GameObject? _playerGameObject;
        private IntVector2 _finishGlobalPosition;

        public void CreatePlayer(GameWorld gameWorld, Rect areaOfMovement, char playerChar)
        {
            _playerGameObject = new GameObject(gameWorld, new IntVector2(2, 1));
            _playerGameObject.AddComponent(new CharRenderer(_playerGameObject, playerChar));
            _playerGameObject.AddComponent(new PlayerController(_playerGameObject, 1, areaOfMovement));
            _playerGameObject.AddComponent(new CharCollider(_playerGameObject, false, false));

            _playerGameObject.Create();
        }

        public void GenerateMaze(GameWorld gameWorld, Rect mazeArea, char wallChar)
        {
            Random random = new Random();
            IntVector2 startLocalPosition = new IntVector2(random.Next(1, mazeArea.Size.X - 1), 0);
            IntVector2 finishLocalPosition = new IntVector2(random.Next(1, mazeArea.Size.X - 1), mazeArea.Size.Y - 1);
            _finishGlobalPosition = finishLocalPosition + mazeArea.Position;

            _mazeGenerator = new MazeGenerator(mazeArea, wallChar, gameWorld, startLocalPosition, finishLocalPosition);
            _mazeGenerator.GenerateMaze();
        }

        public void SpawnCoins(GameWorld gameWorld, char coinChar, int coinNumber)
        {
            if (_mazeGenerator == null)
            {
                throw new Exception("Maze Generator is null");
            }

            CoinGenerator coinGenerator = new CoinGenerator(gameWorld, _mazeGenerator.GetAllEmptyCellGlobalPositions(), coinChar, coinNumber);
            coinGenerator.SpawnCoins();
        }

        public void SetupWinConditions(GameWorld gameWorld)
        {
            if (_playerGameObject == null)
            {
                throw new Exception("Player GameObject doesn't exist");
            }

            GameObject winConditionsManagerGameObject = new GameObject(gameWorld, IntVector2.Zero);
            WinConditionsManager winConditionsManager = winConditionsManagerGameObject.AddComponent(new WinConditionsManager(winConditionsManagerGameObject, _playerGameObject, _finishGlobalPosition));
            winConditionsManager.OnMazeCompleted += () => Environment.Exit(0);

            winConditionsManagerGameObject.Create();
        }
    }
}