using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_ProjectileRain : BaseAbility
{
    public override void OnNotifyReceived(GameObject Caster)
    {
        WavesCount++;
        if (NumberOfWaves < WavesCount) return;

        for (int i = 0; i < NumberOfAbility; i++)
        {
            GameObject temp = Instantiate(instantiatePrefab);
            temp.GetComponent<DamageComponent>().Owner = gameObject;
            temp.transform.position = new Vector3(transform.position.x, transform.position.y, transform.position.z);
            float velX = Random.Range(-1f, 1f) * 5;
            if (velX == 0) velX = 2;

            float velY = Random.Range(-1f, 1f) * 5;
            if (velY == 0) velY = 2;
            temp.GetComponent<Rigidbody2D>().velocity = new Vector2(velX, velY);
        }

        base.OnNotifyReceived(Caster);
    }
}
