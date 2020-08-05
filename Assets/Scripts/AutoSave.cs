using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AutoSave : MonoBehaviour
{
     bool AlreadySaved = false;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!AlreadySaved)
        {
            if (GameController.Control.GetLastPlayedScene() == SceneManager.GetActiveScene().buildIndex)
            {
                if (GameController.Control.GetLastAutoSaveName() == this.name) return;
            }

            GameController.Control.Save(this.name);
        }
    }
}
