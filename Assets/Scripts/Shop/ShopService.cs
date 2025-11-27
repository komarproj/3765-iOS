using Game.UserData;

namespace DefaultNamespace.Shop
{
    public class ShopService
    {
        private readonly SaveSystem _saveSystem;

        public ShopService(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }
        
        public bool CanPurchase(int price)
        {
            var balanceProperty = _saveSystem.Data.InventoryData.Balance;
            return balanceProperty.Value >= price;
        }

        public void PurchaseItem(int price) => _saveSystem.Data.InventoryData.Balance.Value -= price;
    }
}