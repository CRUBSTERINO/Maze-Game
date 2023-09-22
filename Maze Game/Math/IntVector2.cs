namespace Maze_Game.Math
{
    public struct IntVector2
    {
        private int _x, _y;

        public int X => _x;
        public int Y => _y;

        public static IntVector2 Up => new IntVector2(0, 1);
        public static IntVector2 Down => new IntVector2(0, -1);
        public static IntVector2 Left => new IntVector2(-1, 0);
        public static IntVector2 Right => new IntVector2(1, 0);

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