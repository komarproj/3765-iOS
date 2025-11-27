using System.Collections.Generic;

namespace Data
{
    public class Constants
    {
        public const int MaxEnergy = 100;
        public const int HeatPrice = 20;

        public const int ChickenTypes = 10;

        public const float GameTimer = 30;

        public const float StartingEggHealth = 0.25f;
        public const float HealthDecreaseTime = 15f;

        public const float HealAmount = 0.5f;

        public const int ExperiencePerIncubation = 100;
        public const int RewardPerIncubation = 50;

        public const int MaxLevel = 3;
        
        public static readonly List<int> ExperienceThresholds = new()
        {
            100,
            400,
            1600
        };

        public const int SecondsPerEnergy = 60;
        
        public const float SecondsPerCoinGeneration = 30;
        public const float SecondsPerEggGeneration = 45;
    }
}