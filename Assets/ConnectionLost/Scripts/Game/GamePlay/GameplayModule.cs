using Infrastructure.DI;
using UnityEngine;


namespace ConnectionLost
{
    internal sealed class GameplayModule : Module
    {
        [SerializeField, Service(typeof(CellsStorage))]
        private CellsStorage cellsSystem = new();

        [SerializeField, Listener, Service(typeof(EnemyBattleSystem))]
        private EnemyBattleSystem enemyBattleSystem = new();

        [SerializeField, Listener, Service(typeof(PlayerWinLoseController))]
        private PlayerWinLoseController playerWinController = new();
    }
}