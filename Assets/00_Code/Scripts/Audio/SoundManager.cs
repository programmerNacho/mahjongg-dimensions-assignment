// Author: Ignacio María Muñoz Márquez

using UnityEngine;
using UnityEngine.Audio;

namespace MahjonggDimensions
{
    public class SoundManager : MonoBehaviour
    {
        public static SoundManager Instance { get; private set; }

        public bool MusicMuted
        {
            get
            {
                _audioMixer.GetFloat("MusicVolume", out float value);

                return value == -80f;
            }

            set
            {
                _audioMixer.SetFloat("MusicVolume", value ? -80f : 0f);
            }
        }

        public bool FXMuted
        {
            get
            {
                _audioMixer.GetFloat("FXVolume", out float value);

                return value == -80f;
            }

            set
            {
                _audioMixer.SetFloat("FXVolume", value ? -80f : 0f);
            }
        }

        public GameAudioClips GameAudioClips { get { return _gameAudioClips; } }

        [SerializeField]
        private AudioMixer _audioMixer;
        [SerializeField]
        private AudioSource _musicAudioSource;
        [SerializeField]
        private AudioSource _fxAudioSource;
        [SerializeField]
        private GameAudioClips _gameAudioClips;

        private void Awake()
        {
            if(Instance != null && Instance != this)
            {
                Destroy(gameObject);
            }
            else
            {
                Instance = this;

                MusicMuted = false;
                FXMuted = false;

                PlayMusic(_gameAudioClips.GameMusic);

                DontDestroyOnLoad(gameObject);
            }
        }

        public void PlayMusic(AudioClip musicClip)
        {
            _musicAudioSource.clip = musicClip;
            _musicAudioSource.Play();
        }

        public void PlayFX(AudioClip fxClip)
        {
            _fxAudioSource.PlayOneShot(fxClip);
        }
    }
}
