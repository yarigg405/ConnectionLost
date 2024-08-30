using Infrastructure.DI;
using UnityEngine;


namespace ConnectionLost
{
    internal sealed class PlayerModule : Module
    {
        [SerializeField, Listener, Service(typeof(PlayerSetup))]
        private PlayerSetup playerSetup = new();

        [SerializeField, Listener, Service(typeof(PlayerStats))]
        private PlayerStats playerStats = new();

        [SerializeField, Listener, Service(typeof(PlayerView))]
        private PlayerView playerView;
    }
}