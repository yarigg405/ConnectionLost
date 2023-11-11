using Game.ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Game.ECS.Systems
{
    internal readonly struct NodesInstantiationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SpawnRequireComponent, GameObjectComponent>, Exc<EnemyViewComponent>> _filter;
        private readonly EcsWorldInject _world;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var poolSpawnReq = _filter.Pools.Inc1;
            var poolGo = _filter.Pools.Inc2;

            foreach (var entity in _filter.Value)
            {
                var spawnReqC = poolSpawnReq.Get(entity);
                ref var goC = ref poolGo.Get(entity);

                var go = Object.Instantiate(Resources.Load<EcsMonoObject>(spawnReqC.PrefabPath), spawnReqC.SpawnRoot);
                go.transform.localPosition = spawnReqC.SpawnLocalPos;

                go.Init(_world.Value);
                go.PackEntity(entity);
                goC.GameObject = go.gameObject;

                poolSpawnReq.Del(entity);
            }
        }
    }
}
