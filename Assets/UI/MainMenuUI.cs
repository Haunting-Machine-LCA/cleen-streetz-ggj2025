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
    private AudioManager am;

    // Start is called before the first frame update
    void Start()
    {
        uiDoc = GetComponent<UIDocument>();
        st = GetComponent<SceneTransition>();
        am = GameObject.Find("AudioManager").GetComponent<AudioManager>();

        VisualElement root = uiDoc.rootVisualElement;
        playBtn = root.Q("PlayBtn") as Button;
        instructionsBtn = root.Q("InstructionsBtn") as Button;
        optionsBtn = root.Q("OptionsBtn") as Button;
        quitBtn = root.Q("QuitBtn") as Button;

        foreach (Button btn in new Button[]{playBtn, instructionsBtn, optionsBtn, quitBtn})
        {
            btn.RegisterCallback<PointerOverEvent>(OnHover, TrickleDown.TrickleDown);
        }
        
        playBtn.clicked += () => {PlayGame();};
    }

    public void PlayGame()
    {
        Debug.Log("Play");
        am.PlaySelectSFX();
        am.ToGameMusic();
        st.LoadScene("GGJ2025_Main");
    }

    private void OnHover(PointerOverEvent evt)
    {
        am.PlayNavSFX();
    }
}
