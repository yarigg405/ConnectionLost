using System;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class CellClickSystem
    {
        internal void CellClicked(Cell cell)
        {
            if (cell.Status != CellStatus.Opened) return;
            cell.OpenContainer();
        }      
    }
}
