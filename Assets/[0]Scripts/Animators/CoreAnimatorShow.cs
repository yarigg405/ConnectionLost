using DG.Tweening;
using UnityEngine;
using Yrr.UI.Animators;


namespace Game
{
    internal sealed class CoreAnimatorShow : TweenAnimator
    {
        [SerializeField] private Transform mainRoot;
        [SerializeField] private float mainRotaionDuration;

       
        protected override Sequence GetSequence()
        {
            mainRoot.transform.localScale = Vector3.zero;

            var seq = DOTween.Sequence(this).SetUpdate(true)

                .Append(mainRoot.DOLocalRotate(new Vector3(0, 0, 360), mainRotaionDuration))
                .Join(mainRoot.DOScale(1.1f, mainRotaionDuration))

                .Append(mainRoot.DOScale(1.0f, mainRotaionDuration * 0.3f))

                ;

            return seq;
        }

        protected override void ResetToDefault()
        {
            mainRoot.transform.localScale = Vector3.one;
            mainRoot.transform.localRotation = Quaternion.identity;
        }
    }
}
