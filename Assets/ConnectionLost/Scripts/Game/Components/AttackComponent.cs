using UnityEngine;
using Yrr.Entitaz;


namespace ConnectionLost
{
    internal sealed class AttackComponent : MonoBehaviour, IEntitazComponent
    {
        [field: SerializeField] public int AttackDamage;
    }
}