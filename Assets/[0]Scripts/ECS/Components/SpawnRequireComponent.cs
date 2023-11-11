using UnityEngine;


namespace Game.ECS.Components
{
    internal struct SpawnRequireComponent
    {
        public string PrefabPath;
        public GameObject Prefab;        
        public Transform SpawnRoot;
        public Vector3 SpawnLocalPos;
    }
}
