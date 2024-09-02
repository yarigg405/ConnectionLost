using UnityEngine;
using UnityEngine.UI;
using TMPro;


namespace ConnectionLost
{
    internal abstract class BonusLogic
    {
        internal int SlotIndex { get; set; }

        public abstract BonusLogic GetLogic();

        public abstract void UseBonus();
    }
}