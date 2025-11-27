using DefaultNamespace.Achievements;
using Game.UserData;
using UnityEngine;
using Zenject;

namespace UI.Popups
{
    public class AchievementsPopup : BasePopup
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private AchievementRewardDisplay _achievementRewardDisplay;
        
        [Inject]
        private AchievementsFactory _factory;
        
        [Inject]
        private SaveSystem _saveSystem;

        private void Start()
        {
            foreach (var view in _factory.CreateAchievementViews())
            {
                view.transform.SetParent(_content, false);
                view.OnClaimed += ClaimAchievement;
            }
        }

        private void ClaimAchievement(AchievementConfig data, AchievementProgressData progData)
        {
            AudioManager.Instance.PlayCoinsSound();
            progData.Claimed = true;
            _saveSystem.Data.InventoryData.Balance.Value += data.Reward;
            _achievementRewardDisplay.ShowReward(data.Reward);
        }
    }
}