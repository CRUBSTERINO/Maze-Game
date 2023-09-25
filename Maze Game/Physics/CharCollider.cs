using Maze_Game.Math;

namespace Maze_Game.Physics
{
    public class CharCollider : Collider
    {
        public CharCollider(bool isStatic, GameObject gameObject) : base(isStatic, gameObject)
        {

        }

        public override bool CheckCollision(Collider collider)
        {
            if (collider.GetCollision(_gameObject.Position))
            {
                _isInCollision = true;
            }

            return _isInCollision;
        }

        public override bool GetCollision(IntVector2 point)
        {
            return _gameObject.Position.Equals(point);
        }
    }
}