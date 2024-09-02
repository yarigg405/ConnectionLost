using System;
using System.Linq;
using UniRx;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Yrr.Utils;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class EnemySpawner
    {
        [SerializeField] private UnityDictionary<GridDifficult, EnemySpawnInfo> spawnInfoMap;

        [Inject] private readonly CellsStorage _cellsStorage;
        [Inject] private readonly EnemyStorage _enemyStorage;
        [Inject] private readonly GameBalanceSettings _balance;
        [Inject] private readonly PlayerWinLoseController _playerWinLoseController;
        [Inject] private readonly IObjectResolver _objectResolver;

        internal void SpawnEnemies(GridStats stats)
        {
            _enemyStorage.Clear();
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
                    _objectResolver.InjectGameObject(enemy.gameObject);
                    enemy.SetupEntita();
                    enemy.GetEntitaComponent<DestroyComponent>().OnDestroy += () => _enemyStorage.Remove(enemy);
                    enemy.transform.SetParent(randCell.transform);
                    enemy.transform.localPosition = Vector3.zero;
                    enemy.gameObject.SetActive(false);
                    _enemyStorage.Add(enemy);
                    contentContainer.SetContent(enemy);

                    enemiesCount--;
                }
            }

            while (true)
            {
                var randCell = cells.GetRandomItem();
                var contentContainer = randCell.GetEntitaComponent<ContentContainer>();
                if (contentContainer.IsEmpty)
                {
                    var enemy = GameObject.Instantiate(data.CoreEnemy);
                    enemy.SetupEntita();
                    var destroy = enemy.GetEntitaComponent<DestroyComponent>();
                    destroy.OnDestroy += () => _playerWinLoseController.PlayerWin();
                    enemy.transform.SetParent(randCell.transform);
                    enemy.transform.localPosition = Vector3.zero;
                    enemy.gameObject.SetActive(false);
                    contentContainer.SetContent(enemy);
                    break;
                }
            }
        }
    }

    [Serializable]
    internal struct EnemySpawnInfo
    {
        public UnityKeyValuePair<Enemy, float>[] SpawnData;
        public Enemy CoreEnemy;
    }
}