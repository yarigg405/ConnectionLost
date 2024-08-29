using Infrastructure.DI;
using UnityEngine;


namespace ConnectionLost
{
    internal sealed class InputModule : Module
    {
        [SerializeField, Listener, Service(typeof(InputSystem))]
        private InputSystem inputSystem = new();

        [SerializeField, Listener, Service(typeof(CellClickSystem))]
        private CellClickSystem cellClickHandler = new();

        [SerializeField, Listener, Service(typeof(EnemyStorage))]
        private EnemyStorage enemyClickHandler = new();

        [SerializeField, Listener, Service(typeof(BonusSystem))]   
        private BonusSystem bonusesClickHandler = new();
    }
}