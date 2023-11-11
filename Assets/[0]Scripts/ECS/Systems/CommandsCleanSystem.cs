using Game.ECS.Commands;
using Game.ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace Game.ECS.Systems
{
    internal readonly struct CommandsCleanSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<GenerateGridCommand>> _generateGridFilter;
        private readonly EcsFilterInject<Inc<ClickHandleComponent>> _clickHandleFilter;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var command in _generateGridFilter.Value)
            {
                var pool = _generateGridFilter.Pools.Inc1;
                pool.Del(command);
            }
            
            foreach (var command in _clickHandleFilter.Value)
            {
                var pool = _clickHandleFilter.Pools.Inc1;
                pool.Del(command);
            }
        }
    }
}
