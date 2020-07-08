using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public static GameController Control;
    public int Coins;
    public float TimeElapsed;

    public void Awake()
    {
        if (Control == null)
        {
            Control = this;
        }
        else if (Control != this)
            Destroy(gameObject);
    }

    public void Update()
    {
        TimeElapsed = Time.deltaTime + TimeElapsed;
    }
}
