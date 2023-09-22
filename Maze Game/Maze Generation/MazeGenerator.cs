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
        private GameObject[,] _wallGameObjects;
        private GameWorld _gameWorld;

        public MazeGenerator(Rect mazeArea, char wallChar, GameWorld gameWorld)
        {
            _area = mazeArea;
            _wallChar = wallChar;
            _gameWorld = gameWorld;
            _wallGameObjects = new GameObject[mazeArea.Size.X, mazeArea.Size.Y];

            _grid = new MazeGrid(_area.Size, new IntVector2(3, 0));
        }

        public void GenerateMaze()
        {
            // Fill maze with cells
            foreach (MazeCell cell in _grid.Cells) 
            {
                CreateWallGameObject(cell.Position + _area.Position);
            }

            RandomizedDFS(_grid.InitialCell);
        }

        private void RandomizedDFS(MazeCell previousCell)
        {
            MazeCell nextCell;
            previousCell.MarkAsVisited();

            while (_grid.TryGetNextCell(previousCell, out nextCell))
            {
                nextCell.SetPreviousCell(previousCell);
                DestroyWallGameObject(nextCell.Position);

                RandomizedDFS(nextCell);
            }
        }

        private void CreateWallGameObject(IntVector2 position)
        {
            GameObject wallGameObject = new GameObject(_gameWorld, position);
            wallGameObject.AddComponent(new CharRenderer(_wallChar, wallGameObject));
            wallGameObject.Create();

            position -= _area.Position;
            _wallGameObjects[position.X, position.Y] = wallGameObject;
        }

        private void DestroyWallGameObject(IntVector2 position)
        {
            _wallGameObjects[position.X, position.Y].Destroy();
        }
    }
}