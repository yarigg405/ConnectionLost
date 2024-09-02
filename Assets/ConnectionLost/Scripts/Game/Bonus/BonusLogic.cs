using VContainer;


namespace ConnectionLost
{
    internal abstract class BonusLogic
    {
        [Inject] private readonly PlayerBonusInventory _playerBonusInventory;

        internal int SlotIndex { get; set; }

        public abstract BonusLogic GetLogic();

        public abstract void UseBonus();

        protected void RemoveBonusFromSlot()
        {
            _playerBonusInventory.RemoveAt(SlotIndex);
        }
    }
}