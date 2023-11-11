using Game.ECS.Views;
using UnityEngine;


namespace Game.GridGeneration.Models
{
    [CreateAssetMenu(fileName = "EnemyModel", menuName = "Grid/EnemyModel", order = 51)]
    internal sealed class EnemyModel : ScriptableObject
    {
        [field: SerializeField] public EnemyType EnemyType { get; private set; }
        [field: SerializeField] public float DefaultHealth { get; private set; }
        [field: SerializeField] public float DefaultDamage { get; private set; }
        [field: SerializeField] public EnemyView ViewPrefab { get; private set; }
        [field: SerializeField] public bool IsBlocker { get; private set; } = true;
    }
}
