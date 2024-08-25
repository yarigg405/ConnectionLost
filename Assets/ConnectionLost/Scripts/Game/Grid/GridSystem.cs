using ConnectionLost.Camera;
using Sirenix.OdinInspector;
using System;
using UnityEngine;
using VContainer;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class GridSystem
    {
        [SerializeField] private Grid[] grids;

        [Inject] private readonly GridStatsFactory _statsFactory;
        [Inject] private readonly GridSpawner _spawner;
        [Inject] private readonly CameraSystem _cameraSystem;

        private readonly GridGenerator _generator = new();

        private GridDifficult _currentDifficult = GridDifficult.Tutorial;
        private int _currentGridNum;

        [Button]
        public void SpawnGrid()
        {
            var stats = _statsFactory.BuildGridStats(_currentDifficult);
            var gridData = _generator.GenerateRandomGrid(stats);
            _spawner.SpawnGrid(gridData, grids[_currentGridNum]);
            _cameraSystem.LookAt(grids[_currentGridNum].CameraLookPoint);
            _cameraSystem.FollowAt(grids[_currentGridNum].CameraFollowPoint);
        }

        [Button]
        public void NextGrid()
        {
            _currentGridNum++;
            if (_currentGridNum > grids.Length - 1)
                _currentGridNum = 0;

            SpawnGrid();
        }
    }
}
