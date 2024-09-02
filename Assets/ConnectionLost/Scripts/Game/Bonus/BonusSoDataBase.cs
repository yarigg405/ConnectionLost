using System;
using UnityEngine;
using Yrr.Utils;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class BonusSoDataBase
    {
        [SerializeField] private UnityDictionary<BonusType, BonusSO> bonusesDataBase;

        public BonusSO GetBonusInfo(BonusType bonusType)
        {
            return bonusesDataBase.Get(bonusType);
        }
    }
}