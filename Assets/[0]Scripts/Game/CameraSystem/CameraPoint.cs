using UnityEngine;


namespace Game.CameraSystem
{
    internal sealed class CameraPoint : MonoBehaviour
    {
        [field: SerializeField] public Transform CameraFollowPoint;
        [field: SerializeField] public Transform CameraLookPoint;
        [field: SerializeField] public Transform CameraPositionPoint;
    }
}
