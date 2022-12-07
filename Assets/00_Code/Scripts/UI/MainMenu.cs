// Author: Ignacio María Muñoz Márquez

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

namespace MahjonggDimensions
{
    public class MainMenu : MonoBehaviour
    {
        [SerializeField]
        private Button _playButton;
        [SerializeField]
        private GameObject _loading;

        private const string _gameSceneName = "Game";

        private void Awake()
        {
            _loading.SetActive(false);
        }

        private void OnEnable()
        {
            _playButton.onClick.AddListener(LoadGame);
        }

        private void OnDisable()
        {
            _playButton.onClick.RemoveListener(LoadGame);
        }

        private void LoadGame()
        {
            _playButton.interactable = false;
            _loading.SetActive(true);

            SceneManager.LoadSceneAsync(_gameSceneName, LoadSceneMode.Single);

            SceneManager.sceneLoaded += UnloadMainMenu;
        }

        private void UnloadMainMenu(Scene scene, LoadSceneMode loadSceneMode)
        {
            SceneManager.sceneLoaded -= UnloadMainMenu;

            if (scene.name != _gameSceneName)
            {
                _playButton.interactable = true;
                _loading.SetActive(false);

                SceneManager.UnloadSceneAsync(scene.name);
                return;
            }

            SceneManager.SetActiveScene(scene);
        }
    }
}
