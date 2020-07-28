using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageComponent : MonoBehaviour
{
    StatComponent myStats;
    public bool IsTrap;
    public float damageByTrap = 0;

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
                if (transform.position.y + .3f >= collision.transform.position.y)
                otherStats.ModifyHealthBy(-Random.Range(myStats.minimumDamage, myStats.maximumDamage), myStats.damageMultiplayer);
                else myStats.ModifyHealthBy(-Random.Range(otherStats.minimumDamage, otherStats.maximumDamage), otherStats.damageMultiplayer);
            }
            else
            {
                otherStats.ModifyHealthBy(-damageByTrap, 1f);
            }
        }
    }
}
