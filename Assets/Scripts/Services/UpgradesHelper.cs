using UnityEngine;

namespace Game.UserData
{
    public class UpgradesHelper
    {
        public static int GetUpgradePrice(int level)
        {
            int levelPrice = 25;
            
            return levelPrice * (level + 1);
        }

        public static float GetGreenZoneMultiplier(int level)
        {
            const float growthRate = 0.3f;
            float amount = 1f - Mathf.Exp(-growthRate * level);

            return 1f + amount; 
        }
        
        public static float GetSpeedMultiplier(int level)
        {
            const float decayRate = 0.15f;
            float multiplier = 1f - Mathf.Exp(-decayRate * level);
            return 1 + multiplier;
        }
        
        public static float GetTemperatureTimeMultiplier(int level)
        {
            const float decayRate = 0.1f;
            float multiplier = Mathf.Exp(-decayRate * level);
            return multiplier;
        }
    }
}