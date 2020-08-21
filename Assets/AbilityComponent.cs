using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityComponent : MonoBehaviour
{
    private StatComponent SC;
    private DamageComponent DC;
    public List<BaseAbility> abilities;

    public bool ShouldTeleportToCenterOfNavGrid = false;

    public GameObject NavGrid;
    


    public void Awake()
    {
        SC = GetComponent<StatComponent>();
        DC = GetComponent<DamageComponent>();
        NavGrid = GameObject.Find("NavGrid");
    }

    public void Start()
    {
        AbilityToUse();
        StartCoroutine(UseAnotherAbility());
    }

    public void AbilityToUse()
    {
        if (ShouldTeleportToCenterOfNavGrid)
        transform.position = NavGrid.GetComponent<BoxCollider2D>().bounds.center;
        abilities[0].OnNotifyReceived(this.gameObject);
    }

    public IEnumerator UseAnotherAbility()
    {
        yield return new WaitForSeconds(16f);
        AbilityToUse();
    }
}
