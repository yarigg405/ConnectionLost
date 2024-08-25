using Cysharp.Threading.Tasks;
using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;


namespace Infrastructure.LoadingPipeline
{
    internal sealed class LoadingTask_StartGame : MonoBehaviour, ILoadingTask
    {
        [Inject] private GameMachine _gameMachine;

        async UniTask<LoadingResult> ILoadingTask.Do()
        {
            var tcs = new UniTaskCompletionSource<LoadingResult>();
            await UniTask.RunOnThreadPool(_gameMachine.StartGame);
            var result = new LoadingResult(true);
            return result;
        }
    }
}