using Maze_Game.Math;

namespace Maze_Game.Rendering
{
    public abstract class Renderer : Component
    {
        public Renderer(GameObject gameObject) : base(gameObject)
        {

        }

        public abstract char GetHit(IntVector2 position);

        public bool TryGetHit(IntVector2 position, out char outChar)
        {
            char hitChar = GetHit(position);
            outChar = hitChar;

            if (hitChar != ' ')
            {
                return true;
            }

            return false;
        }
    }
}