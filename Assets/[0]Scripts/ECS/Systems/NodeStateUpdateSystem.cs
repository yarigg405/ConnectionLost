using Game.ECS.Commands;
using Game.ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace Game.ECS.Systems
{
    internal readonly struct NodeStateUpdateSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<UpdateNodeConditionCommand, NodeConditionComponent>> _filter;
        private readonly EcsWorldInject _world;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var poolNodeCondition = _world.Value.GetPool<NodeConditionComponent>();
            var poolUpdateConditionCommand = _world.Value.GetPool<UpdateNodeConditionCommand>();
            var poolViews = _world.Value.GetPool<NodeViewComponent>();

            foreach (var entity in _filter.Value)
            {
                var condition = poolNodeCondition.Get(entity);
                ref var view = ref poolViews.Get(entity);

                view.View.SetCondition(condition.NodeCondition);
                poolUpdateConditionCommand.Del(entity);
            }
        }
    }
}
