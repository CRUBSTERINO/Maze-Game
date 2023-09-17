namespace Maze_Game.GameLoop
{
    public abstract class GameLoop
    {

        public void StartGameLoop()
        {
            while (true)
            {
                Time();
                ProcessInput();
                Update();
                Render();
            }
        }

        protected abstract void Time();

        protected abstract void ProcessInput();

        protected abstract void Update();

        protected abstract void Render();
    }
}