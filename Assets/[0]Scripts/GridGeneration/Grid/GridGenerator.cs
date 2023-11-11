using Game.GridGeneration.Models;
using System;
using System.Collections.Generic;
using Yrr.Utils;


namespace Game.GridGeneration.Grid
{
    internal sealed class GridGenerator
    {
        private readonly PlaneCreator _planeCreator = new();
        private readonly GridConfigsStorage _storage;


        internal GridGenerator(GridConfigsStorage storage)
        {
            _storage = storage;
        }

        internal IEnumerable<NodeModel> GenerateRandomGrid(int selectedLevel)
        {
            var config = GetConfig(selectedLevel);
            return GenerateGrid(config);
        }

        private GridConfig GetConfig(int selectedLevel)
        {
            return _storage.GetConfig(selectedLevel);
        }

        private IEnumerable<NodeModel> GenerateGrid(GridConfig config)
        {
            var nodes = _planeCreator.CreatePlane();
            var curCell = nodes.GetRandomItem();

            var countOfCells = config.NodesCount;
            var result = new List<NodeModel>(countOfCells);

            while (countOfCells > 0)
            {
                if (!result.Contains(curCell))
                {
                    result.Add(curCell);
                    countOfCells--;
                }

                curCell = curCell.GetRandomNeighbour();
            }

            foreach (var c in nodes)
            {
                if (!result.Contains(c))
                {
                    c.RemoveNeighbours();
                }
            }

            return result;
        }

        internal EnemyModel[] GenerateEnemies(int selectedLevel)
        {
            var config = GetConfig(selectedLevel);
            return config.Enemies;
        }
    }
}
