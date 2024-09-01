using UnityEngine;
using UnityEngine.UI;


namespace ConnectionLost
{
    internal sealed class BonusButton : MonoBehaviour
    {
        [SerializeField] private Image bonusIcon;


        internal void RemoveBonus()
        {
            
        }

        internal void SetBonus(BonusSO bonus)
        {
            bonusIcon.sprite = bonus.BonusIcon;
        }
    }
}