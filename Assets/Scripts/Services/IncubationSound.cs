using UnityEngine;
using Zenject;

namespace Game.UserData
{
    public class IncubationSound : MonoBehaviour
    {
        [SerializeField] private AudioSource _clockSource;
        [SerializeField] private AudioSource _incubationSource;

        [Inject]
        private void Construct(SaveSystem saveSystem)
        {
            var settings = saveSystem.Data.SettingsData;

            bool isSound = settings.IsSoundOn.Value;

            _clockSource.volume = isSound ? 1 : 0;
            _incubationSource.volume = isSound ? 1 : 0;
        }
        
        public void StartAudio()
        {
            _clockSource.Play();
            _incubationSource.Play();
        }

        public void StopAudio()
        {
            _clockSource.Stop();
            _incubationSource.Stop();
        }
    }
}