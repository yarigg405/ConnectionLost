using System.Linq;
using UnityEngine;
using VContainer;
using Yrr.Utils;


namespace ConnectionLost
{
    internal sealed class EnemyHealerComponent : MonoBehaviour, IStartableComponent
    {
        [SerializeField] private int healValue = 20;

        [Inject] private readonly EnemyStorage _enemyStorage;
        [Inject] private readonly NextTurnObserver _nextTurnObserver;


        void IStartableComponent.StartComponent()
        {
            _nextTurnObserver.OnNextTurn += Heal;
        }

        void IStartableComponent.StopComponent()
        {
            _nextTurnObserver.OnNextTurn -= Heal;
        }

        private void Heal()
        {
            var enemies = _enemyStorage.GetValues()
                 .Where(x => x.IsAlive && x.gameObject != gameObject);

            if (enemies.Any())
            {
                var random = enemies.GetRandomItem();
                var health = random.GetEntitaComponent<HealthComponent>();
                health.GetHeal(healValue);
            }
        }
    }
}