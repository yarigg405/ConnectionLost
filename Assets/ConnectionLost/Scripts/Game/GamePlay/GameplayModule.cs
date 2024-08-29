using Infrastructure.DI;
using UnityEngine;


namespace ConnectionLost
{
    internal sealed class GameplayModule : Module
    {
        [SerializeField, Service(typeof(CellsSystem))]
        private CellsSystem cellsSystem;
    }
}