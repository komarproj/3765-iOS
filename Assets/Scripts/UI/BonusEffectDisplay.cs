using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class BonusEffectDisplay : MonoBehaviour
    {
        [SerializeField] private Image _image;

        [SerializeField] private Color _healthColor;
        [SerializeField] private Color _freezeColor;
        [SerializeField] private Color _timeColor;

        [SerializeField] private float _fadeTime;
        
        private readonly Color _defaultColor = new Color(0,0,0,0);
        
        public void ShowHealth()
        {
            ShowEffect(_healthColor);
        }

        public void ShowFreeze()
        {
            ShowEffect(_freezeColor);
        }

        public void ShowTime()
        {
            ShowEffect(_timeColor);
        }

        private void ShowEffect(Color color)
        {
            _image.DOKill();
            _image.color = _defaultColor;

            _image.DOColor(color, _fadeTime).SetEase(Ease.InOutSine).SetLink(gameObject).OnComplete(() =>
            {
                _image.DOColor(_defaultColor, _fadeTime).SetEase(Ease.InOutSine).SetLink(gameObject);
            });
        }
    }
}