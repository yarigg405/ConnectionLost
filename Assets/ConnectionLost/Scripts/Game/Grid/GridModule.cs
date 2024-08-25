using Infrastructure.DI;
using UnityEngine;


namespace ConnectionLost
{
    internal sealed class GridModule : Module
    {
        [SerializeField, Listener, Service(typeof(GridSystem))]
        private GridSystem gridSystem = new();

        [SerializeField, Listener, Service(typeof(GridStatsFactory))]
        private GridStatsFactory gridStatsFactory = new();

        [SerializeField, Service(typeof(GridSpawner))]
        private GridSpawner gridSpawner = new();
    }
}