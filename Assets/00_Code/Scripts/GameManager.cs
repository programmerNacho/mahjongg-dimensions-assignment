// Author: Ignacio María Muñoz Márquez

using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace MahjonggDimensions
{
    public class GameManager : MonoBehaviour
    {
        public delegate void GameManagerEvent();

        public static bool GamePaused
        {
            get
            {
                return Time.timeScale <= 0f;
            }

            set
            {
                Time.timeScale = value ? 0f : 1f;
            }
        }

        public static event GameManagerEvent OnFigureGroupCompleted;
        public static event GameManagerEvent OnGameLose;
        private static event GameManagerEvent OnLoadNextLevel;

        [SerializeField]
        private FiguresGroupCreator _figuresGroupCreator;
        [SerializeField]
        private FigureSelector _figureSelector;
        [SerializeField]
        private GameTimer _gameTimer;

        private static Figure[,,] _figureGroup;
        private static int _pairsLeftToComplete;

        private void Awake()
        {
            CreateFiguresGroup();
            _gameTimer.StartTimer();
        }

        private void OnEnable()
        {
            _figureSelector.OnFigurePairCompleted += FigurePairCompleted;
            _gameTimer.OnGameTimerEnd += LoseGame;
            OnLoadNextLevel += CreateFiguresGroup;
        }

        private void OnDisable()
        {
            _figureSelector.OnFigurePairCompleted -= FigurePairCompleted;
            _gameTimer.OnGameTimerEnd -= LoseGame;
            OnLoadNextLevel -= CreateFiguresGroup;
        }

        private void CreateFiguresGroup()
        {
            _figureGroup = _figuresGroupCreator.CreateFiguresGroup(out _pairsLeftToComplete);
        }

        private void FigurePairCompleted()
        {
            _pairsLeftToComplete -= 1;

            if(_pairsLeftToComplete <= 0f)
            {
                // Hacky way of calling the event, but it was giving me problems with
                // the order of events.
                StopAllCoroutines();
                StartCoroutine(WaitAFrameAndInvokeFigureGroupCompleted());
            }
        }

        private IEnumerator WaitAFrameAndInvokeFigureGroupCompleted()
        {
            yield return null;
            OnFigureGroupCompleted?.Invoke();
            SoundManager.Instance.PlayFX(SoundManager.Instance.GameAudioClips.GameWin);
        }

        private void LoseGame()
        {
            OnGameLose?.Invoke();
            SoundManager.Instance.PlayFX(SoundManager.Instance.GameAudioClips.GameLose);
        }

        public static void LoadNextLevel()
        {
            OnLoadNextLevel?.Invoke();
        }

        public static void RestartGame()
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
