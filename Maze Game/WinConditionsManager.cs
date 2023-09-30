﻿using Maze_Game.Math;

namespace Maze_Game
{
    public class WinConditionsManager : Component
    {
        private GameObject _playerGameObject;
        private IntVector2 _finishCellPosition;

        public event Action? OnMazeCompleted;

        public WinConditionsManager(GameObject gameObject, GameObject playerGameObject, IntVector2 finishCellPosition) : base(gameObject)
        {
            _playerGameObject = playerGameObject;
            _finishCellPosition = finishCellPosition;
        }

        public override void Update()
        {
            if (_playerGameObject.Position.Equals(_finishCellPosition))
            {
                OnMazeCompleted?.Invoke();
            }
        }
    }
}