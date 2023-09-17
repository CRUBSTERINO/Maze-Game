using Maze_Game.GameLoop;
using Maze_Game.GameWorlds;
using Maze_Game.Math;
using Maze_Game.Rendering;
using System.Runtime.Versioning;

namespace Maze_Game
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IntVector2 viewportSize = new IntVector2(30, 30);

            IntVector2 consoleSize = new IntVector2(30, 30);

            Rect gameWorldSize = new Rect(0, 0, 30, 30);

            SetConsoleScreenSize(consoleSize);

            GameWorld gameWorld = new GameWorld(gameWorldSize);

            GameRenderer gameRenderer = new GameRenderer(gameWorld, viewportSize);

            MazeGameLoop mazeGameLoop = new MazeGameLoop(gameWorld, gameRenderer);

            GameObject playerGameObject = new GameObject(new IntVector2(2, 1));
            gameWorld.Create(playerGameObject);
            playerGameObject.AddComponent(new CharRenderer(playerGameObject, '@'));

            mazeGameLoop.StartGameLoop();
        }

        [SupportedOSPlatform("windows")]
        public static void SetConsoleScreenSize(IntVector2 size)
        {
            Console.SetWindowSize(size.X, size.Y);
            Console.SetBufferSize(size.X, size.Y);
        }
    }
}