using System;
using System.Linq;
using UnityEngine;
using VContainer;
using Yrr.Utils;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class CellsSpawner
    {
        [SerializeField] private Cell cellPrefab;

        [Inject] private readonly CellsStorage _cellsSystem;

        internal void SpawnGrid(GridModel gridData, Grid grid)
        {
            grid.CellsContainer.ClearChildren();
            _cellsSystem.Clear();            


            {
                var p = gridData.CellsPositions.First();
                var cell = GameObject.Instantiate(cellPrefab, grid.CellsContainer);
                cell.SetupEntita();
                cell.Status.Value = CellStatus.Opened;
                var x = (p.x + p.y % 2 * 0.5f) * 1.65f;
                var z = p.y * 1.45f;
                cell.transform.localPosition = new Vector3(x, 0, z);
                _cellsSystem.Add(cell);
            }

            foreach (var p in gridData.CellsPositions.Skip(1))
            {
                var cell = GameObject.Instantiate(cellPrefab, grid.CellsContainer);
                cell.SetupEntita();
                var x = (p.x + p.y % 2 * 0.5f) * 1.65f;
                var z = p.y * 1.45f;
                cell.transform.localPosition = new Vector3(x, 0, z);
                _cellsSystem.Add(cell);
            }
        }
    }
}
