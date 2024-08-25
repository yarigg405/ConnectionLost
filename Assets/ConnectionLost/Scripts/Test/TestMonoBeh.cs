using Infrastructure.GameSystem;
using UnityEngine;
using VContainer;


namespace ConnectionLost
{
    internal sealed class TestMonoBeh : MonoBehaviour, IUpdateListener
    {
        [Inject] private readonly GameMachine machine;
        [Inject] private readonly TickableProcessor processor;

        void IUpdateListener.OnUpdate(float deltaTime)
        {
            Debug.Log($"{machine == null} - {processor == null}");
        }
    }
}