using UnityEngine;
using Yrr.Utils;


namespace Game.Player
{
    internal sealed class PlayerStats
    {
        public ReactiveInt PlayerAttack = new ReactiveInt(10);
        public ReactiveInt PlayerDefaultHealth = new ReactiveInt(40);
    }
}
