using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FadeController : MonoBehaviour
{
    public Image image;

    void Start()
    {
        Color tempColor = image.color;
        tempColor.a = 0f;
        image.color = tempColor;
    }

    public void FadeIn(float fadeTime, System.Action nextEvent = null)
    {
        StartCoroutine(CoFadeIn(fadeTime, nextEvent));
    }

    public void FadeOut(float fadeTime, System.Action nextEvent = null)
    {
        StartCoroutine(CoFadeOut(fadeTime, nextEvent));
    }

    IEnumerator CoFadeIn(float fadeTime, System.Action nextEvent = null)
    {
        Color tempColor = image.color;
        while(image.color.a > 0f)
        {
            tempColor.a -= Time.deltaTime / fadeTime;
            image.color = tempColor;

            yield return null;
        }

        tempColor.a = 0f;
        image.color = tempColor;

        if (nextEvent != null) nextEvent();
    }

    IEnumerator CoFadeOut(float fadeTime, System.Action nextEvent = null)
    {
        Color tempColor = image.color;
        while(tempColor.a < 1f)
        {
            tempColor.a += Time.deltaTime / fadeTime;
            image.color = tempColor;
            yield return new WaitForFixedUpdate();
        }
        tempColor.a = 1f;
        image.color = tempColor;

        if (nextEvent != null) nextEvent();
    }
}
