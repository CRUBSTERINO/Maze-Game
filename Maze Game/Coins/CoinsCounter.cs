using Maze_Game.Rendering;

namespace Maze_Game.Coins
{
    public class CoinsCounter : Component
    {
        private TextFieldRenderer _textRenderer;

        public CoinsCounter(GameObject gameObject, CoinsManager coinsManager, TextFieldRenderer textRenderer) : base(gameObject)
        {
            _textRenderer = textRenderer;
            _textRenderer.SetText("Coins: 0");
            coinsManager.OnCoinsCountChanged += CoinsCountUpdatedHandler;
        }

        private void CoinsCountUpdatedHandler(int coinsCount)
        {
            _textRenderer.SetText($"Coins: {coinsCount}");
        }
    }
}