using System;
using Data;
using DefaultNamespace.Data;

namespace Game.UserData
{
    [Serializable]
    public class UserData
    {
        public SettingsData SettingsData = new();
        public LoginData LoginData = new();
        public TutorialData TutorialData = new();
        public InventoryData InventoryData = new();
        public EnergyData EnergyData = new();
        public ExperienceData ExperienceData = new();
        public UpgradesData UpgradesData = new();
        public FieldData FieldData = new();
        public PlayerAchievementsData PlayerAchievementsData = new();
    }
}