using Maze_Game.Math;

namespace Maze_Game
{
    public struct PlayerSettings
    {
        private Rect _areaOfMovement;
        private char _playerChar;
        private IntVector2 _position;

        public Rect AreaOfMovement => _areaOfMovement;
        public char PlayerChar => _playerChar;
        public IntVector2 Position => _position;

        public PlayerSettings(Rect areaOfMovement, char playerChar, IntVector2 position)
        {
            _areaOfMovement = areaOfMovement;
            _playerChar = playerChar;
            _position = position;
        }
    }
}