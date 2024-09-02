using System;


namespace ConnectionLost
{
    [Serializable]
    internal sealed class NextTurnObserver
    {
        public event Action OnNextTurn;

        public void NextTurn()
        {
            OnNextTurn?.Invoke();
        }

        public void Reset()
        {
            OnNextTurn = null;
        }
    }
}
