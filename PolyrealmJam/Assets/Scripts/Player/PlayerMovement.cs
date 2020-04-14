using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float speed = 2f;

    public float moveInput { get; set; }

    public void EnableMovement() => enabled = true;
    public void DisableMovement() => enabled = false;

    private void Update()
    {
        Move();
    }

    public void OnEnable()
    {

    }

    public void OnDisable()
    {
        moveInput = 0f;
    }

    private void Move()
    {
        transform.Translate(Vector2.right * moveInput * speed * Time.deltaTime);
    }
}
