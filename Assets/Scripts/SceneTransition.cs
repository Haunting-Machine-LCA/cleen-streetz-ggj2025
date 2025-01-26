using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

namespace Hmlca.Untitled
{
    public class SceneTransition : MonoBehaviour
    {
        private UIDocument uiDoc;
        private VisualElement fadeScreen;
        // Start is called before the first frame update
        void Start()
        {
            uiDoc = GetComponent<UIDocument>();

            VisualElement root = uiDoc.rootVisualElement;

            // Black fade screen
            fadeScreen = new VisualElement();
            fadeScreen.style.backgroundColor = new Color(0, 0, 0, 0);
            fadeScreen.style.position = Position.Absolute;
            fadeScreen.style.width = Length.Percent(100);
            fadeScreen.style.height = Length.Percent(100);
            fadeScreen.style.display = DisplayStyle.None;
            root.Add(fadeScreen);
        }

        public void LoadScene(string sceneName)
        {
            StartCoroutine(FadeAndLoad(sceneName));
        }

        private IEnumerator FadeAndLoad(string sceneName)
        {
            float duration = 1f;

            // Fade out music

            // Fade out view
            fadeScreen.style.display = DisplayStyle.Flex;
            for (float t=0; t < duration; t+=Time.deltaTime)
            {
                fadeScreen.style.backgroundColor = new Color(0, 0, 0, t / duration);
                yield return null;
            }

            SceneManager.LoadScene(sceneName);
        }
    }
}
