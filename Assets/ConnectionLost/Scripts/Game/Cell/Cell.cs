using UnityEngine;
using Yrr.Entitaz;
using Yrr.UI.Animators;
using Yrr.Utils;


namespace ConnectionLost
{
    internal sealed class Cell : Entita
    {
        [SerializeField] private TweenAnimator animator;
        [SerializeField] private Collider clickTrigger;

        internal ReactiveValue<CellStatus> Status = new();
        internal int BlocksCount;
    }
}