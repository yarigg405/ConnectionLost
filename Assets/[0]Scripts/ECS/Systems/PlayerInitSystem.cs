using Game.ECS.Components;
using Game.ECS.Configs;
using Game.Player;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;


namespace Game.ECS.Systems
{
    internal readonly struct PlayerInitSystem : IEcsInitSystem
    {
        private readonly EcsWorldInject _world;
        private readonly EcsCustomInject<PlayerProvider> _playerProvider;
        private readonly EcsCustomInject<PlayerConfig> _playerConfig;



        void IEcsInitSystem.Init(IEcsSystems systems)
        {
            var entity = _world.Value.NewEntity();
            var poolHealth = _world.Value.GetPool<HealthComponent>();
            ref var healtC = ref poolHealth.Add(entity);
            //healtC.Health = enemy.EnemyModel.DefaultHealth;




        }
    }
}
