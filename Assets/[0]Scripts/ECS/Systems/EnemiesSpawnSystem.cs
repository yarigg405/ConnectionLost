using Game.ECS.Commands;
using Game.ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Game.ECS.Systems
{
    internal readonly struct EnemiesSpawnSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsFilterInject<Inc<GenerateGridCommand>> _commandsFilter;
        private readonly EcsFilterInject<Inc<EnemyViewComponent, NotInitializedFlag>> _filter;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var poolEnemies = _world.Value.GetPool<EnemyViewComponent>();
            var poolNodes = _world.Value.GetPool<NodeViewComponent>();
            var poolSpawnReq = _world.Value.GetPool<SpawnRequireComponent>();
            var poolFlag = _world.Value.GetPool<NotInitializedFlag>();


            foreach (var command in _commandsFilter.Value)
            {
                foreach (var entity in _filter.Value)
                {
                    var enemy = poolEnemies.Get(entity);
                    var node = poolNodes.Get(entity);

                    ref var spawnReqC = ref poolSpawnReq.Add(entity);
                    spawnReqC.Prefab = enemy.EnemyModel.ViewPrefab.gameObject;
                    spawnReqC.SpawnRoot = node.View.transform;
                    spawnReqC.SpawnLocalPos = Vector3.zero;

                    var poolHealth = _world.Value.GetPool<HealthComponent>();
                    ref var healtC = ref poolHealth.Add(entity);
                    healtC.Health = enemy.EnemyModel.DefaultHealth;

                    poolFlag.Del(entity);
                }
            }
        }
    }
}
