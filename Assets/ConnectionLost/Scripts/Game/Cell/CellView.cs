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
        [OdinSerialize] private Dictionary<CellStatus, TweenAnimator> animators;

        private void OnEnable()
        {
            cell.Status.OnChange += OnStateChanged;
            OnStateChanged(cell.Status);
        }

        private void OnDisable()
        {
            cell.Status.OnChange -= OnStateChanged;
        }

        private void OnStateChanged(CellStatus state)
        {
            var animator = animators[state];
            animator.Animate();
        }
    }
}