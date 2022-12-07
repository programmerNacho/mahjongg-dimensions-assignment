// Author: Ignacio María Muñoz Márquez

using UnityEngine;
using UnityEngine.UI;

namespace MahjonggDimensions
{
    public class ButtonClickSound : MonoBehaviour
    {
        private Button _button;

        private void Awake()
        {
            _button = GetComponent<Button>();
        }

        private void OnEnable()
        {
            _button.onClick.AddListener(PlaySound);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(PlaySound);
        }

        private void PlaySound()
        {
            SoundManager.Instance.PlayFX(SoundManager.Instance.GameAudioClips.ButtonClick);
        }
    }
}
