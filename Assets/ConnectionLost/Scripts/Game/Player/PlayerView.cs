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

        [Inject] private readonly PlayerStats _playerStats;

        private void Start()
        {
            _playerStats.PlayerAttack.OnChange += UpdateAttack;
            _playerStats.PlayerHealth.OnChange += UpdateHealth;
            UpdateAttack(_playerStats.PlayerAttack);
            UpdateHealth(_playerStats.PlayerHealth);
        }


        private void OnDestroy()
        {
            _playerStats.PlayerAttack.OnChange -= UpdateAttack;
            _playerStats.PlayerHealth.OnChange -= UpdateHealth;
        }

        private void UpdateHealth(int health)
        {
            hpTmp.text = $"<sprite=0>{health}";
        }

        private void UpdateAttack(int attack)
        {
            attackTmp.text = $"<sprite=1>{attack}";
        }
    }
}