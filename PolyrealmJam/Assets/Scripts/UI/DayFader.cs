using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Text = TMPro.TextMeshProUGUI;

public class DayFader : MonoBehaviour
{
    public float fadeSpeed;

    [SerializeField] CanvasGroup faderGroup;
    [SerializeField] Text dayText;
    [SerializeField] Text resultText;

    public void SetMainMenu(bool active)
    {

    }
    public void SetGameGUI(bool active)
    {

    }

    public void FadeOutToBlack(string dayString, string resultString)
    {
        if (dayString != null)
        {
            
        }
    }

    private IEnumerator FadeOutToBlackCo(string dayString, string resultString)
    {
        dayText.text = dayString;
        resultText.text = resultString;

        while (faderGroup.alpha < 1f)
        {
            faderGroup.alpha += fadeSpeed * Time.deltaTime;
            yield return null;
        }
    }
}
