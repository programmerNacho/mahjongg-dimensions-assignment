// Author: Ignacio María Muñoz Márquez

using UnityEngine;
using UnityEngine.UI;

namespace MahjonggDimensions
{
    public class AudioVolumeButtonToggle : MonoBehaviour
    {
        private enum AudioType { Music, FX }

        [SerializeField]
        private AudioType _audioType;
        [SerializeField]
        private Sprite _onSprite;
        [SerializeField]
        private Sprite _offSprite;

        private Image _image;
        private Button _button;

        private void Awake()
        {
            _image = GetComponentInChildren<Image>();
            _button = GetComponentInChildren<Button>();
        }

        private void OnEnable()
        {
            RefreshImage();

            _button.onClick.AddListener(ToggleAudio);
        }

        private void OnDisable()
        {
            _button.onClick.RemoveListener(ToggleAudio);
        }

        private void RefreshImage()
        {
            switch (_audioType)
            {
                case AudioType.Music:
                    _image.sprite = SoundManager.Instance.MusicMuted ? _offSprite : _onSprite;
                    break;
                case AudioType.FX:
                    _image.sprite = SoundManager.Instance.FXMuted ? _offSprite : _onSprite;
                    break;
            }
        }

        private void ToggleAudio()
        {
            switch (_audioType)
            {
                case AudioType.Music:
                    SoundManager.Instance.MusicMuted = !SoundManager.Instance.MusicMuted;
                    break;
                case AudioType.FX:
                    SoundManager.Instance.FXMuted = !SoundManager.Instance.FXMuted;
                    break;
            }

            RefreshImage();
        }
    }
}
