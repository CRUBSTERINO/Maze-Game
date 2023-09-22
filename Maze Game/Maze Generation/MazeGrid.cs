using Maze_Game.Math;

namespace Maze_Game.MazeGeneration
{
    public class MazeGrid
    {
        private IntVector2 _size;
        private MazeCell[,] _cells;
        private Random _randomGenerator;
        private MazeCell _initialCell;

        public MazeCell InitialCell => _initialCell;
        public MazeCell[,] Cells => _cells;

        public MazeGrid(IntVector2 size, IntVector2 initialActiveCellPosition)
        {
            _size = size;
            _cells = new MazeCell[size.X , size.Y];
            _randomGenerator = new Random();

            for (int y = 0; y < size.Y; y++)
            {
                for (int x = 0; x < size.X; x++)
                {
                    _cells[x, y] = new MazeCell(new IntVector2(x, y));
                }
            }

            _initialCell = _cells[initialActiveCellPosition.X, initialActiveCellPosition.Y];
            _initialCell.IsInitialCell = true;
        }

        public bool TryGetNextCell(MazeCell cell, out MazeCell neighbourCell)
        {
            neighbourCell = GetUnvisitedNeighbour(cell);

            if (neighbourCell == null)
            {
                return false;
            }
            
            return true;
        }

        private MazeCell GetUnvisitedNeighbour(MazeCell cell)
        {
            List<MazeCell> avaliableCells = new List<MazeCell>(4);
            MazeCell nextCell;

            if (cell.Position.Y + IntVector2.Up.Y < _size.Y &&
                !_cells[cell.Position.X, cell.Position.Y + 1].IsVisited &&
                CanCellBeVisited(cell, _cells[cell.Position.X, cell.Position.Y + 1]))
            {
                avaliableCells.Add(_cells[cell.Position.X, cell.Position.Y + 1]);
            }

            if (cell.Position.Y + IntVector2.Down.Y >= 0 &&
                !_cells[cell.Position.X, cell.Position.Y - 1].IsVisited &&
                CanCellBeVisited(cell, _cells[cell.Position.X, cell.Position.Y - 1]))
            {
                avaliableCells.Add(_cells[cell.Position.X, cell.Position.Y - 1]);
            }

            if (cell.Position.X + IntVector2.Left.X >= 0 &&
                !_cells[cell.Position.X - 1, cell.Position.Y].IsVisited &&
                CanCellBeVisited(cell, _cells[cell.Position.X - 1, cell.Position.Y]))
            {
                avaliableCells.Add(_cells[cell.Position.X - 1, cell.Position.Y]);
            }

            if (cell.Position.X + IntVector2.Right.X < _size.X &&
                !_cells[cell.Position.X + 1, cell.Position.Y].IsVisited &&
                CanCellBeVisited(cell, _cells[cell.Position.X + 1, cell.Position.Y]))
            {
                avaliableCells.Add(_cells[cell.Position.X + 1, cell.Position.Y]);
            }

            if (avaliableCells.Count == 0)
            {
                if (cell.IsInitialCell)
                {
                    return null;
                }

                nextCell = cell.PreviousCell;
                return GetUnvisitedNeighbour(nextCell);
            }

            nextCell = avaliableCells[(int)_randomGenerator.NextInt64(0, avaliableCells.Count)];

            return nextCell;
        }

        private bool CanCellBeVisited(MazeCell previousCell, MazeCell cell)
        {
            bool areNeighbouringCellsNotVisited = true;

            if (cell.Position.Y + IntVector2.Up.Y < _size.Y &&
                !_cells[cell.Position.X, cell.Position.Y + 1].Equals(previousCell) &&
                _cells[cell.Position.X, cell.Position.Y + 1].IsVisited)
            {
                areNeighbouringCellsNotVisited = false;
            }

            if (cell.Position.Y + IntVector2.Down.Y >= 0 &&
                !_cells[cell.Position.X, cell.Position.Y - 1].Equals(previousCell) &&
                _cells[cell.Position.X, cell.Position.Y - 1].IsVisited)
            {
                areNeighbouringCellsNotVisited = false;
            }

            if (cell.Position.X + IntVector2.Left.X >= 0 &&
                !_cells[cell.Position.X - 1, cell.Position.Y].Equals(previousCell) &&
                _cells[cell.Position.X - 1, cell.Position.Y].IsVisited)
            {
                areNeighbouringCellsNotVisited = false;
            }

            if (cell.Position.X + IntVector2.Right.X < _size.X &&
                !_cells[cell.Position.X + 1, cell.Position.Y].Equals(previousCell) &&
                _cells[cell.Position.X + 1, cell.Position.Y].IsVisited)
            {
                areNeighbouringCellsNotVisited = false;
            }

            return areNeighbouringCellsNotVisited;
        }
    }
}