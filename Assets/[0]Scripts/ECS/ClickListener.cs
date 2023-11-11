using System;
using UnityEngine;


namespace Game.InputSystem
{
    internal sealed class ClickListener : MonoBehaviour, IClickListener
    {
        public event Action OnClicked;

        void IClickListener.HandleClick()
        {
            OnClicked?.Invoke();
        }
    }
}
