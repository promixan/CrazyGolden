using TMPro;
using UnityEngine;

public class ResultsHandler : MonoBehaviour
{
    public static ResultsHandler Instance;

    public TMP_Text score;

    public int CurrentScore { get; private set; }

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }

        Instance = this;
        RefreshDisplaedScore();
    }

    void Start()
    {
        ResetScore();
    }

    public void UpdateScore(int value)
    {
        CurrentScore += value;
        RefreshDisplaedScore();
    }

    public void SetScore(int value)
    {
        CurrentScore = value;
        RefreshDisplaedScore();
    }

    public void ResetScore()
    {
        CurrentScore = 0;
        RefreshDisplaedScore();
    }

    private void RefreshDisplaedScore()
    {
        score.text = CurrentScore.ToString();
    }
}
