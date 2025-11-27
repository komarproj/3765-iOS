using System;

namespace DefaultNamespace.Achievements
{
    [Serializable]
    public class AchievementProgressData
    {
        public string Key;

        public int Progress;
        public int Target;
        
        public bool Claimed;

        public void AddProgress(int progress)
        {
            Progress += progress;
            if(Progress >= Target)
                Progress = Target;
        }
    }
}