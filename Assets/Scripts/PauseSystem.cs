using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Hmlca.Untitled
{
    public class PauseSystem : MonoBehaviour
    {
        public UnityEvent<bool> OnPause = new UnityEvent<bool>();
        private const KeyCode PAUSE = KeyCode.Escape;
        private bool isPaused = false;
        private float pauseHeldSeconds = 0f;
        private float pauseHeldSecondsToClose = 1f;



        private void Update()
        {
            if (Input.GetKeyDown(PAUSE))
            {
                isPaused = !isPaused;
                Time.timeScale = isPaused ? 0 : 1;
                BattleSystem.GetSingleton()
                    .GoToState(isPaused
                        ? BattleSystem.BattleState.PAUSED
                        : BattleSystem.BattleState.COMMAND
                    );
            }
            if (Input.GetKey(PAUSE))
            {
                pauseHeldSeconds += Time.unscaledDeltaTime;
                if (pauseHeldSeconds >= pauseHeldSecondsToClose)
#if UNITY_EDITOR
                    UnityEditor.EditorApplication.isPlaying = false;
#else
                    Application.Quit();
#endif
            }
            else
            {
                pauseHeldSeconds = 0f;
            }
        }
    }
}
