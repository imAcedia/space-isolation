using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NavSystem : Interactable
{
    public Animator cockpitAnimator;

    public Dialogue turnOnDialogue;

    public override InteractionType InteractionType
    {
        get
        {
            if (!GameStats.Instance.navigationSystem.isFixed)
                return InteractionType.None;

            if (!GameStats.Instance.navigationSystem.isOn)
                return InteractionType.Use;

            return InteractionType.None;
        }
    }

    public override string InteractionMessage
    {
        get
        {
            if (!GameStats.Instance.navigationSystem.isFixed)
                return null;

            if (!GameStats.Instance.navigationSystem.isOn)
                return "Turn on Navigation";

            return null;
        }
    }

    public override bool CanInteract => (!GameStats.Instance.navigationSystem.isOn) && (GameStats.Instance.navigationSystem.isFixed);

    private void Update()
    {
        cockpitAnimator.SetBool("broken", !GameStats.Instance.navigationSystem.isFixed);
        cockpitAnimator.SetBool("on", GameStats.Instance.navigationSystem.isOn);
    }

    public override void Interact(PlayerInput player)
    {
        if (!GameStats.Instance.navigationSystem.isFixed)
        {

        }
        else if (!GameStats.Instance.navigationSystem.isOn)
        {
            StartCoroutine(TurnOnNavigation(player));
        }
    }

    private IEnumerator TurnOnNavigation(PlayerInput player)
    {
        player.isInteracting = true;

        player.isUsing = true;
        yield return new WaitForSeconds(1f);
        player.isUsing = false;

        player.isInteracting = false;

        DialogueManager.ActiveManager.StartConversation(turnOnDialogue, "Terminal");

        GameStats.Instance.navigationSystem.isOn = true;
    }
}
