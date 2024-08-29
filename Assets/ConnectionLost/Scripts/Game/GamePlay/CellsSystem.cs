using System;
using System.Collections.Generic;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class CellsSystem
    {
        private readonly List<Cell> _cells = new();

        internal void AddCell(Cell cell)
        {
            _cells.Add(cell);
        }

        internal void Clear()
        {
            _cells.Clear();
        }
    }
}