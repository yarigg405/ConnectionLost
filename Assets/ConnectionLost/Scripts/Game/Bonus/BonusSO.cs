using Sirenix.OdinInspector;
using System;
using UnityEngine;


namespace ConnectionLost
{
    [CreateAssetMenu(fileName = "BonusSO", menuName = "ScriptableObjects/BonusSO", order = 51)]
    internal sealed class BonusSO : SerializedScriptableObject
    {
        [field: SerializeField] public Sprite BonusIcon { get; private set; }
        [field: SerializeField] public BonusLogic BonusLogic { get; private set; }
    }
}