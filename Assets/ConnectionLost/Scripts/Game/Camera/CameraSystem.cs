using Cinemachine;
using Infrastructure.GameSystem;
using System;
using UnityEngine;


namespace ConnectionLost.Camera
{
    [Serializable]
    internal sealed class CameraSystem : IGameStartListener
    {
        [SerializeField] private CinemachineVirtualCamera vCam;
        public UnityEngine.Camera MainCamera { get; private set; }


        void IGameStartListener.OnGameStart()
        {
            MainCamera = UnityEngine.Camera.main;
        }


        internal void LookAt(Transform tr)
        {
            vCam.LookAt = tr;
        }

        internal void FollowAt(Transform tr)
        {
            vCam.Follow = tr;
        }
    }
}