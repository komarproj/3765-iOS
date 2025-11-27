using System;
using DefaultNamespace.Achievements;
using DefaultNamespace.Shop;
using DefaultNamespace.UI;
using Game.UserData;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.Gameplay.Bonuses
{
    public class HealthBonus : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private ErrorDisplay _errorDisplay;
        [SerializeField] private Button _button;
        [SerializeField] private BonusEffectDisplay _bonusEffectDisplay;

        private SaveSystem _saveSystem;
        private GameplayData _gameplayData;
        
        [Inject]
        private void Construct(SaveSystem saveSystem, GameplayData gameplayData)
        {
            _saveSystem = saveSystem;
            _gameplayData = gameplayData;
            
            var data = saveSystem.Data.InventoryData;
            data.HealItems.Subscribe((amount) => _amountText.text = amount.ToString()).AddTo(gameObject);
        }

        private void Awake()
        {
            _button.onClick.AddListener(TryUseBonus);
        }

        private void TryUseBonus()
        {
            var data = _saveSystem.Data.InventoryData;

            if (data.HealItems.Value <= 0)
            {
                _errorDisplay.ShowError("NO HEAL ITEMS!");
                return;
            }
            
            for (int i = 0; i < _gameplayData.IncubatedEggs.Count; i++)
            {
                var egg = _gameplayData.IncubatedEggs[i];

                egg.Health.Value += 0.5f;
                
                if(!egg.IsActive.Value)
                    AchievementsUnlocker.Instance.OnHealthUsed();

                egg.IsActive.Value = true;
            }
            
            _bonusEffectDisplay.ShowHealth();
            _saveSystem.Data.InventoryData.HealItems.Value--;
            AudioManager.Instance.PlayHealSound();
        }
    }
}