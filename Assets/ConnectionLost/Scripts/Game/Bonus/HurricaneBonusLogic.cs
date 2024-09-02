using System;
using UnityEngine;
using VContainer;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class HurricaneBonusLogic : BonusLogic
    {
        [SerializeField] private int countOfTurns;
        [SerializeField] private int damageEveryTurn;

        [Inject] private readonly InputSystem _inputSytem;
        [Inject] private readonly NextTurnObserver _nextTurnObserver;

        private Enemy _targetEnemy;


        public override BonusLogic GetLogic()
        {
            return new HurricaneBonusLogic
            {
                countOfTurns = countOfTurns,
                damageEveryTurn = damageEveryTurn,
            };
        }

        public override void UseBonus()
        {
            _inputSytem.OverrideEnemyClick(ClickOnEnemy);            
        }

        private void ClickOnEnemy(Enemy enemy)
        {
            RemoveBonusFromSlot();
            _targetEnemy = enemy;
            _nextTurnObserver.OnNextTurn += DamageEnemy;            
            DamageEnemy();
        }

        private void DamageEnemy()
        {
            if (_targetEnemy == null || !_targetEnemy.IsAlive)
            {
                _nextTurnObserver.OnNextTurn -= DamageEnemy;
                return;
            }

            var health = _targetEnemy.GetEntitaComponent<HealthComponent>();
            health.GetDamage(damageEveryTurn);
            countOfTurns--;

            if (countOfTurns < 1)
                _nextTurnObserver.OnNextTurn -= DamageEnemy;
        }
    }
}
