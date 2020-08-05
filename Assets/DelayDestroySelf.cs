using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DelayDestroySelf : MonoBehaviour
{
    public float Delay;
    public void Start()
    {
        StartCoroutine(destroySelfAfterDelay());
    }

    IEnumerator destroySelfAfterDelay()
    {
        yield return new WaitForSeconds(Delay);
        Destroy(gameObject);
    }
}
