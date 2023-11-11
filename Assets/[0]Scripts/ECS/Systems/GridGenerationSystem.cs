using Game.ECS.Commands;
using Game.ECS.Components;
using Game.GridGeneration.Grid;
using Game.GridGeneration.Models;
using Game.Player;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System.Collections.Generic;


namespace Game.ECS.Systems
{
    internal readonly struct GridGenerationSystem : IEcsRunSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsFilterInject<Inc<GenerateGridCommand>> _filter;

        private readonly EcsCustomInject<PlayerProvider> _playerProvider;
        private readonly EcsCustomInject<GridGenerator> _gridGenerator;



        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            foreach (var command in _filter.Value)
            {
                var player = _playerProvider.Value;
                var level = player.PlayerProgress.CurrentLevel;
                var gen = _gridGenerator.Value;

                var nodes = gen.GenerateRandomGrid(level);
                CreateNodes(nodes);
            }
        }

        private void CreateNodes(IEnumerable<NodeModel> cells)
        {
            var poolNotInit = _world.Value.GetPool<NotInitializedFlag>();
            var poolHex = _world.Value.GetPool<HexCoordinatesComponent>();
            var poolConditions = _world.Value.GetPool<NodeConditionComponent>();
            var poolUpdateCommand = _world.Value.GetPool<UpdateNodeConditionCommand>();

            bool firstInited = false;

            foreach (var model in cells)
            {
                var en = _world.Value.NewEntity();
                poolNotInit.Add(en);

                ref var hexC = ref poolHex.Add(en);
                hexC.X = model.Coordinates.x;
                hexC.Z = model.Coordinates.y;

                ref var condition = ref poolConditions.Add(en);

                if (!firstInited)
                {
                    condition.NodeCondition = Views.NodeCondition.Opened;
                    firstInited = true;
                }
                else
                {
                    condition.NodeCondition = Views.NodeCondition.Closed;
                }

                poolUpdateCommand.Add(en);
            }
        }
    }
}
