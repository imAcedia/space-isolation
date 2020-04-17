using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerEngine : Interactable
{
    [SerializeField] Animator engineAnimator;
    [SerializeField] Animator lightsAnimator;

    public Dialogue fixDialogue;
    public Dialogue addCellDialogue;

    public override InteractionType InteractionType
    {
        get
        {
            if (!GameStats.Instance.engine.isFixed)
                return InteractionType.Fix;

            if (!GameStats.Instance.engine.isOn)
                return InteractionType.Use;

            return InteractionType.None;
        }
    }

    public override string InteractionMessage
    {
        get
        {
            if (!GameStats.Instance.engine.isFixed)
                return "Fix Engine";

            if (GameStats.Instance.powerCell > 0)
                return "Add Power Cell";

            return null;
        }
    }

    public override bool CanInteract => (!GameStats.Instance.engine.isFixed) || (GameStats.Instance.powerCell > 0);

    private void Update()
    {
        engineAnimator.SetBool("broken", !GameStats.Instance.engine.isFixed);
        engineAnimator.SetBool("on", GameStats.Instance.engine.isOn);

        lightsAnimator.SetBool("broken", !GameStats.Instance.engine.isFixed);
        lightsAnimator.SetBool("on", GameStats.Instance.engine.isOn);
    }

    public override void Interact(PlayerInput player)
    {
        if (!GameStats.Instance.engine.isFixed)
        {
            StartCoroutine(FixEngine(player));
        }
        else if (GameStats.Instance.powerCell > 0)
        {
            StartCoroutine(AddPowerCell(player));
        }
    }

    private IEnumerator FixEngine(PlayerInput player)
    {
        player.isInteracting = true;
        player.isFixing = true;
        yield return new WaitForSeconds(3f);
        player.isFixing = false;
        player.isInteracting = false;

        DialogueManager.ActiveManager.StartConversation(fixDialogue, "Terminal");
        GameStats.Instance.engine.isFixed = true;
    }

    private IEnumerator AddPowerCell(PlayerInput player)
    {
        player.isInteracting = true;

        player.isUsing = true;
        yield return new WaitForSeconds(1f);
        player.isUsing = false;

        player.isInteracting = false;

        DialogueManager.ActiveManager.StartConversation(addCellDialogue, "Terminal");

        GameStats.Instance.powerCell--;
        GameStats.Instance.powerLevel += .2f;
    }
}
