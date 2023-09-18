namespace Maze_Game
{
    public static class InputManager
    {
        private static ConsoleKey _pressedKey;

        public static ConsoleKey PressedKey => _pressedKey;

        public static void ProcessInput()
        {
            if (Console.KeyAvailable)
            {
                _pressedKey = Console.ReadKey(true).Key; 
            }
            else
            {
                _pressedKey = ConsoleKey.NoName;
            }
        }
    }
}