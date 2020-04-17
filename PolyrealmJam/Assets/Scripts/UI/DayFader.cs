using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class DayFader : MonoBehaviour
{
    public static DayFader instance;

    private void Awake()
    {
        instance = this;
    }

    public float fadeSpeed;

    [SerializeField] CanvasGroup faderGroup;
    [SerializeField] Text dayText;
    [SerializeField] Text resultText;


    public bool fading = false;

    public void SetMainMenu(bool active)
    {

    }
    public void SetGameGUI(bool active)
    {

    }
    
    private IEnumerator Start()
    {
        SceneManager.LoadScene(1, LoadSceneMode.Additive);

        fading = true;

        while (faderGroup.alpha > 0f)
        {
            faderGroup.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }

        fading = false;
    }

    public void FadeOutToBlack(string dayString, string resultString)
    {
        StartCoroutine(FadeOutToBlackCo(dayString, resultString));
    }

    public void FadeOutToBlackEnd(string dayString, string resultString)
    {
        StartCoroutine(FadeOutToBlackEndCo(dayString, resultString));
    }

    private IEnumerator FadeOutToBlackCo(string dayString, string resultString)
    {
        yield return new WaitForSeconds(3f);
        fading = true;

        dayText.text = dayString;
        resultText.text = resultString;

        while (faderGroup.alpha < 1f)
        {
            faderGroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(3f);

        while (faderGroup.alpha > 0f)
        {
            faderGroup.alpha -= fadeSpeed * Time.deltaTime;
            yield return null;
        }

        fading = false;
    }
    private IEnumerator FadeOutToBlackEndCo(string dayString, string resultString)
    {
        yield return new WaitForSeconds(3f);
        fading = true;

        dayText.text = dayString;
        resultText.text = resultString;

        while (faderGroup.alpha < 1f)
        {
            faderGroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }

        yield return new WaitForSeconds(5f);
        Application.Quit();
    }
}
