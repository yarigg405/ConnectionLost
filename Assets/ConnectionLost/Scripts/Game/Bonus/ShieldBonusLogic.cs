using UnityEngine;
using VContainer;


namespace ConnectionLost
{
    internal sealed class ShieldBonusLogic : BonusLogic
    {
        [SerializeField] private int shieldCount;

        [Inject] private readonly PlayerStats playerStats;

        public override BonusLogic GetLogic()
        {
            return new ShieldBonusLogic
            {
                shieldCount = shieldCount,
            };
        }

        public override void UseBonus()
        {
            playerStats.ShieldsCount.Value += shieldCount;
            RemoveBonusFromSlot();
        }
    }
}
