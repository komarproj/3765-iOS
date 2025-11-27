using Data;
using DefaultNamespace.Achievements;
using DefaultNamespace.Shop;
using DefaultNamespace.UI;
using Game.UserData;
using UniRx;
using UnityEngine;
using Zenject;

namespace UI.Popups
{
    public class IncubatorUpgradePopup : BasePopup
    {
        [SerializeField] private UpgradeButton _sizeUpgrade;
        [SerializeField] private UpgradeButton _speedUpgrade;
        [SerializeField] private UpgradeButton _temperatureUpgrade;
        [SerializeField] private ErrorDisplay _errorDisplay;

        private SaveSystem _saveSystem;
        
        [Inject]
        private void Construct(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }
        
        public void SetData(IncubatorUpgradeData data)
        {
            data.SizeUpgrade.Subscribe(level => _sizeUpgrade.SetData(UpgradesHelper.GetUpgradePrice(level)));
            data.SpeedUpgrade.Subscribe(level => _speedUpgrade.SetData(UpgradesHelper.GetUpgradePrice(level)));
            data.TimeUpgrade.Subscribe(level => _temperatureUpgrade.SetData(UpgradesHelper.GetUpgradePrice(level)));
            
            _sizeUpgrade.Button.onClick.AddListener(() => PurchaseUpgrade(data.SizeUpgrade));
            _speedUpgrade.Button.onClick.AddListener(() => PurchaseUpgrade(data.SpeedUpgrade));
            _temperatureUpgrade.Button.onClick.AddListener(() => PurchaseUpgrade(data.TimeUpgrade));
        }

        private void PurchaseUpgrade(IntReactiveProperty levelProperty)
        {
            int price = UpgradesHelper.GetUpgradePrice(levelProperty.Value);
            
            if(!TryPurchase(price))
                return;

            levelProperty.Value++;
            AchievementsUnlocker.Instance.OnUpgradePurchased(levelProperty.Value);
        }
        
        private bool TryPurchase(int price)
        {
            if (_saveSystem.Data.InventoryData.Balance.Value < price)
            {
                _errorDisplay.ShowError();
                return false;
            }

            _saveSystem.Data.InventoryData.Balance.Value -= price;
            AudioManager.Instance.PlayCoinsSound();
            return true;
        }
    }
}