using Cinemachine;
using System.Collections;
using UnityEngine;


namespace Game.CameraSystem
{
    internal sealed class CameraSystem : MonoBehaviour
    {
        [SerializeField] private Camera mainCamera;
        [SerializeField] private CinemachineVirtualCamera vCam;


        [SerializeField] private float changeCameraDuration = 0.7f;


        private Coroutine _setNewFollowCoroutine;
        private Coroutine _setNewLookCoroutine;


        internal void SetFollowPosition(Transform followPosition)
        {
            if (_setNewFollowCoroutine != null)
                StopCoroutine(_setNewFollowCoroutine);

            _setNewFollowCoroutine = StartCoroutine(ChangeFollowCoroutine(followPosition));
        }

        private IEnumerator ChangeFollowCoroutine(Transform followPosition)
        {
            yield return new WaitForSecondsRealtime(changeCameraDuration);
            vCam.Follow = followPosition;
        }

        internal void SetLookPosition(Transform lookPosition)
        {
            if (_setNewLookCoroutine != null)
                StopCoroutine(_setNewLookCoroutine);

            _setNewLookCoroutine = StartCoroutine(ChangeLookPosition(lookPosition));
        }

        private IEnumerator ChangeLookPosition(Transform lookPosition)
        {
            yield return new WaitForSecondsRealtime(changeCameraDuration);
            vCam.LookAt = lookPosition;
        }

        internal void SetCameraPosition(Transform cameraPositionPoint)
        {
            vCam.transform.position = cameraPositionPoint.position;
        }
    }
}
