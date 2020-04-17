using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphics : MonoBehaviour
{
    [SerializeField] PlayerMovement movement = null;
    [SerializeField] PlayerInput input = null;

    private SpriteRenderer spriteRenderer;
    private Animator animator;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (movement.moveInput.x > 0f)
            spriteRenderer.flipX = true;

        if (movement.moveInput.x < 0f)
            spriteRenderer.flipX = false;

        animator.SetBool("moving", Mathf.Abs(movement.moveInput.x) > 0f);
        animator.SetBool("fixing", input.isFixing);
        animator.SetBool("using", input.isUsing);
        animator.SetBool("climbing", input.IsClimbing);

        if (input.IsClimbing)
        {
            if (Mathf.Abs(movement.moveInput.y) > 0f)
            {
                animator.speed = 1f;
            }
            else
            {
                animator.speed = 0f;
            }
        }
        else
        {
            animator.speed = 1f;
        }
    }
}
