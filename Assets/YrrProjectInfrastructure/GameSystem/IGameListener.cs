namespace Infrastructure.GameSystem
{
    public interface IGameListener
    {
    }

    public interface IGameStartListener : IGameListener
    {
        void OnGameStart();
    }

    public interface IGameFinishListener : IGameListener
    {
        void OnGameFinish();
    }

    public interface IGamePauseListener : IGameListener
    {
        void OnGamePaused();
        void OnGameUnPaused();
    }

    public interface IUpdateListener : IGameListener
    {
        void OnUpdate(float deltaTime);
    }

    public interface IFixedUpdateListener : IGameListener
    {
        void OnFixedUpdate(float fixedDeltaTime);
    }
}