using Maze_Game.Math;
using Maze_Game.Physics;

namespace Maze_Game.GameWorlds
{
    public class GameWorld
    {
        protected Rect _worldSize;
        protected List<GameObject> _gameObjects;
        private List<GameObject> _destroyedGameObjects;

        public GameWorld(Rect worldSize)
        {
            _worldSize = worldSize;
            _gameObjects = new List<GameObject>();
            _destroyedGameObjects = new List<GameObject>();
        }

        public void Create(GameObject gameObject)
        {
            _gameObjects.Add(gameObject);
        }

        public void Destroy(GameObject gameObject)
        {
            if (_gameObjects.Contains(gameObject))
            {
                _destroyedGameObjects.Add(gameObject);
            }
        }

        public List<T> FindAllComponentsInWorld<T>() where T : Component
        {
            List<T> result = new List<T>();
            T? tempComponent;
            foreach (GameObject gameObject in _gameObjects) 
            {
                if (gameObject.TryGetComponent(out tempComponent) && tempComponent != null)
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

        public void UpdateCollisions()
        {
            List<Collider> colliders = new List<Collider>();
            foreach (GameObject gameObject in _gameObjects)
            {
                if (gameObject.TryGetComponent<Collider>(out Collider? collider) && collider != null)
                {
                    colliders.Add(collider);
                }
            }

            for (int colliderIndex = 0;  colliderIndex < colliders.Count; colliderIndex++)
            {
                if (!colliders[colliderIndex].IsStatic)
                {
                    for (int colliderCheckingIndex = 0; colliderCheckingIndex < colliders.Count; colliderCheckingIndex++)
                    {
                        if (colliderCheckingIndex != colliderIndex)
                        {
                            colliders[colliderIndex].CheckCollision(colliders[colliderCheckingIndex]);
                        }
                    }
                }
            }
        }

        public void UpdateGameObjectsPhysics()
        {
            foreach (var gameObject in _gameObjects)
            {
                gameObject.PhysicsUpdate();
            }
        }

        public void CleanUpDestroyedGameObjects()
        {
            foreach (GameObject gameObject in _destroyedGameObjects)
            {
                _gameObjects.Remove(gameObject);
            }

            _destroyedGameObjects.Clear();
        }
    }
}