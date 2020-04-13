using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;

    public float input { get; private set; }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        input = 0f;

        if (Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.RightArrow))
            input += 1f;

        if (Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.LeftArrow))
            input -= 1f;

        Move(input);
    }

    private void Move(float dir)
    {
        transform.Translate(Vector2.right * dir * speed * Time.deltaTime);
    }
}
