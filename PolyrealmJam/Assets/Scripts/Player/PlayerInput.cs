using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Text = TMPro.TextMeshProUGUI;

[RequireComponent(typeof(PlayerMovement))]
public class PlayerInput : MonoBehaviour
{
    public delegate void InteractionEvent(Interactable interactable);
    public event InteractionEvent OnInteract;

    public string interactableTag = "Interactable";
    public LayerMask interactableLayer;

    [Space]
    [SerializeField] Transform interactionPoint = null;
    [SerializeField] float interactionRadius = .2f;

    private PlayerMovement movement;
    private DialogueManager dialogueManager;

    private bool handlingDialogue = false;

    public bool IsClimbing => movement.IsClimbing;

    public bool isFixing = false;
    public bool isUsing = false;

    public bool isInteracting = false;

    private void StartDialogue() => handlingDialogue = true;
    private void EndDialogue() => handlingDialogue = false;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
        dialogueManager.OnDialogueStart += StartDialogue;
        dialogueManager.OnDialogueEnd += EndDialogue;
    }

    private void OnDestroy()
    {
        dialogueManager.OnDialogueStart -= StartDialogue;
        dialogueManager.OnDialogueEnd -= EndDialogue;
    }

    private void Update()
    {
        if (handlingDialogue)
        {
            HandleDialogue();
            return;
        }

        if (isInteracting) return;

        HandleMovementInput();
        HandleInteraction();
    }

    private void HandleDialogue()
    {
        if (Input.GetKeyDown(KeyCode.Space) || Input.GetKeyDown(KeyCode.Return))
        {
            dialogueManager.DisplayNextSentence();
        }
    }

    private void HandleMovementInput()
    {
        if (!movement.enabled) return;

        movement.moveInput = Vector2.zero;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            movement.moveInput += Vector2.right;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            movement.moveInput += Vector2.left;

        if (Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.UpArrow))
            movement.moveInput += Vector2.up;

        if (Input.GetKey(KeyCode.S) || Input.GetKey(KeyCode.DownArrow))
            movement.moveInput += Vector2.down;
    }

    [SerializeField] Canvas interactionCanvas;
    [SerializeField] Text interactionText;

    private void HandleInteraction()
    {
        List<Collider2D> overlapColls = new List<Collider2D>(Physics2D.OverlapCircleAll(interactionPoint.position, interactionRadius, interactableLayer));

        if (overlapColls.Count <= 0)
        {
            interactionCanvas.enabled = false;
            return;
        }

        for (int i = overlapColls.Count - 1; i >= 0; i--)
        {
            Interactable inter = overlapColls[i].GetComponent<Interactable>();

            if (inter == null)
                overlapColls.RemoveAt(i);

            else if (!inter.CanInteract)
                overlapColls.RemoveAt(i);
        }

        if (overlapColls.Count <= 0)
        {
            interactionCanvas.enabled = false;
            return;
        }

        Interactable interactable = overlapColls[0].GetComponent<Interactable>();

        if (!interactionCanvas.enabled)
        {
            interactionText.text = interactable.InteractionMessage;
            interactionCanvas.enabled = true;
        }

        if (Input.GetKeyDown(KeyCode.E))
        {
            OnInteract?.Invoke(interactable);
            interactable.Interact(this);

            movement.moveInput = Vector2.zero;
            interactionCanvas.enabled = false;
        }
    }
    private void OnDrawGizmosSelected()
    {
        UnityEditor.Handles.color = Color.cyan;
        UnityEditor.Handles.DrawWireDisc(interactionPoint.position, Vector3.forward, interactionRadius);
    }
}
