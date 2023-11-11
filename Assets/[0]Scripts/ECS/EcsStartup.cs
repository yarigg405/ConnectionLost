using Game.ECS.Configs;
using Game.ECS.Systems;
using Game.GridGeneration.Grid;
using Game.Player;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using Leopotam.EcsLite.UnityEditor;
using UnityEngine;


namespace Game.ECS
{
    internal sealed class EcsStartup : MonoBehaviour
    {
        private EcsWorld _world;
        private IEcsSystems _systems;

        [SerializeField] private SpawnConfig spawnConfig;
        [SerializeField] private PrefabsConfig prefabsConfig;

        [SerializeField] private GridsSelector gridsSelector;
        [SerializeField] private GridConfigsStorage gridConfigsStorage;
        [SerializeField] private PlayerProvider playerProvider;


        [Space]
        [SerializeField] private EcsMonoObject[] ecsMonoObjects;


        private void Start()
        {
            _world = new EcsWorld();
            _systems = new EcsSystems(_world);

            _systems
#if UNITY_EDITOR
                .Add(new EcsWorldDebugSystem())
#endif
                .Add(new PlayerInitSystem())

                .Add(new GridGenerationSystem())
                .Add(new NodesCleanupSystem())
                .Add(new NodesSpawnSystem())
                .Add(new NodesInstantiationSystem())
                .Add(new NodeViewInitializeSystem())
                .Add(new EnemiesGenerationSystem())
                .Add(new EnemiesSpawnSystem())
                .Add(new EnemyInstantiationSystem())
                .Add(new BonusesGenerationSystem())

                .Add(new ClickHandleEnemySystem())
                .Add(new ClickHandleBonusSystem())
                .Add(new ClickHandleEmptyNodeSystem())
                .Add(new OpenEnemySystem())
                .Add(new AttackEnemySystem())

                .Add(new NodeStateUpdateSystem())
                .Add(new CommandsCleanSystem())


                .Inject(spawnConfig)
                .Inject(prefabsConfig)
                .Inject(gridsSelector)
                .Inject(playerProvider)
                .Inject(new GridGenerator(gridConfigsStorage))

                .Init();



            foreach (var obj in ecsMonoObjects)
            {
                obj.Init(_world);
            }
        }

        private void Update()
        {
            _systems?.Run();
        }

        private void OnDestroy()
        {
            if (_systems != null)
            {
                _systems.Destroy();
                _systems = null;
            }

            if (_world != null)
            {
                _world.Destroy();
                _world = null;
            }
        }
    }
}
