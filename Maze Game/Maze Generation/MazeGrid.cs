using Maze_Game.Math;

namespace Maze_Game.MazeGeneration
{
    public class MazeGrid
    {
        private IntVector2 _size;
        private MazeCell[,] _cells;
        private Random _randomGenerator;
        private MazeCell _initialCell;
        private MazeCell _finishCell;

        public MazeCell InitialCell => _initialCell;
        public MazeCell FinishCell => _finishCell;
        public MazeCell[,] Cells => _cells;

        public MazeGrid(IntVector2 size, IntVector2 initialCellPosition, IntVector2 finishCellPosition)
        {
            _size = size;
            _cells = new MazeCell[size.X , size.Y];
            _randomGenerator = new Random();

            for (int y = 0; y < size.Y; y++)
            {
                for (int x = 0; x < size.X; x++)
                {
                    _cells[x, y] = new MazeCell(new IntVector2(x, y), true);
                }
            }

            _initialCell = _cells[initialCellPosition.X, initialCellPosition.Y];
            _initialCell.IsInitialCell = true;

            _finishCell = _cells[finishCellPosition.X, finishCellPosition.Y];
            _finishCell.IsFinishCell = true;
        }

        public bool TryGetNextCell(MazeCell cell, out MazeCell? neighbourCell)
        {
            neighbourCell = GetUnvisitedNeighbour(cell);

            if (neighbourCell == null)
            {
                return false;
            }
            
            return true;
        }

        private MazeCell? GetUnvisitedNeighbour(MazeCell cell)
        {
            List<MazeCell> avaliableCells = new List<MazeCell>(4);
            MazeCell nextCell;
            List<MazeCell> neighbouringCells = GetNeighbouringCells(cell);
            
            foreach (MazeCell neighbourCell in neighbouringCells)
            {
                if (CanCellBeVisited(cell, neighbourCell))
                {
                    avaliableCells.Add(neighbourCell);
                }
            }

            if (avaliableCells.Count == 0)
            {
                return null;
            }

            nextCell = avaliableCells[(int)_randomGenerator.NextInt64(0, avaliableCells.Count)];

            return nextCell;
        }

        private bool CanCellBeVisited(MazeCell previousCell, MazeCell cell)
        {
            bool areNeighbouringCellsNotVisited = true;

            if (!cell.CanBeVisited || cell.IsVisited)
            {
                return false;
            }

            List<MazeCell> neighbouringCells = GetNeighbouringCells(cell);

            foreach (MazeCell neighbourCell in neighbouringCells)
            {
                if (neighbourCell.IsVisited &&
                    !neighbourCell.Equals(previousCell))
                {
                    areNeighbouringCellsNotVisited = false;
                }

                if (neighbourCell.IsFinishCell)
                {
                    return true;
                }
            }

            return areNeighbouringCellsNotVisited;
        }

        private List<MazeCell> GetNeighbouringCells(MazeCell cell)
        {
            List<MazeCell> neighbouringCells = new List<MazeCell>(4);

            if (cell.Position.Y + IntVector2.Up.Y < _size.Y)
            {
                neighbouringCells.Add(_cells[cell.Position.X, cell.Position.Y + 1]);
            }

            if (cell.Position.Y + IntVector2.Down.Y >= 0)
            {
                neighbouringCells.Add(_cells[cell.Position.X, cell.Position.Y - 1]);
            }

            if (cell.Position.X + IntVector2.Left.X >= 0)
            {
                neighbouringCells.Add(_cells[cell.Position.X - 1, cell.Position.Y]);
            }

            if (cell.Position.X + IntVector2.Right.X < _size.X)
            {
                neighbouringCells.Add(_cells[cell.Position.X + 1, cell.Position.Y]);
            }

            return neighbouringCells;
        }
    }
}