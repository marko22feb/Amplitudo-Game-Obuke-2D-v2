using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPickup : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            GameController.Control.Coins++;
            Destroy(gameObject);
        }
    }
}
