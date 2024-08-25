using Infrastructure.GameSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using VContainer;


namespace ConnectionLost
{
    internal sealed class RemoveOnReleaseDebugSceneLoader : MonoBehaviour
    {
        [Inject] private GameMachine gameMachine;

        private void Start()
        {
            if (gameMachine == null)
            {
                SceneManager.LoadScene("StartScene");
            }
        }
    }
}