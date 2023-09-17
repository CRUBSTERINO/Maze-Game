namespace Maze_Game.Math
{
    public struct Rect
    {
        private int _x, _y;
        private int _width, _height;

        public int X => _x;
        public int Y => _y;
        public int Width => _width;
        public int Height => _height;

        public Rect(int x, int y, int width, int height)
        {
            _x = x;
            _y = y;
            _width = width;
            _height = height;
        }
    }
}