using System;
using System.Linq;
using VContainer;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class PlayerBonusInventory
    {
        public event Action<int> OnBonusAdded;
        public event Action<int> OnBonusRemoved;

        [Inject] private readonly BonusSoDataBase _dataBase;

        private BonusType[] _bonusesInInventory = new BonusType[3];


        public void RemoveAt(int bonusIndex)
        {
            _bonusesInInventory[bonusIndex] = BonusType.Empty;
            OnBonusRemoved?.Invoke(bonusIndex);
        }

        public BonusType GetBonusInSlot(int index)
        {
            return _bonusesInInventory[index];
        }

        public void InsertBonus(BonusType bonusType)
        {
            for (int i = 0; i < _bonusesInInventory.Length; i++)
            {
                if (_bonusesInInventory[i] == BonusType.Empty)
                {
                    _bonusesInInventory[i] = bonusType;
                    OnBonusAdded?.Invoke(i);
                    return;
                }
            }
        }

        public bool CanAddNew()
        {
            return _bonusesInInventory.Any(x => x == BonusType.Empty);
        }
    }
}