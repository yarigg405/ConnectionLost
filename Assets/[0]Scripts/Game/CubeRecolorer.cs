using DG.Tweening;
using Game.ECS.Views;
using UnityEngine;


namespace Game
{
    internal sealed class CubeRecolorer : MonoBehaviour
    {
        [SerializeField] private NodeView view;


        [SerializeField] private MeshRenderer mRenderer;
        [SerializeField] private Color colorClicked;
        private Color _defaultColor;

        [SerializeField] private float durationUp;
        [SerializeField] private float durationDown;


        private void Start()
        {
            _defaultColor = mRenderer.material.color;
            view.OnNodeClicked += RecolorClick;
        }

        private void OnDestroy()
        {
            view.OnNodeClicked -= RecolorClick;
        }

        private void RecolorClick()
        {
            AnimateRecolor();
        }

        private void AnimateRecolor()
        {
            var seq = DOTween.Sequence(this).SetUpdate(true)
                .Append(DOVirtual.Color(_defaultColor, colorClicked, durationUp, (value) =>
                {
                    mRenderer.material.color = value;
                }))
                .Append(DOVirtual.Color(colorClicked, _defaultColor, durationDown, (value) =>
                {
                    mRenderer.material.color = value;
                }))

                ;
        }
    }
}
