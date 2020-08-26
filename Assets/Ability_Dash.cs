using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ability_Dash : BaseAbility
{
    private Vector3 startPosition;
        private float alpha;
    public override void OnNotifyReceived(GameObject Caster)
    {
        AC.IsUsingAbility = true;
        base.OnNotifyReceived(Caster);
        startPosition = CasterRefference.transform.position;
        alpha = 0;
        StartCoroutine(MoveToPosition());
    }

    IEnumerator MoveToPosition()
    {
        yield return new WaitForEndOfFrame();
        alpha += Time.deltaTime * 3f;
        CasterRefference.transform.position = Vector3.Lerp(startPosition, startPosition + new Vector3(10f, 0, 0), alpha);
        if (alpha < 1) StartCoroutine(MoveToPosition()); else AC.IsUsingAbility = false;
    }
}
