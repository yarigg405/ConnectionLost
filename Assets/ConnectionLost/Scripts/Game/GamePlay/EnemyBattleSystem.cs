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
                if (_playerStats.ShieldsCount > 0)
                {
                    _playerStats.ShieldsCount.Value--;
                }
                else
                {
                    var enemyAttack = enemy.GetEntitaComponent<AttackComponent>();
                    _playerStats.Damage(enemyAttack.AttackDamage);
                }
            }
        }
    }
}