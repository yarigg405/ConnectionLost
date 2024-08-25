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
        [Inject] private readonly CellClickHandler _cellClickHandler;
        [Inject] private readonly EnemyClickHandler _enemyClickHandler;
        [Inject] private readonly BonusesClickHandler _bonusClickHandler;


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
                        _cellClickHandler.CellClicked(cell);
                    }

                    //else if(entita is Enemy enemy)
                    //{
                    //    _enemyClickHandler.EnemyClicked(enemy);
                    //}
                }
            }
        }
    }
}