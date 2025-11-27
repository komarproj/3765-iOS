using System;
using UnityEngine;

namespace DefaultNamespace.Achievements
{
    [Serializable]
    public class AchievementConfig
    {
        public string Key;
        public Sprite Icon;
        public string Name;
        public string Description;
        public int Reward;
    }
}