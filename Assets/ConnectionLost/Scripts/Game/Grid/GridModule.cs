using Infrastructure.DI;
using UnityEngine;


namespace ConnectionLost
{
    internal sealed class GridModule : Module
    {
        [SerializeField, Listener, Service(typeof(GridStarter))]
        private GridStarter gridSystem = new();

        [SerializeField, Listener, Service(typeof(GridStatsFactory))]
        private GridStatsFactory gridStatsFactory = new();

        [SerializeField, Listener, Service(typeof(CellsSpawner))]
        private CellsSpawner gridSpawner = new();

        [SerializeField, Listener, Service(typeof(EnemySpawner))]
        private EnemySpawner enemySpawner = new();
    }
}