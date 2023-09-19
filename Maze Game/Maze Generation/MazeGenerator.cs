using Maze_Game.GameWorlds;
using Maze_Game.Math;
using Maze_Game.Rendering;

namespace Maze_Game.MazeGeneration
{
    public class MazeGenerator
    {
        private Rect _area;
        private char _wallChar;
        private MazeGrid _grid;
        private GameWorld _gameWorld;

        public MazeGenerator(Rect mazeArea, char wallChar, GameWorld gameWorld)
        {
            _area = mazeArea;
            _wallChar = wallChar;
            _gameWorld = gameWorld;

            _grid = new MazeGrid(new IntVector2(_area.Width, _area.Height), new IntVector2(0, 0));
        }

        public void GenerateMaze()
        {
            do
            {
                CreateWallGameObject(_grid.ActiveCell.Position);
            }
            while (_grid.TryGetNextCell());
        }

        private void CreateWallGameObject(IntVector2 position)
        {
            GameObject wallGameObject = new GameObject(_gameWorld, position);
            wallGameObject.AddComponent(new CharRenderer(_wallChar, wallGameObject));
            wallGameObject.Create();
        }
    }

    public class MazeGrid
    {
        private IntVector2 _size;
        private MazeCell[,] _cells;
        private MazeCell _activeCell;

        public MazeCell ActiveCell => _activeCell;

        public MazeGrid(IntVector2 size, IntVector2 initialActiveCellPosition)
        {
            _size = size;
            _cells = new MazeCell[size.X , size.Y];

            for (int y = 0; y < size.Y; y++)
            {
                for (int x = 0; x < size.X; x++)
                {
                    _cells[x, y] = new MazeCell(new IntVector2(x, y));
                }
            }

            _activeCell = _cells[initialActiveCellPosition.X, initialActiveCellPosition.Y];
        }

        public bool TryGetNextCell()
        {
            if (_activeCell.Position.X + 1 < _size.X)
            {
                _activeCell = _cells[_activeCell.Position.X + 1, _activeCell.Position.Y];
            }
            else if (_activeCell.Position.Y + 1 < _size.Y)
            {
                _activeCell = _cells[0, _activeCell.Position.Y + 1];
            }
            else
            {
                return false;
            }

            return true;
        }
    }

    public struct MazeCell
    {
        private IntVector2 _position;
        private bool _isVisited;

        public IntVector2 Position => _position;
        public bool IsVisited => _isVisited;

        public MazeCell(IntVector2 position)
        {
            _position = position;
        }
    }
}