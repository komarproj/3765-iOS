using System;
using DG.Tweening;
using Game.UserData;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class PagesCircleDisplay : MonoBehaviour
    {
        [SerializeField] private Image[] _circleImages;
        
        [SerializeField] private Sprite _activeSprite;
        [SerializeField] private Sprite _inactiveSprite;

        private float _animTime = 0.3f;
        private Vector3 _maxScale = Vector3.one * 1.3f;
        
        private int _prevIndex = 0;

        private void Awake()
        {
            PlayEnableAnim(_circleImages[0]);
        }

        public void SetEnabled(int index)
        {
            PlayDisableAnim(_circleImages[_prevIndex]);
            PlayEnableAnim(_circleImages[index]);
            _prevIndex = index;
            
            AudioManager.Instance.PlayPageChangeSound();
        }

        private void PlayEnableAnim(Image image)
        {
            var transform = image.transform;
            transform.DOKill();
            
            image.sprite = _activeSprite;
            transform.DOScale(_maxScale, _animTime).SetLink(gameObject);
        }

        private void PlayDisableAnim(Image image)
        {
            var transform = image.transform;
            transform.DOKill();
            
            image.sprite = _inactiveSprite;
            transform.DOScale(Vector3.one, _animTime).SetLink(gameObject);
        }
    }
}