using System;
using UnityEngine;
using UnityEngine.Events;
using Yrr.Entitaz;


namespace ConnectionLost
{
    internal sealed class HealthComponent : MonoBehaviour, IEntitazComponent
    {
        [field: SerializeField] public int MaxHealth { get; private set; }
        [field: SerializeField] public int CurrentHealth { get; private set; }

        [SerializeField] private UnityEvent onDamageReceived;
        [SerializeField] private UnityEvent onHealReceived;

        public event Action OnDamageReceived;
        public event Action OnHealReceived;
        public event Action OnDeath;

        private void OnEnable()
        {
            CurrentHealth = MaxHealth;
        }

        internal void GetDamage(int damage)
        {
            CurrentHealth -= damage;
            OnDamageReceived?.Invoke();
            onDamageReceived?.Invoke();

            if (CurrentHealth <= 0)
            {
                OnDeath?.Invoke();
            }
        }

        internal void GetHeal(int heal)
        {
            CurrentHealth += heal;
            onHealReceived?.Invoke();
            OnHealReceived?.Invoke();
        }
    }
}