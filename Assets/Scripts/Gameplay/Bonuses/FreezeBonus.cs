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
    public class FreezeBonus : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private ErrorDisplay _errorDisplay;
        [SerializeField] private Button _button;
        [SerializeField] private BonusEffectDisplay _bonusEffectDisplay;
        
        private SaveSystem _saveSystem;
        private IncubationService _incubationService;
        
        [Inject]
        private void Construct(SaveSystem saveSystem, IncubationService incubationService)
        {
            _saveSystem = saveSystem;
            _incubationService = incubationService;
            
            var data = saveSystem.Data.InventoryData;
            data.FreezeItems.Subscribe((amount) => _amountText.text = amount.ToString()).AddTo(gameObject);
        }

        private void Awake()
        {
            _button.onClick.AddListener(TryUseBonus);
        }

        private void TryUseBonus()
        {
            var data = _saveSystem.Data.InventoryData;

            if (data.FreezeItems.Value <= 0)
            {
                _errorDisplay.ShowError("NO FREEZE ITEMS!");
                return;
            }

            if (_incubationService.FreezeTime > 0)
            {
                _errorDisplay.ShowError("FREEZE IS ALREADY ACTIVE!");
                return;
            }
            
            _incubationService.AddFreezeTime(+3);
            _bonusEffectDisplay.ShowFreeze();
            _saveSystem.Data.InventoryData.FreezeItems.Value--;
            AchievementsUnlocker.Instance.OnFreezeUsed();
            AudioManager.Instance.PlayFreezeSound();
        }
    }
}