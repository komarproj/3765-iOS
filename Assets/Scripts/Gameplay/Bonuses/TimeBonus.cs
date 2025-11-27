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
    public class TimeBonus : MonoBehaviour
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
            data.TimeItems.Subscribe((amount) => _amountText.text = amount.ToString()).AddTo(gameObject);
        }

        private void Awake()
        {
            _button.onClick.AddListener(TryUseBonus);
        }

        private void TryUseBonus()
        {
            var data = _saveSystem.Data.InventoryData;

            if (data.TimeItems.Value <= 0)
            {
                _errorDisplay.ShowError("NO TIME ITEMS!");
                return;
            }

            _gameplayData.LevelTime.Value -= 5;
            _bonusEffectDisplay.ShowTime();
            _saveSystem.Data.InventoryData.TimeItems.Value--;
            AchievementsUnlocker.Instance.OnHourglassUsed();
            AudioManager.Instance.PlayTimeStopSound();
        }
    }
}