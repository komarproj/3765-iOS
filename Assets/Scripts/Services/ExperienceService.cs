using Data;
using DefaultNamespace.Achievements;

namespace Game.UserData
{
    public class ExperienceService
    {
        private readonly SaveSystem _saveSystem;

        public ExperienceService(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }
        
        public void AddExperience(int amount)
        {
            int currentExp = _saveSystem.Data.ExperienceData.Experience;

            int curLevel = GetLevelFromExperience(currentExp);
            
            currentExp += amount;
            _saveSystem.Data.ExperienceData.Experience = currentExp;

            int nextLevel = GetLevelFromExperience(currentExp);
            _saveSystem.Data.ExperienceData.Level = nextLevel;
            
            if(nextLevel > curLevel)
                AchievementsUnlocker.Instance.OnLevelUp();
        }

        private int GetLevelFromExperience(int experience)
        {
            var thresholds = Constants.ExperienceThresholds;

            for (int i = 0; i <= Constants.MaxLevel; i++)
            {
                if(experience < thresholds[i])
                    return i;
            }
            
            return Constants.MaxLevel;
        }
    }
}