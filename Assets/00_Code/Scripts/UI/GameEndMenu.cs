// Author: Ignacio María Muñoz Márquez

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MahjonggDimensions
{
    public class GameEndMenu : MonoBehaviour
    {
        [SerializeField]
        private GameObject _panel;
        [SerializeField]
        private TextMeshProUGUI _titleText;
        [SerializeField]
        private Button _actionButton;

        private TextMeshProUGUI _actionButtonText;

        private const string _titleWinText = "Level Completed!";
        private const string _titleLoseText = "Time's up!";
        private const string _actionButtonWinText = "Next Level";
        private const string _actionButtonLoseText = "Restart";

        private void Awake()
        {
            _panel.SetActive(false);

            _actionButtonText = _actionButton.GetComponentInChildren<TextMeshProUGUI>();
        }

        private void OnEnable()
        {
            GameManager.OnFigureGroupCompleted += ShowWin;
            GameManager.OnGameLose += ShowLose;
        }

        private void OnDisable()
        {
            GameManager.OnFigureGroupCompleted -= ShowWin;
            GameManager.OnGameLose -= ShowLose;
        }

        private void ShowWin()
        {
            GameManager.GamePaused = true;
            _panel.SetActive(true);

            _titleText.text = _titleWinText;

            _actionButton.onClick.AddListener(LoadNextLevel);
            _actionButtonText.text = _actionButtonWinText;
        }

        private void ShowLose()
        {
            GameManager.GamePaused = true;
            _panel.SetActive(true);

            _titleText.text = _titleLoseText;

            _actionButton.onClick.AddListener(RestartGame);
            _actionButtonText.text = _actionButtonLoseText;
        }

        private void LoadNextLevel()
        {
            _actionButton.onClick.RemoveListener(LoadNextLevel);

            GameManager.GamePaused = false;
            _panel.SetActive(false);

            GameManager.LoadNextLevel();
        }

        private void RestartGame()
        {
            _actionButton.onClick.RemoveListener(RestartGame);

            GameManager.GamePaused = false;
            _panel.SetActive(false);

            GameManager.RestartGame();
        }
    }
}
