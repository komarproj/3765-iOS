using DefaultNamespace.Achievements;
using DefaultNamespace.UI;
using Game.UserData;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Shop
{
    public class EggPurchasePage : MonoBehaviour
    {
        [SerializeField] private RectTransform _shopItemsParent;
        [SerializeField] private ErrorDisplay _errorDisplay;
        [SerializeField] private PurchaseDisplay _purchaseDisplay;
        
        private SaveSystem _saveSystem;
        private ShopService _shopService;

        [Inject]
        private void Construct(ShopFactory factory, SaveSystem saveSystem, ShopService shopService)
        {
            _saveSystem = saveSystem;
            _shopService = shopService;

            foreach (var item in factory.CreateShopItems())
            {
                item.transform.SetParent(_shopItemsParent, false);
                item.OnPurchasePressed += TryPurchaseEgg;
            }
        }

        private void TryPurchaseEgg(int id, int price)
        {
            if (!_shopService.CanPurchase(price))
            {
                _errorDisplay.ShowError();
                return;
            }
            
            _saveSystem.Data.InventoryData.Eggs.Add(id);
            _shopService.PurchaseItem(price);
            _purchaseDisplay.ShowEgg();
            AchievementsUnlocker.Instance.OnEggPurchased();
            AudioManager.Instance.PlayCoinsSound();
        }
    }
}