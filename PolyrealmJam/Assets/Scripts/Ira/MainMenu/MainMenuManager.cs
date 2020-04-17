using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuManager : MonoBehaviour
{

    [SerializeField] Animator aboutAnimator;
    [SerializeField] GameObject mainMenu;

    //starting game
    public void GameStart()
    {
        mainMenu.SetActive(false);
    }

    //Open the about section
    public void GameOpenAbout()
    {
        //do animation to show "gameabout"
        aboutAnimator.SetBool("isOpen" ,true);
    }

    //Close the about section
    public void GameCloseAbout()
    {
        //do animation to show "gameabout"
        aboutAnimator.SetBool("isOpen", false);
    }

    //quit game
    public void GameQuit()
    {
        Application.Quit();
    }
}
