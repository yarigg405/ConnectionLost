using Infrastructure.DI;
using UnityEngine;


namespace ConnectionLost
{
    internal sealed class GameplayModule : Module
    {
        [SerializeField, Service(typeof(CellsStorage))]
        private CellsStorage cellsStorage = new();

        [SerializeField, Service(typeof(EnemyStorage))]
        private EnemyStorage enemyStorage = new();

        [SerializeField, Service(typeof(BonusStorage))]
        private BonusStorage bonusStorage = new();

        [SerializeField, Service(typeof(BonusSoDataBase))]
        private BonusSoDataBase bonusSoDataBase = new();

        [SerializeField, Listener, Service(typeof(BonusUseSystem))]
        private BonusUseSystem bonusUseSystem = new();

        [SerializeField, Listener, Service(typeof(EnemyBattleSystem))]
        private EnemyBattleSystem enemyBattleSystem = new();

        [SerializeField, Listener, Service(typeof(PlayerWinLoseController))]
        private PlayerWinLoseController playerWinController = new();
    }
}