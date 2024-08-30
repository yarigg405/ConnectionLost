using System;
using UnityEngine;
using Yrr.Entitaz;
using Yrr.Utils;
using static Codice.CM.WorkspaceServer.WorkspaceTreeDataStore;


namespace ConnectionLost
{
    internal sealed class Cell : Entita
    {
        internal ReactiveValue<CellStatus> Status = new();

        public override void SetupEntita()
        {
            base.SetupEntita();
            GetEntitaComponent<CanBeBlockedComponent>().IsBlocked.OnChange += SetBlockStatus;
        }

        private void OnDestroy()
        {
            GetEntitaComponent<CanBeBlockedComponent>().IsBlocked.OnChange -= SetBlockStatus;
        }


        private void SetBlockStatus(bool blocked)
        {
            if (Status == CellStatus.Empty) return;

            if (blocked)
            {
                Status.Value = CellStatus.Blocked;
            }

            else
            {
                var content = GetEntitaComponent<ContentContainer>().ContentEntita;
                if (content == null || !content.gameObject.activeSelf)
                {
                    Status.Value = CellStatus.Opened;
                }

                else
                {
                    Status.Value = CellStatus.HaveContent;
                }
            }
        }

        internal void OpenContainer()
        {
            var content = GetEntitaComponent<ContentContainer>();

            if (content.ContentEntita)
            {
                Status.Value = CellStatus.HaveContent;
                content.ContentEntita.gameObject.SetActive(true);
                if (content.ContentEntita.TryGetEntitaComponent<BlockerComponent>(out var block))
                {
                    block.StartBlock();
                }

                else
                {
                    var neighboursColliders = Physics.OverlapSphere(transform.position, 1f);
                    for (int i = 0; i < neighboursColliders.Length; i++)
                    {
                        if (neighboursColliders[i].TryGetComponent<Cell>(out var neighour))
                        {
                            if (neighour.Status == CellStatus.Closed)
                            {
                                neighour.Status.Value = CellStatus.Opened;
                            }
                        }
                    }
                }

                content.ContentEntita.GetEntitaComponent<DestroyComponent>().OnDestroy += OnContentDestroyed;
            }

            else
            {
                Status.Value = CellStatus.Empty;

                var neighboursColliders = Physics.OverlapSphere(transform.position, 1f);
                for (int i = 0; i < neighboursColliders.Length; i++)
                {
                    if (neighboursColliders[i].TryGetComponent<Cell>(out var neighour))
                    {
                        if (neighour.Status == CellStatus.Closed)
                        {
                            neighour.Status.Value = CellStatus.Opened;
                        }
                    }
                }
            }
        }

        private void OnContentDestroyed()
        {
            Status.Value = CellStatus.Empty;

            GetEntitaComponent<ContentContainer>()
                .ContentEntita.GetEntitaComponent<DestroyComponent>()
                .OnDestroy -= OnContentDestroyed;
        }
    }
}