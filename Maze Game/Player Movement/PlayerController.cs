using Maze_Game.Math;
using System.Diagnostics;

namespace Maze_Game
{

    public class PlayerController : Component
    {
        private int _speed;
        private MovementDirection _movementDirection;

        public PlayerController(int speed, GameObject gameObject) : base(gameObject)
        {
            _speed = speed;
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
                        movementVector = new IntVector2(0, -1);
                        break;
                    case MovementDirection.Down:
                        movementVector = new IntVector2(0, 1);
                        break;
                    case MovementDirection.Left:
                        movementVector = new IntVector2(-1, 0);
                        break;
                    case MovementDirection.Right:
                        movementVector = new IntVector2(1, 0);
                        break;
                    default:
                        movementVector = new IntVector2(0, 0);
                        break;
                }

                _gameObject.Move(movementVector);
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