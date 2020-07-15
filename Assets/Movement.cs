using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{
    Transform tr;
    public Rigidbody2D rd2D;
    BoxCollider2D boxCollider;
    public LayerMask layer = default;
    GameObject floor;
    Animator anim;
    public float Speed = 0;
    public float JumpHeight = 2;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        rd2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        floor = GameObject.Find("FloorHolder");
    }

    bool IsOnGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.2f, layer);
        if (hit.collider != null) { anim.SetBool("IsOnGround", true); return true; }
        anim.SetBool("IsOnGround", false);
        return false;
    }

    void Update()
    {

        Move(Input.GetAxis("Horizontal"));

        if (IsOnGround())
        {
            if (Input.GetButtonDown("Jump"))
            {
                {
                    Jump(false);
                }
            }
        }
    }

    public void Jump(bool forced)
    {
        Vector3 Velocity = new Vector3(rd2D.velocity.x, rd2D.velocity.y + JumpHeight, 0);
        rd2D.velocity = Velocity;
        if (!forced) anim.SetTrigger("DidJump");
    }
    private void Move(float value)
    {
        if (value != 0) {
            Vector3 Velocity = new Vector3(Speed * Input.GetAxis("Horizontal") * Time.deltaTime, rd2D.velocity.y, 0);
            rd2D.velocity = Velocity;
            anim.SetBool("IsMoving", true);

            if (value < 0)
            {
                tr.localRotation = new Quaternion(0, 180, 0, 0);
            }
            else tr.localRotation = new Quaternion(0, 0, 0, 0);
        }
        else anim.SetBool("IsMoving", false);
    }

    private void FixedUpdate()
    {
        
    }
}
