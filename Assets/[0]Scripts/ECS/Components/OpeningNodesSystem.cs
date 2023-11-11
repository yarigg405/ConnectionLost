using Game.ECS.Commands;
using Game.ECS.Components;
using Game.ECS.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace Game.ECS.Systems
{
    internal readonly struct OpeningNodesSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            throw new System.NotImplementedException();
        }
    }
}
