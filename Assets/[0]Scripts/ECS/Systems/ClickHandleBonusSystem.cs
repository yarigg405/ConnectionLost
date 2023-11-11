using Game.ECS.Commands;
using Game.ECS.Components;
using Game.ECS.Views;
using Leopotam.EcsLite;
using Leopotam.EcsLite.Di;
using System;
using System.Collections.Generic;


namespace Game.ECS.Systems
{
    internal readonly struct ClickHandleBonusSystem : IEcsRunSystem
    {        
        void IEcsRunSystem.Run(IEcsSystems systems)
        {
        }
    }
}
