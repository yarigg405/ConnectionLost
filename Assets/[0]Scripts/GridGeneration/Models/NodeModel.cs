using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Yrr.Utils;


namespace Game.GridGeneration.Models
{
    internal sealed class NodeModel
    {
        private readonly NodeModel[] _neighbours = new NodeModel[6];
        public Vector2Int Coordinates { get; set; }


        public IEnumerable<NodeModel> GetNeighboursList()
        {
            return _neighbours.Where(x => x != null);
        }

        public NodeModel GetRandomNeighbour()
        {
            return _neighbours.Where(x => x != null).ToList().GetRandomItem();
        }

        public void SetNeighbour(HexDirection direction, NodeModel cell)
        {
            if (cell == null) return;

            _neighbours[(int)direction] = cell;
            cell._neighbours[(int)direction.Opposite()] = this;
        }

        public void RemoveNeighbours()
        {
            for (var i = 0; i < _neighbours.Length; i++)
            {
                if (_neighbours[i] == null) continue;

                var direction = (HexDirection)i;
                _neighbours[i]._neighbours[(int)direction.Opposite()] = null;
                _neighbours[i] = null;
            }
        }
    }
}
