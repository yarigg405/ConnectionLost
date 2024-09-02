using UnityEngine;
using VContainer;


namespace ConnectionLost
{
    internal sealed class ShieldBonusLogic : BonusLogic
    {
        [SerializeField] private int shieldCount;

        [Inject] private readonly PlayerStats _playerStats;

        public override BonusLogic GetLogic()
        {
            return new ShieldBonusLogic
            {
                shieldCount = shieldCount,
            };
        }

        public override void UseBonus()
        {
            _playerStats.ShieldsCount.Value += shieldCount;
            RemoveBonusFromSlot();
        }
    }
}
