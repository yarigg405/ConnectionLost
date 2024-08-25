using Cysharp.Threading.Tasks;
using Infrastructure.LoadingPipeline;
using UnityEngine.SceneManagement;


namespace ConnectionLost
{
    internal sealed class LoadingTask_LoadGameScene : ILoadingTask
    {
        private const string _gameSceneindex = "GameScene";

        async UniTask<LoadingResult> ILoadingTask.Do()
        {
            await SceneManager.LoadSceneAsync(_gameSceneindex);
            var result = new LoadingResult(true);
            return result;
        }
    }
}