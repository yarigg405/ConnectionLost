using System;
using Yrr.Utils;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class PlayerStats
    {
        public ReactiveValue<int> PlayerHealth = new();
        public ReactiveValue<int> PlayerAttack = new();
    }
}