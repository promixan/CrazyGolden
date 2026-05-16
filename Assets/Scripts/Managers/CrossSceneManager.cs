using System;
using System.Collections.Generic;
using System.IO;
using Data;
using UnityEngine;

public class CrossSceneManager : MonoBehaviour
{
    public static CrossSceneManager Instance;
    public DifficultiesDatabase DifficultiesDatabase;
    
    protected string PlayerDataPath;
    protected string BestScoresDataPath;

    public string DefaultPlayerName {get; private set;} = "Unnamed";

    private string m_playerName;
    public string PlayerName
    {
        get => string.IsNullOrEmpty(m_playerName) ? DefaultPlayerName : m_playerName;
        set
        {
            if (string.IsNullOrEmpty(value))
            {
                m_playerName = DefaultPlayerName;
                return;
            }

            if (value.Length > GameConstants.Player.MaxPlayerNameLength)
            {
                throw new ArgumentException("Maximum characters allowed: " + GameConstants.Player.MaxPlayerNameLength);
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

    public void SelectDifficulty(int difficultyIndex)
    {
        var difficulty = DifficultiesDatabase.GetById(difficultyIndex);
        if (difficulty != null)
        {
            Difficulty = difficulty;
        }
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

    private void SaveBestScoresData()
    {
        var bestScores = new BestScores
        {
            scores = BestScores
        };
        var json = JsonUtility.ToJson(bestScores);
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

                SelectDifficulty(data.LastChoosenDifficulty);
            }
        }
    }

    private void LoadBestScoresData()
    {
        if (!File.Exists(BestScoresDataPath)) return;
        var json = File.ReadAllText(BestScoresDataPath);
        var bestScores = JsonUtility.FromJson<BestScores>(json);
        if (bestScores is { scores: { Count: > 0 } })
        {
            BestScores = bestScores.scores;
        }
    }

    public void UpdateBestScores(List<ScoreData> scores)
    {
        if (scores is { Count: > 0 })
        {
            BestScores = scores;
        }
    }

    public DifficultiesDatabase GetDifficultiesDatabase()
    {
        return DifficultiesDatabase;
    }
}
