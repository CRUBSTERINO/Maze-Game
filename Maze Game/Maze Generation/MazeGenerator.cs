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
        private MazeGrid? _grid;
        private GameWorld _gameWorld;
        private List<GameObject> _wallGameObjects;

        public MazeGenerator(Rect mazeArea, char wallChar, GameWorld gameWorld)
        {
            _area = mazeArea;
            _wallChar = wallChar;
            _gameWorld = gameWorld;
            _wallGameObjects = new List<GameObject>();
        }

        public void GenerateMaze(out IntVector2 startLocalPosition, out IntVector2 finishLocalPosition)
        {
            Random random = new Random();
            startLocalPosition = new IntVector2(random.Next(1, _area.Size.X - 1), 0);
            finishLocalPosition = new IntVector2(random.Next(1, _area.Size.X - 1), _area.Size.Y - 1);

            _grid = new MazeGrid(_area.Size, startLocalPosition, finishLocalPosition);

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

            List<IntVector2> occupiedCells = _grid.GetAllOccupiedCellLocalPositions();
            _wallGameObjects = new List<GameObject>(occupiedCells.Count);
            foreach (IntVector2 emptyCellPosition in occupiedCells)
            {
                CreateWallGameObject(emptyCellPosition + _area.Position);
            }
        }

        public void DestroyAllMaze()
        {
            foreach (GameObject wall in _wallGameObjects)
            {
                wall.Destroy();
            }
        }

        public List<IntVector2> GetAllEmptyCellGlobalPositions()
        {
            if (_grid == null )
            {
                throw new Exception("Grid is not initialized");
            }

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
            if (_grid == null)
            {
                throw new Exception("Grid is not initialized");
            }

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

            _wallGameObjects?.Add(wallGameObject);
        }
    }
}