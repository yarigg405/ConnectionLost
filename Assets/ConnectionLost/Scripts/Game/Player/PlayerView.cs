using System;
using TMPro;
using UnityEngine;
using VContainer;


namespace ConnectionLost
{
    internal sealed class PlayerView : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI attackTmp;
        [SerializeField] private TextMeshProUGUI hpTmp;
        [SerializeField] private BonusButton[] bonusButtons;

        [Inject] private readonly PlayerStats _playerStats;
        [Inject] private readonly PlayerBonusInventory _playerBonusInventory;
        [Inject] private readonly BonusUseSystem _bonusUseSystem;
        [Inject] private readonly BonusSoDataBase _dataBase;


        private void Start()
        {
            _playerStats.PlayerAttack.OnChange += UpdateAttack;
            _playerStats.PlayerHealth.OnChange += UpdateHealth;
            UpdateAttack(_playerStats.PlayerAttack);
            UpdateHealth(_playerStats.PlayerHealth);

            _playerBonusInventory.OnBonusAdded += UpdateSlot;
            _playerBonusInventory.OnBonusRemoved += UpdateSlot;

            for (int i = 0; i < bonusButtons.Length; i++)
            {
                UpdateSlot(i);
            }
        }

        private void OnDestroy()
        {
            _playerStats.PlayerAttack.OnChange -= UpdateAttack;
            _playerStats.PlayerHealth.OnChange -= UpdateHealth;

            _playerBonusInventory.OnBonusAdded -= UpdateSlot;
            _playerBonusInventory.OnBonusRemoved -= UpdateSlot;
        }


        private void UpdateAttack(int attack)
        {
            attackTmp.text = $"<sprite=1>{attack}";
        }

        private void UpdateHealth(int health)
        {
            hpTmp.text = $"<sprite=0>{health}";
        }


        public void ClickOnBonus(int slotIndex)
        {
            _bonusUseSystem.UseBonus(slotIndex);
        }


        private async void UpdateSlot(int slotIndex)
        {
            var bonusType = _playerBonusInventory.GetBonusInSlot(slotIndex);

            if (bonusType == BonusType.Empty)
            {
                bonusButtons[slotIndex].RemoveBonus();
                bonusButtons[slotIndex].gameObject.SetActive(false);
            }

            else
            {
                var bonus = await _dataBase.GetBonusInfo(bonusType);
                bonusButtons[slotIndex].SetBonus(bonus);
                bonusButtons[slotIndex].gameObject.SetActive(true);
            }
        }
    }
}