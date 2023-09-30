using Maze_Game.Math;

namespace Maze_Game.MazeGeneration
{
    public class MazeCell
    {
        private IntVector2 _position;
        private bool _isVisited;
        private bool _isInitialCell;
        private bool _isFinishCell;
        private bool _canBeVisited;
        private bool _isEmpty;
        private MazeCell? _previousCell;

        public IntVector2 Position => _position;
        public bool IsVisited { get => _isVisited; set { _isVisited = value; } }
        public bool IsInitialCell { get => _isInitialCell; set { _isInitialCell = value; } }
        public bool IsFinishCell { get => _isFinishCell; set { _isFinishCell = value; } }
        public bool CanBeVisited { get => _canBeVisited; set { _canBeVisited = value; } }
        public bool IsEmpty { get => _isEmpty; set { _isEmpty = value; } }
        public MazeCell? PreviousCell { get => _previousCell; set { _previousCell = value; } }

        public MazeCell(IntVector2 position, bool canBeVisited, bool isEmpty)
        {
            _position = position;
            _canBeVisited = canBeVisited;
            IsEmpty = isEmpty;
        }
    }
}