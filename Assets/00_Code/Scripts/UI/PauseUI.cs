// Author: Ignacio María Muñoz Márquez

using UnityEngine;
using UnityEngine.UI;

namespace MahjonggDimensions
{
    public class PauseUI : MonoBehaviour
    {
        [SerializeField]
        private Button _pauseButton;
        [SerializeField]
        private Button _resumeButton;
        [SerializeField]
        private Button _restartButton;
        [SerializeField]
        private GameObject _pauseMenu;

        private void OnEnable()
        {
            _pauseButton.onClick.AddListener(Pause);
            _resumeButton.onClick.AddListener(Resume);
            _restartButton.onClick.AddListener(Restart);
        }

        private void OnDisable()
        {
            _pauseButton.onClick.RemoveListener(Pause);
            _resumeButton.onClick.RemoveListener(Resume);
            _restartButton.onClick.RemoveListener(Restart);
        }

        private void Awake()
        {
            Resume();
        }

        private void Pause()
        {
            GameManager.GamePaused = true;

            _pauseMenu.SetActive(GameManager.GamePaused);
        }

        private void Resume()
        {
            GameManager.GamePaused = false;

            _pauseMenu.SetActive(GameManager.GamePaused);
        }

        private void Restart()
        {
            GameManager.RestartGame();
        }
    }
}
