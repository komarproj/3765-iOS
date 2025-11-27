using System;
using Data;
using DefaultNamespace.UI;
using Game.UserData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.Shop
{
    public class EnergyPurchasePage : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private Button _purchaseButton;
        [SerializeField] private Button _lessButton;
        [SerializeField] private Button _moreButton;
        [SerializeField] private ErrorDisplay _errorDisplay;
        [SerializeField] private PurchaseDisplay _purchaseDisplay;
        
        private const int MinAmount = 1;
        private const int MaxAmount = Constants.MaxEnergy;
        private const int PricePerEnergy = 5;

        private int _amount = 1;
        
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
            _lessButton.onClick.AddListener(() => IncrementAmount(-1));
            _moreButton.onClick.AddListener(() => IncrementAmount(+1));
            _purchaseButton.onClick.AddListener(TryPurchase);

            IncrementAmount(0);
        }

        private void IncrementAmount(int increment)
        {
            if(increment < 0 && _amount <= MinAmount)
                return;
            
            if(increment > 0 && _amount >= MaxAmount)
                return;
            
            _amount += increment;

            UpdateFromAmount();
        }

        private void UpdateFromAmount()
        {
            int price = _amount * PricePerEnergy;
            _priceText.text = price.ToString();
            
            _amountText.text = "+" + _amount.ToString();
        }

        private void TryPurchase()
        {
            int price = _amount * PricePerEnergy;

            if (!_shopService.CanPurchase(price))
            {
                _errorDisplay.ShowError();
                return;
            }
            
            _shopService.PurchaseItem(price);
            _saveSystem.Data.EnergyData.Energy.Value += _amount;
            _purchaseDisplay.ShowEnergy(_amount);
            AudioManager.Instance.PlayCoinsSound();
        }
    }
}