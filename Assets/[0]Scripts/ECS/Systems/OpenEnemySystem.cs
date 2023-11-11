using Game.ECS.Commands;
using Game.ECS.Components;
using Game.ECS.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using System.Collections.Generic;


namespace Game.ECS.Systems
{
    internal readonly struct OpenEnemySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<OpenEnemyCommand>> _filter;
        private readonly EcsFilterInject<Inc<NodeConditionComponent, GameObjectComponent>> _openingFilter;
        private readonly EcsWorldInject _world;



        void IEcsRunSystem.Run(IEcsSystems systems)
        {

            var poolEnemies = _world.Value.GetPool<EnemyViewComponent>();
            var poolConditions = _world.Value.GetPool<NodeConditionComponent>();    
            var poolUpdateCondition = _world.Value.GetPool<UpdateNodeConditionCommand>();
            var poolBlocks = _world.Value.GetPool<BlockComponent>();



            foreach (var entity in _filter.Value)
            {
                var pool = _filter.Pools.Inc1;
                pool.Del(entity);

                var enemy = poolEnemies.Get(entity);
                enemy.View.gameObject.SetActive(true);
                enemy.View.ShowView();
                ref var conditionComponent = ref poolConditions.Get(entity);
                conditionComponent.NodeCondition = NodeCondition.HaveContent;
                if(!poolConditions.Has(entity))
                    poolUpdateCondition.Add(entity);


                if (enemy.EnemyModel.IsBlocker)
                {
                    var neighbourEntities = GetNeighbourEntities(entity);
                    foreach (var neighbour in neighbourEntities)
                    {
                        ref var block = ref poolBlocks.Get(neighbour);
                        block.BlocksCount++;
                        if (block.BlocksCount == 1)
                        {
                            ref var condition = ref poolConditions.Get(neighbour);
                            condition.NodeCondition = NodeCondition.Blocked;
                            if(!poolUpdateCondition.Has(neighbour))
                                poolUpdateCondition.Add(neighbour);
                        }
                    }
                }
            }
        }

        private IEnumerable<int> GetNeighbourEntities(int centerNodeEntity)
        {
            var poolGameObjects = _world.Value.GetPool<GameObjectComponent>();
            var centerNodeTransform = poolGameObjects.Get(centerNodeEntity).GameObject.transform;
            float radiusForOpenNeihboursNodes = 5f;


            foreach (var entity in _openingFilter.Value)
            {
                if (entity == centerNodeEntity) continue;

                var checkedNodeTransform = poolGameObjects.Get(entity).GameObject.transform;
                var distance = (checkedNodeTransform.position - centerNodeTransform.position).sqrMagnitude;
                if (distance < radiusForOpenNeihboursNodes)
                {
                    yield return entity;
                }
            }
        }
    }
}
