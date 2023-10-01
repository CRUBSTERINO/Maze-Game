using Maze_Game.Math;

namespace Maze_Game.Rendering
{
    public class TextFieldRenderer : Renderer
    {
        private string _text;

        public TextFieldRenderer(GameObject gameObject, string text) : base(gameObject)
        {
            _text = text;
        }

        public override char GetHit(IntVector2 position)
        {
            if (position.Y == _gameObject.Position.Y &&
                _gameObject.Position.X <= position.X && position.X < _gameObject.Position.X + _text.Length)
            {
                int charIndex = position.X - _gameObject.Position.X;
                return _text[charIndex];
            }

            return ' ';
        }

        public void SetText(string text)
        {
            _text = text;
        }
    }
}