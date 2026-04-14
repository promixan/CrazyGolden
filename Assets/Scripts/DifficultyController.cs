using System;
using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(Button))]
public class DifficultyController : MonoBehaviour
{
    private Button button;
    private DifficultyManager difficultyManager;

    public int difficulty;

    void Start()
    {
        button = GetComponent<Button>();
        button.onClick.AddListener(SetDifficulty);
        difficultyManager = GameObject.Find("Difficulty Manager").GetComponent<DifficultyManager>();
        if (difficultyManager.GetCurrentDifficulty() == difficulty)
        {
            button.Select();
        }
    }

    private void SetDifficulty()
    {
        difficultyManager.SetCurrentDifficulty(difficulty);
    }

}
