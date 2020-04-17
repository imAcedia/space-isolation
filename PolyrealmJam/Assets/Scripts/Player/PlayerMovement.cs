using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public LayerMask climbableMask;
    public float speed = 2f;

    public Vector2 moveInput { get; set; }

    public bool IsClimbing => climbedLadder != null;

    private Rigidbody2D rb;
    private Vector2 velocity;

    private Ladder climbedLadder = null;

    private void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        Move();
    }

    private void Move()
    {
        velocity.x = moveInput.x * speed;

        if (!IsClimbing)
            CheckClimb();
        else
            ClimbLadder();

        rb.velocity = velocity;
    }

    private void ClimbLadder()
    {
        if (!IsClimbing)
        {
            return;
        }

        velocity.x = 0f;
        velocity.y = moveInput.y * speed;

        if (moveInput.x != 0f)
        {
            float? yDropOff = climbedLadder.GetDropOffLevel(transform.position);

            Debug.Log(yDropOff);

            if (yDropOff != null)
            {
                climbedLadder = null;
                velocity.y = 0f;

                Vector2 newPos = transform.position;
                newPos.y = (float)yDropOff;
                rb.MovePosition(newPos);
                return;
            }
        }

        if (moveInput.y > 0f)
        {
            if (transform.position.y >= climbedLadder.HighestLevel)
                velocity.y = 0f;
        }

        if (moveInput.y < 0f)
        {
            if (transform.position.y <= climbedLadder.LowestLevel)
                velocity.y = 0f;
        }
    }

    private void CheckClimb()
    {
        if (moveInput.y == 0f) return;

        ContactFilter2D contactFilter = new ContactFilter2D();
        contactFilter.NoFilter();
        contactFilter.SetLayerMask(climbableMask);

        List<Collider2D> colls = new List<Collider2D>();

        if (rb.OverlapCollider(contactFilter, colls) > 0)
        {
            Ladder nearestLadder = null;
            float minDist = float.PositiveInfinity;

            for (int i = 0; i < colls.Count; i++)
            {
                Ladder ladder = colls[i].GetComponent<Ladder>();

                if (ladder == null)
                    continue;

                float sqDist = (colls[i].transform.position - transform.position).sqrMagnitude;
                if (sqDist < minDist)
                {
                    minDist = sqDist;
                    nearestLadder = ladder;
                }
            }

            if (nearestLadder != null)
            {
                Vector3 newPos = nearestLadder.transform.position;
                newPos.y = transform.position.y;

                rb.MovePosition(newPos);
                climbedLadder = nearestLadder;
            }
        }
    }
}
