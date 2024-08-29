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


            cell.Status.Value = CellStatus.Empty;

            var neighboursColliders = Physics.OverlapSphere(cell.transform.position, 1f);
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
}
