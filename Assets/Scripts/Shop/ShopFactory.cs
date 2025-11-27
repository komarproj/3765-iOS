using System.Collections.Generic;
using AssetsProvider;
using Game.UserData;
using Runtime.Game.Services.SettingsProvider;

namespace DefaultNamespace.Shop
{
    public class ShopFactory
    {
        private readonly PrefabsProvider _prefabsProvider;
        private readonly ConfigsProvider _configsProvider;
        private readonly DIFactory _factory;

        public ShopFactory(PrefabsProvider prefabsProvider, ConfigsProvider configsProvider, DIFactory factory)
        {
            _prefabsProvider = prefabsProvider;
            _configsProvider = configsProvider;
            _factory = factory;
        }

        public List<EggPurchaseView> CreateShopItems()
        {
            List<EggPurchaseView> result = new List<EggPurchaseView>();

            var prefab = _prefabsProvider.Get("EggPurchasePrefab");
            var config = _configsProvider.Get<ShopConfig>().ShopItems;

            for (var index = 0; index < config.Count; index++)
            {
                var shopItem = config[index];
                var instance = _factory.Create<EggPurchaseView>(prefab);
                instance.Initialize(shopItem.EggSprite, shopItem.Price, index);
                result.Add(instance);
            }

            return result;
        }
    }
}