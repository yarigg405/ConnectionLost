using System;
using VContainer;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class EnemyBattleSystem
    {
        [Inject] private readonly EnemyStorage _enemyStorage;
        [Inject] private readonly PlayerStats _playerStats;

        internal void Attack(Enemy enemy)
        {
            var enemyHealth = enemy.GetEntitaComponent<HealthComponent>();
            enemyHealth.GetDamage(_playerStats.PlayerAttack);

            if (enemy && enemy.IsAlive)
            {
                var enemyAttack = enemy.GetEntitaComponent<AttackComponent>();
                _playerStats.PlayerHealth.Value -= enemyAttack.AttackDamage;
            }
        }
    }
}