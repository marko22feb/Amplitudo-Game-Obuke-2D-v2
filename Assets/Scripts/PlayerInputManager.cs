using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour
{
    public Door door;
    public GameObject InfoText;
    Canvas MainHud;
    GameObject TextPanel;

    public void Awake()
    {
        MainHud = GameObject.Find("MainHUD").GetComponent<Canvas>();
        TextPanel = GameObject.Find("TextPanel");
    }

    public void Start()
    {
        GameController.Control.Load();
        Time.timeScale = 1f;
    }
    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            if (door != null)
            {
                if (!door.Locked)
                {
                    if (door.coinsRequired == 0)
                    {
                        SceneManager.LoadScene(2);
                    } else
                    {
                        if (GameController.Control.Coins < door.coinsRequired)
                        {
                            GameObject temp = Instantiate(InfoText, TextPanel.transform);
                            int missingCoins = door.coinsRequired - GameController.Control.Coins;
                            temp.GetComponent<Text>().text = "You do not have enough coins to enter here, you need " + missingCoins + " more.";
                        } else
                        {
                            SceneManager.LoadScene(2);
                        }
                    }
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.F8))
        {
            GameController.Control.LoadLastSavedScene();
        }
    }
}
