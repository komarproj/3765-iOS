using Data;
using DG.Tweening;
using Game.UserData;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace Gameplay.Services
{
    public class ChickenRewardDisplay : MonoBehaviour
    {
        [SerializeField] private Button _claimButton;
        [SerializeField] private TextMeshProUGUI _rewardText;
        
        [Inject]
        private SaveSystem _saveSystem;
        
        private Sequence _sequence;
        
        public void SetData(ChickenData chickenData)
        {
            chickenData.CoinsGenerated.Subscribe(value =>
            {
                _claimButton.gameObject.SetActive(value > 0);
            }).AddTo(gameObject);
            
            _claimButton.onClick.AddListener(() =>
            {
                int coins = chickenData.CoinsGenerated.Value;
                _saveSystem.Data.InventoryData.Balance.Value += coins;
                chickenData.CoinsGenerated.Value = 0;
                AnimateReward(coins);
                AudioManager.Instance.PlayCoinsSound();
            });
        }

        private void AnimateReward(int amount)
        {
            _rewardText.text = "+" + amount;
            _rewardText.transform.localScale = Vector3.zero;
            
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            
            _sequence.Append(_rewardText.transform.DOScale(Vector3.one, 0.25f).SetEase(Ease.InOutBounce));
            _sequence.AppendInterval(0.66f);
            _sequence.Append(_rewardText.transform.DOScale(Vector3.zero, 0.4f));
            _sequence.SetLink(gameObject);
        }
    }
}