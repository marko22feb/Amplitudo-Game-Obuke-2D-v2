﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.Tilemaps;

public class Node : MonoBehaviour
{
    public Vector2 graphPosition;
    public SpriteRenderer sr;
    public List<GameObject> neighbors;
    public float weight = float.MaxValue;
    public Transform previousNode = null;
    private FindShortestPath FSP;
    public Tilemap floorTM;

    public void Awake()
    {
        FSP = GameObject.Find("NavGrid").GetComponent<FindShortestPath>();
        sr = GetComponent<SpriteRenderer>();
    }

    public void CheckIfCanExist()
    {
        Vector3Int nodePosition = new Vector3Int(Mathf.FloorToInt(transform.position.x), Mathf.FloorToInt(transform.position.y), Mathf.FloorToInt(transform.position.z));
        TileBase temp = floorTM.GetTile(nodePosition);

        if (temp != null)
        {
            DestroyImmediate(gameObject);
        }
    }

    public void ResetNode()
    {
        weight = float.MaxValue;
        previousNode = null;
        sr.color = Color.white;
    }

    public void OnMouseDown()
    {
        /*
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
        */
    }
}
