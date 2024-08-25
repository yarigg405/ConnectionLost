using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Infrastructure.DI
{
    internal sealed class ModulesInstaller : MonoBehaviour
    {
        [field: SerializeField] public Module[] Modules { get; private set; }

        [Inject] private readonly GameMachine _gameMachine;
        [Inject] private readonly TickableProcessor _tickableProcessor;
        [Inject] private readonly IObjectResolver _container;
        [Inject] private readonly GameLifetimeScope _lifetimeScope;

        internal void InstallModules()
        {
            for (int i = 0; i < Modules.Length; i++)
            {
                InstallModule(Modules[i]);
            }
        }

        private void InstallModule(Module module)
        {
            foreach (var item in module.GetServices())
            {
                var service = item.Item2;
                if (service is MonoBehaviour mb)
                {
                    _container.InjectGameObject(mb.gameObject);
                }
                else
                {
                    _container.Inject(service);
                }

                if (service is IUpdateListener tickable)
                {
                    _tickableProcessor.AddTickable(tickable);
                }

                if (service is IFixedUpdateListener fixedTickable)
                {
                    _tickableProcessor.AddFixedTickable(fixedTickable);
                }

                if (service is IGameListener gameListener)
                {
                    _gameMachine.AddListener(gameListener);
                }
            }
        }
    }
}