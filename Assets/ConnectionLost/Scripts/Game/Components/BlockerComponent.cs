using UnityEngine;
using Yrr.Entitaz;


namespace ConnectionLost
{
    internal sealed class BlockerComponent : MonoBehaviour, IStartableComponent
    {
        void IStartableComponent.StartComponent()
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

        void IStartableComponent.StopComponent()
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