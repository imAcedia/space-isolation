using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OxGenerator : Interactable
{
    public Animator oxGenAnimator;

    public Dialogue fixDialogue;
    public Dialogue turnOnDialogue;

    public override InteractionType InteractionType
    {
        get
        {
            if (!GameStats.Instance.oxygenGenerator.isFixed)
                return InteractionType.Fix;

            if (!GameStats.Instance.oxygenGenerator.isOn)
                return InteractionType.Use;

            return InteractionType.None;
        }
    }

    public override string InteractionMessage
    {
        get
        {
            if (!GameStats.Instance.oxygenGenerator.isFixed)
                return "Fix Generator";

            if (!GameStats.Instance.oxygenGenerator.isOn)
                return "Turn on Generator";

            return null;
        }
    }

    public override bool CanInteract => (!GameStats.Instance.oxygenGenerator.isOn) || (!GameStats.Instance.oxygenGenerator.isFixed);

    private void Update()
    {
        oxGenAnimator.SetBool("broken", !GameStats.Instance.oxygenGenerator.isFixed);
        oxGenAnimator.SetBool("on", GameStats.Instance.oxygenGenerator.isOn);
    }

    public override void Interact(PlayerInput player)
    {
        if (!GameStats.Instance.oxygenGenerator.isFixed)
        {
            StartCoroutine(FixGenerator(player));
        }
        else if (!GameStats.Instance.oxygenGenerator.isOn)
        {
            StartCoroutine(TurnOnGenerator(player));
        }
    }

    private IEnumerator FixGenerator(PlayerInput player)
    {
        player.isInteracting = true;
        player.isFixing = true;
        yield return new WaitForSeconds(3f);
        player.isFixing = false;
        player.isInteracting = false;

        DialogueManager.ActiveManager.StartConversation(fixDialogue, "Terminal");
        GameStats.Instance.oxygenGenerator.isFixed = true;
    }

    private IEnumerator TurnOnGenerator(PlayerInput player)
    {
        player.isInteracting = true;

        player.isUsing = true;
        yield return new WaitForSeconds(1f);
        player.isUsing = false;

        player.isInteracting = false;

        DialogueManager.ActiveManager.StartConversation(turnOnDialogue, "Terminal");

        GameStats.Instance.oxygenGenerator.isOn = true;
        GameStats.Instance.oxygenLevel += .3f;
    }
}
