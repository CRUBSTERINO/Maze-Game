﻿using Maze_Game.GameWorlds;
using Maze_Game.Rendering;

namespace Maze_Game.GameLoop
{
    public class MazeGameLoop : GameLoop
    {
        private GameWorld _gameWorld;
        private GameRenderer _gameRenderer;

        public MazeGameLoop(GameWorld gameWorld, GameRenderer gameRenderer)
        {
            _gameWorld = gameWorld;
            _gameRenderer = gameRenderer;
        }

        protected override void Time()
        {
            Thread.Sleep(5);
        }

        protected override void ProcessInput()
        {
            InputManager.ProcessInput();
        }

        protected override void Update()
        {
            _gameWorld.UpdateGameObjects();
            _gameWorld.UpdateCollisions();
            _gameWorld.UpdateGameObjectsPhysics();
            _gameWorld.CleanUpDestroyedGameObjects();
            _gameWorld.InstantiateCreatedGameObjects();
        }

        protected override void Render()
        {
            string frame = _gameRenderer.RenderFrame();
            DisplayRenderedFrame(frame);
        }

        private void DisplayRenderedFrame(string renderedFrame)
        {
            Console.SetCursorPosition(0, 0);
            Console.Write(renderedFrame);
        }
    }
}