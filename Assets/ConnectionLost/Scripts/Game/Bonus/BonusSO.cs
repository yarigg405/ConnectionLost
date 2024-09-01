using UnityEngine;


namespace ConnectionLost
{
    [CreateAssetMenu(fileName = "BonusSO", menuName = "ScriptableObjects/BonusSO", order = 51)]
    internal sealed class BonusSO : ScriptableObject
    {
        [field: SerializeField] public Sprite BonusIcon { get; private set; }

    }
}