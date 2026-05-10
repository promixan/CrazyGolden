using System;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class ResultsHandler : MonoBehaviour
{
    private const int MaxBestScoresNumber = 10;
    
    private CrossSceneManager _crossSceneManager;
    private int CurrentScore { get; set; }
    
    public TMP_Text score;

    private void Start()
    {
        _crossSceneManager = CrossSceneManager.Instance;
        ServiceLocator.Register(this);
        ResetScore();
    }

    private void OnDestroy()
    {
        ServiceLocator.Unregister<ResultsHandler>();
    }

    public void UpdateScore(int value)
    {
        CurrentScore += value;
        RefreshDisplayedScore();
    }

    public void ApplyScore()
    {
        var bestScores = CrossSceneManager.Instance.BestScores;
        var currentScore = new ScoreData
        {
            Score = CurrentScore,
            PlayerName = _crossSceneManager.PlayerName,
            Difficulty = _crossSceneManager.Difficulty.Id
        };
        bestScores.Add(currentScore);
        var sortedList = bestScores.OrderBy(s => s.Score)
            .Reverse()
            .ToList();
        RemoveIfExceeds(sortedList);
        _crossSceneManager.UpdateBestScores(sortedList);
    }

    public void SetScore(int value)
    {
        CurrentScore = value;
        RefreshDisplayedScore();
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        RefreshDisplayedScore();
    }

    private void RefreshDisplayedScore()
    {
        score.text = CurrentScore.ToString();
    }

    public void RemoveIfExceeds(List<ScoreData> list)
    {
        while (true)
        {
            if (list.Count() > MaxBestScoresNumber)
            {
                list.RemoveAt(list.Count() - 1);
                continue;
            }
            break;
        }
    }
}
