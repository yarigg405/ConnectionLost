using Infrastructure.GameSystem;
using UnityEngine;


namespace Infrastructure.DI
{
    internal sealed class InfrastructureModule : Module
    {
        [SerializeField, Service(typeof(GameMachine))]
        private GameMachine gameMachine = new();

        [SerializeField, Listener, Service(typeof(TickableProcessor))]
        private TickableProcessor tickableProcessor;
    }
}