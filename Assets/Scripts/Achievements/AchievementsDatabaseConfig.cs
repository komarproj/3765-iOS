using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace DefaultNamespace.Achievements
{
    [CreateAssetMenu(menuName = "Config/AchievementsDatabaseConfig")]
    public class AchievementsDatabaseConfig : BaseConfig
    {
        public List<AchievementConfig> Achievements;
        
        private Dictionary<string, AchievementConfig> _lookup;

        public void OnEnable()
        {
            _lookup = Achievements.ToDictionary(a => a.Key, a => a);
        }

        public AchievementConfig GetConfig(string key) => _lookup.GetValueOrDefault(key);
    }
}