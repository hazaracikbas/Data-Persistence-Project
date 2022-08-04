using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class PlayerData : MonoBehaviour
{
    public static PlayerData Instance;
    public static string playerInput;
    public TMP_InputField inputField;
    public TextMeshProUGUI bestScoreText;

    public int bestScore = MainManager.highScore;
    public string bestPlayer = MainManager.highScoreName;
    

    private void Awake()
    {
        
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        LoadPlayerName();
        MainManager.LoadHighScore();
        SetBestScore();

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }


    public void SetUserName()
    {
        if(inputField != null)
        {
            playerInput = inputField.GetComponent<TMP_InputField>().text;
            Debug.Log("Player Name is: " + playerInput);
        }
        else
        {
            Debug.Log("Player Name is not set");
        }
    }

    public void SetBestScore()
    {
        bestScoreText.text = "Best Score: " + MainManager.highScoreName + ": " + MainManager.highScore;
    }


    [System.Serializable]
    class SaveData
    {
        public string playerInput;
    }

    public void SavePlayerName()
    {
        SaveData data = new SaveData();
        data.playerInput = playerInput;

        string json = JsonUtility.ToJson(data);

        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadPlayerName()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            SaveData data = JsonUtility.FromJson<SaveData>(json);

            playerInput = data.playerInput;
        }
    }

}
