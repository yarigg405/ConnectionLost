using Game.CameraSystem;
using UnityEngine;
using VContainer;


namespace Game
{
    internal sealed class Grid : MonoBehaviour
    {
        [SerializeField] private CameraPoint cameraPoint;
        [field: SerializeField] public Transform NodesContainer { get; private set; }
        [Inject] private readonly CameraSystem.CameraSystem _cameraSystem;


        public void SetSelected()
        {
            _cameraSystem.SetLookPosition(cameraPoint.CameraLookPoint);
            _cameraSystem.SetFollowPosition(cameraPoint.CameraFollowPoint);
            _cameraSystem.SetCameraPosition(cameraPoint.CameraPositionPoint);
        }

        public void SetDeselected()
        {

        }
    }
}
