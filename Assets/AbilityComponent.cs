using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityComponent : MonoBehaviour
{
    public enum ActivateOn { Health, Timer, Cooldown }
    public enum ArithmeticOperation { Null, Greater, GreaterOrEqual, Equal, Less, LessOrEqual }

    public enum AbilityType { Projectile, Movement, AreaOfEffect }
    public enum DamageType { Magic, Physical }
    [System.Serializable] public struct AbilityBaseData { public AbilityType abilityType; public DamageType damageType; public bool ShouldTeleportToCenterOfNavGrid; }
    [System.Serializable] public struct ActivateData { public ActivateOn activateOn; public ArithmeticOperation operation; public float Value; public float timerValue; public void SetTimerValue(float value) { timerValue = value;} }
    [System.Serializable] public struct AbilityData { public ActivateData activateData; public AbilityBaseData baseData; }


    private StatComponent SC;
    private DamageComponent DC;
    public List<BaseAbility> abilities;
    public List<AbilityData> abilityData;
    public GameObject NavGrid;
    public bool IsUsingAbility = false;
    private float AbilityWaitTime = 5f;
    private float currentAbilityWaitTime = 0f;

    public void Awake()
    {
        SC = GetComponent<StatComponent>();
        DC = GetComponent<DamageComponent>();
        NavGrid = GameObject.Find("NavGrid");
    }

    private void FixedUpdate()
    {
        if (currentAbilityWaitTime < AbilityWaitTime)
            currentAbilityWaitTime += Time.fixedDeltaTime;
        else
        {
            for (int i = 0; i < abilityData.Count; i++)
            {
                if (abilityData[i].activateData.activateOn == ActivateOn.Timer)
                {
                    AbilityData tempData = abilityData[i];
                    float newTimerValue = abilityData[i].activateData.timerValue + Time.fixedDeltaTime;
                    tempData.activateData.SetTimerValue(newTimerValue);
                    abilityData[i] = tempData;
                }
            }

            for (int i = 0; i < abilityData.Count; i++)
            {
                bool canUse = CanUseAbility(i);
                if (canUse)
                {
                    IsUsingAbility = true;
                    AbilityToUse(i);
                }
            }
        }
    }

    public void Start()
    {

    }

    public bool CanUseAbility(int index) 
    {
        if (IsUsingAbility) return false;

        bool canUse = false;

        switch (abilityData[index].activateData.activateOn)
        {
            case ActivateOn.Health:
                switch (abilityData[index].activateData.operation)
                {
                    case ArithmeticOperation.Null:
                        break;
                    case ArithmeticOperation.Greater:
                        if (SC.currentHealth > abilityData[index].activateData.Value)
                            canUse = true;
                        break;
                    case ArithmeticOperation.GreaterOrEqual:
                        if (SC.currentHealth >= abilityData[index].activateData.Value)
                            canUse = true;
                        break;
                    case ArithmeticOperation.Equal:
                        if (SC.currentHealth == abilityData[index].activateData.Value)
                            canUse = true;
                        break;
                    case ArithmeticOperation.Less:
                        if (SC.currentHealth < abilityData[index].activateData.Value)
                            canUse = true;
                        break;
                    case ArithmeticOperation.LessOrEqual:
                        if (SC.currentHealth <= abilityData[index].activateData.Value)
                            canUse = true;
                        break;
                    default:
                        break;
                }
                break;
            case ActivateOn.Timer:
                if (abilityData[index].activateData.timerValue >= abilityData[index].activateData.Value)
                    canUse = true;
                break;
            case ActivateOn.Cooldown:
                if (abilities[index].cooldownExpired) canUse = true;
                break;
            default:
                break;
        }

        return canUse;
    }

    public void AbilityToUse(int index)
    {
        
        Debug.Log(index);
        if (abilityData[index].baseData.ShouldTeleportToCenterOfNavGrid)
        transform.position = NavGrid.GetComponent<BoxCollider2D>().bounds.center;
        if (abilities[index].AC == null) abilities[index].AC = this;
        abilities[index].OnNotifyReceived(this.gameObject);

        AbilityData tempData = abilityData[index];
        tempData.activateData.SetTimerValue(0f);
        abilityData[index] = tempData;
    }
}
