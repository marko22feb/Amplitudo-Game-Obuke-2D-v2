using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement : MonoBehaviour
{    
    Transform tr;    
    BoxCollider2D boxCollider;
    GameObject floor;
    GameObject UP;
    GameObject DOWN;

    [HideInInspector]
    public Animator anim;

    [HideInInspector]
    public bool isUsingUItoMove = false;

    [Header("Refferences")]
    public Rigidbody2D rd2D;
    public LayerMask layer = default;

    [Header("Ladder")]
    public GameObject bottomLadder = null;
    public bool canExitLadder = true;
    public bool isOnLadder = false;
    public bool isNearLadder = false;

    [Header("Stats")]
    public float Speed = 0;
    public float JumpHeight = 2;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        rd2D = GetComponent<Rigidbody2D>();
        boxCollider = GetComponent<BoxCollider2D>();
        anim = GetComponent<Animator>();
        floor = GameObject.Find("FloorHolder");
        UP = GameObject.Find("VerticalUp");
        DOWN = GameObject.Find("VerticalDown");
    }

    private void Start()
    {
        UP.SetActive(false);
        DOWN.SetActive(false);
    }

    bool IsOnGround()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.down, 0.2f, layer);
        if (hit.collider != null) { anim.SetBool("IsOnGround", true); return true; }
        anim.SetBool("IsOnGround", false);
        return false;
    }

    void IsBoardAbove()
    {
        RaycastHit2D hit = Physics2D.BoxCast(boxCollider.bounds.center, boxCollider.bounds.size, 0, Vector2.up, 1.5f, layer);
        if (hit.collider != null && hit.collider.tag == "Board")
        {
            hit.collider.GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    void Update()
    {

        if (Input.GetAxis("Horizontal") != 0)
        {
            if (Input.GetButtonDown("Jump") && isOnLadder)
            {
                ExitLadder(GameObject.Find("LadderTop(Clone)"));
                Vector3 Velocity = new Vector3(Input.GetAxis("Horizontal") * 35, rd2D.velocity.y, 0);
                rd2D.velocity = Velocity;
                Jump(false);
            }
        }

        if (IsOnGround())
        {
            if (Input.GetButtonDown("Jump"))
            {
                {
                    Jump(false);
                }
            }

            if (Input.GetButtonDown("Crouch"))
            {
                {
                    Crouch();
                }
            }
            if (Input.GetButtonUp("Crouch"))
            {
                anim.SetBool("IsCrouching", false);
            }
        }
    }

    public void Crouch()
    {
        RaycastHit2D hit = Physics2D.Raycast(transform.position, Vector2.down, 1.1f, layer);
        if (hit.collider != null)
        {
            if (hit.collider.gameObject.tag == "Ladder")
            {
                hit.collider.GetComponent<BoxCollider2D>().isTrigger = true;
                EnterLadder();
                StartCoroutine(delayLadderExit(hit.collider.transform));
            }
            else if (hit.collider.gameObject.tag == "Board")
            {
                hit.collider.GetComponent<BoxCollider2D>().isTrigger = true;
            }
            else
                anim.SetBool("IsCrouching", true);
        }
    }

    public void EnterLadder()
    {
        anim.SetBool("IsOnLadder", true);
        rd2D.gravityScale = 0;
        isOnLadder = true;
        UP.SetActive(true);
        DOWN.SetActive(true);
    }
    public void ExitLadder(GameObject LadderTop)
    {
        if (!canExitLadder) return;
        isOnLadder = false;
        anim.SetBool("IsOnLadder", false);
        rd2D.gravityScale = 7;
        LadderTop.GetComponent<BoxCollider2D>().isTrigger = false;
        UP.SetActive(false);
        DOWN.SetActive(false);
        UP.GetComponent<AndroidMovementUI>().LetGoOfButton();
        DOWN.GetComponent<AndroidMovementUI>().LetGoOfButton();
    }

    public void Jump(bool forced)
    {
        if (!isNearLadder)
        {
            Vector3 Velocity = new Vector3(rd2D.velocity.x, rd2D.velocity.y + JumpHeight, 0);
            rd2D.velocity = Velocity;
            if (!forced) anim.SetTrigger("DidJump");
        } else
        {
            EnterLadder();
            transform.position = bottomLadder.transform.position;
        }
    }

    public void Move(float value)
    {
        if (value != 0) {
            Vector3 Velocity = new Vector3(Speed * value, rd2D.velocity.y, 0);
            rd2D.velocity = Velocity;
            anim.SetBool("IsMoving", true);

            if (value < 0)
            {
                tr.localRotation = new Quaternion(0, 180, 0, 0);
            }
            else tr.localRotation = new Quaternion(0, 0, 0, 0);
        }
        
        else { anim.SetBool("IsMoving", false); Vector3 Velocity = new Vector3(0, rd2D.velocity.y, 0); }
    }

    public void MoveVertical(float value)
    {
        if (value != 0)
        {
            Vector3 Velocity = new Vector3(0, Speed * value, 0);
            rd2D.velocity = Velocity;
            anim.SetBool("IsMoving", true);

            if (value < 0)
            {
                if (IsOnGround())
                {
                    ExitLadder(GameObject.Find("LadderTop(Clone)"));
                }
            }
        }
        else anim.SetBool("IsMoving", false);
    }

    IEnumerator delayLadderExit(Transform tr)
    {
        canExitLadder = false;
        transform.position = tr.position;
        yield return new WaitForSeconds(0.1f);
        canExitLadder = true;
    }

    private void FixedUpdate()
    {
        if (!isUsingUItoMove)
        {
            if (!isOnLadder)
                Move(Input.GetAxis("Horizontal"));
            else
                MoveVertical(Input.GetAxis("Vertical"));
        }
        IsBoardAbove();
    }
}
