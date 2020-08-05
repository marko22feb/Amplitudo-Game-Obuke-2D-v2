using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Control;
    public bool IsNewGame = false;
    public int Coins;
    public float TimeElapsed;

    public void Awake()
    {
        DontDestroyOnLoad(this);

        if (Control == null)
        {
            Control = this;
        }
        else if (Control != this)
        {
            Destroy(gameObject);
        }
    }

    public void Update()
    {
        TimeElapsed = Time.deltaTime + TimeElapsed;
    }

    public void Save(string AutoSave)
    {
        SaveGame save = new SaveGame();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/gamesave.123");
        GameObject player = GameObject.Find("Player");

        save.Coins = Coins;
        save.CurrentHP = player.GetComponent<StatComponent>().currentHealth;
        save.playerPositionX = player.transform.position.x;
        save.playerPositionY = player.transform.position.y;
        save.playerPositionZ = player.transform.position.z;
        save.lastPlayedScene = SceneManager.GetActiveScene().buildIndex;
        save.lastUsedAutoSave = AutoSave;

        bf.Serialize(file, save);
        file.Close();
    }

    public void Load()
    {
        if (File.Exists(Application.persistentDataPath + "/gamesave.123"))
        {
            SaveGame save = new SaveGame();
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/gamesave.123", FileMode.Open);
            save = (SaveGame)bf.Deserialize(file);
            GameObject player = GameObject.Find("Player");

            Coins = save.Coins;
            player.GetComponent<StatComponent>().currentHealth = save.CurrentHP;
            if (SceneManager.GetActiveScene().buildIndex == save.lastPlayedScene)
            player.transform.position = new Vector3(save.playerPositionX, save.playerPositionY, save.playerPositionZ);

            file.Close();
        }
    }

    public void LoadLastSavedScene()
    {
        SaveGame save = new SaveGame();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.123", FileMode.Open);
        save = (SaveGame)bf.Deserialize(file);

        SceneManager.LoadScene(save.lastPlayedScene);

        file.Close();
    }

    public bool CanLoadGame()
    {
        return true;
    }

    public int GetLastPlayedScene()
    {
        SaveGame save = new SaveGame();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.123", FileMode.Open);
        save = (SaveGame)bf.Deserialize(file);

        int temp = save.lastPlayedScene;

        file.Close();

        return temp;
    }

    public string GetLastAutoSaveName()
    {
        SaveGame save = new SaveGame();
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Open(Application.persistentDataPath + "/gamesave.123", FileMode.Open);
        save = (SaveGame)bf.Deserialize(file);

        string temp = save.lastUsedAutoSave;

        file.Close();

        return temp;
    }
}

[System.Serializable]
public class SaveGame
{
    public int Coins;
    public float CurrentHP;
    public int lastPlayedScene;
    public string lastUsedAutoSave;

    public float playerPositionX;
    public float playerPositionY;
    public float playerPositionZ;

    public SaveGame()
    {

    }
}
