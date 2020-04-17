using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PauseManager : MonoBehaviour
{
    [Header("Pop Up Animator")]
    [SerializeField] Animator pausAnimator;
    [SerializeField] Animator popUpAnimator;

    [Header("Pop Up Message")]
    [SerializeField] TextMeshProUGUI warningTxt;

    string warningMsg = "Warning!!! this action will cause you to lose all of your progress, do you want to proceed ? " ;
    string popUpType;

    public void ResumeGame()
    {
        pausAnimator.SetBool("isPause", false);
    }

    public void RestartGame()
    {
        OpenPopUpMsg(warningTxt, warningMsg, "Restart");
    }

    public void QuitGame()
    {
        OpenPopUpMsg(warningTxt, warningMsg, "quit");
    }

    public void OpenPopUpMsg(TextMeshProUGUI warnText, string msg, string type)
    {
        popUpType = type;
        warnText.text = msg;
        popUpAnimator.SetBool("isOpen",true);
    }

    public void AvoidWarning()
    {
        if(popUpType == null)
            return;
        if(popUpType == "Restart")
            Debug.Log("Restarting game");
        else
            Debug.Log("Quiting To main menu");
    }

    public void ClosePopUpMsg()
    {
        popUpAnimator.SetBool("isOpen", false);
    }
}