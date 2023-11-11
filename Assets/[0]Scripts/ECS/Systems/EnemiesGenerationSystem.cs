using Game.ECS.Commands;
using Game.ECS.Components;
using Game.ECS.Views;
using Game.GridGeneration.Grid;
using Game.GridGeneration.Models;
using Game.Player;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;
using Yrr.Utils;


namespace Game.ECS.Systems
{
    internal readonly struct EnemiesGenerationSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsFilterInject<Inc<GenerateGridCommand>> _commandsFilter;
        private readonly EcsFilterInject<Inc<NodeViewComponent>, Exc<UnusedFlag>> _freeNodesFilter;


        private readonly EcsCustomInject<PlayerProvider> _playerProvider;

        private readonly EcsCustomInject<GridGenerator> _gridGenerator;

        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var command in _commandsFilter.Value)
            {
                var player = _playerProvider.Value;
                var level = player.PlayerProgress.CurrentLevel;
                var gen = _gridGenerator.Value;

                var enemies = gen.GenerateEnemies(level);
                CreateEnemies(enemies);
            }
        }

        private void CreateEnemies(EnemyModel[] enemies)
        {
            for (var i = 0; i < enemies.Length; i++)
            {
                var enemy = enemies[i];
                var node = GetNodeForEnemy();

                CreateEnemy(enemy, node);
            }
        }

        private int GetNodeForEnemy()
        {
            var poolCondition = _world.Value.GetPool<NodeConditionComponent>();
            var poolEnemies = _world.Value.GetPool<EnemyViewComponent>();

            var origin = new List<int>(30);


            foreach (var entity in _freeNodesFilter.Value)
            {
                if (poolCondition.Get(entity).NodeCondition == NodeCondition.Opened) continue;
                if (poolEnemies.Has(entity)) continue;

                origin.Add(entity);
            }

            var result = origin.GetRandomItem();
            return result;
        }

        private void CreateEnemy(EnemyModel enemy, int entity)
        {
            var poolViews = _world.Value.GetPool<EnemyViewComponent>();
            ref var viewC = ref poolViews.Add(entity);
            viewC.EnemyModel = enemy;

            var poolNotInit = _world.Value.GetPool<NotInitializedFlag>();
            poolNotInit.Add(entity);
        }
    }
}