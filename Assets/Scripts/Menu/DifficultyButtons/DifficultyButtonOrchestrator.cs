using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Menu.DifficultyButtons
{
    public class DifficultyButtonOrchestrator : MonoBehaviour
    {
        [SerializeField] protected Button[] difficultyButtons;
        [SerializeField] protected Color defaultButtonColor;
        [SerializeField] protected Color pressedButtonColor;

        protected void ChooseDifficultyButton(int difficultyIndex)
        {
            for (var i = 0; i < difficultyButtons.Length; i++)
            {
                var button = difficultyButtons[i];
                var buttonColors = button.colors;
                buttonColors.normalColor = i == difficultyIndex ? pressedButtonColor : defaultButtonColor;
                button.colors = buttonColors;
            }
        }
    }
}