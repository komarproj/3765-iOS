using System;
using Data;
using DefaultNamespace.UI;
using DG.Tweening;
using Game.UserData;
using UI.Popups;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.Gameplay
{
    public class IncubatorGame : MonoBehaviour
    {
        [SerializeField] private int _id;
        
        [SerializeField] private Slider _slider;
        [SerializeField] private Image _fillImage;
        [SerializeField] private Button _button;
        [SerializeField] private GameObject _cross;
        
        [SerializeField] private Color _maxColor;
        [SerializeField] private Color _minColor;
        [SerializeField] private IncubatorAnimator _incubatorAnimator;
        
        private CompositeDisposable _disposables;
        
        private IncubatedEggData _eggData;
        
        [Inject]
        private UIFactory _uiFactory;

        [Inject]
        private SaveSystem _saveSystem;
        
        private void Awake()
        {
            _button.onClick.AddListener(() =>
            {
                var popup = _uiFactory.CreatePopup<HeatGamePopup>();

                var upgradesData = _saveSystem.Data.UpgradesData.Upgrades[_id];
                popup.StartGame(upgradesData);
                popup.OnSuccess += () =>
                {
                    float health = _eggData.Health.Value;
                    _eggData.Health.Value = Mathf.Clamp01(health + Constants.HealAmount);
                };
            });
        }

        public void SetEgg(IncubatedEggData eggData)
        {
            _disposables?.Dispose();
            
            _disposables = new();

            _eggData = eggData;
            _eggData.Health.Subscribe(SetProgress).AddTo(_disposables);
            _eggData.IsActive.Subscribe((value) =>
            {
                _cross.SetActive(!value);
                _button.enabled = value;
            }).AddTo(_disposables);
        }

        public void Enable(bool enable)
        {
            if (_eggData == null)
                enable = false;
            
            _slider.gameObject.SetActive(enable);
            _button.gameObject.SetActive(enable);
            
            if(enable)
                _incubatorAnimator.StartShaking();
            else
                _incubatorAnimator.StopShaking();
        }

        private void SetProgress(float progress)
        {
            _slider.value = progress;
            UpdateColor();
        }

        private void UpdateColor()
        {
            float value = _slider.value;
            
            Color color = Color.Lerp(_minColor, _maxColor, value);
            _fillImage.color = color;
        }
    }
}