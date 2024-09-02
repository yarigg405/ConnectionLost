using VContainer;


namespace ConnectionLost
{
    internal sealed class BonusUseSystem
    {
        [Inject] private readonly PlayerBonusInventory _playerBonusInventory;
        [Inject] private readonly BonusSoDataBase _dataBase;
        [Inject] private readonly IObjectResolver _objectResolver;


        internal void UseBonus(int slotIndex)
        {
            var bonusType = _playerBonusInventory.GetBonusInSlot(slotIndex);
            var bonus = _dataBase.GetBonusInfo(bonusType);
            var logic = bonus.BonusLogic.GetLogic();
            logic.SlotIndex = slotIndex;
            _objectResolver.Inject(logic);
            logic.UseBonus();
        }     
    }
}