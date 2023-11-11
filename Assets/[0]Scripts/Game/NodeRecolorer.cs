using Game.ECS.Views;
using UnityEngine;
using Yrr.UI.Animators;
using Yrr.Utils;


namespace Game
{
    internal sealed class NodeRecolorer : MonoBehaviour
    {
        [SerializeField] private NodeView view;

        [Space]
        [SerializeField] private TweenAnimator OnClickAnimator;
        [SerializeField] private UnityDictionary<NodeCondition, TweenAnimator> animators;



        private void OnEnable()
        {
            view.OnConditionChanged += SetState;
            view.OnNodeClicked += SetClicked;

        }

        private void OnDisable()
        {
            view.OnConditionChanged -= SetState;
            view.OnNodeClicked -= SetClicked;
        }

        private void SetClicked()
        {
            OnClickAnimator.Animate();
        }

        private void SetState(NodeCondition condition)
        {
            var anim = animators.Get(condition);
            if (!anim) return;

            anim.Animate();
        }
    }
}
