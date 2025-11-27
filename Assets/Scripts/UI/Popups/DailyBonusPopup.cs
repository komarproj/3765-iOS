using System;
using System.Collections.Generic;
using Data;
using DefaultNamespace.Achievements;
using DefaultNamespace.Daily;
using DefaultNamespace.UI;
using Game.UserData;
using Spinner;
using UnityEngine;
using UnityEngine.UI;
using Zenject;
using Random = UnityEngine.Random;

namespace UI.Popups
{
    public class DailyBonusPopup : BasePopup
    {
        [SerializeField] private List<RewardView> _rewards;
        [SerializeField] private Button _spinButton;
        [SerializeField] private GameObject _error;
        [SerializeField] private RectTransform _rouletteRect;
        
        private SpinnerService _spinnerService;
        private LoginService _loginService;
        private UIFactory _uiFactory;
        private SaveSystem _saveSystem;

        [Inject]
        private void Construct(SpinnerService spinnerService, LoginService loginService,
            UIFactory uiFactory, SaveSystem saveSystem)
        {
            _spinnerService = spinnerService;
            _loginService = loginService;
            _uiFactory = uiFactory;
            _saveSystem = saveSystem;
        }

        private void Start()
        {
            var canSpin = _loginService.CanDailySpin();

            if (!canSpin)
            {
                _error.SetActive(true);
                _spinButton.gameObject.SetActive(false);
                return;
            }
            
            _spinButton.onClick.AddListener(Spin);
        }

        private async void Spin()
        {
            var targetIndex = Random.Range(0, _rewards.Count);
            
            _closeButton.interactable = false;
            _spinButton.interactable = false;
            
            AudioManager.Instance.PlayRouletteSound();
            
            await _spinnerService.Spin(_rouletteRect, targetIndex);
            
            var reward = _rewards[targetIndex].Type;
            ProcessReward(reward, _rewards[targetIndex].Amount);

            var popup = _uiFactory.CreatePopup<DailyRewardPopup>();
            popup.SetData(_rewards[targetIndex]);
            popup.OnDestroyed += DestroyPopup;
        }

        private void ProcessReward(RewardType rewardType, int amount)
        {
            _loginService.RecordDailySpin();
            AchievementsUnlocker.Instance.OnDailyLogin();

            var inv = _saveSystem.Data.InventoryData;
            switch (rewardType)
            {
                case RewardType.Egg:
                    inv.Eggs.Add(Random.Range(0, Constants.ChickenTypes));
                    break;
                case RewardType.Freeze:
                    inv.FreezeItems.Value += amount;
                    break;
                case RewardType.Time:
                    inv.TimeItems.Value += amount;
                    break;
                case RewardType.Health:
                    inv.HealItems.Value += amount;
                    break;
                case RewardType.Coins:
                    inv.Balance.Value += amount;
                    break;
            }
        }
    }
}