using Cysharp.Threading.Tasks;
using System;
using UnityEngine;
using UnityEngine.AddressableAssets;
using Yrr.Utils;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class BonusSoDataBase
    {
        [SerializeField] private UnityDictionary<BonusType, AssetReference> bonusesDataBase;

        public async UniTask<BonusSO> GetBonusInfo(BonusType bonusType)
        {
            var operationHandlee = Addressables.LoadAssetAsync<BonusSO>(bonusesDataBase.Get(bonusType)).Task.AsUniTask();
            BonusSO result = await operationHandlee;
            return result;
        }
    }
}