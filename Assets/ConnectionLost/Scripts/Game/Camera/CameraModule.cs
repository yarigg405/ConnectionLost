using Infrastructure.DI;
using UnityEngine;


namespace ConnectionLost.Camera
{
    internal sealed class CameraModule : Module
    {
        [SerializeField, Listener, Service(typeof(CameraSystem))]
        private CameraSystem cameraSystem = new();
    }
}