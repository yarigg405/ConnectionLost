using System.Collections.Generic;
using UnityEngine;
using Yrr.Utils;


namespace ConnectionLost
{
    internal sealed class GridGenerator
    {
        internal GridModel GenerateRandomGrid(GridStats gridStats)
        {
            var cells = GeneratePlane(gridStats);

            var model = new GridModel
            {
                CellsPositions = cells,
            };
            return model;
        }

        private IEnumerable<Vector2Int> GeneratePlane(GridStats gridStats)
        {
            var cells = new HashSet<Vector2Int>(gridStats.Width * gridStats.Height);
            var cellsCount = 0;
            while (cellsCount < gridStats.CellsCount)
            {
                var point = GeneratePoint(cells.GetRandomItem(), gridStats);

                if (!cells.Contains(point))
                {
                    cells.Add(point);
                    cellsCount++;
                }
            }

            return cells;
        }

        private Vector2Int GeneratePoint(Vector2Int point, GridStats gridStats)
        {
            if (Random.Range(0f, 1f) < 0.5f)
            {
                if (Random.Range(0f, 1f) < 0.5f)
                {
                    point.x += 1;
                }
                else
                {
                    point.x -= 1;
                }

                if (point.x > gridStats.Width)
                    point.x -= 2;

                if (point.x < 0)
                    point.x += 2;
            }

            else
            {
                if (Random.Range(0f, 1f) < 0.5f)
                {
                    point.y += 1;
                }
                else
                {
                    point.y -= 1;
                }

                if (point.y > gridStats.Height)
                    point.y -= 2;

                if (point.y < 0)
                    point.y += 2;
            }

            return point;
        }
    }
}