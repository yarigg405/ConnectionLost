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

        [SerializeField, Listener, Service(typeof(BonusClickSystem))]   
        private BonusClickSystem bonusesClickHandler = new();
    }
}