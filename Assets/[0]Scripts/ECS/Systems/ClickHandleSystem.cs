using Game.ECS.Commands;
using Game.ECS.Components;
using Game.ECS.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using System.Collections.Generic;


namespace Game.ECS.Systems
{
    internal readonly struct ClickHandleSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ClickHandleComponent>> _filter;
        private readonly EcsFilterInject<Inc<NodeConditionComponent, GameObjectComponent>> _openingFilter;
        private readonly EcsWorldInject _world;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var poolClicks = _filter.Pools.Inc1;
            var poolConditions = _world.Value.GetPool<NodeConditionComponent>();

            foreach (var entity in _filter.Value)
            {
                ref var condition = ref poolConditions.Get(entity);
                HandleClickByState(condition, entity);
                poolClicks.Del(entity);
            }
        }


        private void HandleClickByState(NodeConditionComponent conditionComponent, int entity)
        {
            var condition = conditionComponent.NodeCondition;


            switch (condition)
            {
                case NodeCondition.Opened:
                    {
                        OpenNode(entity);
                    }; break;

                case NodeCondition.HaveContent:
                    {
                        HandleContent(entity);
                    }; break;
            }
        }

        private void OpenNode(int entity)
        {
            var poolEnemies = _world.Value.GetPool<EnemyViewComponent>();
            if (poolEnemies.Has(entity))
            {
                var enemy = poolEnemies.Get(entity);
                SetEnemy(entity, enemy);
            }

            else
            {
                SetEmpty(entity);
            }
        }


        private void SetEnemy(int entity, EnemyViewComponent enemy)
        {
            enemy.View.gameObject.SetActive(true);
            enemy.View.ShowView();

            var poolHealth = _world.Value.GetPool<HealthComponent>();
            ref var healtC = ref poolHealth.Add(entity);
            healtC.Health = enemy.EnemyModel.DefaultHealth;

            var poolConditions = _world.Value.GetPool<NodeConditionComponent>();
            ref var conditionComponent = ref poolConditions.Get(entity);
            conditionComponent.NodeCondition = NodeCondition.HaveContent;

            var poolUpdateCondition = _world.Value.GetPool<UpdateNodeConditionCommand>();
            poolUpdateCondition.Add(entity);


            if (enemy.EnemyModel.IsBlocker)
            {
                var neighbourEntities = GetNeighbourEntities(entity);
                var poolBlocks = _world.Value.GetPool<BlockComponent>();
                foreach (var neighbour in neighbourEntities)
                {
                    ref var block = ref poolBlocks.Get(neighbour);
                    block.BlocksCount++;
                    if (block.BlocksCount == 1)
                    {
                        ref var condition = ref poolConditions.Get(neighbour);
                        condition.NodeCondition = NodeCondition.Blocked;
                        poolUpdateCondition.Add(neighbour);
                    }
                }
            }
        }

        private void SetEmpty(int entity)
        {
            var poolNodeCondition = _world.Value.GetPool<NodeConditionComponent>();
            ref var conditionComponent = ref poolNodeCondition.Get(entity);

            conditionComponent.NodeCondition = NodeCondition.Empty;

            var poolUpdateCondition = _world.Value.GetPool<UpdateNodeConditionCommand>();
            poolUpdateCondition.Add(entity);

            var poolConditions = _world.Value.GetPool<NodeConditionComponent>();

            var neighbourEntities = GetNeighbourEntities(entity);

            foreach (var neighbour in neighbourEntities)
            {
                ref var condition = ref poolConditions.Get(neighbour);
                if (condition.NodeCondition == NodeCondition.Closed)
                {
                    condition.NodeCondition = NodeCondition.Opened;
                    poolUpdateCondition.Add(neighbour);
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


        private void HandleContent(int entity)
        {
            throw new NotImplementedException();
        }
    }
}
