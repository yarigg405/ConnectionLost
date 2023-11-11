using Leopotam.EcsLite;
using System.ComponentModel;
using UnityEngine;


namespace Game.ECS
{
    internal abstract class EcsMonoObject : MonoBehaviour
    {
        public EcsPackedEntity Entity { get; private set; }
        protected EcsWorld World;

        public void Init(EcsWorld world)
        {
            World = world;
        }

        public void PackEntity(int entity)
        {
            Entity = World.PackEntity(entity);
        }        
    }
}
