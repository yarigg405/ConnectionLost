using Game.ECS.Commands;
using Game.ECS.Components;
using Game.ECS.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;


namespace Game.ECS.Systems
{
    internal readonly struct ClickHandleEmptyNodeSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ClickHandleComponent>, Exc<UnusedFlag>> _filter;
        private readonly EcsFilterInject<Inc<GameObjectComponent>> _openingFilter;
        private readonly EcsWorldInject _world;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var poolConditions = _world.Value.GetPool<NodeConditionComponent>();
            var poolBlock = _world.Value.GetPool<BlockComponent>();
            var poolUpdateCondition = _world.Value.GetPool<UpdateNodeConditionCommand>();


            foreach (var entity in _filter.Value)
            {
                ref var condition = ref poolConditions.Get(entity);
                var block = poolBlock.Get(entity); 
                
                if (condition.NodeCondition != NodeCondition.Opened) continue; 
                if (block.BlocksCount > 0) continue;

                condition.NodeCondition = NodeCondition.Empty;
                poolUpdateCondition.Add(entity);

                var neighbourEntities = GetNeighbourEntities(entity);
                foreach (var neighbour in neighbourEntities)
                {
                    condition = ref poolConditions.Get(neighbour);
                    if (condition.NodeCondition == NodeCondition.Closed)
                    {
                        condition.NodeCondition = NodeCondition.Opened;
                        poolUpdateCondition.Add(neighbour);
                    }
                }
            }
        }


        private IEnumerable<int> GetNeighbourEntities(int centerNodeEntity)
        {
            var poolGameObjects = _world.Value.GetPool<GameObjectComponent>();
            var centerNodeTransform = poolGameObjects.Get(centerNodeEntity).GameObject.transform;

            foreach (var entity in _openingFilter.Value)
            {
                if (entity == centerNodeEntity) continue;

                var checkedNodeTransform = poolGameObjects.Get(entity).GameObject.transform;
                var distance = (checkedNodeTransform.position - centerNodeTransform.position).sqrMagnitude;
                if (distance < GameConfig.NodesNeighboursSqrRadius)
                {
                    yield return entity;
                }
            }
        }
    }
}
