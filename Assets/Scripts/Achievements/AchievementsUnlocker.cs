using Data;
using Game.UserData;
using UniRx;

namespace DefaultNamespace.Achievements
{
    public class AchievementsUnlocker
    {
        private readonly SaveSystem _saveSystem;

        public static AchievementsUnlocker Instance;
            
        public AchievementsUnlocker(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;

            if (GetData().IncubationStreak.Progress < GetData().IncubationStreak.Target)
                GetData().IncubationStreak.Progress = 0;
            
            Instance = this;

            _saveSystem.Data.InventoryData.Balance.Pairwise().Subscribe(pair =>
            {
                int prev = pair.Previous;
                int current = pair.Current;
                
                int difference = current - prev;
                
                if(difference < 0)
                    return;
                
                OnCoinsEarned(difference);
            });
        }

        public void OnTutorialComplete() => GetData().WarmUpComplete.AddProgress(+1);
        public void OnHatchComplete(int id)
        {
            GetData().FirstHatch.AddProgress(+1);
            GetData().IncubationStreak.AddProgress(+1);
            GetData().SeasonedIncubator.AddProgress(+1);
            
            if(id == 9)
                GetData().RareFind.AddProgress(+1);
        }

        public void OnGreenZoneHit() => GetData().SteadyHands.AddProgress(+1);

        public void ResetGreenZone()
        {
            if(GetData().SteadyHands.Progress < GetData().SteadyHands.Target)
                GetData().SteadyHands.Progress = 0;
        }
        
        public void OnPerfectIncubation() => GetData().PerfectTemperature.AddProgress(+1);

        public void OnLevelUp()
        {
            GetData().SecondIncubator.AddProgress(+1);
            GetData().TripleHatchery.AddProgress(+1);
            GetData().FullCoop.AddProgress(+1);
        }

        public void OnUpgradePurchased(int level)
        {
            GetData().Tinkerer.AddProgress(+1);
            if(level == 4)
                GetData().ThermostatPro.AddProgress(+1);
        }

        public void OnHourglassUsed() => GetData().HourglassHero.AddProgress(+1);
        public void OnFreezeUsed() => GetData().IceWillDoNicely.AddProgress(+1);
        public void OnHealthUsed() => GetData().BackFromTheBrink.AddProgress(+1);
        public void OnEggPurchased() => GetData().EggInvestor.AddProgress(+1);
        public void OnChickenAdded(int sum)
        {
            if(sum == 10)
                GetData().ChickenKeeper.AddProgress(+1);
        }

        public void OnMerge() => GetData().MergeMaster.AddProgress(+1);
        public void OnCoinsEarned(int amount) => GetData().GoldenCoop.AddProgress(+amount);

        public void OnDailyLogin() => GetData().DailyGrind.AddProgress(+1);

        private PlayerAchievementsData GetData() => _saveSystem.Data.PlayerAchievementsData;
    }
}