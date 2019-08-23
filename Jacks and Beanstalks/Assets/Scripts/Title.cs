using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject titleImage;
    public GameObject titleStartMenu;
    public GameObject titleQuitMenu;

    public FadeController fadeController;

    Sprite on;

    Sprite off;

    Sprite start;
    Sprite startSelected;
    Sprite quit;
    Sprite quitSelected;

    private enum SelectState
    {
        Start, Quit
    };

    SelectState select = SelectState.Start;

    bool isOn = true;

    public AudioClip bgmClip;
    public AudioClip MenuMove;
    public AudioClip MenuSelect;

    void Start()
    {
        AudioManager.BGMStart(bgmClip);

        on = titleImage.GetComponent<SpriteRenderer>().sprite;

        off = Resources.Load<Sprite>("Images/Title_OFF");

        start = Resources.Load<Sprite>("Images/Title_Start");
        startSelected = Resources.Load<Sprite>("Images/Title_Start_Selected");

        quit = Resources.Load<Sprite>("Images/Title_Quit");
        quitSelected = Resources.Load<Sprite>("Images/Title_Quit_Selected");

        StartCoroutine(Blink());
    }

    void Update()
    {
        if (Input.GetButtonDown("Vertical"))
        {
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                if (select > SelectState.Start) select--;
                Debug.Log(select);
                AudioManager.AudioPlay(MenuMove);
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                if (select < SelectState.Quit) select++;
                Debug.Log(select);
                AudioManager.AudioPlay(MenuMove);
            }
        }

        if (Input.GetButtonDown("Submit"))
        {
            AudioManager.AudioPlay(MenuSelect);
            if(select == SelectState.Start)
            {
                fadeController.FadeOut(0.5f, () => {SceneManager.LoadScene("MainScene");});
            }
            else if(select == SelectState.Quit)
            {
                fadeController.FadeOut(0.5f,
                () =>
                {
                    #if UNITY_EDITOR
                        UnityEditor.EditorApplication.isPlaying = false;
                    #elif UNITY_WEBPLAYER
                        Application.OpenURL("http://google.com");
                    #else
                        Application.Quit();
                    #endif
                });
            }
        }

        switch(select)
        {
            case SelectState.Start:
                titleStartMenu.GetComponent<SpriteRenderer>().sprite = startSelected;
                titleQuitMenu.GetComponent<SpriteRenderer>().sprite = quit;
                break;
            case SelectState.Quit:
                titleStartMenu.GetComponent<SpriteRenderer>().sprite = start;
                titleQuitMenu.GetComponent<SpriteRenderer>().sprite = quitSelected;
                break;
        }
    }

    IEnumerator Blink()
    {
        if (isOn)
        {
            titleImage.GetComponent<SpriteRenderer>().sprite = off;
            isOn = false;
        }
        else
        {
            titleImage.GetComponent<SpriteRenderer>().sprite = on;
            isOn = true;
        }

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(Blink());
    }
}
