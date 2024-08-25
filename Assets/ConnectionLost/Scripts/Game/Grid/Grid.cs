using UnityEngine;


namespace ConnectionLost
{
    internal sealed class Grid : MonoBehaviour
    {
        [field: SerializeField] public Transform CellsContainer { get; private set; }
        [field: SerializeField] public Transform CameraFollowPoint { get; private set; }
        [field: SerializeField] public Transform CameraLookPoint { get; private set; }
    }
}