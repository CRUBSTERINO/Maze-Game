using Maze_Game.Math;
using System.Runtime;

namespace Maze_Game.Physics
{
    public abstract class Collider : Component
    {
        private IntVector2 _previousPosition;
        private bool _isStatic; // static = doesn't check the collisions with other / non-static = checks his collisions every frame
        protected bool _isInCollision;

        public bool IsStatic => _isStatic;

        public Collider(bool isStatic, GameObject gameObject) : base(gameObject)
        {
            _isStatic = isStatic;
        }

        public override void Start()
        {
            _previousPosition = _gameObject.Position;
        }

        /*        public override void Update()
                {
                    if (_isStatic)
                    {
                        return;
                    }

                    if (_isInCollision)
                    {
                        _gameObject.SetPosition(_previousPosition);
                        _isInCollision = false;
                    }
                }*/

        public void RecoverToPreviousPosition()
        {
            _gameObject.SetPosition(_previousPosition);
        }

        public void UpdatePreviousPosition()
        {
            _previousPosition = _gameObject.Position;
        }

        public void ResetCollisionStatus()
        {
            _isInCollision = false;
        }

        public abstract bool CheckCollision(Collider collider);

        public abstract bool GetCollision(IntVector2 point);
    }
}