using Game.GridGeneration.Models;
using System.Collections.Generic;
using UnityEngine;


namespace Game.GridGeneration.Grid
{
    internal sealed class PlaneCreator
    {
        internal IEnumerable<NodeModel> CreatePlane()
        {
            var cells = new NodeModel[GameConfig.GamefieldSizeX * GameConfig.GamefieldSizeY];

            for (int z = 0, i = 0; z < GameConfig.GamefieldSizeY; z++)
            {
                for (var x = 0; x < GameConfig.GamefieldSizeX; x++)
                {
                    var cell = cells[i] = CreateNode(x, z);

                    if (x > 0)
                    {
                        cell.SetNeighbour(HexDirection.West, cells[i - 1]);
                    }

                    if (z > 0)
                    {
                        if ((z & 1) == 0)
                        {
                            cell.SetNeighbour(HexDirection.SouthEast, cells[i - GameConfig.GamefieldSizeX]);
                            if (x > 0)
                            {
                                cell.SetNeighbour(HexDirection.SouthWest, cells[i - GameConfig.GamefieldSizeX - 1]);
                            }
                        }

                        else
                        {
                            cell.SetNeighbour(HexDirection.SouthWest, cells[i - GameConfig.GamefieldSizeX]);
                            if (x < GameConfig.GamefieldSizeX - 1)
                            {
                                cell.SetNeighbour(HexDirection.SouthEast, cells[i - GameConfig.GamefieldSizeX + 1]);
                            }
                        }
                    }

                    i++;
                }
            }

            return cells;
        }


        private static NodeModel CreateNode(int x, int z)
        {
            var cell = new NodeModel
            {
                Coordinates = new Vector2Int(x, z),
            };

            return cell;
        }
    }
}
