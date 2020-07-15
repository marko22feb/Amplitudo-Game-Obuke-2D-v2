using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    StatComponent myStats;

    void Awake()
    {
        myStats = GetComponent<StatComponent>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
       StatComponent otherStats = collision.gameObject.GetComponent<StatComponent>();

        if (otherStats != null)
        {
            otherStats.ModifyHealthBy(-Random.Range(myStats.minimumDamage, myStats.maximumDamage), myStats.damageMultiplayer);
        }
    }
}
