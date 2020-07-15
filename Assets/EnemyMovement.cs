using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb2d;
    public GameObject patrolRoute;
    private GameObject[] patrolRoutePoints;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
    }

    private void Start()
    {
        DetectDirection();

        GameObject[] temparray = new GameObject[patrolRoute.transform.childCount];
        for (int i = 0; i < patrolRoute.transform.childCount; i++)
        {
            temparray[i] = patrolRoute.transform.GetChild(i).gameObject;
        }
        patrolRoutePoints = temparray;

    }

    public void DetectDirection()
    {
        GameObject patrolPoint = closestPatrolPoint();
        if (patrolPoint.transform.position.y < transform.position.y) speed = -Mathf.Abs(speed);
        else speed = Mathf.Abs(speed);
    }

    private void Move()
    {
        Vector3 Velocity = new Vector3(speed * Time.deltaTime, rb2d.velocity.y, 0);
        rb2d.velocity = Velocity;

        if (speed < 0)
        {
            transform.localRotation = new Quaternion(0, 0, 0, 0);
        }
        else transform.localRotation = new Quaternion(0, 180, 0, 0);
    }

    private GameObject closestPatrolPoint()
    {
        GameObject closest = null;
        float distance = Mathf.Infinity;


        foreach (Transform child in patrolRoute.transform)
        {
            float tempDistance = Vector2.Distance(child.position, transform.position);
            if (distance > tempDistance)
            {
                distance = tempDistance;
                closest = child.gameObject;
            }
        }

        return closest;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.parent.gameObject == patrolRoute)
        {
            int pointAmount = collision.transform.parent.childCount;
            
            if (collision.gameObject == patrolRoutePoints[0] || collision.gameObject == patrolRoutePoints[pointAmount - 1])
            {
                speed = -speed;
            }
        }
    }

    void Update()
    {
        Move();
    }
}
