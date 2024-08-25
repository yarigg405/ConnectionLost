using System;
using VContainer;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class GridStatsFactory
    {
        [Inject] private readonly GameBalanceSettings settings;

        internal GridStats BuildGridStats(GridDifficult difficult)
        {
            var width = 5;
            var height = 10;
            var cellsCount = settings.CellsCountForGridDifficult * ((int)difficult + 1);

            var stats = new GridStats
            {
                Width = width,
                Height = height,
                CellsCount = cellsCount,
                Difficult = difficult,
            };

            return stats;
        }
    }
}
