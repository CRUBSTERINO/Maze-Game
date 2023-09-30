using Maze_Game.Math;

namespace Maze_Game.Physics
{
    public abstract class Collider : Component
    {
        private IntVector2 _previousPosition;
        private bool _isStatic; // static = doesn't check the collisions with other / non-static = checks his collisions every frame
        protected bool _isTrigger;
        protected bool _isInCollision;

        public bool IsStatic => _isStatic;

        public event Action? OnCollisionEnter;
        public event Action? OnTriggerEnter;

        public Collider(GameObject gameObject, bool isStatic, bool isTrigger) : base(gameObject)
        {
            _isStatic = isStatic;
            _isTrigger = isTrigger;
        }

        public override void Start()
        {
            _previousPosition = _gameObject.Position;
        }

        public override void PhysicsUpdate()
        {
            if (_isInCollision)
            {
                if (_isTrigger)
                {
                    OnTriggerEnter?.Invoke();
                }
                else
                {
                    _gameObject.SetPosition(_previousPosition);
                    _isInCollision = false;

                    OnCollisionEnter?.Invoke();
                }
            }
            else
            {
                _previousPosition = _gameObject.Position;
            }
        }

        public abstract void CheckCollision(Collider collider);

        public abstract bool GetCollision(IntVector2 point);
    }
}