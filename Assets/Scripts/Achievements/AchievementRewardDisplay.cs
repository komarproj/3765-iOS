using DG.Tweening;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Achievements
{
    public class AchievementRewardDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private float _fadeTime = 0.3f;
        [SerializeField] private float _stayTime = 0.5f;
        
        private Sequence _sequence;
        
        public void ShowReward(int amount)
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            
            _canvasGroup.alpha = 0;
            _amountText.text = "+" + amount;
            
            _sequence.Append(_canvasGroup.DOFade(1, _fadeTime).SetEase(Ease.InOutCirc));
            _sequence.AppendInterval(_stayTime);
            _sequence.Append(_canvasGroup.DOFade(0, _fadeTime).SetEase(Ease.InOutCirc));
            _sequence.SetLink(gameObject);
        }
    }
}