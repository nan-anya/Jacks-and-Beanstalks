using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Title : MonoBehaviour
{
    public GameObject titleImaga;

    Sprite on;

    Sprite off;

    bool isOn = true;

    public AudioClip bgmClip;

    void Start()
    {
        AudioManager.BGMStart(bgmClip);

        on = titleImaga.GetComponent<SpriteRenderer>().sprite;

        off = Resources.Load<Sprite>("Images/Title_OFF");

        StartCoroutine(Blink());
    }

    void Update()
    {
        if (Input.anyKey)
        {
            SceneManager.LoadScene("MainScene");
        }
    }

    IEnumerator Blink()
    {
        if (isOn)
        {
            titleImaga.GetComponent<SpriteRenderer>().sprite = off;

            isOn = false;
        }
        else
        {
            titleImaga.GetComponent<SpriteRenderer>().sprite = on;

            isOn = true;
        }

        yield return new WaitForSeconds(1.0f);

        StartCoroutine(Blink());
    }
}
