using UnityEngine;


namespace Game.ECS.Views
{
    internal sealed class EnemyView : MonoBehaviour
    {
        [SerializeField] private EnemyAnimationController animator;


        public void ShowView()
        {
            animator.AnimateShow();
        }

        public void ShowTakingDamage(int damage)
        {
            animator.GetDamage(damage);
        }

        public void ShowDeath()
        {
            animator.SetDead();
        }
    }
}
