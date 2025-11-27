using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Achievements
{
    public class AchievementView : MonoBehaviour
    {
        [SerializeField] private Image _icon;
        [SerializeField] private TextMeshProUGUI _nameText;
        [SerializeField] private TextMeshProUGUI _descText;
        [SerializeField] private Button _claimButton;
        [SerializeField] private GameObject _checkmark;

        public event Action<AchievementConfig, AchievementProgressData> OnClaimed;
        
        public void SetData(AchievementConfig data, AchievementProgressData progressData)
        {
            _icon.sprite = data.Icon;
            _nameText.text = data.Name;
            _descText.text = data.Description;

            bool claimed = progressData.Claimed;
            bool claimable = progressData.Progress >= progressData.Target;

            _claimButton.interactable = false;
            _checkmark.SetActive(false);

            if (claimed)
            {
                SetClaimed();
                return;
            }

            if (claimable)
            {
                SetClaimable();
                _claimButton.onClick.AddListener(() =>
                {
                    OnClaimed?.Invoke(data, progressData);
                    SetClaimed();
                });
            }
        }

        private void SetClaimed()
        {
            _claimButton.interactable = false;
            _claimButton.image.color = Color.white;
            _checkmark.SetActive(true);
        }
        
        private void SetClaimable()
        {
            _claimButton.interactable = true;
            _claimButton.image.color = Color.white;
            _checkmark.SetActive(false);
        }
    }
}