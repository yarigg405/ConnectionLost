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

        [SerializeField, Listener, Service(typeof(GridSpawner))]
        private GridSpawner gridSpawner = new();
    }
}