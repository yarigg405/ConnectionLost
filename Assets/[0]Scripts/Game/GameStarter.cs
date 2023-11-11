using Game.ECS;
using Game.ECS.Commands;
using UnityEngine;
using VContainer;


namespace Game
{
    internal sealed class GameStarter : EcsMonoObject
    {
        [Inject] private readonly GridsSelector gridSelector;


        private void Update()
        {
            if (Input.GetKeyDown(KeyCode.F4))
            {
                StartGrid();
            }
        }

        internal void StartGrid()
        {
            CommandToGenerateGrid();
            gridSelector.NextGrid();
        }

        private void CommandToGenerateGrid()
        {
            var en = World.NewEntity();
            var poolCommands = World.GetPool<GenerateGridCommand>();
            poolCommands.Add(en);
        }
    }
}
