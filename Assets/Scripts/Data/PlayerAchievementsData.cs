using System;
using DefaultNamespace.Achievements;

namespace Data
{
    [Serializable]
    public class PlayerAchievementsData
    {
        public AchievementProgressData WarmUpComplete = new()
        {
            Key = "1",
            Target = 1
        };
        
        public AchievementProgressData FirstHatch = new()
        {
            Key = "2",
            Target = 1
        };
        
        public AchievementProgressData SteadyHands = new()
        {
            Key = "3",
            Target = 10
        };
        
        public AchievementProgressData PerfectTemperature = new()
        {
            Key = "4",
            Target = 1
        };
        
        public AchievementProgressData SecondIncubator = new()
        {
            Key = "5",
            Target = 1
        };
        
        public AchievementProgressData TripleHatchery = new()
        {
            Key = "6",
            Target = 2
        };
        
        public AchievementProgressData FullCoop = new()
        {
            Key = "7",
            Target = 3
        };
        
        public AchievementProgressData Tinkerer = new()
        {
            Key = "8",
            Target = 1
        };
        
        public AchievementProgressData ThermostatPro = new()
        {
            Key = "9",
            Target = 1
        };
        
        public AchievementProgressData HourglassHero = new()
        {
            Key = "10",
            Target = 3
        };
        
        public AchievementProgressData IceWillDoNicely = new()
        {
            Key = "11",
            Target = 1
        };
        
        public AchievementProgressData BackFromTheBrink = new()
        {
            Key = "12",
            Target = 1
        };
        
        public AchievementProgressData EggInvestor = new()
        {
            Key = "13",
            Target = 10
        };
        
        public AchievementProgressData ChickenKeeper = new()
        {
            Key = "14",
            Target = 10
        };
        
        public AchievementProgressData RareFind = new()
        {
            Key = "15",
            Target = 1
        };
        
        public AchievementProgressData MergeMaster = new()
        {
            Key = "16",
            Target = 1
        };
        
        public AchievementProgressData GoldenCoop = new()
        {
            Key = "17",
            Target = 10_000
        };
        
        public AchievementProgressData DailyGrind = new()
        {
            Key = "18",
            Target = 7
        };
        
        public AchievementProgressData IncubationStreak = new()
        {
            Key = "19",
            Target = 5
        };
        
        public AchievementProgressData SeasonedIncubator = new()
        {
            Key = "20",
            Target = 100
        };
    }
}