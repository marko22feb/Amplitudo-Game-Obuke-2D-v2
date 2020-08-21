using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FindShortestPath : MonoBehaviour
{
    public Transform StartNode = null;
    public Transform EndNode = null;

    public List<Transform> path;
    private List<GameObject> nodes;

    public bool IsPlayerInsideOfNavGrid = false;

    public void FindPath()
    {
        path = findShortestPath(StartNode, EndNode);

        foreach(Transform tr in path)
        {
            tr.GetComponent<SpriteRenderer>().color = Color.red;
        }

        StartNode = null;
        EndNode = null;
    }

    public List<Transform> findShortestPath (Transform start, Transform end)
    {

        List<Transform> result = new List<Transform>();
        nodes = GetComponent<MakeGrid>().NavGrid;
        Transform node = DijkstraAlgorithm(start, end);

        while (node != null)
        {
            result.Add(node);
            Node currentNode = node.GetComponent<Node>();
            node = currentNode.previousNode;
        }

        result.Reverse();
        return result;
    }

    public Transform DijkstraAlgorithm(Transform start, Transform end)
    {
        List<Transform> unexplored = new List<Transform>();

        foreach(GameObject gob in nodes)
        {
            gob.GetComponent<Node>().ResetNode();
            unexplored.Add(gob.transform);
        }

        Node startNode = start.GetComponent<Node>();
        startNode.weight = 0;

        while (unexplored.Count > 0)
        {
            unexplored.Sort((x, y) => x.GetComponent<Node>().weight.CompareTo(y.GetComponent<Node>().weight));

            Transform current = unexplored[0];

            if (current == end) return current;

            unexplored.Remove(current);

            Node currentNode = current.GetComponent<Node>();
            List<GameObject> neighbors = currentNode.neighbors;

            foreach (GameObject n in neighbors)
            {
                Node neighborNode = n.GetComponent<Node>();

                if (unexplored.Contains(neighborNode.transform))
                {
                    float distance = Vector3.Distance(neighborNode.transform.position, currentNode.transform.position);
                    distance = distance + currentNode.weight;

                    if (distance < neighborNode.weight)
                    {
                        neighborNode.weight = distance;
                        neighborNode.previousNode = currentNode.transform;
                    }
                }
            }
        }

        return end;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player")
        IsPlayerInsideOfNavGrid = true;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Player")
            IsPlayerInsideOfNavGrid = false;
    }
}
