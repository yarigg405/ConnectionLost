using System;
using UnityEngine;


namespace Game.ECS.Views
{
    [Serializable]
    internal enum EnemyType
    {
        None,
        Antivirus,
        Firewall,
        Healer,
        Suppressor,

        Core,
    }
}
