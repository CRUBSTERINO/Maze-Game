using Maze_Game.Math;

namespace Maze_Game
{

    public class PlayerController : Component
    {
        private int _speed;
        private Rect _areaOfMovement;
        private MovementDirection _movementDirection;

        public PlayerController(GameObject gameObject, int speed, Rect areaOfMovement) : base(gameObject)
        {
            _speed = speed;
            _areaOfMovement = areaOfMovement;
        }

        public override void Update()
        {
            IntVector2 movementVector;

            _movementDirection = GetMovementDirection(InputManager.PressedKey);

            if (_movementDirection != MovementDirection.Stopped)
            {
                switch (_movementDirection)
                {
                    case MovementDirection.Up:
                        movementVector = IntVector2.Down * _speed;
                        break;
                    case MovementDirection.Down:
                        movementVector = IntVector2.Up * _speed;
                        break;
                    case MovementDirection.Left:
                        movementVector = IntVector2.Left * _speed;
                        break;
                    case MovementDirection.Right:
                        movementVector = IntVector2.Right * _speed;
                        break;
                    default:
                        movementVector = new IntVector2(0, 0);
                        break;
                }

                if (_areaOfMovement.IsInBounds(_gameObject.Position + movementVector))
                {
                    _gameObject.Move(movementVector);
                }
            }
        }

        private MovementDirection GetMovementDirection(ConsoleKey pressedKey)
        {
            MovementDirection movementDirection;

            switch (pressedKey)
            {
                case ConsoleKey.W:
                    movementDirection = MovementDirection.Up;
                    break;
                case ConsoleKey.S:
                    movementDirection = MovementDirection.Down;
                    break;
                case ConsoleKey.A:
                    movementDirection = MovementDirection.Left;
                    break;
                case ConsoleKey.D:
                    movementDirection = MovementDirection.Right;
                    break;
                default:
                    movementDirection = MovementDirection.Stopped;
                    break;
            }

            return movementDirection;
        }
    }
}