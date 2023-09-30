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

        public const int _mazeWidth = 30;
        public const int _mazeHeight = 30;

        public const int _coinNumber = 20;

        public const char _playerChar = '@';
        public const char _mazeWallChar = '█';
        public const char _coinChar = '$';

        static void Main(string[] args)
        {
            Console.CursorVisible = false;

            IntVector2 viewportSize = new IntVector2(_gameFieldWidth, _gameFieldHeight);
            Rect gameWorldSize = new Rect(0, 0, _gameFieldWidth, _gameFieldWidth);

            GameWorld gameWorld = new GameWorld(gameWorldSize);
            GameRenderer gameRenderer = new GameRenderer(gameWorld, viewportSize);
            MazeGameLoop mazeGameLoop = new MazeGameLoop(gameWorld, gameRenderer);
            GameSetup gameSetup = new GameSetup();

            Rect playerAreaOfMovement = new Rect(0, 0, _gameFieldWidth, _gameFieldHeight);
            gameSetup.CreatePlayer(gameWorld, playerAreaOfMovement, _playerChar);

            Rect mazeArea = new Rect(0, 5, _mazeWidth, _mazeHeight);
            gameSetup.GenerateMaze(gameWorld, mazeArea, _mazeWallChar);

            gameSetup.SpawnCoins(gameWorld, _coinChar, _coinNumber);

            gameSetup.SetupWinConditions(gameWorld);

            mazeGameLoop.StartGameLoop();
        }
    }
}