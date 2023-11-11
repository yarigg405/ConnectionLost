using UnityEngine;
using Yrr.UI.Animators;


namespace Game
{
    internal sealed class EnemyAnimationController : MonoBehaviour
    {
        [SerializeField] private TweenAnimator onShowAnimator;
        [SerializeField] private TweenAnimator gettingDamageAnimator;
        [SerializeField] private TweenAnimator gettingHealAnimator;
        [SerializeField] private TweenAnimator deathAnimator;


        private TweenAnimator _lastAnimator;


        private void Start()
        {
            if (onShowAnimator != null)
            {
                PlayAnimator(onShowAnimator);
            }
        }


        internal void AnimateShow()
        {
            PlayAnimator(onShowAnimator);
        }

        internal void GetDamage(int damage)
        {
            PlayAnimator(gettingDamageAnimator);
        }

        internal void GetHeal(int heal)
        {
            PlayAnimator(gettingHealAnimator);
        }

        internal void SetDead()
        {
            PlayAnimator(deathAnimator);
        }




        private void PlayAnimator(TweenAnimator newAnimator)
        {
            StopCurrentAnimator();
            _lastAnimator = newAnimator;
            _lastAnimator.Animate();
        }

        private void StopCurrentAnimator()
        {
            if (_lastAnimator != null)
            {
                _lastAnimator.StopAnimation();
            }
        }
    }
}
