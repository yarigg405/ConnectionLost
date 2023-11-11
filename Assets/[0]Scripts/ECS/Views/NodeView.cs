using System;
using UnityEngine;


namespace Game.ECS.Views
{
    internal sealed class NodeView : MonoBehaviour
    {
        public event Action OnNodeClicked;
        public event Action<NodeCondition> OnConditionChanged;

        public NodeCondition CurrentCondition { get; private set; }


        public void SetClicked()
        {
            OnNodeClicked?.Invoke();
        }

        public void SetCondition(NodeCondition newCondition)
        {
            if (CurrentCondition == newCondition) return;
            if (CurrentCondition == NodeCondition.Empty) return;


            CurrentCondition = newCondition;
            OnConditionChanged?.Invoke(newCondition);
        }
    }
}
