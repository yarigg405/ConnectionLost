using System;


namespace Game.ECS.Configs
{
    [Serializable]
    internal sealed class SpawnConfig
    {
        public float OuterRadius { get => 1f; }
        public float InnerRadius { get => OuterRadius * 0.866025404f; }
    }
}
