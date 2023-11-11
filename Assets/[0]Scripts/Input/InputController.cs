using UnityEngine;
using UnityEngine.EventSystems;


namespace Game.InputSystem
{
    internal sealed class InputController : MonoBehaviour
    {
        private Camera _camera;

        private void Awake()
        {
            _camera = Camera.main;
        }


        private void Update()
        {
            if (Input.GetMouseButtonDown(0))
            {
                if (EventSystem.current.IsPointerOverGameObject()) return;

                HandleInput();
            }
        }


        private void HandleInput()
        {
            Ray inputRay = _camera.ScreenPointToRay(Input.mousePosition);
            if (Physics.Raycast(inputRay, out var hit))
            {
                if (hit.collider.TryGetComponent<IClickListener>(out var clicked))
                {
                    clicked.HandleClick();
                }
            }
        }
    }
}
