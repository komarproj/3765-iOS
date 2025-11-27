using System;
using Data;
using DefaultNamespace.Achievements;
using DefaultNamespace.Gameplay;
using DefaultNamespace.Gameplay.HeatMiniGame;
using DG.Tweening;
using Game.UserData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Popups
{
    public class HeatGamePopup : BasePopup
    {
        [SerializeField] private Button _button;
        [SerializeField] private RectTransform _lineRect;
        [SerializeField] private RectTransform _greenZone;
        [SerializeField] private Image _greenZoneImg;
        [SerializeField] private RectTransform _heatIcon;
        [SerializeField] private HeatZonePositioner _heatZonePositioner;
        [SerializeField] private HeatAnimator _heatAnimator;

        private IncubationService _incubationService;
        private GameplayTimer _gameplayTimer;

        private int _clicks = 0;
        private int _clicksToFinish = 3;

        public event Action OnSuccess;

        [Inject]
        private void Construct(IncubationService incubationService, GameplayTimer gameplayTimer)
        {
            _incubationService = incubationService;
            _gameplayTimer = gameplayTimer;
        }

        public void StartGame(IncubatorUpgradeData upgrades)
        {
            _incubationService.SetEnabled(false);
            _gameplayTimer.SetPaused(true);

            SetGreenZoneSize(upgrades.SizeUpgrade.Value);
            _heatAnimator.ApplyUpgrade(UpgradesHelper.GetSpeedMultiplier(upgrades.SpeedUpgrade.Value));
            
            _button.onClick.AddListener(ProcessClick);
            _heatZonePositioner.PlaceGreenZone();
        }

        private void SetGreenZoneSize(int level)
        {
            var height = _greenZone.rect.height;
            height *= UpgradesHelper.GetGreenZoneMultiplier(level);
            _greenZone.sizeDelta = new Vector2(_greenZone.sizeDelta.x, height);
        }

        private void OnDestroy()
        {
            _incubationService.SetEnabled(true);
            _gameplayTimer.SetPaused(false);
        }

        private async void ProcessClick()
        {
            if (IsInsideGreenZone())
            {
                _clicks++;
                _greenZone.DOPunchScale(Vector3.one * 0.1f, 0.2f);
                _heatIcon.DOPunchScale(Vector3.one * 0.1f, 0.2f);
                AchievementsUnlocker.Instance.OnGreenZoneHit();
                AudioManager.Instance.PlaySuccessSound();
            }
            else
            {
                _heatAnimator.StopAnimation();
                _greenZone.DOShakeScale(0.2f, 0.2f);
                _heatIcon.DOShakeScale(0.2f, 0.2f);
                
                _button.enabled = false;
                AudioManager.Instance.PlayErrorSound();
                await _greenZoneImg.DOColor(Color.black, 1f).AsyncWaitForCompletion();
                
                DestroyPopup();
                return;
            }

            if (_clicks >= _clicksToFinish)
            {
                _heatAnimator.StopAnimation();
                
                _button.enabled = false;
                await _greenZone.DOScale(Vector3.one * 1.2f, 1f).AsyncWaitForCompletion();
                
                DestroyPopup();
                OnSuccess?.Invoke();
            }
            else
                _heatZonePositioner.PlaceGreenZone();
        }

        private bool IsInsideGreenZone()
        {
            float lineY = _lineRect.anchoredPosition.y;
            float zoneY = _greenZone.anchoredPosition.y;

            float lineHalf = _lineRect.rect.height * 0.5f;
            float zoneHalf = _greenZone.rect.height * 0.5f;

            float lineTop = lineY + lineHalf;
            float lineBottom = lineY - lineHalf;

            float zoneTop = zoneY + zoneHalf;
            float zoneBottom = zoneY - zoneHalf;

            return lineTop > zoneBottom && lineBottom < zoneTop;
        }
    }
}