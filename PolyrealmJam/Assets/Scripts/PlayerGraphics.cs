using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerGraphics : MonoBehaviour
{
    private PlayerMovement movement;

    [SerializeField] SpriteRenderer spriteRenderer;

    private void Awake()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (movement.input > 0f)
            spriteRenderer.flipY = true;

        if (movement.input < 0f)
            spriteRenderer.flipY = false;
    }
}
