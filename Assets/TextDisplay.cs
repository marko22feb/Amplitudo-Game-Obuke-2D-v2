using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextDisplay : MonoBehaviour
{
    public Text text;
    public int Command;

    void Update()
    {
        switch (Command)
        {
            case 0:
                text.text = "Coins: " + GameController.Control.Coins;
                break;
            case 1:
                text.text = "Time Elapsed: " + Mathf.Round(GameController.Control.TimeElapsed);
                break;
            default:
                break;
        }
    }
}
