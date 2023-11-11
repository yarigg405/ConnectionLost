using Game.ECS;
using Game.ECS.Components;
using Game.InputSystem;
using UnityEngine;


namespace Game
{
    internal sealed class EcsClickListener : EcsMonoObject
    {
        [SerializeField] private ClickListener clickListener;


        private void Start()
        {
            clickListener.OnClicked += HandleClick;
        }

        private void HandleClick()
        {
            if (World == null) return;

            var entity = PackerEntityUtils.UnpackEntity(World, Entity);
            var poolClickC = World.GetPool<ClickHandleComponent>();
            poolClickC.Add(entity);
        }
    }
}
