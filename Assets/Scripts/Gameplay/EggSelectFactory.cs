using System.Collections.Generic;
using AssetsProvider;
using Game.UserData;
using Runtime.Game.Services.SettingsProvider;

namespace DefaultNamespace.Gameplay
{
    public class EggSelectFactory
    {
        private readonly PrefabsProvider _prefabsProvider;
        private readonly SaveSystem _saveSystem;
        private readonly ConfigsProvider _configsProvider;
        private readonly DIFactory _factory;

        public EggSelectFactory(PrefabsProvider prefabsProvider, 
            SaveSystem saveSystem, 
            ConfigsProvider configsProvider,
            DIFactory factory)
        {
            _prefabsProvider = prefabsProvider;
            _saveSystem = saveSystem;
            _configsProvider = configsProvider;
            _factory = factory;
        }

        public List<EggSelectView> CreateEggSelectViews()
        {
            List<EggSelectView> eggSelectViews = new List<EggSelectView>();

            var prefab = _prefabsProvider.Get("EggSelectPrefab");
            var inventory = _saveSystem.Data.InventoryData.Eggs;
            var config = _configsProvider.Get<ChickensConfig>();

            foreach (var id in inventory)
            {
                var instance = _factory.Create<EggSelectView>(prefab);
                instance.Initialize(id, config.Chickens[id].EggSprite);
                eggSelectViews.Add(instance);
            }
            
            return eggSelectViews;
        }
    }
}