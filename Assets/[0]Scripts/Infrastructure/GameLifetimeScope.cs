using Game;
using Game.CameraSystem;
using UnityEngine;
using VContainer;
using VContainer.Unity;


namespace Infrastructrure.Di
{
    public class GameLifetimeScope : LifetimeScope
    {
        [SerializeField] private CameraSystem cameraSystem;
        [SerializeField] private GridsSelector gridsSelector;
        [SerializeField] private GameStarter gameStarter;



        protected override void Configure(IContainerBuilder builder)
        {
            builder.RegisterComponent(cameraSystem);
            builder.RegisterComponent(gridsSelector);
            builder.RegisterComponent(gameStarter);
        }
    }
}