﻿using Maze_Game.Rendering;

namespace Maze_Game.Timer
{
    public class GameTimerDisplay : Component
    {
        private GameTimer _gameTimer;
        private TextFieldRenderer _textRenderer;

        public GameTimerDisplay(GameObject gameObject, GameTimer gameTimer, TextFieldRenderer textRenderer) : base(gameObject)
        {
            _gameTimer = gameTimer;
            _textRenderer = textRenderer;
        }

        public override void Update()
        {
            long timeLeft;

            if (_gameTimer.IsStarted)
            {
                timeLeft = (_gameTimer.FinishTime.Ticks - DateTime.Now.Ticks) / TimeSpan.TicksPerSecond;
            }
            else
            {
                timeLeft = _gameTimer.Interval.Seconds;
            }

            _textRenderer.SetText($"Time: {timeLeft}");
        }
    }
}