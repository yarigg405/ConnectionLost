using Sirenix.OdinInspector;
using Sirenix.Serialization;
using System.Collections.Generic;
using UnityEngine;
using Yrr.UI.Animators;


namespace ConnectionLost
{
    internal sealed class CellView : SerializedMonoBehaviour
    {
        [SerializeField] private Cell cell;
        [OdinSerialize] private Dictionary<CellState, TweenAnimator> animators;

        private void OnEnable()
        {
            cell.State.OnChange += OnStateChanged;
            OnStateChanged(cell.State);
        }

        private void OnDisable()
        {
            cell.State.OnChange -= OnStateChanged;
        }

        private void OnStateChanged(CellState state)
        {
            var animator = animators[state];
            animator.Animate();
        }
    }
}