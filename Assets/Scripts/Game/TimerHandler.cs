using System.Collections;
using Data;
using TMPro;
using UnityEngine;

namespace Game
{
    public class TimerHandler : MonoBehaviour
    {
        [SerializeField] private TMP_Text timerText;

        private GameManager _gameManager;
        private Coroutine _timerRoutine;
        private int _currentDefaultValue;
        private int _timerValue;
    
        private void Start()
        {
            _gameManager = GameObject.Find("Game Manager").GetComponent<GameManager>();
            ServiceLocator.Register(this);
            UpdateDefaultTimerValue(GameConstants.Gameplay.DefaultGameStartTimerValue);
        }

        public void StartTimer()
        {
            UpdateCurrentTimerValue(_currentDefaultValue);
            if (_timerRoutine != null)
            {
                StopCoroutine(_timerRoutine);
            }
            _timerRoutine = StartCoroutine(TimerRoutine());
        }
    
        IEnumerator TimerRoutine()
        {
            while (_gameManager.IsGameActive())
            {
                if (_timerValue <= 0)
                {
                    _gameManager.GameOver();
                }
                yield return new WaitForSeconds(1.0f);
                if (!_gameManager.IsGameActive()) continue;
                UpdateCurrentTimerValue(--_timerValue);
            }
        }

        private void UpdateCurrentTimerValue(int timerValue)
        {
            _timerValue = timerValue;
            timerText.text = _timerValue + " s";
        }

        public void AppendCurrentTimerTime(int timeToAppend)
        {
            _timerValue += timeToAppend;
            timerText.text = _timerValue + " s";
        }
        
        public void DecreaseCurrentTimerTime(int timeToAppend)
        {
            _timerValue -= timeToAppend;
            timerText.text = _timerValue + " s";
        }
    
        private void UpdateDefaultTimerValue(int timerValue)
        {
            _currentDefaultValue = timerValue;
        }
    }
}
