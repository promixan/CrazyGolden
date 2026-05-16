using TMPro;
using UnityEngine;

namespace UI
{
    public class ScrollAreaContentBuilder : MonoBehaviour
    {
        [SerializeField] private TMP_Text rank;
        [SerializeField] private TMP_Text userName;
        [SerializeField] private TMP_Text difficulty;
        [SerializeField] private TMP_Text score;

        public void Build(int place, ScoreData scoreData)
        {
            rank.text = place.ToString();
            userName.text = scoreData.PlayerName;
            score.text = scoreData.Score.ToString();
            var difficultyData = CrossSceneManager.Instance.GetDifficultiesDatabase().GetById(scoreData.Difficulty);
            if (difficultyData == null) return;
            difficulty.text = difficultyData.Name;
            difficulty.color = difficultyData.NameColor;
        }
    }
}