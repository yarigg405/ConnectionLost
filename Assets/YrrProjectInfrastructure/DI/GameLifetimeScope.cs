using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Infrastructure.DI
{
    internal sealed class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private ModulesInstaller moduleInstaller;

        protected override void Configure(IContainerBuilder builder)
        {
            base.Configure(builder);
            for (int i = 0; i < moduleInstaller.Modules.Length; i++)
            {
                Module module = moduleInstaller.Modules[i];
                foreach (var item in module.GetServices())
                {
                    var type = item.Item1;
                    var service = item.Item2;

                    if (service is MonoBehaviour mb)
                    {
                        builder.RegisterComponent(service).As(type);
                    }
                    else
                    {
                        builder.RegisterInstance(service).As(type);
                    }
                }
            }
        }
    }
}