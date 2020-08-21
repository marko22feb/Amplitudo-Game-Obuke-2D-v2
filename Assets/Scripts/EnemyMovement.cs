using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class EnemyMovement : MonoBehaviour
{
    public bool IsStunned = false;
    public float speed;
    private Rigidbody2D rb2d;
    public GameObject patrolRoute;
    private GameObject[] patrolRoutePoints;
    private FindShortestPath FSP;
    
    public List<Collider2D> allTheTouchingNodes;
    public List<Transform> navPoints;

    public bool ShouldCheck = false;
    public bool CheckStarted = false;
    public bool CheckIsRunning = false;
    public bool CanCheckAgain = true;
    Vector3 startPosition;

    private void Awake()
    {
        rb2d = GetComponent<Rigidbody2D>();
        FSP = GameObject.Find("NavGrid").GetComponent<FindShortestPath>();
    }

    private void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            ShouldCheck = true;
        }

        if (Input.GetButtonUp("Interact"))
        {
            ShouldCheck = false;
        }
        /*       if (FSP.IsPlayerInsideOfNavGrid && CanCheckAgain)
               {
                   ShouldCheck = true;
                   CanCheckAgain = false;
               }

               if (!FSP.IsPlayerInsideOfNavGrid)
               {
                   ShouldCheck = false;

              }  
               */
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (ShouldCheck)
        {
            CheckStarted = true;
            ShouldCheck = false;
            allTheTouchingNodes.Add(collision);
            if (!CheckIsRunning)
            {
                StartCoroutine(DelayFindList());
            }
        }
    }

    IEnumerator DelayFindList()
    {
        CheckIsRunning = true;
        yield return new WaitForSeconds(0.05f);
        FSP.StartNode = ClosestNode();
        GameObject Player = GameObject.Find("Player");

        
        Player.GetComponent<Movement>().ShouldCheck = true;
        yield return new WaitForSeconds(0.1f);

        FSP.EndNode = Player.GetComponent<Movement>().allTheTouchingNodes[0].transform;
        FSP.FindPath();
        yield return new WaitForSeconds(0.1f);
        navPoints = FSP.path;
        ShouldCheck = false;
        CheckStarted = false;
        MoveOnNavPath();
        CheckIsRunning = false;
    }

    public void MoveOnNavPath()
    {
        if (navPoints.Count < 1) return;

        float alpha = 0;
        float speed = 2.5f;

        startPosition = transform.position;

        StartCoroutine(MoveOnNavPathCor(alpha, speed));
    }

    IEnumerator MoveOnNavPathCor(float alpha, float speed)
    {
        yield return new WaitForSeconds(0.00001f);
        if (navPoints.Count < 1)
        {
            allTheTouchingNodes.Clear();
            GameObject Player = GameObject.Find("Player");
            Player.GetComponent<Movement>().allTheTouchingNodes.Clear();
            StopCoroutine(MoveOnNavPathCor(alpha, speed));
            CanCheckAgain = true;
        }
        else
        {
            alpha += Time.deltaTime * speed;
            transform.position = Vector3.Lerp(startPosition, navPoints[0].position, alpha);

            if (alpha >= 1)
            {
                alpha = 0;
                navPoints.Remove(navPoints[0]);
                navPoints = navPoints.Where(item => item != null).ToList();
                startPosition = transform.position;
            }
            StartCoroutine(MoveOnNavPathCor(alpha, speed));
        }
    }
    private Transform ClosestNode()
    {
        float distance = Mathf.Infinity;
        Transform closestNode = null;

        foreach (Collider2D col in allTheTouchingNodes)
        {
            float tempDist = Vector3.Distance(col.transform.position, transform.position);
            if (tempDist < distance)
            {
                distance = tempDist;
                closestNode = col.transform;
            }
        }

        return closestNode;
    }
    /*
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
        if (IsStunned) {
            Vector3 vel = new Vector3(0, 0, 0);
            rb2d.velocity = vel;
            return;
        }

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
    */
}
