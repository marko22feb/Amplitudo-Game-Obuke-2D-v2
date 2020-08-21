using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BaseAbility : MonoBehaviour
{
    public int NumberOfWaves;
    public int NumberOfAbility;
    public float FrequencyOfAbility;
    public float AbilityCooldown;

    public bool cooldownExpired;

    public GameObject instantiatePrefab;
    public GameObject CasterRefference;

    protected int WavesCount;

    public virtual void OnNotifyReceived(GameObject Caster)
    {
        CasterRefference = Caster;
        StartCoroutine(Cooldown());
        if (FrequencyOfAbility != 0)
        StartCoroutine(Frequency());
    }

    public IEnumerator Frequency()
    {
        yield return new WaitForSeconds(FrequencyOfAbility);
        OnNotifyReceived(CasterRefference);
    }

    public IEnumerator Cooldown()
    {
        cooldownExpired = false;
        yield return new WaitForSeconds(AbilityCooldown);
        WavesCount = 0;
        cooldownExpired = true;
    }
}
