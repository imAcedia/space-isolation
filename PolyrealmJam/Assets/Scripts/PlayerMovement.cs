using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public string interactableTag = "Interactable";
    public float speed = 2f;

    private bool movementEnabled = true;

    public float moveInput { get; private set; }
    public bool interactInput { get; private set; }

    private DialogueManager dialogueManager;

    private void Start()
    {
        dialogueManager = FindObjectOfType<DialogueManager>();
    }

    private void Update()
    {
        HandleInput();
    }

    public void EnableMovement()
    {
        movementEnabled = true;
    }

    public void DisableMovement()
    {
        movementEnabled = false;
    }

    private void HandleInput()
    {
        interactInput = Input.GetKeyDown(KeyCode.E);

        if (movementEnabled)
            HandleMovementInput();
    }

    private void HandleMovementInput()
    {
        moveInput = 0f;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            moveInput += 1f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            moveInput -= 1f;

        Move(moveInput);
    }

    private void Move(float dir)
    {
        transform.Translate(Vector2.right * dir * speed * Time.deltaTime);
    }

    private void CheckInteractionInput(Interactable i)
    {
        if (interactInput)
        {
            i.Interact(this);
            Debug.Log("Interacting with " + i.name);
        }
    }

    private void OnTriggerStay2D(Collider2D other)
    {
        if (other.CompareTag(interactableTag))
        {
            CheckInteractionInput(other.gameObject.GetComponent<Interactable>());
        }
    }
}
