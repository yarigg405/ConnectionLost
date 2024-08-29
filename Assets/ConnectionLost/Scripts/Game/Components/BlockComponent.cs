using UnityEngine;
using Yrr.Entitaz;


namespace ConnectionLost
{
    internal sealed class BlockComponent : MonoBehaviour, IEntitazComponent
    {
        [field: SerializeField] public bool IsBlocker { get; private set; }
        [field: SerializeField] public bool IsCanBeBlocked { get; private set; }
        [field: SerializeField] public int BlocksCount { get; private set; }


        internal void Block()
        {
            BlocksCount++;
        }

        internal void Unblock()
        {
            BlocksCount--;
        }
    }
}