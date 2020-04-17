using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactables : MonoBehaviour
{
    [SerializeField] Dialogue interactDialogue = null;

    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void Interact(PlayerMovement player)
    {
        dialogueManager.StartConversation(interactDialogue, "Console");
    }
}
