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

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Floor")
        {
            StopCoroutine(destroySelfAfterDelay());
            Destroy(gameObject);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

         if (collision.gameObject.tag == "Floor")
        {
            StopCoroutine(destroySelfAfterDelay());
            Destroy(gameObject);
        }
    }
}
