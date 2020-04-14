using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    public string interactableTag = "Interactable";
    public LayerMask interactableLayer;

    [Space]
    [SerializeField] Transform interactionPoint = null;
    [SerializeField] float interactionRadius = .2f;

    private PlayerMovement movement;
    private DialogueManager dialogueManager;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.OnDialogueStart += movement.DisableMovement;
        dialogueManager.OnDialogueEnd += movement.EnableMovement;
    }

    private void OnDestroy()
    {
        dialogueManager.OnDialogueStart -= movement.DisableMovement;
        dialogueManager.OnDialogueEnd -= movement.EnableMovement;
    }

    private void Update()
    {
        HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        if (!movement.enabled) return;

        movement.moveInput = 0f;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            movement.moveInput += 1f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            movement.moveInput -= 1f;
    }

    private void CheckInteraction()
    {
        List<Collider2D> overlapColls = new List<Collider2D>(Physics2D.OverlapPointAll(interactionPoint.position, interactableLayer));

        if (overlapColls.Count <= 0)
        {
            return;
        }

        for (int i = overlapColls.Count - 1; i >= 0; i--)
        {
            if (overlapColls[i].GetComponent<Interactable>() == null)
                overlapColls.RemoveAt(i);
        }

        Interactable interactable = overlapColls[0].GetComponent<Interactable>();

        if (Input.GetKeyDown(KeyCode.E))
        {
            interactable.Interact(movement);
            Debug.Log("Interacting with " + interactable.name);
        }
    }
}
