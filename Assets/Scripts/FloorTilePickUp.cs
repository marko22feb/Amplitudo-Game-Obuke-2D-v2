using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FloorTilePickUp : MonoBehaviour
{
    private GameObject toBeSpawned = null;
    public enum PickUpType { Coins, PowerUp, HealthRegen, Projectile };
    [Header("Enums")]
    public PickUpType pickUpType;

    [Header ("Prefabs")]
    public GameObject coinsPrefab;
    public GameObject powerUpPrefab;
    public GameObject healthRegenPrefab;
    public GameObject projectilePrefab;

    [Header("Data")]
    public int amountOfPickUps = 1;

    public void Awake()
    {
        switch (pickUpType)
        {
            case PickUpType.Coins:
                toBeSpawned = coinsPrefab;
                break;
            case PickUpType.PowerUp:
                toBeSpawned = powerUpPrefab;
                break;
            case PickUpType.HealthRegen:
                toBeSpawned = healthRegenPrefab;
                break;
            case PickUpType.Projectile:
                toBeSpawned = projectilePrefab;
                break;
            default:
                Debug.Log("Unknown PickUp Type");
                break;
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (amountOfPickUps <= 0) {
            return; }

        if (collision.gameObject.tag == "Player")
        {
            if (collision.transform.position.y >= transform.position.y) return;
            GameObject temp = Instantiate(toBeSpawned);
            amountOfPickUps--;
            temp.transform.position = new Vector3(transform.position.x, transform.position.y + 1f, 0);
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(Random.Range(-5, 5), 5);
            if (amountOfPickUps <= 0)
            GetComponent<SpriteRenderer>().color = Color.gray;
        }
    }
}
