using System;
using UnityEngine;
using VContainer;
using Yrr.Utils;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class PlayerStats
    {
        [Inject] private readonly GameBalanceSettings _balance;

        public ReactiveValue<int> PlayerHealth = new();
        public ReactiveValue<int> PlayerAttack = new();
        public ReactiveValue<int> ShieldsCount = new();

        private int _attackDeBuff;


        internal void Heal(int healthRepair)
        {
            PlayerHealth.Value += healthRepair;
        }

        internal void Damage(int damage)
        {
            PlayerHealth.Value -= damage;
        }



        internal void AddAttackDebuff()
        {
            _attackDeBuff++;
            UpdateAttack();
        }

        internal void RemoveAttackDebuff()
        {
            _attackDeBuff--;
            if (_attackDeBuff < 0)
                _attackDeBuff = 0;
            UpdateAttack();
        }

        private void UpdateAttack()
        {
            var attackModification = Mathf.RoundToInt(_attackDeBuff * _balance.SuppressorDeBuffValuePerLevel);

            var damage = PlayerAttack.Value - attackModification;
            if (damage < _balance.PlayerMinAttack)
            {
                damage = _balance.PlayerMinAttack;
            }
            PlayerAttack.Value = damage;
        }

        internal void ResetDebuff()
        {
            _attackDeBuff = 0;
        }
    }
}