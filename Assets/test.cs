using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class test : MonoBehaviour
{
    void Start()
    {
        Debug.Log("Position: " + transform.position);
        Debug.Log("Local Position: " + transform.localPosition);
    }
}
