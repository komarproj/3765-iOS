using UniRx;
using UnityEngine;
using Zenject;

namespace Game.UserData
{
    public class AudioManager : MonoBehaviour
    {
        public static AudioManager Instance;
        
        [SerializeField] private AudioSource _musicSource;
        [SerializeField] private AudioSource _soundSource;

        [SerializeField, Space] private AudioClip _mainOst;
        [SerializeField] private AudioClip _gameOst;

        [SerializeField, Space] private AudioClip _buttonSound;
        [SerializeField] private AudioClip _clockSound;
        [SerializeField] private AudioClip _eggSound;
        [SerializeField] private AudioClip _errorSound;
        [SerializeField] private AudioClip _freezeSound;
        [SerializeField] private AudioClip _healSound;
        [SerializeField] private AudioClip _incubatorSound;
        [SerializeField] private AudioClip _mergeErrorSound;
        [SerializeField] private AudioClip _mergeSound;
        [SerializeField] private AudioClip _pageChangeSound;
        [SerializeField] private AudioClip _popupSound;
        [SerializeField] private AudioClip _coinsSound;
        [SerializeField] private AudioClip _rouletteSound;
        [SerializeField] private AudioClip _timeStopSound;
        [SerializeField] private AudioClip _successSound;

        private void Awake()
        {
            Instance = this;
        }

        [Inject]
        private void Construct(SaveSystem saveSystem)
        {
            var settings = saveSystem.Data.SettingsData;

            settings.IsMusicOn.Subscribe(value =>
            {
                _musicSource.volume = value ? 1f : 0f;
            }).AddTo(gameObject);

            settings.IsSoundOn.Subscribe(value =>
            {
                _soundSource.volume = value ? 1f : 0f;
            }).AddTo(gameObject);
        }
        
        public void SetMenuMusic()
        {
            _musicSource.clip = _mainOst;
            _musicSource.Play();
        }

        public void SetGameMusic()
        {
            _musicSource.clip = _gameOst;
            _musicSource.Play();
        }

        public void PlayButtonSound() => _soundSource.PlayOneShot(_buttonSound);

        public void PlayClockSound() => _soundSource.PlayOneShot(_clockSound);
        public void PlayEggSound() => _soundSource.PlayOneShot(_eggSound);
        public void PlayErrorSound() => _soundSource.PlayOneShot(_errorSound);
        public void PlayFreezeSound() => _soundSource.PlayOneShot(_freezeSound);
        public void PlayHealSound() => _soundSource.PlayOneShot(_healSound);
        public void PlayIncubatorSound() => _soundSource.PlayOneShot(_incubatorSound);
        public void PlayMergeErrorSound() => _soundSource.PlayOneShot(_mergeErrorSound);
        public void PlayMergeSound() => _soundSource.PlayOneShot(_mergeSound);
        public void PlayPageChangeSound() => _soundSource.PlayOneShot(_pageChangeSound);
        public void PlayPopupSound() => _soundSource.PlayOneShot(_popupSound);
        public void PlayCoinsSound() => _soundSource.PlayOneShot(_coinsSound);
        public void PlayRouletteSound() => _soundSource.PlayOneShot(_rouletteSound);
        public void PlayTimeStopSound() => _soundSource.PlayOneShot(_timeStopSound);
        public void PlaySuccessSound() => _soundSource.PlayOneShot(_successSound);
    }
}