using Game.ECS.Components;
using Game.ECS.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace Game.ECS.Systems
{
    internal readonly struct NodeViewInitializeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<GameObjectComponent, NotInitializedFlag>> _filter;
        private readonly EcsWorldInject _world;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var poolGo = _world.Value.GetPool<GameObjectComponent>();
            var poolNode = _world.Value.GetPool<NodeViewComponent>();
            var poolNotInit = _world.Value.GetPool<NotInitializedFlag>();

            foreach (var entity in _filter.Value)
            {
                ref var nodeC = ref poolNode.Get(entity);
                var gameObject = poolGo.Get(entity).GameObject;
                nodeC.View = gameObject.GetComponent<NodeView>();

                poolNotInit.Del(entity);
            }
        }
    }
}
