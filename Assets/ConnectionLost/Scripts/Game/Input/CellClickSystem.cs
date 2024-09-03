using System;
using VContainer;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class CellClickSystem
    {
        [Inject] private readonly NextTurnObserver _nextTurnObserver;

        internal void CellClicked(Cell cell)
        {
            if (cell.Status != CellStatus.Opened) return;
            cell.OpenContainer();
            _nextTurnObserver.NextTurn();
        }      
    }
}
