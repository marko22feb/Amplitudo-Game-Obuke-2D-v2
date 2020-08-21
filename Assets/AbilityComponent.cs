using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityComponent : MonoBehaviour
{
    private StatComponent SC;
    private DamageComponent DC;

    public GameObject ProjectilePrefab;
    public GameObject NavGrid;
    public int NumberOfProjectileWaves;
    public int WavesCount;
    public int NumberOfProjectiles;
    public float FrequencyOfProjectile;
    public float ProjectileAbilityCooldown;

    public void Awake()
    {
        SC = GetComponent<StatComponent>();
        DC = GetComponent<DamageComponent>();
        NavGrid = GameObject.Find("NavGrid");
        
    }

    public void Start()
    {
        AbilityOne();
        StartCoroutine(Cooldown());
    }

    public void AbilityOne()
    {
        transform.position = NavGrid.GetComponent<BoxCollider2D>().bounds.center;
        WavesCount++;
        if (NumberOfProjectileWaves < WavesCount) return;

        for (int i = 0; i < NumberOfProjectiles; i++)
        {
            GameObject temp = Instantiate(ProjectilePrefab);
            temp.GetComponent<DamageComponent>().Owner = gameObject;
            temp.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            float velX = Random.Range(-1f, 1f) * 5;
            if (velX == 0) velX = 2;
            
            float velY = Random.Range(-1f, 1f) * 5;
            if (velY == 0) velY = 2;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(velX, velY);
        }

        StartCoroutine(Frequency());
    }

    public IEnumerator Frequency()
    {
        yield return new WaitForSeconds(FrequencyOfProjectile);
        AbilityOne();
    }

    public IEnumerator Cooldown()
    {
        yield return new WaitForSeconds(ProjectileAbilityCooldown);
        WavesCount = 0;
        AbilityOne();
        StartCoroutine(Cooldown());
    }

    public void AbilityTwo()
    {
    }

    public void AbilityThree()
    {
    }
}
