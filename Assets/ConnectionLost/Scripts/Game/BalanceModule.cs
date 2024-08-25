using Infrastructure.DI;
using UnityEngine;


namespace ConnectionLost
{
    internal sealed class BalanceModule : Module
    {
        [SerializeField, Service(typeof(GameBalanceSettings))]
        private GameBalanceSettings settings = new();

    }
}