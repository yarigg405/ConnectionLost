using System;
using UnityEngine;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class CellClickHandler
    {
        internal void CellClicked(Cell cell)
        {
            cell.State.Value = CellState.Empty;

            var neighboursColliders = Physics.OverlapSphere(cell.transform.position, 1f);
            for (int i = 0; i < neighboursColliders.Length; i++)
            {
                if (neighboursColliders[i].TryGetComponent<Cell>(out var neighour))
                {
                    if (neighour.State == CellState.Closed)
                    {
                        neighour.State.Value = CellState.Opened;
                    }
                }
            }
        }
    }
}
