using UnityEngine;
using Yrr.Entitaz;
using Yrr.Utils;


namespace ConnectionLost
{
    internal sealed class CanBeBlockedComponent : MonoBehaviour, IEntitazComponent
    {
        public ReactiveValue<bool> IsBlocked;

        [SerializeField] private int _blocksCount;

        internal void Block()
        {
            _blocksCount++;
            IsBlocked.Value = _blocksCount > 0;
        }

        internal void Unblock()
        {
            _blocksCount--;
            IsBlocked.Value = _blocksCount > 0;
        }
    }
}