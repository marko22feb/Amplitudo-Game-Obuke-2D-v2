using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnitPickup : MonoBehaviour
{
    public FloorTilePickUp.PickUpType pickUpType;
    public float RegenerateHealthBy = 25f;
    public float powerUpDuration = 20f;
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            switch (pickUpType)
            {
                case FloorTilePickUp.PickUpType.Coins:
                    GameController.Control.Coins++;
                    break;
                case FloorTilePickUp.PickUpType.PowerUp:
                    collision.gameObject.GetComponent<StatComponent>().StartCoroutine(collision.gameObject.GetComponent<StatComponent>().PowerUp(powerUpDuration));
                    break;
                case FloorTilePickUp.PickUpType.HealthRegen:
                    collision.gameObject.GetComponent<StatComponent>().ModifyHealthBy(RegenerateHealthBy, 1f, true);
                    break;
                case FloorTilePickUp.PickUpType.Projectile:
                    collision.gameObject.GetComponent<PlayerInputManager>().CanShoot = true;
                    break;
                default:
                    break;
            }
            Destroy(gameObject);
        }
    }
}
