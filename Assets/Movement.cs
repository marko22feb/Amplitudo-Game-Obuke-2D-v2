using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Test : MonoBehaviour
{
    Transform tr;
    Rigidbody2D rd2D;
    public float Speed = 0;
    public float JumpHeight = 2;

    private void Awake()
    {
        tr = GetComponent<Transform>();
        rd2D = GetComponent<Rigidbody2D>();
    }
    // Start is called before the first frame update
    void Start()
    {
     
    }

    // Update is called once per frame
    void Update()
    {
        Debug.Log(Speed * Time.deltaTime);
        if (Input.GetAxis("Horizontal") != 0)
        {
            Vector3 Velocity = new Vector3(rd2D.velocity.x + Speed * Input.GetAxis("Horizontal")  * Time.deltaTime, rd2D.velocity.y, 0);
            rd2D.velocity = Velocity;
        }

        if (Input.GetButtonDown("Jump")) 
        {
            Vector3 Velocity = new Vector3(rd2D.velocity.x, rd2D.velocity.y + JumpHeight, 0);
            rd2D.velocity = Velocity;
        }
    }

    private void FixedUpdate()
    {
        
    }
}
