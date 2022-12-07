// Author: Ignacio María Muñoz Márquez

using UnityEngine;
using TMPro;

namespace MahjonggDimensions
{
    public class GameTimerUI : MonoBehaviour
    {
        [SerializeField]
        private GameTimer _gameTimer;
        [SerializeField]
        private TextMeshProUGUI _timeLeftText;

        private void Awake()
        {
            SetTimeLeftColor(Color.white);
        }

        private void OnEnable()
        {
            _gameTimer.OnGameTimerEnd += OnGameTimerEnd;
        }

        private void OnDisable()
        {
            _gameTimer.OnGameTimerEnd -= OnGameTimerEnd;
        }

        private void Update()
        {
            UpdateTimeLeftText();

            SetTimeLeftColor(_gameTimer.TimeLeft <= _gameTimer.AlmostFinishedTime ? Color.red : Color.white);
        }

        private void SetTimeLeftColor(Color color)
        {
            _timeLeftText.color = color;
        }

        private void OnGameTimerEnd()
        {
            SetTimeLeftColor(Color.red);
        }

        private void UpdateTimeLeftText()
        {
            float minuteDecimal = _gameTimer.TimeLeft / 60f;
            float minute = (int)minuteDecimal;
            int seconds = (int)((minuteDecimal - minute) * 60f);

            _timeLeftText.text = minute + ":" + (seconds < 10 ? "0" + seconds : seconds);
        }
    }
}
