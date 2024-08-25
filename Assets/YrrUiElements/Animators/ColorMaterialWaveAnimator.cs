using DG.Tweening;
using UnityEngine;


namespace Yrr.UI.Animators
{
    internal sealed class ColorMaterialWaveAnimator : TweenAnimator
    {
        [SerializeField] private MeshRenderer mRenderer;
        [SerializeField] private Color endColor;
        private Color _startColor;

        [SerializeField] private float durationUp = 0.3f;
        [SerializeField] private float durationDown = 0.3f;

        [SerializeField] private bool needReturn;

        private void Awake()
        {
            _startColor = mRenderer.material.color;
        }

        protected override Sequence GetSequence()
        {
            DOTween.Kill(mRenderer);

            var seq = DOTween.Sequence(mRenderer).SetUpdate(true)
                .Append(DOVirtual.Color(mRenderer.material.color, endColor, durationUp, (value) =>
                {
                    mRenderer.material.color = value;
                }));

            if (needReturn)
                seq.Append(DOVirtual.Color(mRenderer.material.color, _startColor, durationDown, (value) =>
                {
                    mRenderer.material.color = value;
                }));

            return seq;
        }

        protected override void ResetToDefault()
        {
            if (needReturn)
                mRenderer.material.color = _startColor;
            else
                mRenderer.material.color = endColor;
        }
    }
}