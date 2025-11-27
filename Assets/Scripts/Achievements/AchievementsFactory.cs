using System.Collections.Generic;
using System.Reflection;
using AssetsProvider;
using Data;
using Game.UserData;
using Runtime.Game.Services.SettingsProvider;

namespace DefaultNamespace.Achievements
{
    public class AchievementsFactory
    {
        private readonly PrefabsProvider _prefabsProvider;
        private readonly DIFactory _factory;
        private readonly ConfigsProvider _configsProvider;
        private readonly SaveSystem _saveSystem;

        public AchievementsFactory(PrefabsProvider prefabsProvider, DIFactory factory, ConfigsProvider configsProvider,
            SaveSystem saveSystem)
        {
            _prefabsProvider = prefabsProvider;
            _factory = factory;
            _configsProvider = configsProvider;
            _saveSystem = saveSystem;
        }

        public List<AchievementView> CreateAchievementViews()
        {
            List<AchievementView> result = new();
            var dataList = _configsProvider.Get<AchievementsDatabaseConfig>().Achievements;

            var prefab = _prefabsProvider.Get("AchievementViewPrefab");

            var dataDict = GetDataDict(_saveSystem.Data.PlayerAchievementsData);
            
            foreach (var data in dataList)
            {
                var instance = _factory.Create<AchievementView>(prefab);
                instance.SetData(data, dataDict[data.Key]);
                result.Add(instance);
            }
            
            return result;
        }

        private Dictionary<string, AchievementProgressData> GetDataDict(PlayerAchievementsData playerAchievementsData)
        {
            Dictionary<string, AchievementProgressData> result = new();

            var fields = typeof(PlayerAchievementsData).GetFields(BindingFlags.Public | BindingFlags.Instance);

            foreach (var field in fields)
            {
                if (field.FieldType == typeof(AchievementProgressData))
                {
                    var value = field.GetValue(playerAchievementsData) as AchievementProgressData;
                    if (value != null && !string.IsNullOrEmpty(value.Key))
                    {
                        result[value.Key] = value;
                    }
                }
            }

            return result;
        }
    }
}