using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MakeGrid : MonoBehaviour
{
    public int SizeX = 5;
    public int SizeY = 5;

    public List<GameObject> NavGrid;
    public GameObject nodePrefab;

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
            }
        }

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
        }
    }
}
