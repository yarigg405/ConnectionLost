using Cysharp.Threading.Tasks;


namespace Infrastructure.LoadingPipeline
{
    public interface ILoadingTask
    {
        UniTask<LoadingResult> Do();
    }
}