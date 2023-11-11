using UnityEngine;


namespace Game.GridGeneration.Grid
{
    internal sealed class GridConfigsStorage : MonoBehaviour
    {
        [SerializeField] private GridConfig[] configs;

        internal GridConfig GetConfig(int levelNum)
        {
            if (configs.Length > levelNum)
            {
                return configs[levelNum];
            }

            Debug.LogError($"Requested {levelNum}/{configs.Length} config");
            return configs[0];
        }
    }
}
