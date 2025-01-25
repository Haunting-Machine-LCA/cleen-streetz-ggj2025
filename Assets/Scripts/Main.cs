using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

namespace Hmlca.Untitled
{
    public class Main : Singleton<Main>
    {
        public UnityEvent OnGameStart = new UnityEvent();
        public UnityEvent OnGameLoad = new UnityEvent();


        private IEnumerator Start()
        {
            OnGameStart.Invoke();
            yield return new WaitUntil(() =>
            {
                return Input.GetKeyDown(KeyCode.Space);
            });
            OnGameLoad.Invoke();
        }


        public void DebugPrint(string message) => print(message);
    }
}
