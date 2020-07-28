using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ladder : MonoBehaviour
{
    public bool top = false;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (top)
        {
            if (collision.gameObject.transform.position.y < transform.position.y)
                GetComponent<BoxCollider2D>().isTrigger = true;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            GameObject player = collision.gameObject;
            if (!top)
            {
                player.GetComponent<Movement>().isNearLadder = true;
                player.GetComponent<Movement>().bottomLadder = gameObject;
            }
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.tag == ("Player"))
        {
            GameObject player = collision.gameObject;
            if (top)
            {
                if (player.transform.position.y < transform.position.y) return;

                if (player.GetComponent<Movement>().isOnLadder)
                {
                    player.GetComponent<Movement>().ExitLadder(gameObject);
                }
            }
            else {
                player.GetComponent<Movement>().isNearLadder = false;
            }
        }
    }
}
