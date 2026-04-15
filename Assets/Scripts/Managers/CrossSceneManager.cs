using System;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class CrossSceneManager : MonoBehaviour
{
    public static CrossSceneManager Instance;

    public const int MaxPlayerNameLenght = 10;

    protected string PlayerDataPath;
    protected string BestScoresDataPath;

    public string PlayerName
    {
        get => PlayerName;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                PlayerName = "Unnamed";
            }

            if (value.Length > MaxPlayerNameLenght)
            {
                throw new ArgumentException("Maximum characters allowed: " + MaxPlayerNameLenght);
            }

            PlayerName = value;
        }
    }
    public int Difficulty { get; private set; }
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
        PlayerDataPath = Application.persistentDataPath + "playedData.json";
        BestScoresDataPath = Application.persistentDataPath + "bestScores.json";
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
            LastChoosenDifficulty = Difficulty
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
                    PlayerName = name;
                }
                Difficulty = data.LastChoosenDifficulty;
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
