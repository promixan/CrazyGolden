using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using Menu.DifficultyButtons;
using UnityEngine;
using UnityEngine.UI;

namespace UI
{
    public class ScrollAreaResultsBuilder : DifficultyButtonOrchestrator
    {
        [SerializeField] private GameObject scrollAreaExample;
        [SerializeField] private ScrollRect scrollRect;
        private Transform _scrollArea;

        private void Awake()
        {
            scrollRect = GetComponentInParent<ScrollRect>();
        }

        private void Start()
        {
            _scrollArea = gameObject.transform;
            for (var i = 0; i < difficultyButtons.Length; i++)
            {
                var button = difficultyButtons[i];
                var id = i;
                button.onClick.AddListener(() => RenderScrollAreaContent(id));
            }
            
            RenderScrollAreaContent(0);
            ChooseDifficultyButton(0);
        }

        private void RenderScrollAreaContent(int difficultyId)
        {
            ClearScrollArea();

            var bestScores = CrossSceneManager.Instance.BestScores
                .Where(score => score.Difficulty == difficultyId)
                .ToList();
            for (var i = 0; i < bestScores.Count; i++)
            {
                var score = bestScores[i];
                var content = Instantiate(scrollAreaExample, _scrollArea);
                var builder = content.GetComponent<ScrollAreaContentBuilder>();
                builder.Build(i + 1, score);
                content.SetActive(true);
            }
            StartCoroutine(ScrollToTop());
            
            ChooseDifficultyButton(difficultyId);
        }

        private void ClearScrollArea()
        {
            for (var i = _scrollArea.childCount - 1; i >= 1; i--)
            {
                Destroy( _scrollArea.GetChild(i).gameObject);
            } 
        }
        
        private IEnumerator ScrollToTop()
        {
            yield return new WaitForEndOfFrame();
            scrollRect.verticalNormalizedPosition = 1f;
        }
    
    }
}
