using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AutoSave : MonoBehaviour
{
     bool AlreadySaved = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!AlreadySaved)
        {
            GameController.Control.Save();
        }
    }
}
