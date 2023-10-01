using Maze_Game.GameLoop;
using Maze_Game.GameWorlds;
using Maze_Game.Math;
using Maze_Game.Rendering;

namespace Maze_Game
{
    internal class Program
    {
        public const int _gameFieldWidth = 30;
        public const int _gameFieldHeight = 35;

        public const int _viewportWidth = 50;
        public const int _viewportHeight = 35;

        public const int _mazeWidth = 30;
        public const int _mazeHeight = 30;

        public const int _coinNumber = 20;
        public const int _coinsCounterX = 33;
        public const int _coinsCounterY = _gameFieldHeight / 2 - 2;

        public const int _gameTimeInSeconds = 20;
        public const int _timerPositionX = 33;
        public const int _timerPositionY = _gameFieldHeight / 2 + 2;

        public const char _playerChar = '@';
        public const char _mazeWallChar = '█';
        public const char _coinChar = '$';

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            IntVector2 viewportSize = new IntVector2(_viewportWidth, _viewportHeight);
            Rect gameWorldSize = new Rect(0, 0, _gameFieldWidth, _gameFieldWidth);

            GameWorld gameWorld = new GameWorld(gameWorldSize);
            GameRenderer gameRenderer = new GameRenderer(gameWorld, viewportSize);
            MazeGameLoop mazeGameLoop = new MazeGameLoop(gameWorld, gameRenderer);
            GameManager gameManager = new GameManager(gameWorld);

            Rect playerAreaOfMovement = new Rect(0, 0, _gameFieldWidth, _gameFieldHeight);
            gameManager.CreatePlayer(playerAreaOfMovement, _playerChar);

            Rect mazeArea = new Rect(0, 5, _mazeWidth, _mazeHeight);
            gameManager.GenerateMaze(mazeArea, _mazeWallChar);

            gameManager.SpawnCoins(_coinChar, _coinNumber);
            gameManager.CreateCoinsCounter(new IntVector2(_coinsCounterX, _coinsCounterY));

            gameManager.CreateGameTimer(TimeSpan.FromSeconds(_gameTimeInSeconds), new IntVector2(_timerPositionX, _timerPositionY));

            gameManager.SetupWinConditions();

            mazeGameLoop.StartGameLoop();
        }
    }
}