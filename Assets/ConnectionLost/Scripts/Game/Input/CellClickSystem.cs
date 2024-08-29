using System;
using UnityEngine;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class CellClickSystem
    {
        internal void CellClicked(Cell cell)
        {
            if (cell.Status != CellStatus.Opened) return;

            var contentContainer = cell.GetEntitaComponent<ContentContainer>();
            if (contentContainer.IsEmpty)
            {
                cell.Status.Value = CellStatus.Empty;

                var neighboursColliders = Physics.OverlapSphere(cell.transform.position, 1f);
                for (int i = 0; i < neighboursColliders.Length; i++)
                {
                    if (neighboursColliders[i].TryGetComponent<Cell>(out var neighour))
                    {
                        Open(neighour);
                    }
                }
            }

            else
            {
                contentContainer.ContentEntita.gameObject.SetActive(true);
                cell.Status.Value = CellStatus.HaveContent;

                var block = contentContainer.ContentEntita.GetEntitaComponent<BlockComponent>();
                if (block.IsBlocker)
                {
                    var neighboursColliders = Physics.OverlapSphere(cell.transform.position, 1f);
                    for (int i = 0; i < neighboursColliders.Length; i++)
                    {
                        if (neighboursColliders[i].TryGetComponent<Cell>(out var neighour))
                        {
                            Block(neighour);
                        }
                    }
                }

                else
                {
                    var neighboursColliders = Physics.OverlapSphere(cell.transform.position, 1f);
                    for (int i = 0; i < neighboursColliders.Length; i++)
                    {
                        if (neighboursColliders[i].TryGetComponent<Cell>(out var neighour))
                        {
                            Open(neighour);
                        }
                    }
                }
            }
        }

        private void Open(Cell neighour)
        {
            if (neighour.Status == CellStatus.Closed)
            {
                neighour.Status.Value = CellStatus.Opened;
            }
        }

        private void Block(Cell neighour)
        {
            if (neighour.Status == CellStatus.Empty) return;

            var block = neighour.GetEntitaComponent<BlockComponent>();
            var content = neighour.GetEntitaComponent<ContentContainer>();

            if (content.IsEmpty)
            {
                block.Block();
                neighour.Status.Value = CellStatus.Blocked;
            }

            else
            {
                var contentBlock = content.ContentEntita.GetEntitaComponent<BlockComponent>();
                if (contentBlock.IsCanBeBlocked)
                {
                    contentBlock.Block();
                    block.Block();
                    neighour.Status.Value = CellStatus.Blocked;
                }
            }
        }
    }
}
