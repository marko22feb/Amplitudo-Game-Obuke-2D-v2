using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    StatComponent myStats;
    public bool IsTrap;
    public float damageByTrap = 0;
    public bool ignoreIframes = false;

    void Awake()
    {
        myStats = GetComponent<StatComponent>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        StatComponent otherStats = collision.gameObject.GetComponent<StatComponent>();

        if (otherStats != null)
        {
            if (!IsTrap)
            {
                if (transform.position.y + .3f >= collision.transform.position.y && otherStats.defenseMultiplayer != 0)
                    otherStats.ModifyHealthBy(-Random.Range(myStats.minimumDamage, myStats.maximumDamage), myStats.damageMultiplayer, false);
                else myStats.ModifyHealthBy(-Random.Range(otherStats.minimumDamage, otherStats.maximumDamage), otherStats.damageMultiplayer, false);
            }
            else
            {
                otherStats.ModifyHealthBy(-damageByTrap, 1f, ignoreIframes);
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        StatComponent otherStats = collision.gameObject.GetComponent<StatComponent>();

        if (otherStats != null)
        {
            if (!IsTrap)
            {
                if (transform.position.y + .3f >= collision.transform.position.y)
                    otherStats.ModifyHealthBy(-Random.Range(myStats.minimumDamage, myStats.maximumDamage), myStats.damageMultiplayer, false);
                else myStats.ModifyHealthBy(-Random.Range(otherStats.minimumDamage, otherStats.maximumDamage), otherStats.damageMultiplayer, false);
            }
            else
            {
                otherStats.ModifyHealthBy(-damageByTrap, 1f, ignoreIframes);
            }
        }
    }
}
