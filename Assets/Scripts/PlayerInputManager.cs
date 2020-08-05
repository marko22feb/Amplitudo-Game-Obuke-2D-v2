using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class PlayerInputManager : MonoBehaviour
{
    public Door door;
    public Lever lever;
    public GameObject InfoText;
    public bool IsNearLever = false;
    public bool CanShoot = false;
    Canvas MainHud;
    GameObject TextPanel;
    public GameObject projectilePrefab;

    public void Awake()
    {
        MainHud = GameObject.Find("MainHUD").GetComponent<Canvas>();
        TextPanel = GameObject.Find("TextPanel");
    }

    public void Start()
    {
        if (!GameController.Control.IsNewGame) GameController.Control.Load();
        Time.timeScale = 1f;
    }

    void Update()
    {
        if (Input.GetButtonDown("Interact"))
        {
            Interact();
        }

        if (Input.GetKeyDown(KeyCode.F8))
        {
            GameController.Control.LoadLastSavedScene();
        }

        if (Input.GetButtonDown("Projectile"))
        {
            Shoot();
        }
    }

    public void Interact()
    {
        if (door != null)
        {
            if (IsNearLever) {
                lever.TriggerLever();
            }
            else {
                if (!door.Locked)
                {
                    if (door.coinsRequired == 0)
                    {
                        SceneManager.LoadScene(2);
                    }
                    else
                    {
                        if (GameController.Control.Coins < door.coinsRequired)
                        {
                            GameObject temp = Instantiate(InfoText, TextPanel.transform);
                            int missingCoins = door.coinsRequired - GameController.Control.Coins;
                            temp.GetComponent<Text>().text = "You do not have enough coins to enter here, you need " + missingCoins + " more.";
                        }
                        else
                        {
                            SceneManager.LoadScene(2);
                        }
                    }
                }
                else
                {
                    GameObject temp = Instantiate(InfoText, TextPanel.transform);
                    temp.GetComponent<Text>().text = "The doors are locked. Find a lever.";
                }
            }
        }
    }

    public void Shoot()
    {
        if (!CanShoot) return;
        int facingRight = 1;
        if (transform.localRotation.y != 0) facingRight = -1;
        GameObject temp = Instantiate(projectilePrefab);
        temp.transform.position = new Vector2(transform.position.x + facingRight, transform.position.y);
        temp.GetComponent<Rigidbody2D>().velocity = new Vector2(10f * facingRight, 0);
    }
}
