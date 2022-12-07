// Author: Ignacio María Muñoz Márquez

using UnityEngine;

namespace MahjonggDimensions
{
    public class Puntuation : MonoBehaviour
    {
        public delegate void PuntuationEvent();

        public int TotalPuntuation { get; private set; }
        public float TimeToAchieveMoreMultiplier { get { return _timeToAchieveMoreMultiplier; } }
        public float TimeLeftToAchieveMoreMultiplier { get { return _timeLeftToAchieveMoreMultiplier; } }
        public int Multiplier { get; private set; }

        public PuntuationEvent OnPuntuationAdded;
        public PuntuationEvent OnMultiplierAdded;
        public PuntuationEvent OnMultiplierReset;

        [SerializeField]
        private FigureSelector _figureSelector;
        [SerializeField]
        private int _basePointsPerFigureCompleted;
        [SerializeField]
        private float _timeToAchieveMoreMultiplier;

        private float _timeLeftToAchieveMoreMultiplier;
        private bool _noMultiplierPuntuation;

        private void Awake()
        {
            TotalPuntuation = 0;

            ResetMultiplier();
        }

        private void OnEnable()
        {
            GameManager.OnFigureGroupCompleted += ResetMultiplier;
            GameManager.OnGameLose += ResetMultiplier;

            _figureSelector.OnFigurePairCompleted += CheckMultiplier;
        }

        private void OnDisable()
        {
            GameManager.OnFigureGroupCompleted -= ResetMultiplier;
            GameManager.OnGameLose -= ResetMultiplier;

            _figureSelector.OnFigurePairCompleted -= CheckMultiplier;
        }

        private void Update()
        {
            CheckMultiplierReset();
        }

        private void ResetMultiplier()
        {
            _noMultiplierPuntuation = true;
            Multiplier = 1;
            _timeLeftToAchieveMoreMultiplier = 0f;

            OnMultiplierReset?.Invoke();
        }

        private void CheckMultiplier()
        {
            if(_noMultiplierPuntuation)
            {
                _noMultiplierPuntuation = false;
            }
            else
            {
                Multiplier += 1;

                OnMultiplierAdded?.Invoke();
            }

            TotalPuntuation += _basePointsPerFigureCompleted * Multiplier;
            _timeLeftToAchieveMoreMultiplier = _timeToAchieveMoreMultiplier;

            OnPuntuationAdded?.Invoke();
        }

        private void CheckMultiplierReset()
        {
            if (!_noMultiplierPuntuation)
            {
                _timeLeftToAchieveMoreMultiplier -= Time.deltaTime;

                if (_timeLeftToAchieveMoreMultiplier <= 0f)
                {
                    ResetMultiplier();
                }
            }
        }
    }
}
