using UnityEngine;
using UnityEngine.UI;
using TMPro;
using VContainer;


namespace ConnectionLost
{
    internal sealed class SuppressPlayerAttackComponent : MonoBehaviour, IStartableComponent
    {
        [Inject] private readonly PlayerStats _playerStats;


        void IStartableComponent.StartComponent()
        {
            _playerStats.AddAttackDebuff();
        }

        void IStartableComponent.StopComponent()
        {
            _playerStats.RemoveAttackDebuff();
        }
    }
}