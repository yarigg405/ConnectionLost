using Yrr.Entitaz;


namespace ConnectionLost
{
    internal interface IStartableComponent : IEntitazComponent
    {
        void StartComponent();

        void StopComponent();
    }
}
