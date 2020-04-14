using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphics : MonoBehaviour
{
    [SerializeField] PlayerMovement movement = null;

    private SpriteRenderer spriteRenderer = null;
    private Animator animator = null;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    private void Update()
    {
        if (movement.input > 0f)
            spriteRenderer.flipX = true;

        if (movement.input < 0f)
            spriteRenderer.flipX = false;

        animator.SetBool("moving", Mathf.Abs(movement.input) > 0f);
    }
}
