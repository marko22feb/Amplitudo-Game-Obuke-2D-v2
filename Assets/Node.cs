using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Node : MonoBehaviour
{
    public Vector2 graphPosition;
    public SpriteRenderer sr;
    public List<GameObject> neighbors;
    public float weight = float.MaxValue;
    public Transform previousNode = null;
    private FindShortestPath FSP;

    public void Awake()
    {
        FSP = GameObject.Find("NavGrid").GetComponent<FindShortestPath>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void ResetNode()
    {
        weight = float.MaxValue;
        previousNode = null;
        sr.color = Color.white;
    }

    public void OnMouseDown()
    {
        Debug.Log("Clicked");
        if (FSP.StartNode == null)
        {
            FSP.StartNode = this.transform;
            sr.color = Color.blue;
        } else
        {
            if (FSP.StartNode != this.transform)
            {
                FSP.EndNode = this.transform;
                FSP.FindPath();
            }
        }
    }
}
