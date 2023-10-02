namespace Maze_Game
{
    public abstract class Component
    {
        protected GameObject _gameObject;

        public GameObject GameObject => _gameObject;

        public Component(GameObject gameObject)
        {
            _gameObject = gameObject;
        }

        public virtual void Start()
        {

        }

        public virtual void Update()
        {

        }

        public virtual void PhysicsUpdate()
        {

        }

        public virtual void OnDestroy()
        {

        }
    }
}