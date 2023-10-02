using Maze_Game.Math;

namespace Maze_Game.MazeGeneration
{
    public struct MazeSettings
    {
        private Rect _area;
        private char _wallChar;

        public Rect Area => _area;
        public char WallChar => _wallChar;

        public MazeSettings(Rect area, char wallChar)
        {
            _area = area;
            _wallChar = wallChar;
        }
    }
}
