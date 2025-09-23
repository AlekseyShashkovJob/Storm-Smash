using UnityEngine;

namespace Misc.Services
{
    public class MusicManager : MonoBehaviour
    {
        [SerializeField] private AudioClip _musicClip;
        private AudioSource _audioSource;

        private void Awake()
        {
            DontDestroyOnLoad(gameObject);
            _audioSource = gameObject.AddComponent<AudioSource>();
            _audioSource.clip = _musicClip;
            _audioSource.loop = true;
            UpdateMusicState();
        }

        private void OnEnable()
        {
            View.UI.Menu.OptionsScreen.OnSoundStateChanged += UpdateMusicState;
        }

        private void OnDisable()
        {
            View.UI.Menu.OptionsScreen.OnSoundStateChanged -= UpdateMusicState;
        }

        private void UpdateMusicState()
        {
            bool isSoundOn = PlayerPrefs.GetInt(PlayerPrefsKeys.SoundOn, 1) == 1;
            if (isSoundOn)
            {
                if (!_audioSource.isPlaying)
                    _audioSource.Play();
            }
            else
            {
                _audioSource.Stop();
            }
        }
    }
}