using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstantiateTrapParts : MonoBehaviour
{
    private PrepareTrap pt;

    private void Awake()
    {
        pt = GetComponent<PrepareTrap>();
    }

    private void Start()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }


        for (int i = 0; i < pt.trapSize; i++)
        {
            GameObject temp = Instantiate(pt.trapPart, this.transform);
            temp.transform.localPosition = new Vector2(-(1.15f + pt.colliderSize * i), 0f);
        }
    }
}
