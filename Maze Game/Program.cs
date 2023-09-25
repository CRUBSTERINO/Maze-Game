using Maze_Game.GameLoop;
using Maze_Game.GameWorlds;
using Maze_Game.Math;
using Maze_Game.MazeGeneration;
using Maze_Game.Physics;
using Maze_Game.Rendering;

namespace Maze_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            const int gameFieldWidth = 30;
            const int gameFieldHeight = 35;

            const int mazeWidth = 30;
            const int mazeHeight = 30;

            const char playerChar = '@';
            const char mazeWallChar = '█';

            Console.CursorVisible = false;

            IntVector2 viewportSize = new IntVector2(gameFieldWidth, gameFieldHeight);
            Rect gameWorldSize = new Rect(0, 0, gameFieldWidth, gameFieldWidth);

            GameWorld gameWorld = new GameWorld(gameWorldSize);
            GameRenderer gameRenderer = new GameRenderer(gameWorld, viewportSize);
            MazeGameLoop mazeGameLoop = new MazeGameLoop(gameWorld, gameRenderer);

            #region Player Creation
            Rect playerAreaOfMovement = new Rect(0, 0, gameFieldWidth, gameFieldHeight);

            GameObject playerGameObject = new GameObject(gameWorld, new IntVector2(2, 1));
            playerGameObject.AddComponent(new CharRenderer(playerChar, playerGameObject));
            playerGameObject.AddComponent(new PlayerController(1, playerAreaOfMovement, playerGameObject));
            playerGameObject.AddComponent(new CharCollider(false, playerGameObject));

            playerGameObject.Create();
            #endregion

            #region Maze Generation
            Random random = new Random();
            Rect mazeArea = new Rect(0, 5, mazeWidth, mazeHeight);
            IntVector2 startPosition = new IntVector2(random.Next(1, mazeWidth), 0);
            IntVector2 finishPosition = new IntVector2(random.Next(1, mazeWidth), mazeHeight - 1);

            MazeGenerator mazeGenerator = new MazeGenerator(mazeArea, mazeWallChar, gameWorld, startPosition, finishPosition);
            mazeGenerator.GenerateMaze();
            #endregion

            mazeGameLoop.StartGameLoop();
        }
    }
}