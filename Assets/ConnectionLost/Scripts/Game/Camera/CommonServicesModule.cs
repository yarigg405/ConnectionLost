using Infrastructure.DI;
using UnityEngine;
using Yrr.UI;


namespace ConnectionLost.Camera
{
    internal sealed class CommonServicesModule : Module
    {
        [SerializeField, Listener, Service(typeof(CameraSystem))]
        private CameraSystem cameraSystem = new();

        [SerializeField, Service(typeof(UIManager))]
        private UIManager uiManager;
    }
}