using Yrr.Entitaz;


namespace ConnectionLost
{
    internal sealed class Enemy : Entita
    {
        public bool IsAlive => GetEntitaComponent<HealthComponent>().CurrentHealth > 0;

        public override void SetupEntita()
        {
            base.SetupEntita();

            var healt = GetEntitaComponent<HealthComponent>();
            healt.OnDeath += OnDeath;
        }

        private void OnDeath()
        {            
            var healt = GetEntitaComponent<HealthComponent>();
            healt.OnDeath -= OnDeath;

            foreach (var startable in GetEntitaComponents<IStartableComponent>())
            {
                startable.StopComponent();
            }

            var destroy = GetEntitaComponent<DestroyComponent>();
            destroy.Destroy();
            destroy.Clear();

            Destroy(gameObject);
        }
    }
}