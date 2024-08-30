using UnityEngine;
using Yrr.Entitaz;


namespace ConnectionLost
{
    internal sealed class BlockerComponent : MonoBehaviour, IEntitazComponent
    {
        internal void StartBlock()
        {
            var neighboursColliders = Physics.OverlapSphere(transform.position, 1f);
            for (int i = 0; i < neighboursColliders.Length; i++)
            {
                if (neighboursColliders[i].TryGetComponent<IEntita>(out var neighour))
                {
                    if (neighour.TryGetEntitaComponent<CanBeBlockedComponent>(out var block))
                    {
                        block.Block();
                    }
                }
            }
        }

        internal void StopBlocking()
        {
            var neighboursColliders = Physics.OverlapSphere(transform.position, 1f);
            for (int i = 0; i < neighboursColliders.Length; i++)
            {
                if (neighboursColliders[i].TryGetComponent<IEntita>(out var neighour))
                {
                    if (neighour.TryGetEntitaComponent<CanBeBlockedComponent>(out var block))
                    {
                        block.Unblock();
                    }
                }
            }
        }
    }
}