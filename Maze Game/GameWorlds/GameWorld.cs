using Maze_Game.Math;
using Maze_Game.Physics;

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

        public void Destroy(GameObject gameObject)
        {
            if (_gameObjects.Contains(gameObject))
            {
                _gameObjects.Remove(gameObject);
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

        public void UpdatePhysics()
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
                    bool isCollided = false;
                    for (int colliderCheckingIndex = 0; colliderCheckingIndex < colliders.Count; colliderCheckingIndex++)
                    {
                        if (colliderCheckingIndex != colliderIndex)
                        {
                            if(colliders[colliderIndex].CheckCollision(colliders[colliderCheckingIndex]))
                            {
                                isCollided = true;
                            }
                        }
                    }

                    if (!isCollided)
                    {
                        colliders[colliderIndex].UpdatePreviousPosition();
                    }
                    else
                    {
                        colliders[colliderIndex].RecoverToPreviousPosition();
                        colliders[colliderIndex].ResetCollisionStatus();
                    }
                }
            }
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