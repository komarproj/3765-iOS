using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class PurchaseDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _amountText;
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _canvasGroup;

        [SerializeField] private Sprite _health;
        [SerializeField] private Sprite _freeze;
        [SerializeField] private Sprite _time;
        [SerializeField] private Sprite _energy;
        [SerializeField] private Sprite _egg;
        
        private Sequence _sequence;

        public void ShowHealth() => Show(1, _health);
        public void ShowFreeze() => Show(1, _freeze);
        public void ShowTime() => Show(1, _time);
        public void ShowEnergy(int amount) => Show(amount, _energy);
        public void ShowEgg() => Show(1, _egg);

        private void Show(int amount, Sprite sprite)
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            
            _image.sprite = sprite;
            _amountText.text = "+"+amount.ToString();

            _canvasGroup.alpha = 0;
            
            _sequence.Append(_canvasGroup.DOFade(1, 0.2f));
            _sequence.AppendInterval(0.5f);
            _sequence.Append(_canvasGroup.DOFade(0, 0.3f));
            _sequence.SetLink(gameObject);
        }
    }
}