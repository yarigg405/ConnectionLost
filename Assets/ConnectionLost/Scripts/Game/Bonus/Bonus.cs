using UnityEngine;
using Yrr.Entitaz;


namespace ConnectionLost
{
    internal sealed class Bonus : Entita
    {
        [field: SerializeField] public BonusType BonusType { get; private set; }


        public override void SetupEntita()
        {
            base.SetupEntita();

            var destroy = GetEntitaComponent<DestroyComponent>();
            destroy.OnDestroy += OnDeath;
        }

        private void OnDeath()
        {
            var destroy = GetEntitaComponent<DestroyComponent>();
            destroy.Clear();
            GameObject.Destroy(gameObject);
        }
    }

    public enum BonusType
    {
        Empty = 0,
        Repair,
        Shield,
        Hurricane,
    }
}