using Menu.DifficultyButtons;

namespace Menu
{
    public class SettingsController : DifficultyButtonOrchestrator
    {
        private void Start()
        {
            for (var i = 0; i < difficultyButtons.Length; i++)
            {
                var button = difficultyButtons[i];
                var id = i;
                button.onClick.AddListener(() => SelectDifficulty(id));
            }
        
            var difficulty = CrossSceneManager.Instance.Difficulty;
            ChooseDifficultyButton(difficulty.Id);
        }

        private void SelectDifficulty(int difficultyIndex)
        {
            CrossSceneManager.Instance.SelectDifficulty(difficultyIndex);
            ChooseDifficultyButton(difficultyIndex);
        }
    }
}
