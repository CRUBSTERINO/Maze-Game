using Maze_Game.Math;

namespace Maze_Game.Rendering
{
    public class CharRenderer : Renderer
    {
        private char _renderingChar;

        public CharRenderer(char renderingChar, GameObject gameObject) : base(gameObject)
        {
            _renderingChar = renderingChar;
        }

        public override char GetHit(IntVector2 position)
        {
            if (position.Equals(_gameObject.Position))
            {
                return _renderingChar;
            }

            return ' ';
        }

        public void SetRenderingChar(char renderingChar) 
        {
            _renderingChar = renderingChar;
        }
    }
}