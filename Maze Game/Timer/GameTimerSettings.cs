using Maze_Game.Math;

namespace Maze_Game.Timer
{
    public struct GameTimerSettings
    {
        private TimeSpan _interval;
        private IntVector2 _position;

        public TimeSpan Interval => _interval;
        public IntVector2 Position => _position;

        public GameTimerSettings(TimeSpan interval, IntVector2 position)
        {
            _interval = interval;
            _position = position;
        }
    }
}