using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Transform tr;
    Rigidbody2D rd2D;
    BoxCollider2D boxCollider;
    public LayerMask layer = default;
    public float Speed = 0;
    public float JumpHeight = 2;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        rd2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
    }

    bool IsOnGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 1.1f, layer);
        if (hit.collider != null) { 
            if (hit.collider.gameObject.layer == 10)
            {
                return true;
            } else return false;
        }
        return false;
    }

    void Update()
    {
        IsOnGround();
        if (Input.GetAxis("Horizontal") != 0)
        {
            Vector3 Velocity = new Vector3(rd2D.velocity.x + Speed * Input.GetAxis("Horizontal")  * Time.deltaTime, rd2D.velocity.y, 0);
            rd2D.velocity = Velocity;
        }
        if (IsOnGround())
        {
            if (Input.GetButtonDown("Jump"))
            {
                {
                    Vector3 Velocity = new Vector3(rd2D.velocity.x, rd2D.velocity.y + JumpHeight, 0);
                    rd2D.velocity = Velocity;
                }
            }
        }
    }

    private void FixedUpdate()
    {
        
    }
}
