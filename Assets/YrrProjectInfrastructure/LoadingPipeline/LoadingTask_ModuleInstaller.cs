using Cysharp.Threading.Tasks;
using Infrastructure.DI;
using UnityEngine;


namespace Infrastructure.LoadingPipeline
{
    internal sealed class LoadingTask_ModuleInstaller : MonoBehaviour, ILoadingTask
    {
        [SerializeField] private ModulesInstaller installer;

        async UniTask<LoadingResult> ILoadingTask.Do()
        {
            installer.InstallModules();
            await UniTask.Yield();
            var result = new LoadingResult(true);
            return result;
        }
    }
}