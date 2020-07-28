using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LoadLevel : MonoBehaviour
{
    public bool LoadCurrentScene = false;
    public int buildIndex = 0;
    public string sceneName = "";

    public void LoadScene()
    {
        if (LoadCurrentScene) SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        else
        {
            if (sceneName == "") SceneManager.LoadScene(buildIndex);
            else SceneManager.LoadScene(sceneName);
        }
    }
}
