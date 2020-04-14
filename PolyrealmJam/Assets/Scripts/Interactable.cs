using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] Dialogue interactDialogue = null;

    public DialogueManager dialogueManager { get; private set; }

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    public void Interact(PlayerMovement player)
    {
        dialogueManager.StartConversation(interactDialogue, "Console");
    }
}
