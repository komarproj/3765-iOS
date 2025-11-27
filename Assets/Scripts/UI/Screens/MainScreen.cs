using System;
using DefaultNamespace.UI;
using UI.Popups;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Screens
{
    public class MainScreen : BaseScreen
    {
        [SerializeField] private Button _playButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _achButton;
        [SerializeField] private Button _setButton;
        [SerializeField] private Button _dailyButton;
        
        public event Action OnPlayPressed;
        
        [Inject]
        private UIFactory _uiFactory;
        
        public void Initialize()
        {
            SubscribeEvents();
        }
        
        private void SubscribeEvents()
        {
            _playButton.onClick.AddListener(() => OnPlayPressed?.Invoke());
            
            _shopButton.onClick.AddListener(() => _uiFactory.CreatePopup<ShopPopup>());
            _achButton.onClick.AddListener(() => _uiFactory.CreatePopup<AchievementsPopup>());
            _setButton.onClick.AddListener(() => _uiFactory.CreatePopup<SettingsPopup>());
            _dailyButton.onClick.AddListener(() => _uiFactory.CreatePopup<DailyBonusPopup>());
        }
    }
}