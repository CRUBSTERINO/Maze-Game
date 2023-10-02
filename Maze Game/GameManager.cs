using Maze_Game.Coins;
using Maze_Game.GameWorlds;
using Maze_Game.Math;
using Maze_Game.MazeGeneration;
using Maze_Game.Physics;
using Maze_Game.Rendering;
using Maze_Game.Timer;

namespace Maze_Game
{
    public class GameManager
    {
        private GameWorld _gameWorld;
        private MazeGenerator? _mazeGenerator;
        private GameObject? _playerGameObject;
        private GameObject? _mazeFinishGameObject;
        private CoinsManager? _coinsManager;
        private GameTimer? _gameTimer;
        private IntVector2 _finishGlobalPosition;

        PlayerSettings _playerSettings;
        MazeSettings _mazeSettings;
        CoinsGenerationsSettings _coinGenerationSettings;
        CoinsCounterSettings _coinsCounterSettings;
        GameTimerSettings _gameTimerSettings;

        public GameManager(GameWorld gameWorld, PlayerSettings playerSettings, MazeSettings mazeSettings, CoinsGenerationsSettings coinGenerationSettings,
                           CoinsCounterSettings coinsCounterSettings, GameTimerSettings gameTimerSettings)
        {
            _gameWorld = gameWorld;
            _playerSettings = playerSettings;
            _mazeSettings = mazeSettings;
            _coinGenerationSettings = coinGenerationSettings;
            _coinsCounterSettings = coinsCounterSettings;
            _gameTimerSettings = gameTimerSettings;
        }

        public void SetupGame()
        {
            _coinsManager = new CoinsManager();

            CreatePlayer();
            GenerateMaze();
            GenerateCoins();
            CreateCoinsCounter();
            CreateGameTimer();
            SetupWinConditions();
            SetupLooseConditions();
        }

        private void RestartLevel()
        {
            _mazeGenerator?.DestroyAllMaze();
            _coinsManager?.DestroyAllSpawnedCoins();
            _mazeFinishGameObject?.Destroy();

            _playerGameObject?.SetPosition(_playerSettings.Position);
            GenerateMaze();
            GenerateCoins();
            CreateCoinsCounter();
            _gameTimer?.StartTimer();
            SetupWinConditions();
        }

        private void CompleteLevel()
        {
            _coinsManager?.ConsolidateCollectedOnLevelCoins();
            RestartLevel();
        }

        private void FailLevel()
        {
            _coinsManager?.RevertCollectedOnLevelCoins();
            RestartLevel();
        }

        private void CreatePlayer()
        {
            _playerGameObject = new GameObject(_gameWorld, _playerSettings.Position);
            _playerGameObject.AddComponent(new CharRenderer(_playerGameObject, _playerSettings.PlayerChar));
            _playerGameObject.AddComponent(new PlayerController(_playerGameObject, 1, _playerSettings.AreaOfMovement));
            _playerGameObject.AddComponent(new CharCollider(_playerGameObject, false, false));

            _playerGameObject.Create();
        }

        private void GenerateMaze()
        {
            _mazeGenerator = new MazeGenerator(_mazeSettings.Area, _mazeSettings.WallChar, _gameWorld);
            _mazeGenerator.GenerateMaze(out IntVector2 startLocalPosition, out IntVector2 finishLocalPosition);
            _finishGlobalPosition = finishLocalPosition + _mazeSettings.Area.Position;
        }

        private void GenerateCoins()
        {
            if (_mazeGenerator == null)
            {
                throw new Exception("Maze Generator is not initialized");
            }

            CoinGenerator coinGenerator = new CoinGenerator(_gameWorld, _mazeGenerator.GetAllEmptyCellGlobalPositions(), _coinGenerationSettings.CoinChar, _coinGenerationSettings.CoinNumber);
            List<Coin> coins = coinGenerator.SpawnCoins();

            if (_coinsManager != null)
            {
                _coinsManager.AssignCoins(coins);
            }
            else
            {
                throw new Exception("Coins Manager is not initialized");
            }
        }

        private void CreateCoinsCounter()
        {
            if (_coinsManager == null)
            {
                throw new Exception("Coin Manager is not initialized");
            }

            GameObject coinsCounterGameObject = new GameObject(_gameWorld, _coinsCounterSettings.Position);
            TextFieldRenderer textRenderer = coinsCounterGameObject.AddComponent(new TextFieldRenderer(coinsCounterGameObject, ""));
            coinsCounterGameObject.AddComponent(new CoinsCounter(coinsCounterGameObject, _coinsManager, textRenderer));

            coinsCounterGameObject.Create();
        }

        private void CreateGameTimer()
        {
            GameObject timerGameObject = new GameObject(_gameWorld, _gameTimerSettings.Position);
            _gameTimer = timerGameObject.AddComponent(new GameTimer(timerGameObject, _gameTimerSettings.Interval));
            TextFieldRenderer textRenderer = timerGameObject.AddComponent(new TextFieldRenderer(timerGameObject, ""));
            timerGameObject.AddComponent(new GameTimerDisplay(timerGameObject, _gameTimer, textRenderer));

            timerGameObject.Create();

            _gameTimer.StartTimer();
        }

        private void SetupWinConditions()
        {
            if (_playerGameObject == null)
            {
                throw new Exception("Player GameObject is not initialized");
            }

            _mazeFinishGameObject = new GameObject(_gameWorld, IntVector2.Zero);
            MazeFinish mazeFinish = _mazeFinishGameObject.AddComponent(new MazeFinish(_mazeFinishGameObject, _playerGameObject, _finishGlobalPosition));
            mazeFinish.OnMazeCompleted += CompleteLevel;

            _mazeFinishGameObject.Create();
        }

        private void SetupLooseConditions()
        {
            if (_gameTimer == null)
            {
                throw new Exception("Game Timer is not initialized");
            }

            _gameTimer.OnTimerFinished += FailLevel;
        }
    }
}