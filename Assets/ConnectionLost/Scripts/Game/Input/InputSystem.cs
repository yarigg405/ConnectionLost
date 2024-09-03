using ConnectionLost.Camera;
using Infrastructure.GameSystem;
using System;
using VContainer;
using UnityEngine;
using Yrr.Entitaz;
using UnityEngine.EventSystems;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class InputSystem : IUpdateListener
    {
        [Inject] private readonly CameraSystem _cameraSystem;
        [Inject] private readonly CellClickSystem _cellClickSystem;
        [Inject] private readonly EnemyBattleSystem _enemyBattleSystem;
        [Inject] private readonly BonusClickSystem _bonusClickSystem;

        private Action<Enemy> _customClickEnemy;

        void IUpdateListener.OnUpdate(float deltaTime)
        {
            if (Input.GetMouseButtonDown(0))
            {
                HandleInput();
            }
        }

        private void HandleInput()
        {
            if (EventSystem.current.IsPointerOverGameObject()) return;

            Ray inputRay = _cameraSystem.MainCamera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(inputRay, out var hit))
            {
                if (hit.collider.TryGetComponent<IEntita>(out var entita))
                {
                    if (entita is Cell cell)
                    {
                        _cellClickSystem.CellClicked(cell);                        
                    }

                    else if (entita is Enemy enemy)
                    {
                        if (_customClickEnemy != null)
                        {
                            _customClickEnemy.Invoke(enemy);
                            _customClickEnemy = null;
                        }

                        else
                        {
                            _enemyBattleSystem.Attack(enemy);
                        }
                    }

                    else if (entita is Bonus bonus)
                    {
                        _bonusClickSystem.ClickOnBonus(bonus);
                    }
                }
            }
        }

        internal void OverrideEnemyClick(Action<Enemy> customClick)
        {
            _customClickEnemy = customClick;
        }
    }
}