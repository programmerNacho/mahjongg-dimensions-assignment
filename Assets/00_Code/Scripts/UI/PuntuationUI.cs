// Author: Ignacio María Muñoz Márquez

using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace MahjonggDimensions
{
    public class PuntuationUI : MonoBehaviour
    {
        [SerializeField]
        private Puntuation _puntuation;
        [SerializeField]
        private TextMeshProUGUI _puntuationText;
        [SerializeField]
        private Image _multiplierTimeFill;
        [SerializeField]
        private TextMeshProUGUI _multiplierText;

        private void Awake()
        {
            _puntuationText.text = 0.ToString();
            _multiplierTimeFill.fillAmount = 1f;
            _multiplierText.text = 1.ToString();
        }

        private void OnEnable()
        {
            _puntuation.OnPuntuationAdded += UpdatePuntuation;
            _puntuation.OnMultiplierAdded += UpdateMultiplier;
            _puntuation.OnMultiplierReset += UpdateMultiplier;
        }

        private void OnDisable()
        {
            _puntuation.OnPuntuationAdded -= UpdatePuntuation;
            _puntuation.OnMultiplierAdded -= UpdateMultiplier;
            _puntuation.OnMultiplierReset -= UpdateMultiplier;
        }

        private void Update()
        {
            UpdateMultiplierTime();
        }

        private void UpdatePuntuation()
        {
            _puntuationText.text = _puntuation.TotalPuntuation.ToString();
        }

        private void UpdateMultiplierTime()
        {
            if(_puntuation.TimeLeftToAchieveMoreMultiplier <= 0f)
            {
                _multiplierTimeFill.fillAmount = 1f;
            }
            else
            {
                _multiplierTimeFill.fillAmount = _puntuation.TimeLeftToAchieveMoreMultiplier / _puntuation.TimeToAchieveMoreMultiplier;
            }
        }

        private void UpdateMultiplier()
        {
            _multiplierText.text = _puntuation.Multiplier.ToString();
        }
    }
}
