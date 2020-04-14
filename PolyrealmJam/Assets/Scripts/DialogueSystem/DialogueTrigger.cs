using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogueTrigger : MonoBehaviour
{    
    public Dialogue dialogue;
    private DialogueManager dManager;

    private void Awake()
    {
        dManager = FindObjectOfType<DialogueManager>();
    }

    private void OnMouseDown()
    {
        OnTriggerDialogue();
    }

    public void OnTriggerDialogue()
    {
        dManager.StartConversation(dialogue, gameObject.name);
    }
}
