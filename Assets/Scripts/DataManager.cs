using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.IO;

public class DataManager : MonoBehaviour
{
    public string playerName;
    public string bestPlayerName = "Name";
    public int bestScore = 0;
    public static DataManager Instance;

    private void Start()
    {
        
    }
    private void Awake()
    {
        if(Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    [System.Serializable]
    class BestPlayerData
    {
        public string bestPlayerName;
        public int bestScore;
    }

    public void Compare(int currentScore, int pastScore, string currentName)
    {
        if(currentScore >= pastScore)
        {
            bestScore = currentScore;
            bestPlayerName = currentName;
        }
    }

    public void SaveData()
    {
        BestPlayerData data = new BestPlayerData();
        data.bestPlayerName = bestPlayerName;
        data.bestScore = bestScore;
        string json = JsonUtility.ToJson(data);
        File.WriteAllText(Application.persistentDataPath + "/savefile.json", json);
    }

    public void LoadData()
    {
        string path = Application.persistentDataPath + "/savefile.json";
        if (File.Exists(path))
        {
            string json = File.ReadAllText(path);
            BestPlayerData data = JsonUtility.FromJson<BestPlayerData>(json);
            bestPlayerName = data.bestPlayerName;
            bestScore = data.bestScore;
        }
    }
    public void ReadName(string s)
    {
        DataManager.Instance.playerName = s;
        Debug.Log(DataManager.Instance.playerName);
    }
}
