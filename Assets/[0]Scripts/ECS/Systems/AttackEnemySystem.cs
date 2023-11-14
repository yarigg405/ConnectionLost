using Game.ECS.Commands;
using Game.ECS.Components;
using Game.Player;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace Game.ECS.Systems
{
    internal readonly struct AttackEnemySystem : IEcsRunSystem
    {
        private readonly EcsFilterInject<Inc<AttackEnemyCommand>> _filter;
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<PlayerProvider> _playerProvider;


        void IEcsRunSystem.Run(IEcsSystems systems)
        {
            var poolHealth = _world.Value.GetPool<HealthComponent>();
            var poolEnemies = _world.Value.GetPool<EnemyViewComponent>();
            var playerStats = _playerProvider.Value.PlayerStats;

            foreach (var entity in _filter.Value)
            {
                var pool = _filter.Pools.Inc1;
                pool.Del(entity);

                ref var enemyHealth = ref poolHealth.Get(entity);
                var playerDamage = playerStats.PlayerAttack.Value;
                enemyHealth.Health -= playerDamage;


                if (enemyHealth.Health > 0)
                {
                    var enemyView = poolEnemies.Get(entity);
                    enemyView.View.ShowTakingDamage(playerDamage);
                }

                else
                {
                    var poolCommands = _world.Value.GetPool<HideEnemyCommand>();
                    poolCommands.Add(entity);
                }
            }
        }
    }
}
