// Author: Ignacio María Muñoz Márquez

using UnityEngine;

namespace MahjonggDimensions
{
    [CreateAssetMenu(fileName = "GameAudioClips", menuName = "MahjonggDimensions/Audio/GameAudioClips", order = 1)]
    public class GameAudioClips : ScriptableObject
    {
        public AudioClip GameMusic { get { return _gameMusic; } }
        public AudioClip ButtonClick { get { return _buttonClick; } }
        public AudioClip TimeAlmostFinished { get { return _timeAlmostFinished; } }
        public AudioClip FigureSelected { get { return _figureSelected; } }
        public AudioClip FigureCannotSelect { get { return _figureCannotSelect; } }
        public AudioClip FigurePairCompleted { get { return _figurePairCompleted; } }
        public AudioClip GameWin { get { return _gameWin; } }
        public AudioClip GameLose { get { return _gameLose; } }

        [SerializeField]
        private AudioClip _gameMusic;
        [SerializeField]
        private AudioClip _buttonClick;
        [SerializeField]
        private AudioClip _timeAlmostFinished;
        [SerializeField]
        private AudioClip _figureSelected;
        [SerializeField]
        private AudioClip _figureCannotSelect;
        [SerializeField]
        private AudioClip _figurePairCompleted;
        [SerializeField]
        private AudioClip _gameWin;
        [SerializeField]
        private AudioClip _gameLose;
    }
}
