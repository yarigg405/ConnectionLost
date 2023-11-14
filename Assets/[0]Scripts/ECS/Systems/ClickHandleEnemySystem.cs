using Game.ECS.Commands;
using Game.ECS.Components;
using Game.ECS.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace Game.ECS.Systems
{
    internal readonly struct ClickHandleEnemySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<ClickHandleComponent, EnemyViewComponent>, Exc<UnusedFlag>> _filter;
        private readonly EcsWorldInject _world;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var poolConditions = _world.Value.GetPool<NodeConditionComponent>();

            foreach (var entity in _filter.Value)
            {
                var condition = poolConditions.Get(entity);
                if (condition.NodeCondition == NodeCondition.Closed) continue;

                if (condition.NodeCondition == NodeCondition.HaveContent)
                {
                    var poolAttack = _world.Value.GetPool<AttackEnemyCommand>();
                    poolAttack.Add(entity);
                }

                else
                {
                    var poolOpen = _world.Value.GetPool<OpenEnemyCommand>();
                    poolOpen.Add(entity);
                }
            }
        }
    }
}
