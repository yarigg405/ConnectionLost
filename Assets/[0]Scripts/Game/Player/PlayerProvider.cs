using UnityEngine;


namespace Game.Player
{
    internal sealed class PlayerProvider : MonoBehaviour
    {
        public PlayerProgress PlayerProgress => _progress;
        private PlayerProgress _progress = new();

        public PlayerStats PlayerStats => _stats;
        private PlayerStats _stats=new();


    }
}
