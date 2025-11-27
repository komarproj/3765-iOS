using System;
using DefaultNamespace.UI;
using Game.UserData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Popups
{
    public class SettingsPopup : BasePopup
    {
        [SerializeField] private CustomToggle _soundToggle;
        [SerializeField] private CustomToggle _musicToggle;

        [SerializeField] private Button _ppButton;
        [SerializeField] private Button _touButton;
        
        private SaveSystem _saveSystem;
        private UIFactory _uiFactory;
        
        [Inject]
        private void Construct(SaveSystem saveSystem, UIFactory uiFactory)
        {
            _saveSystem = saveSystem;
            _uiFactory = uiFactory;
        }

        private void Start()
        {
            var data = _saveSystem.Data.SettingsData;
            
            _soundToggle.SetEnabled(data.IsSoundOn.Value);
            _musicToggle.SetEnabled(data.IsMusicOn.Value);

            _soundToggle.OnValueChanged += (value) => data.IsSoundOn.Value = value;
            _musicToggle.OnValueChanged += (value) => data.IsMusicOn.Value = value;
            
            _ppButton.onClick.AddListener(() => _uiFactory.CreatePopup<PrivacyPolicyPopup>());
            _touButton.onClick.AddListener(() => _uiFactory.CreatePopup<TermsOfUsePopup>());
        }
    }
}