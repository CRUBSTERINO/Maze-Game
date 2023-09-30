using Maze_Game.Math;
namespace Maze_Game.Physics
{
    public class CharCollider : Collider
    {
        public CharCollider(GameObject gameObject, bool isStatic, bool isTrigger) : base(gameObject, isStatic, isTrigger)
        {

        }

        public override void CheckCollision(Collider collider)
        {
            if (collider.GetCollision(_gameObject.Position))
            {
                _isInCollision = true;
            }
        }

        public override bool GetCollision(IntVector2 point)
        {
            if (_isTrigger)
            {
                return false;
            }

            bool collisionResult = _gameObject.Position.Equals(point);

            return collisionResult;
        }
    }
}