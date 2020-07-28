using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuButton : MonoBehaviour
{
  public void NewGame()
    {
        SceneManager.LoadScene(1);
    }

    public void ContinueGame()
    {

    }

    public void ExitGame()
    {
        Application.Quit();
    }

}
