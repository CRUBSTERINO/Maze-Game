namespace Maze_Game.Math
{
    public struct Rect
    {
        private IntVector2 _position;
        private IntVector2 _size;

        public IntVector2 Position => _position;
        public IntVector2 Size => _size;

        public Rect(int x, int y, int width, int height)
        {
            _position = new IntVector2(x, y);
            _size = new IntVector2(width, height);
        }

        public Rect(IntVector2 position, IntVector2 size)
        {
            _position = position;
            _size = size;
        }

        public bool IsInBounds(IntVector2 point)
        {
            return (point.X >= _position.X && point.X < _position.X + _size.X) && (point.Y >= _position.Y && point.Y < _position.Y + _size.Y);
        }
    }
}