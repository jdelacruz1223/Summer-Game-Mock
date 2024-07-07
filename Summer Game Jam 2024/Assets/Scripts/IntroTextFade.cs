using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class IntroTextFade : MonoBehaviour
{
    public TextMeshProUGUI welcomeSignTxt;
    public TextMeshProUGUI townTxt;
    public float fadeInDuration = 2.0f;
    public float fadeOutDuration = 2.0f;
    public float displayDuration = 2.0f;

    private void Start()
    {
        StartCoroutine(FadeInAndOut());
    }

    private IEnumerator FadeInAndOut()
    {
        welcomeSignTxt.canvasRenderer.SetAlpha(0.0f);
        townTxt.canvasRenderer.SetAlpha(0.0f);

        welcomeSignTxt.CrossFadeAlpha(1.0f, fadeInDuration, false);
        townTxt.CrossFadeAlpha(1.0f, fadeInDuration + 1, false);

        yield return new WaitForSeconds(fadeInDuration + displayDuration + 1);

        welcomeSignTxt.CrossFadeAlpha(0.0f, fadeOutDuration, false);
        townTxt.CrossFadeAlpha(0.0f, fadeOutDuration, false);

    }
}
