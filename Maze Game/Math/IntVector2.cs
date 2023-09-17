using System.Numerics;

namespace Maze_Game.Math
{
    public struct IntVector2
    {
        private int _x, _y;

        public int X => _x;
        public int Y => _y;

        public IntVector2(int x, int y)
        {
            _x = x;
            _y = y;
        }
    }
}