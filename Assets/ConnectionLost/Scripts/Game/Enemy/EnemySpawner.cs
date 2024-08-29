using System;
using System.Linq;
using UniRx;
using UnityEngine;
using VContainer;
using Yrr.Utils;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class EnemySpawner
    {
        [SerializeField] private UnityDictionary<GridDifficult, DifficultSpawnInfo> spawnInfoMap;

        [Inject] private readonly CellsStorage _cellsStorage;
        [Inject] private readonly EnemyStorage _enemyStorage;
        [Inject] private readonly GameBalanceSettings _balance;


        internal void SpawnEnemies(GridStats stats)
        {
            var randomizator = new RandomizerByWeight<Enemy>();
            var data = spawnInfoMap.Get(stats.Difficult);
            foreach (var pair in data.SpawnData)
            {
                randomizator.AddVariant(pair.Key, pair.Value);
            }

            var enemiesCount = stats.CellsCount * _balance.EnemiesPercentByGrid;
            var cells = _cellsStorage.GetValues().Where(x => x.Status == CellStatus.Closed);

            while (enemiesCount > 0)
            {
                var randCell = cells.GetRandomItem();
                var contentContainer = randCell.GetEntitaComponent<ContentContainer>();
                if (contentContainer.IsEmpty)
                {
                    var randomEnemyPrefab = randomizator.GetRandom();
                    var enemy = GameObject.Instantiate(randomEnemyPrefab);
                    enemy.SetupEntita();
                    enemy.transform.SetParent(randCell.transform);
                    enemy.transform.localPosition = Vector3.zero;
                    enemy.gameObject.SetActive(false);
                    _enemyStorage.Add(enemy);
                    contentContainer.SetContent(enemy);

                    enemiesCount--;
                }
            }
        }
    }

    [Serializable]
    internal struct DifficultSpawnInfo
    {
        public UnityKeyValuePair<Enemy, float>[] SpawnData;
    }
}