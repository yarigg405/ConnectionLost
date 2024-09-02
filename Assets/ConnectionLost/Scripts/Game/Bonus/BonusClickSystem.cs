using System;
using VContainer;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class BonusClickSystem
    {
        [Inject] private readonly PlayerBonusInventory inventory;

        internal void ClickOnBonus(Bonus bonus)
        {
            if (inventory.CanAddNew())
            {
                inventory.InsertBonus(bonus.BonusType);
                bonus.GetEntitaComponent<DestroyComponent>().Destroy();
            }
        }
    }
}