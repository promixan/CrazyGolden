using UnityEngine;
using UnityEngine.UI;

public class SettingsController : MonoBehaviour
{

    public DifficultiesDatabase DifficultiesDatabase;

    [SerializeField] private Button[] difficultyButtons;
    [SerializeField] private Color defaultButtonColor;
    [SerializeField] private Color pressedButtonColor;

    void Start()
    {
        for (int i = 0; i < difficultyButtons.Length; i++)
        {
            Button button = difficultyButtons[i];
            var id = i;
            button.onClick.AddListener(() => SelectDifficulty(id));
        }
        DifficultyData difficulty = CrossSceneManager.Instance.Difficulty;
        ChooseDifficultyButton(difficulty.Id);
    }

    public void SelectDifficulty(int difficultyIndex)
    {
        CrossSceneManager.Instance.SelectDifficulty(difficultyIndex);
        ChooseDifficultyButton(difficultyIndex);
    }

    private void ChooseDifficultyButton(int difficultyIndex)
    {
        for (int i = 0; i < difficultyButtons.Length; i++)
        {
            Button button = difficultyButtons[i];
            var buttonColors = button.colors;
            if (i == difficultyIndex)
            {
                buttonColors.normalColor = pressedButtonColor;
            } else
            {
                buttonColors.normalColor = defaultButtonColor;
            }
            button.colors = buttonColors;
        }
    }
}
