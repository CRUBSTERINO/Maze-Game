namespace Maze_Game.Timer
{
    public class GameTimer : Component
    {
        private TimeSpan _interval;
        private DateTime _startTime;
        private DateTime _finishTime;
        private bool _isStarted;

        public TimeSpan Interval => _interval;
        public DateTime FinishTime => _finishTime;
        public bool IsStarted => _isStarted;

        public Action? OnTimerFinished;

        public GameTimer(GameObject gameObject, TimeSpan interval) : base(gameObject)
        {
            _interval = interval;
        }

        public void StartTimer()
        {
            _startTime = DateTime.Now;
            _finishTime = _startTime.Add(_interval);

            _isStarted = true;
        }

        public override void Update()
        {
            if (!_isStarted)
            {
                return;
            }

            if (DateTime.Now.CompareTo(_finishTime) >= 0)
            {
                OnTimerFinished?.Invoke();
            }
        }
    }
}