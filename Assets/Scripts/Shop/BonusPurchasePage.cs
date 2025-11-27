using System;
using DefaultNamespace.UI;
using Game.UserData;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Shop
{
    public class BonusPurchasePage : MonoBehaviour
    {
        [SerializeField] private BonusPurchaseView _healBonus;
        [SerializeField] private BonusPurchaseView _freezeBonus;
        [SerializeField] private BonusPurchaseView _timeBonus;
        [SerializeField] private ErrorDisplay _errorDisplay;
        [SerializeField] private PurchaseDisplay _purchaseDisplay;
        
        private SaveSystem _saveSystem;
        private ShopService _shopService;
        
        [Inject]
        private void Construct(SaveSystem saveSystem, ShopService shopService)
        {
            _saveSystem = saveSystem;
            _shopService = shopService;
        }

        private void Awake()
        {
            _healBonus.OnPurchasePressed += PurchaseHeal;
            _freezeBonus.OnPurchasePressed += PurchaseFreeze;
            _timeBonus.OnPurchasePressed += PurchaseTime;
        }

        private void PurchaseHeal(int price)
        {
            if(!TryPurchase(price))
                return;

            _saveSystem.Data.InventoryData.HealItems.Value++;
            _purchaseDisplay.ShowHealth();
        }
        
        private void PurchaseFreeze(int price)
        {
            if(!TryPurchase(price))
                return;

            _saveSystem.Data.InventoryData.FreezeItems.Value++;
            _purchaseDisplay.ShowFreeze();
        }
        
        private void PurchaseTime(int price)
        {
            if(!TryPurchase(price))
                return;

            _saveSystem.Data.InventoryData.TimeItems.Value++;
            _purchaseDisplay.ShowTime();
        }

        private bool TryPurchase(int price)
        {
            if (!_shopService.CanPurchase(price))
            {
                _errorDisplay.ShowError();
                return false;
            }

            _shopService.PurchaseItem(price);
            AudioManager.Instance.PlayCoinsSound();
            return true;
        }
    }
}