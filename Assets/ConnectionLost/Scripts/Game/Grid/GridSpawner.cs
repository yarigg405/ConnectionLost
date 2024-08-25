using System;
using UnityEngine;
using Yrr.Utils;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class GridSpawner
    {
        [SerializeField] private Cell cellPrefab;

        internal void SpawnGrid(GridModel gridData, Grid grid)
        {
            grid.CellsContainer.ClearChildren();

            foreach (var p in gridData.CellsPositions)
            {
                var cell = GameObject.Instantiate(cellPrefab, grid.CellsContainer);
                var x = (p.x + p.y % 2 * 0.5f) * 1.65f;
                var z = p.y * 1.45f;
                cell.transform.localPosition = new Vector3(x, 0, z);
            }
        }
    }
}
