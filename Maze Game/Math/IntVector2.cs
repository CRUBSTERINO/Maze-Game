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

        public static IntVector2 operator +(IntVector2 a, IntVector2 b)
        {
            IntVector2 c = new IntVector2(a.X + b.X, a.Y + b.Y);
            return c;
        }

        public static IntVector2 operator -(IntVector2 a, IntVector2 b)
        {
            IntVector2 c = new IntVector2(a.X - b.X, a.Y - b.Y);
            return c;
        }
    }
}