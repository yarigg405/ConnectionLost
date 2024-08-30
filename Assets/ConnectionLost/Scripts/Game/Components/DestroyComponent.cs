using UnityEngine;
using Yrr.Entitaz;
using System;


namespace ConnectionLost
{
    internal sealed class DestroyComponent : MonoBehaviour, IEntitazComponent
    {
        public event Action OnDestroy;

        internal void Destroy()
        {
            OnDestroy?.Invoke();
        }

        internal void Clear()
        {
            OnDestroy = null;
        }
    }
}