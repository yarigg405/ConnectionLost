using Infrastructure.GameSystem;
using System;
using VContainer;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class PlayerSetup : IGameStartListener, IGameFinishListener
    {
        [Inject] private readonly PlayerStats _playerStats;
        [Inject] private readonly GameBalanceSettings _balance;
        [Inject] private readonly PlayerWinLoseController _playerWinLoseController;

        void IGameStartListener.OnGameStart()
        {
            _playerStats.PlayerHealth.OnChange += OnPlayerDamaged;
        }

        void IGameFinishListener.OnGameFinish()
        {
            _playerStats.PlayerHealth.OnChange -= OnPlayerDamaged;
        }

        private void OnPlayerDamaged(int playerHealth)
        {
            if (playerHealth <= 0)
                _playerWinLoseController.PlayerLose();
        }

        internal void SetupPlayer()
        {
            _playerStats.PlayerHealth.Value = _balance.PlayerBaseHealth;
            _playerStats.PlayerAttack.Value = _balance.PlayerBaseAttack;
        }
    }
}