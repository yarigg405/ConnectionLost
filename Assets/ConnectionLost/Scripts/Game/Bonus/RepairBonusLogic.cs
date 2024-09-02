using System;
using UnityEngine;
using VContainer;
using Yrr.Utils;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class RepairBonusLogic : BonusLogic
    {
        [SerializeField] private int countOfTurns;
        [SerializeField] private Vector2 healthRepair;

        [Inject] private readonly PlayerStats _playerStats;
        [Inject] private readonly NextTurnObserver _nextTurnObserver;
        [Inject] private readonly PlayerBonusInventory _playerBonusInventory;

        public override BonusLogic GetLogic()
        {
            return new RepairBonusLogic
            {
                countOfTurns = countOfTurns,
                healthRepair = healthRepair,
            };
        }

        public override void UseBonus()
        {
            _playerBonusInventory.RemoveAt(SlotIndex);
            _nextTurnObserver.OnNextTurn += HealPlayer;
        }

        private void HealPlayer()
        {
            if (_playerStats.PlayerHealth <= 0)
            {
                _nextTurnObserver.OnNextTurn -= HealPlayer;
                return;
            }

            _playerStats.Heal((int)healthRepair.GetRandomValue());
            countOfTurns--;

            if (countOfTurns < 1)
            {
                _nextTurnObserver.OnNextTurn -= HealPlayer;
            }
        }
    }
}
