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
            _gameMachine.StartGame();
            await UniTask.Yield();
            var result = new LoadingResult(true);
            return result;
        }
    }
}