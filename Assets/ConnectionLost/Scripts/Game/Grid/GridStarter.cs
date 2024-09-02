using ConnectionLost.Camera;
using Infrastructure.GameSystem;
using System;
using UnityEngine;
using VContainer;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class GridStarter:IGameStartListener
    {
        [SerializeField] private Grid[] grids;

        [Inject] private readonly GridStatsFactory _statsFactory;
        [Inject] private readonly CellsSpawner _gridSpawner;
        [Inject] private readonly EnemySpawner _enemySpawner;
        [Inject] private readonly BonusSpawner _bonusSpawner;
        [Inject] private readonly CameraSystem _cameraSystem;
        [Inject] private readonly PlayerSetup _playerSetup;
        [Inject] private readonly NextTurnObserver _nexTurnObserver;


        private readonly GridGenerator _generator = new();

        private GridDifficult _currentDifficult = GridDifficult.Tutorial;
        private int _currentGridNum;


        public void StartGrid()
        {
            _playerSetup.SetupPlayer();
            SpawnGrid();
        }

        public void NextGrid()
        {
            _currentGridNum++;
            if (_currentGridNum > grids.Length - 1)
                _currentGridNum = 0;

            SpawnGrid();
        }

        private void SpawnGrid()
        {
            _nexTurnObserver.Reset();
            var stats = _statsFactory.BuildGridStats(_currentDifficult);
            var gridData = _generator.GenerateRandomGrid(stats);
            _gridSpawner.SpawnGrid(gridData, grids[_currentGridNum]);
            _enemySpawner.SpawnEnemies(stats);
            _bonusSpawner.SpawnBonuses(stats);
            _cameraSystem.LookAt(grids[_currentGridNum].CameraLookPoint);
            _cameraSystem.FollowAt(grids[_currentGridNum].CameraFollowPoint);
        }

        void IGameStartListener.OnGameStart()
        {
            StartGrid();
        }
    }
}
