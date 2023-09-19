using Maze_Game.GameLoop;
using Maze_Game.GameWorlds;
using Maze_Game.Math;
using Maze_Game.MazeGeneration;
using Maze_Game.Rendering;
using System.Runtime.Versioning;

namespace Maze_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IntVector2 viewportSize = new IntVector2(30, 35);
            IntVector2 consoleSize = new IntVector2(30, 35);
            Rect gameWorldSize = new Rect(0, 0, 30, 35);

            ConfigureConsole(consoleSize);

            GameWorld gameWorld = new GameWorld(gameWorldSize);
            GameRenderer gameRenderer = new GameRenderer(gameWorld, viewportSize);
            MazeGameLoop mazeGameLoop = new MazeGameLoop(gameWorld, gameRenderer);

            #region Player Creation
            GameObject playerGameObject = new GameObject(gameWorld, new IntVector2(2, 1));
            playerGameObject.AddComponent(new CharRenderer('@', playerGameObject));
            playerGameObject.AddComponent(new PlayerController(1, playerGameObject));

            playerGameObject.Create();
            #endregion

            #region Maze Generation
            Rect mazeArea = new Rect(0, 5, 5, 5);

            MazeGenerator mazeGenerator = new MazeGenerator(mazeArea, '&', gameWorld);
            mazeGenerator.GenerateMaze();
            #endregion

            mazeGameLoop.StartGameLoop();
        }

        [SupportedOSPlatform("windows")]
        public static void ConfigureConsole(IntVector2 size)
        {
            Console.SetWindowSize(size.X, size.Y);
            Console.SetBufferSize(size.X, size.Y);

            Console.CursorVisible = false;
        }
    }
}