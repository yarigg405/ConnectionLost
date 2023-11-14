using Game.ECS.Commands;
using Game.ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace Game.ECS.Systems
{
    internal readonly struct PlayerWinHandleSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<PlayerWinCommand>> _filter;
        private readonly EcsFilterInject<Inc<NodeViewComponent>> _nodesFilter;
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<GridsSelector> _gridsSelector;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var entity in _filter.Value)
            {
                var pool = _filter.Pools.Inc1;
                pool.Del(entity);

                _gridsSelector.Value.NextGrid();

                var poolUnused = _world.Value.GetPool<UnusedFlag>();
                foreach (var en in _nodesFilter.Value)
                {
                    if (!poolUnused.Has(en))
                        poolUnused.Add(en);
                }

                var poolCommands = _world.Value.GetPool<GenerateGridCommand>();
                poolCommands.Add(entity);
            }
        }
    }
}
