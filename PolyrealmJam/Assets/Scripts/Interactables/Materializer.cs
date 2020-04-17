using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Materializer : Interactable
{
    [SerializeField] Animator materializerAnimator;

    public Dialogue fixDialogue;
    public Dialogue makeCellDialogue;
    public Dialogue makeFoodDialogue;

    public override InteractionType InteractionType
    {
        get
        {
            if (!GameStats.Instance.materializer.isFixed)
                return InteractionType.Fix;

            if (GameStats.Instance.powerLevel > .05f)
                return InteractionType.Use;

            return InteractionType.None;
        }
    }

    public override string InteractionMessage
    {
        get
        {
            if (!GameStats.Instance.materializer.isFixed)
                return "Fix Materializer";

            if (GameStats.Instance.powerLevel > .05f)
            {
                if (GameStats.Instance.haveEaten)
                    return "Print Power Cell";

                else
                    return "Print Food";
            }

            return null;
        }
    }

    public override bool CanInteract => (!GameStats.Instance.materializer.isFixed) || (GameStats.Instance.powerLevel > .05f);

    private void Update()
    {
        materializerAnimator.SetBool("broken", !GameStats.Instance.engine.isFixed);
    }

    public override void Interact(PlayerInput player)
    {
        if (!GameStats.Instance.engine.isFixed)
        {
            StartCoroutine(FixEngine(player));
        }
        else if (GameStats.Instance.powerLevel > 0.05f)
        {
            if (GameStats.Instance.haveEaten)
                StartCoroutine(MakePowerCell(player));

            else
                StartCoroutine(MakeFood(player));
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

    private IEnumerator MakePowerCell(PlayerInput player)
    {
        player.isInteracting = true;

        player.isUsing = true;
        yield return new WaitForSeconds(1f);
        player.isUsing = false;

        materializerAnimator.SetBool("on", true);
        yield return new WaitForSeconds(3f);
        materializerAnimator.SetBool("on", false);

        player.isInteracting = false;

        DialogueManager.ActiveManager.StartConversation(makeCellDialogue, "Terminal");

        GameStats.Instance.powerCell++;
        GameStats.Instance.powerLevel -= .05f;
    }

    private IEnumerator MakeFood(PlayerInput player)
    {
        player.isInteracting = true;

        player.isUsing = true;
        yield return new WaitForSeconds(1f);
        player.isUsing = false;

        materializerAnimator.SetBool("on", true);
        yield return new WaitForSeconds(3f);
        materializerAnimator.SetBool("on", false);

        player.isInteracting = false;

        DialogueManager.ActiveManager.StartConversation(makeFoodDialogue, "Terminal");

        GameStats.Instance.haveEaten = true;
        GameStats.Instance.powerLevel -= .05f;
    }
}
