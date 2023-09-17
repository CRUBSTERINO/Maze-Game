using Maze_Game.Math;
namespace Maze_Game.GameWorlds
{
    public class GameWorld
    {
        protected Rect _worldSize;
        protected List<GameObject> _gameObjects;

        public GameWorld(Rect worldSize)
        {
            _worldSize = worldSize;
            _gameObjects = new List<GameObject>();
        }

        public void Create(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public List<T> FindAllComponentsInWorld<T>() where T : Component
        {
            List<T> result = new List<T>();
            T tempComponent;
            foreach (GameObject gameObject in _gameObjects) 
            {
                if (gameObject.TryGetComponent<T>(out tempComponent))
                {
                    result.Add(tempComponent);
                }
            }

            return result;
        }

        public void UpdateGameObjects()
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.Update();
            }
        }
    }
}