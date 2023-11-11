using Game.ECS.Commands;
using Game.ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Game.ECS.Systems
{
    internal readonly struct NodesCleanupSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsFilterInject<Inc<GenerateGridCommand>> _filter;
        private readonly EcsFilterInject<Inc<NodeViewComponent>> _nodesFilter;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var command in _filter.Value)
            {
                var poolNodes = _world.Value.GetPool<NodeViewComponent>();
                var poolUnused = _world.Value.GetPool<UnusedFlag>();


                foreach (var entity in _nodesFilter.Value)
                {
                    if (!poolUnused.Has(entity))
                        poolUnused.Add(entity);
                }
            }
        }
    }
}
