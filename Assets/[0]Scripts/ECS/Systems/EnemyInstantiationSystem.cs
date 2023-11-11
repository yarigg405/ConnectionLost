using Game.ECS.Components;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Game.ECS.Systems
{
    internal readonly struct EnemyInstantiationSystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<SpawnRequireComponent, EnemyViewComponent>> _filter;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var poolSpawnReq = _filter.Pools.Inc1;
            var poolViews = _filter.Pools.Inc2;

            foreach (var entity in _filter.Value)
            {
                var spawnReqC = poolSpawnReq.Get(entity);
                ref var enemy = ref poolViews.Get(entity);

                var model = enemy.EnemyModel;

                var view = Object.Instantiate(model.ViewPrefab, spawnReqC.SpawnRoot);
                view.transform.localPosition = spawnReqC.SpawnLocalPos;
                enemy.View = view;
                view.gameObject.SetActive(false);

                poolSpawnReq.Del(entity);
            }
        }
    }
}
