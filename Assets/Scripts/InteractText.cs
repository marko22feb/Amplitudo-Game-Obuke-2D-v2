using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InteractText : MonoBehaviour
{
    Text t;

    private void Start()
    {
        t = GetComponent<Text>();
    }
}
