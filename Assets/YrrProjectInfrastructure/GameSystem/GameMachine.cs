using Sirenix.OdinInspector;
using System;
using System.Collections.Generic;


namespace Infrastructure.GameSystem
{
    [Serializable]
    public sealed class GameMachine
    {
        private readonly List<IGameListener> _gameListeners = new(30);
        public GameState GameState { get; private set; }


        public void AddListener(IGameListener listener)
        {
            if (listener == null) return;

            _gameListeners.Add(listener);
        }

        public void RemoveListener(IGameListener listener)
        {
            if (listener == null) return;

            _gameListeners.Remove(listener);
        }

        [Button]
        public void StartGame()
        {
            GameState = GameState.Play;

            for (var i = 0; i < _gameListeners.Count; i++)
            {
                var listener = _gameListeners[i];
                if (listener is IGameStartListener stListener) stListener.OnGameStart();
            }
        }

        [Button]
        public void PauseGame()
        {
            GameState = GameState.Pause;

            for (var i = 0; i < _gameListeners.Count; i++)
            {
                var listener = _gameListeners[i];
                if (listener is IGamePauseListener pListener) pListener.OnGamePaused();
            }
        }

        [Button]
        public void UnPauseGame()
        {
            GameState = GameState.Play;

            for (var i = 0; i < _gameListeners.Count; i++)
            {
                var listener = _gameListeners[i];
                if (listener is IGamePauseListener pListener) pListener.OnGameUnPaused();
            }
        }
        [Button]
        public void FinishGame()
        {
            GameState = GameState.Finish;

            for (var i = 0; i < _gameListeners.Count; i++)
            {
                var listener = _gameListeners[i];
                if (listener is IGameFinishListener fListener) fListener.OnGameFinish();
            }
        }
    }

    public enum GameState
    {
        Off,
        Play,
        Pause,
        Finish
    }
}