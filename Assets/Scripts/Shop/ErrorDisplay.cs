using DG.Tweening;
using Game.UserData;
using TMPro;
using UnityEngine;

namespace DefaultNamespace.Shop
{
    public class ErrorDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;

        private Sequence _sequence;
        
        public void ShowError(string message = "NOT ENOUGH BALANCE!")
        {
            AudioManager.Instance.PlayErrorSound();
            _sequence?.Kill();
            _sequence = DOTween.Sequence();
            
            _text.text = message;
            var color = _text.color;
            color.a = 0;
            _text.color = color;

            _sequence.Append(_text.DOFade(1, 0.3f));
            _sequence.AppendInterval(0.5f);
            _sequence.Append(_text.DOFade(0, 0.3f));
            _sequence.SetLink(gameObject);
        }
    }
}