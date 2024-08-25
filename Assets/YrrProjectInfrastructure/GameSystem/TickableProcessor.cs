using System.Collections.Generic;
using UnityEngine;


namespace Infrastructure.GameSystem
{
    public sealed class TickableProcessor : MonoBehaviour, IGameStartListener, IGameFinishListener, IGamePauseListener
    {
        private readonly LinkedList<IFixedUpdateListener> _fixedTickables = new();
        private readonly List<IFixedUpdateListener> _fixedTickablesForAdding = new();
        private readonly List<IFixedUpdateListener> _fixedTickablesForRemoving = new();
        private readonly LinkedList<IUpdateListener> _tickables = new();
        private readonly List<IUpdateListener> _tickablesForRemoving = new();
        private readonly List<IUpdateListener> _tickablesForAdding = new();

        private bool _isGameActive;

        void IGameFinishListener.OnGameFinish()
        {
            _isGameActive = false;
        }

        void IGamePauseListener.OnGamePaused()
        {
            _isGameActive = false;
        }

        void IGamePauseListener.OnGameUnPaused()
        {
            _isGameActive = true;
        }


        void IGameStartListener.OnGameStart()
        {
            _isGameActive = true;
        }


        public void AddTickable(IUpdateListener tickable)
        {
            _tickablesForAdding.Add(tickable);
        }

        public void RemoveTickable(IUpdateListener tickable)
        {
            _tickablesForRemoving.Add(tickable);
        }


        public void AddFixedTickable(IFixedUpdateListener tickable)
        {
            _fixedTickablesForAdding.Add(tickable);
        }

        public void RemoveFixedTickable(IFixedUpdateListener tickable)
        {
            _fixedTickablesForRemoving.Add(tickable);
        }


        private void Update()
        {
            if (!_isGameActive) return;

            HandleAddingTickables();
            HandleRemovingTickables();
            HandleTick();
        }

        private void HandleTick()
        {
            var deltaTime = Time.deltaTime;
            foreach (var tick in _tickables) tick.OnUpdate(deltaTime);
        }

        private void HandleAddingTickables()
        {
            for (var i = 0; i < _tickablesForAdding.Count; i++) _tickables.AddLast(_tickablesForAdding[i]);

            _tickablesForAdding.Clear();
        }

        private void HandleRemovingTickables()
        {
            for (var i = 0; i < _tickablesForRemoving.Count; i++) _tickables.Remove(_tickablesForRemoving[i]);

            _tickablesForRemoving.Clear();
        }


        private void FixedUpdate()
        {
            if (!_isGameActive) return;

            HandleAddingFixedTickables();
            HandleRemovingFixedTickables();
            HandleFixedTick();
        }

        private void HandleFixedTick()
        {
            var deltaTime = Time.fixedDeltaTime;
            foreach (var tick in _fixedTickables) tick.OnFixedUpdate(deltaTime);
        }

        private void HandleAddingFixedTickables()
        {
            for (var i = 0; i < _fixedTickablesForAdding.Count; i++)
                _fixedTickables.AddLast(_fixedTickablesForAdding[i]);

            _fixedTickablesForAdding.Clear();
        }

        private void HandleRemovingFixedTickables()
        {
            for (var i = 0; i < _fixedTickablesForRemoving.Count; i++)
                _fixedTickables.Remove(_fixedTickablesForRemoving[i]);

            _tickablesForRemoving.Clear();
        }
    }
}