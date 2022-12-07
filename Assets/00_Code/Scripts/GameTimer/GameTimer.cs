// Author: Ignacio María Muñoz Márquez

using UnityEngine;

namespace MahjonggDimensions
{
    public class GameTimer : MonoBehaviour
    {
        public delegate void GameTimerEvent();

        public event GameTimerEvent OnGameTimerEnd;

        public bool Active { get; private set; }
        public float TimeLeft { get; private set; }
        public float AlmostFinishedTime { get { return _almostFinishedTime; } }

        [SerializeField]
        private float _startingTime;
        [SerializeField]
        private float _almostFinishedTime;

        private bool _hasPlayedAlmostFinishedSound = false;

        public void StartTimer()
        {
            Active = true;
            TimeLeft = _startingTime;
            _hasPlayedAlmostFinishedSound = false;
        }

        public void PauseTimer()
        {
            Active = false;
        }

        public void ResumeTimer()
        {
            Active = true;
        }

        public void StopTimer()
        {
            Active = false;
            TimeLeft = 0f;
        }

        private void Update()
        {
            if(Active && TimeLeft > 0f)
            {
                TimeLeft -= Time.deltaTime;

                if(!_hasPlayedAlmostFinishedSound && TimeLeft <= _almostFinishedTime)
                {
                    _hasPlayedAlmostFinishedSound = true;

                    SoundManager.Instance.PlayFX(SoundManager.Instance.GameAudioClips.TimeAlmostFinished);
                }

                if(TimeLeft <= 0f)
                {
                    TimeLeft = 0f;

                    OnGameTimerEnd?.Invoke();
                }
            }
        }
    }
}
