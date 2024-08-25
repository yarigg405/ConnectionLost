using Cinemachine;
using System;
using UnityEngine;


namespace ConnectionLost.Camera
{
    [Serializable]
    internal sealed class CameraSystem
    {
        [SerializeField] private CinemachineVirtualCamera vCam;


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