using System;
using VContainer;
using Yrr.UI;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class PlayerWinLoseController
    {
        [Inject] private readonly UIManager _uIManager;

        internal void PlayerWin()
        {
            _uIManager.OpenModal<PlayerWinScreen>();
        }

        internal void PlayerLose()
        {
            _uIManager.OpenModal<PlayerLoseScreen>();
        }
    }
}