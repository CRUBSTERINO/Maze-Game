using Maze_Game.GameWorlds;
using Maze_Game.Math;
using Maze_Game.Physics;
using Maze_Game.Rendering;

namespace Maze_Game.MazeGeneration
{
    public class MazeGenerator
    {
        private Rect _area;
        private char _wallChar;
        private MazeGrid _grid;
        private GameWorld _gameWorld;

        public MazeGenerator(Rect mazeArea, char wallChar, GameWorld gameWorld, IntVector2 startPosition, IntVector2 finishPosition)
        {
            _area = mazeArea;
            _wallChar = wallChar;
            _gameWorld = gameWorld;
            _wallGameObjects = new GameObject[mazeArea.Size.X, mazeArea.Size.Y];

            _grid = new MazeGrid(_area.Size, startPosition, finishPosition);
        }

        public void GenerateMaze()
        {
            MazeCell[,] cells = _grid.Cells;

            for (int x = 0; x < _area.Size.X; x++)
            {
                cells[x, 0].CanBeVisited = false;
                cells[x, _area.Size.Y - 1].CanBeVisited = false;
            }

            for (int y = 0; y < _area.Size.Y; y++)
            {
                cells[0, y].CanBeVisited = false;
                cells[_area.Size.X - 1, y].CanBeVisited = false;
            }

            _grid.InitialCell.CanBeVisited = true;
            _grid.FinishCell.CanBeVisited = true;

            RandomizedDFS(_grid.InitialCell);

            foreach (IntVector2 emptyCellPosition in _grid.GetAllOccupiedCellLocalPositions())
            {
                CreateWallGameObject(emptyCellPosition + _area.Position);
            }
        }

        public List<IntVector2> GetAllEmptyCellGlobalPositions()
        {
            List<IntVector2> emptyCellLocalPositions = _grid.GetAllEmptyCellLocalPositions();
            List<IntVector2> emptyCellGlobalPositions = emptyCellLocalPositions;

            for (int i = 0; i < emptyCellGlobalPositions.Count; i++)
            {
                emptyCellGlobalPositions[i] += _area.Position;
            }

            return emptyCellGlobalPositions;
        }

        private void RandomizedDFS(MazeCell previousCell)
        {
            MazeCell? nextCell;
            previousCell.IsVisited = true;
            previousCell.IsEmpty = true;

            while (_grid.TryGetNextCell(previousCell, out nextCell) && nextCell != null)
            {
                nextCell.PreviousCell = previousCell;

                RandomizedDFS(nextCell);
            }
        }

        private void CreateWallGameObject(IntVector2 position)
        {
            GameObject wallGameObject = new GameObject(_gameWorld, position);
            wallGameObject.AddComponent(new CharRenderer(wallGameObject, _wallChar));
            wallGameObject.AddComponent(new CharCollider(wallGameObject, true, false));
            wallGameObject.Create();

            position -= _area.Position;
        }
    }
}