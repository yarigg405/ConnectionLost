using Game.GridGeneration.Models;
using UnityEngine;


namespace Game.GridGeneration.Grid
{
    [CreateAssetMenu(fileName = "GridConfig", menuName = "Grid/GridConfig", order = 51)]
    internal sealed class GridConfig : ScriptableObject
    {
        [field: SerializeField] public int NodesCount { get; private set; }
        [field: SerializeField] public EnemyModel[] Enemies { get; private set; }


        private void OnValidate()
        {
            if (Enemies.Length > NodesCount * 0.6f)
                Debug.LogError("ERROR! Check nodes count and enemies count!");
        }
    }
}
