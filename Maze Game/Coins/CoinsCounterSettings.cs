using Maze_Game.Math;

namespace Maze_Game.Coins
{
    public struct CoinsCounterSettings
    {
        private IntVector2 _position;

        public IntVector2 Position => _position;

        public CoinsCounterSettings(IntVector2 position)
        {
            _position = position;
        }
    }
}