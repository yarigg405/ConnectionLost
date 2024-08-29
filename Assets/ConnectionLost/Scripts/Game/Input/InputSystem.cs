using ConnectionLost.Camera;
using Infrastructure.GameSystem;
using System;
using VContainer;
using UnityEngine;
using Yrr.Entitaz;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class InputSystem : IUpdateListener
    {
        [Inject] private readonly CameraSystem _cameraSystem;
        [Inject] private readonly CellClickSystem _cellClickSystem;
        [Inject] private readonly EnemyStorage _enemyClickHandler;
        [Inject] private readonly BonusSystem _bonusClickHandler;


        void IUpdateListener.OnUpdate(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleInput();
            }
        }

        private void HandleInput()
        {
            Ray inputRay = _cameraSystem.MainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(inputRay, out var hit))
            {
                if (hit.collider.TryGetComponent<IEntita>(out var entita))
                {
                    if (entita is Cell cell)
                    {
                        _cellClickSystem.CellClicked(cell);
                    }
                }
            }
        }
    }
}