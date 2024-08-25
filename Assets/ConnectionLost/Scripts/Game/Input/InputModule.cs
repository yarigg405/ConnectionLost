using Infrastructure.DI;
using UnityEngine;


namespace ConnectionLost
{
    internal sealed class InputModule : Module
    {
        [SerializeField, Listener, Service(typeof(InputSystem))]
        private InputSystem inputSystem = new();

        [SerializeField, Listener, Service(typeof(CellClickHandler))]
        private CellClickHandler cellClickHandler = new();

        [SerializeField, Listener, Service(typeof(EnemyClickHandler))]
        private EnemyClickHandler enemyClickHandler = new();

        [SerializeField, Listener, Service(typeof(BonusesClickHandler))]   
        private BonusesClickHandler bonusesClickHandler = new();
    }
}