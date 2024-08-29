using Yrr.Entitaz;
using Yrr.Utils;


namespace ConnectionLost
{
    internal sealed class Cell : Entita
    {
        internal ReactiveValue<CellStatus> Status = new();
    }
}