using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] Dialogue interactDialogue = null;

    private DialogueManager dialogueManager;
    private TimelineManager timelineManager;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        timelineManager = FindObjectOfType<TimelineManager>();
    }

    public void Interact(PlayerMovement player)
    {
        dialogueManager.StartConversation(interactDialogue, "Console");
        //timelineManager.PlayChainedCutScene();
    }
}
