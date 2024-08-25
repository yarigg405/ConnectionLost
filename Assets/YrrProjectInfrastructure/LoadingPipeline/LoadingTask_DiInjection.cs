using Cysharp.Threading.Tasks;
using UnityEngine;
using VContainer.Unity;


namespace Infrastructure.LoadingPipeline
{
    internal sealed class LoadingTask_DiInjection : MonoBehaviour, ILoadingTask
    {
        [SerializeField] private LifetimeScope scope;

        async UniTask<LoadingResult> ILoadingTask.Do()
        {
            var tcs = new UniTaskCompletionSource<LoadingResult>();
            scope.Build();
            await UniTask.Yield();
            var result = new LoadingResult(true);
            return result;
        }
    }
}