using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Terminal : Interactable
{
    public Animator terminalAnimator;

    public Dialogue fixDialogue;

    public override InteractionType InteractionType
    {
        get
        {
            if (!GameStats.Instance.navigationSystem.isFixed)
                return InteractionType.Fix;

            return InteractionType.None;;
        }
    }

    public override string InteractionMessage
    {
        get
        {
            if (!GameStats.Instance.navigationSystem.isFixed)
                return "Fix Terminal";

            if (!GameStats.Instance.navigationSystem.isOn)
                return null;

            return null;
        }
    }

    public override bool CanInteract => (!GameStats.Instance.navigationSystem.isFixed);

    private void Update()
    {
        terminalAnimator.SetBool("broken", !GameStats.Instance.navigationSystem.isFixed);
        terminalAnimator.SetBool("on", GameStats.Instance.navigationSystem.isOn);
    }

    public override void Interact(PlayerInput player)
    {
        if (!GameStats.Instance.navigationSystem.isFixed)
        {
            GameStats.Instance.AvailableAction -= 2;
            StartCoroutine(FixTerminal(player));
        }
    }

    private IEnumerator FixTerminal(PlayerInput player)
    {
        player.isInteracting = true;

        player.isFixing = true;
        yield return new WaitForSeconds(3f);
        player.isFixing = false;

        player.isInteracting = false;

        DialogueManager.ActiveManager.StartConversation(fixDialogue, "Terminal");

        GameStats.Instance.navigationSystem.isFixed = true;
    }
}
