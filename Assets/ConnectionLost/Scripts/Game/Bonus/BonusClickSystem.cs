using System;
using VContainer;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class BonusClickSystem
    {
        [Inject] private readonly PlayerBonusInventory inventory;
        [Inject] private readonly NextTurnObserver _nextTurnObserver;

        internal void ClickOnBonus(Bonus bonus)
        {
            if (bonus.GetEntitaComponent<CanBeBlockedComponent>().IsBlocked) return;

            if (inventory.CanAddNew())
            {
                inventory.InsertBonus(bonus.BonusType);
                bonus.GetEntitaComponent<DestroyComponent>().Destroy();
                _nextTurnObserver.NextTurn();
            }
        }
    }
}