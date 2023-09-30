using Maze_Game.Physics;

namespace Maze_Game.Coins
{
    public class Coin : Component
    {
        private Collider _collider;

        public Coin(GameObject gameObject, Collider collider) : base(gameObject)
        {
            _collider = collider;

            _collider.OnTriggerEnter += PickUp;
        }

        private void PickUp()
        {
            _gameObject.Destroy();
            _collider.OnTriggerEnter -= PickUp;
        }
    }
}