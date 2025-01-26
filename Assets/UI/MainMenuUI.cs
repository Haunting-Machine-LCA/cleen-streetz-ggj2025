using System.Collections;
using System.Collections.Generic;
using Hmlca.Untitled;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    private UIDocument uiDoc;
    private Button playBtn, instructionsBtn, optionsBtn, quitBtn;
    private SceneTransition st;

    // Start is called before the first frame update
    void Start()
    {
        uiDoc = GetComponent<UIDocument>();
        st = GetComponent<SceneTransition>();

        VisualElement root = uiDoc.rootVisualElement;
        playBtn = root.Q("PlayBtn") as Button;
        instructionsBtn = root.Q("InstructionsBtn") as Button;
        optionsBtn = root.Q("OptionsBtn") as Button;
        quitBtn = root.Q("QuitBtn") as Button;
        
        playBtn.clicked += () => {PlayGame();};
    }

    public void PlayGame()
    {
        Debug.Log("Play");
        st.LoadScene("GGJ2025_Main");
    }
}
