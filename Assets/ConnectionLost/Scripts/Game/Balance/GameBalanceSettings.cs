using System;
using UnityEngine;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class GameBalanceSettings
    {
        [field: SerializeField] public int FirewallSpawnWeight { get; private set; } = 35;
        [field: SerializeField] public int AntivirusSpawnWeight { get; private set; } = 35;
        [field: SerializeField] public int HealerSpawnWeight { get; private set; } = 14;
        [field: SerializeField] public int SuppressorSpawnWeight { get; private set; } = 12;

        [field: SerializeField] public int RepairBonusSpawnWeight { get; private set; } = 6;
        [field: SerializeField] public int HalfHpBonusSpawnWeight { get; private set; } = 6;
        [field: SerializeField] public int ShieldBonusSpawnWeight { get; private set; } = 3;
        [field: SerializeField] public int HawkEyeBonusSpawnWeight { get; private set; } = 3;


        [field: SerializeField] public int CellsCountForGridDifficult { get; private set; } = 12;
        [field: SerializeField] public float EnemiesPercentByGrid { get; private set; } = 0.15f;
        [field: SerializeField] public float BonusesPercentByGrid { get; private set; } = 0.15f;
        [field: SerializeField] public float SuppressorDeBuffValuePerLevel { get; private set; } = 15f;

        [field: SerializeField] public int PlayerBaseHealth { get; private set; } = 90;
        [field: SerializeField] public int PlayerBaseAttack { get; private set; } = 25;
        [field: SerializeField] public int PlayerMinAttack { get; private set; } = 10;
    }
}
