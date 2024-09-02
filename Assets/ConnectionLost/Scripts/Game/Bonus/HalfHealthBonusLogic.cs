using System;
using VContainer;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class HalfHealthBonusLogic : BonusLogic
    {
        [Inject] private readonly InputSystem _inputSytem;

        public override BonusLogic GetLogic()
        {
            return new HalfHealthBonusLogic();
        }

        public override void UseBonus()
        {
            _inputSytem.OverrideEnemyClick(ClickOnEnemy);
        }

        private void ClickOnEnemy(Enemy enemy)
        {
            RemoveBonusFromSlot();
            var health = enemy.GetEntitaComponent<HealthComponent>();
            var damage = health.CurrentHealth / 2;
            health.GetDamage(damage);
        }
    }
}
