using System;
using UnityEngine;
using Yrr.Entitaz;
using Yrr.UI.Animators;
using Yrr.Utils;


namespace ConnectionLost
{
    internal sealed class Cell : Entita
    {
        [SerializeField] private TweenAnimator animator;

        internal ReactiveValue<CellState> State = new();
        internal ReactiveValue<int> BlocksCount = new();
    }
}