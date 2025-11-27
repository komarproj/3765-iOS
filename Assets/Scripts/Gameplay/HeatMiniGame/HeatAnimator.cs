using System;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Gameplay.HeatMiniGame
{
    public class HeatAnimator : MonoBehaviour
    {
        [SerializeField] private RectTransform _lineRect;
        [SerializeField] private RectTransform _progressRect;
        [SerializeField] private float _speed = 1.5f;

        private Sequence _sequence;
        private float _topLimit;
        private float _bottomLimit;
        
        public void ApplyUpgrade(float multiplier) => _speed *= multiplier;

        private void Start()
        {
            float halfHeight = _lineRect.rect.height * 0.5f;
            float progressHalfHeight = _progressRect.rect.height * 0.5f;

            _topLimit = progressHalfHeight - halfHeight;
            _bottomLimit = -progressHalfHeight + halfHeight;

            AnimateLine();
        }

        private void AnimateLine()
        {
            _sequence?.Kill();
            _sequence = DOTween.Sequence();

            _sequence.Append(_lineRect.DOAnchorPosY(_topLimit, _speed / 2).SetEase(Ease.Linear));
            _sequence.Append(_lineRect.DOAnchorPosY(_bottomLimit, _speed).SetEase(Ease.Linear));
            _sequence.Append(_lineRect.DOAnchorPosY(0, _speed / 2).SetEase(Ease.Linear));
            _sequence.SetLoops(-1, LoopType.Restart);
            _sequence.SetLink(gameObject);
        }
        
        public void StopAnimation() => _sequence?.Kill();

        public float LinePositionY => _lineRect.anchoredPosition.y;
    }
}