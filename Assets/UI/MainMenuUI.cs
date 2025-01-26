using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Hmlca.Untitled;
using UnityEngine;
using UnityEngine.UIElements;

public class MainMenuUI : MonoBehaviour
{
    private UIDocument uiDoc;
    private Button playBtn, instructionsBtn, optionsBtn, quitBtn;
    private SceneTransition st;
    private AudioManager am;
    private VisualElement instructionsScrn, optionsScrn;
    private Button backBtn1, backBtn2;
    private VisualElement backgroundImg, colorOverlay;

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
        instructionsScrn = root.Q("Instructions");
        optionsScrn = root.Q("Options");
        backBtn1 = root.Q("BackBtn1") as Button;
        backBtn2 = root.Q("BackBtn2") as Button;
        backgroundImg = root.Q("BackgroundImg");
        colorOverlay = root.Q("ColorOverlay");

        StartCoroutine(BGLoopMove());

        foreach (Button btn in new Button[]{playBtn, instructionsBtn, optionsBtn, quitBtn, backBtn1, backBtn2})
        {
            btn.RegisterCallback<PointerOverEvent>(OnHover, TrickleDown.TrickleDown);
        }
        
        playBtn.clicked += () => {PlayGame();};
        instructionsBtn.clicked += () => {ViewScreen(instructionsScrn);};
        optionsBtn.clicked += () => {ViewScreen(optionsScrn);};
        quitBtn.clicked += () => {QuitGame();};
        backBtn1.clicked += () => {CloseScreen(instructionsScrn);};
        backBtn2.clicked += () => {CloseScreen(optionsScrn);};
    }

    private void PlayGame()
    {
        Debug.Log("Play");
        am.PlaySelectSFX();
        am.ToGameMusic();
        st.LoadScene("GGJ2025_Main");
    }

    private void ViewScreen(VisualElement ve)
    {
        am.PlaySelectSFX();
        am.Deafen();
        ve.style.display = DisplayStyle.Flex;
    }

    private void CloseScreen(VisualElement ve)
    {
        am.PlaySelectSFX();
        am.Undeafen();
        ve.style.display = DisplayStyle.None;
    }

    private void QuitGame()
    {
        Application.Quit();
    }

    private void OnHover(PointerOverEvent evt)
    {
        am.PlayNavSFX();
    }

    private IEnumerator BGLoopMove()
    {
        while (true)
        {
            Vector3 oldPos = backgroundImg.transform.position;
            Vector3 newPos = new Vector3(Random.Range(-30, 30), Random.Range(-30, 30), 0);
            yield return MoveElement(backgroundImg, oldPos, newPos, Random.Range(0, 10));
            yield return MoveElement(backgroundImg, newPos, oldPos, Random.Range(0, 10));
        }
    }

    private IEnumerator MoveElement(VisualElement ve, Vector3 start, Vector3 end, float duration)
    {
        float time = 0f;
        while (time < duration)
        {
            time += Time.deltaTime;
            float step = time / duration;
            Translate newTranslate = new Translate(Mathf.Lerp(start.x, end.x, step), Mathf.Lerp(start.y, end.y, step), Mathf.Lerp(start.z, end.z, step));
            ve.style.translate = new StyleTranslate(newTranslate);
            yield return null;
        }
    }
}
