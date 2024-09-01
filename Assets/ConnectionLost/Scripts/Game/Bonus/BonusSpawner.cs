using System;
using System.Linq;
using UnityEngine;
using VContainer;
using VContainer.Unity;
using Yrr.Utils;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class BonusSpawner
    {
        [SerializeField] private UnityDictionary<GridDifficult, BonusSpawnInfo> spawnInfoMap;

        [Inject] private readonly CellsStorage _cellsStorage;
        [Inject] private readonly BonusStorage _bonusesStorage;
        [Inject] private readonly GameBalanceSettings _balance;
        [Inject] private readonly IObjectResolver _objectResolver;


        internal void SpawnBonuses(GridStats stats)
        {
            var randomizator = new RandomizerByWeight<Bonus>();
            var data = spawnInfoMap.Get(stats.Difficult);
            foreach (var pair in data.SpawnData)
            {
                randomizator.AddVariant(pair.Key, pair.Value);
            }

            var bonusesCount = stats.CellsCount * _balance.BonusesPercentByGrid;
            var cells = _cellsStorage.GetValues().Where(x => x.Status == CellStatus.Closed);

            while (bonusesCount > 0)
            {
                var randCell = cells.GetRandomItem();
                var contentContainer = randCell.GetEntitaComponent<ContentContainer>();
                if (contentContainer.IsEmpty)
                {
                    var randomBonusPrefab = randomizator.GetRandom();
                    var bonus = GameObject.Instantiate(randomBonusPrefab);
                    _objectResolver.InjectGameObject(bonus.gameObject);
                    bonus.SetupEntita();
                    bonus.GetEntitaComponent<DestroyComponent>().OnDestroy += () => _bonusesStorage.Remove(bonus);
                    bonus.transform.SetParent(randCell.transform);
                    bonus.transform.localPosition = Vector3.zero;
                    bonus.gameObject.SetActive(false);
                    _bonusesStorage.Add(bonus);
                    contentContainer.SetContent(bonus);

                    bonusesCount--;
                }
            }
        }
    }
    [Serializable]
    internal struct BonusSpawnInfo
    {
        public UnityKeyValuePair<Bonus, float>[] SpawnData;
    }
}