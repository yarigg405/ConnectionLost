using Infrastructure.GameSystem;
using System;
using UnityEngine;
using VContainer;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class TestNotMonoBeh : IUpdateListener
    {
        [Inject] private readonly GameMachine machine;
        [Inject] private readonly TickableProcessor processor;

        void IUpdateListener.OnUpdate(float deltaTime)
        {
            Debug.Log($"### {machine == null} - {processor == null}");
        }
    }
}