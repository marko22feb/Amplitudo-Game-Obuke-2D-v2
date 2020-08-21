using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MakeGrid : MonoBehaviour
{
    public int SizeX = 5;
    public int SizeY = 5;

    public List<GameObject> NavGrid;
    public GameObject nodePrefab;
    private BoxCollider2D col2D;

    public Tilemap floorTM;

    public void GenerateNavGrid()
    {
        for (int x = 0; x < SizeX; x++)
        {
            for (int y = 0; y < SizeY; y++)
            {
                GameObject temp = Instantiate(nodePrefab, this.transform);
                temp.transform.localPosition = new Vector2(x, y);
                NavGrid.Add(temp);
                temp.GetComponent<Node>().graphPosition = new Vector2(x, y);
                temp.name = ("Node (" + x + "-" + y + ")");
                temp.GetComponent<Node>().floorTM = floorTM;
                temp.GetComponent<Node>().CheckIfCanExist();
            }
        }
        col2D = GetComponent<BoxCollider2D>();
        col2D.size = new Vector2(SizeX, SizeY);
        col2D.offset = new Vector2(SizeX / 2 - 0.5f, SizeY / 2 - 0.5f);
        NavGrid = NavGrid.Where(item => item != null).ToList();

        GenerateNeighbors();
    }

    public void DestroyCurrentNavGrid()
    {
        while(transform.childCount != 0)
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }

        NavGrid.Clear();
    }

    public void GenerateNeighbors()
    {
        foreach(GameObject gob in NavGrid)
        {
            Node currentNode = gob.GetComponent<Node>();

            //Left
            foreach (GameObject ob in NavGrid)
            {
                Node nodeToCheck = ob.GetComponent<Node>();
                if (nodeToCheck.graphPosition.x + 1 == currentNode.graphPosition.x && nodeToCheck.graphPosition.y == currentNode.graphPosition.y)
                { 
                    currentNode.neighbors.Add(nodeToCheck.gameObject);
                }
            }

            //Right
            foreach (GameObject ob in NavGrid)
            {
                Node nodeToCheck = ob.GetComponent<Node>();
                if (nodeToCheck.graphPosition.x - 1 == currentNode.graphPosition.x && nodeToCheck.graphPosition.y == currentNode.graphPosition.y)
                {
                    currentNode.neighbors.Add(nodeToCheck.gameObject);
                }
            }

            //Up
            foreach (GameObject ob in NavGrid)
            {
                Node nodeToCheck = ob.GetComponent<Node>();
                if (nodeToCheck.graphPosition.x == currentNode.graphPosition.x && nodeToCheck.graphPosition.y -1 == currentNode.graphPosition.y)
                {
                    currentNode.neighbors.Add(nodeToCheck.gameObject);
                }
            }

            //Down
            foreach (GameObject ob in NavGrid)
            {
                Node nodeToCheck = ob.GetComponent<Node>();
                if (nodeToCheck.graphPosition.x == currentNode.graphPosition.x && nodeToCheck.graphPosition.y + 1 == currentNode.graphPosition.y)
                {
                    currentNode.neighbors.Add(nodeToCheck.gameObject);
                }
            }

            // Up Left
            foreach (GameObject ob in NavGrid)
            {
                Node nodeToCheck = ob.GetComponent<Node>();
                if (nodeToCheck.graphPosition.x + 1 == currentNode.graphPosition.x && nodeToCheck.graphPosition.y - 1 == currentNode.graphPosition.y)
                {
                    currentNode.neighbors.Add(nodeToCheck.gameObject);
                }
            }

            // Up Right
            foreach (GameObject ob in NavGrid)
            {
                Node nodeToCheck = ob.GetComponent<Node>();
                if (nodeToCheck.graphPosition.x - 1 == currentNode.graphPosition.x && nodeToCheck.graphPosition.y - 1 == currentNode.graphPosition.y)
                {
                    currentNode.neighbors.Add(nodeToCheck.gameObject);
                }
            }

            // Down Left
            foreach (GameObject ob in NavGrid)
            {
                Node nodeToCheck = ob.GetComponent<Node>();
                if (nodeToCheck.graphPosition.x + 1 == currentNode.graphPosition.x && nodeToCheck.graphPosition.y + 1 == currentNode.graphPosition.y)
                {
                    currentNode.neighbors.Add(nodeToCheck.gameObject);
                }
            }

            // Down Right
            foreach (GameObject ob in NavGrid)
            {
                Node nodeToCheck = ob.GetComponent<Node>();
                if (nodeToCheck.graphPosition.x - 1 == currentNode.graphPosition.x && nodeToCheck.graphPosition.y + 1 == currentNode.graphPosition.y)
                {
                    currentNode.neighbors.Add(nodeToCheck.gameObject);
                }
            }
        }
    }
}
