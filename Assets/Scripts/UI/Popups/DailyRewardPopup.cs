using DefaultNamespace.Daily;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public class DailyRewardPopup : BasePopup
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _amountText;

        public void SetData(RewardView rewardView)
        {
            _icon.sprite = rewardView.Sprite;
            _amountText.text = "+"+rewardView.Amount.ToString();
        }
    }
}