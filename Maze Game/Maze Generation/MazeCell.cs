using Maze_Game.Math;

namespace Maze_Game.MazeGeneration
{
    public class MazeCell
    {
        private IntVector2 _position;
        private bool _isVisited;
        private bool _isInitialCell;
        private MazeCell _previousCell;

        public IntVector2 Position => _position;
        public bool IsVisited => _isVisited;
        public bool IsInitialCell { get => _isInitialCell; set { _isInitialCell = value; } }
        public MazeCell PreviousCell => _previousCell;

        public MazeCell(IntVector2 position)
        {
            _position = position;
        }

        public void MarkAsVisited()
        {
            _isVisited = true;
        }

        public void SetPreviousCell(MazeCell previousCell)
        {
            _previousCell = previousCell;
        }
    }
}