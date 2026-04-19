using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CrossSceneManager : MonoBehaviour
{
    public static CrossSceneManager Instance;
    public DifficultiesDatabase DifficultiesDatabase;

    public const int MaxPlayerNameLength = 10;

    protected string PlayerDataPath;
    protected string BestScoresDataPath;

    public string DefaultPlayerName {get; private set;} = "Unnamed";

    private string m_playerName;
    public string PlayerName
    {
        get
        {
            if (string.IsNullOrEmpty(m_playerName))
            {
                return DefaultPlayerName;
            }
            return m_playerName;
        }
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                m_playerName = DefaultPlayerName;
                return;
            }

            if (value.Length > MaxPlayerNameLength)
            {
                throw new ArgumentException("Maximum characters allowed: " + MaxPlayerNameLength);
            }

            m_playerName = value;
        }
    }
    public DifficultyData Difficulty { get; private set; }
    public List<ScoreData> BestScores { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        DontDestroyOnLoad(gameObject);
        BestScores = new();
        PlayerDataPath = Application.persistentDataPath + "/PlayerData.json";
        BestScoresDataPath = Application.persistentDataPath + "/BestScores.json";
        LoadAllData();
    }

    public void SaveAllData()
    {
        SavePlayerData();
        SaveBestScoresData();
    }

    public void LoadAllData()
    {
        LoadPlayerData();
        LoadBestScoresData();
    }

    public void SavePlayerData()
    {
        PlayerData data = new()
        {
            Name = PlayerName,
            LastChoosenDifficulty = Difficulty.Id
        };

        string json = JsonUtility.ToJson(data);
        File.WriteAllText(PlayerDataPath, json);
    }

    public void SaveBestScoresData()
    {
        string json = JsonUtility.ToJson(BestScores);
        File.WriteAllText(BestScoresDataPath, json);
    }

    public void LoadPlayerData()
    {
        if (File.Exists(PlayerDataPath))
        {
            string json = File.ReadAllText(PlayerDataPath);
            PlayerData data = JsonUtility.FromJson<PlayerData>(json);
            if (data != null)
            {
                if (!string.IsNullOrEmpty(data.Name))
                {
                    PlayerName = data.Name;
                }

                Difficulty = DifficultiesDatabase.GetById(data.LastChoosenDifficulty);
            }
        }
    }

    public void LoadBestScoresData()
    {
        if (File.Exists(BestScoresDataPath))
        {
            string json = File.ReadAllText(BestScoresDataPath);
            List<ScoreData> scores = JsonUtility.FromJson<List<ScoreData>>(json);
            if (scores != null && scores.Count > 0)
            {
                BestScores = scores;
            }
        }
    }

}
