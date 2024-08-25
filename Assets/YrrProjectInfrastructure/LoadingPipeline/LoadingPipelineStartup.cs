using Sirenix.OdinInspector;
using Sirenix.Serialization;


namespace Infrastructure.LoadingPipeline
{
    internal sealed class LoadingPipelineStartup : SerializedMonoBehaviour
    {
        [OdinSerialize] private ILoadingTask[] loadingTasks;

        private async void Awake()
        {
            foreach (var task in loadingTasks)
            {
                var result = await task.Do();
            }
        }
    }
}