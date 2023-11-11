using Game.ECS.Components;
using Game.ECS.Configs;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using UnityEngine;


namespace Game.ECS.Systems
{
    internal readonly struct NodesSpawnSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<SpawnConfig> _spawnConfig;
        private readonly EcsCustomInject<PrefabsConfig> _prefabsConfig;
        private readonly EcsCustomInject<GridsSelector> _gridsSelector;


        private readonly EcsFilterInject<Inc<HexCoordinatesComponent, NotInitializedFlag>> _filter;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var poolViews = _world.Value.GetPool<GameObjectComponent>();
            var poolNodes = _world.Value.GetPool<NodeViewComponent>();
            var poolSpawnReq = _world.Value.GetPool<SpawnRequireComponent>();
            var poolBlock = _world.Value.GetPool<BlockComponent>();
            var poolHex = _filter.Pools.Inc1;


            foreach (var entity in _filter.Value)
            {
                poolViews.Add(entity);

                ref var spawnReqC = ref poolSpawnReq.Add(entity);
                spawnReqC.PrefabPath = _prefabsConfig.Value.NodePrefabPath;

                var hexCoords = poolHex.Get(entity);

                var x = (hexCoords.X + hexCoords.Z % 2 * 0.5f) * (_spawnConfig.Value.InnerRadius * 2f);
                var z = hexCoords.Z * (_spawnConfig.Value.OuterRadius * 1.5f);

                spawnReqC.SpawnLocalPos = new Vector3(x, 0, z);
                spawnReqC.SpawnRoot = _gridsSelector.Value.GetCurrentGrid();

                poolNodes.Add(entity);
                poolBlock.Add(entity);
            }
        }
    }
}
