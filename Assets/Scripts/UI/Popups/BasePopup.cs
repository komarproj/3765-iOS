using System;
using DG.Tweening;
using Game.UserData;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

namespace UI.Popups
{
    public abstract class BasePopup : MonoBehaviour
    {
        [SerializeField] [Header("Animation Properties")]
        private RectTransform _animatedContent;

        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] [Min(0.01f)] private float _animTime = 0.4f;
        [SerializeField] private Ease _animationEaseIn = Ease.OutBack; // gives a “pop” feeling
        [SerializeField] private Ease _animationEaseOut = Ease.InBack; // slightly pulls in when closing
        [SerializeField] protected Button _closeButton;

        public event Action OnDestroyed;

        private Sequence _popupSequence;

        private void Awake()
        {
            AudioManager.Instance.PlayPopupSound();
            _closeButton?.onClick.AddListener(DestroyPopup);
            PlayScaleAnimation();
        }

        private void PlayScaleAnimation()
        {
            if (!_animatedContent)
                return;

            _canvasGroup.DOFade(1, _animTime).SetLink(gameObject);
            
            // Optional: tiny anticipation rotation
            _animatedContent.localScale = Vector3.zero;
            _animatedContent.localRotation = Quaternion.Euler(0, 0, Random.Range(-3f, 3f));

            // Build a short sequence for a more playful effect
            _popupSequence = DOTween.Sequence()
                .Append(_animatedContent.DOScale(1.1f, _animTime * 0.7f)
                    .SetEase(_animationEaseIn))
                .Append(_animatedContent.DOScale(1f, _animTime * 0.3f)
                    .SetEase(Ease.OutSine))
                .Join(_animatedContent.DOLocalRotate(Vector3.zero, _animTime * 0.8f)
                    .SetEase(Ease.OutSine))
                .SetLink(gameObject);
        }

        public void DestroyPopup()
        {
            AudioManager.Instance.PlayButtonSound();

            if (_animatedContent)
            {
                _canvasGroup.DOKill();
                _canvasGroup.DOFade(0, _animTime).SetLink(gameObject);

                _popupSequence?.Kill();
                _animatedContent.DOKill();

                // Bounce-out close animation
                _animatedContent.DOScale(0.8f, _animTime * 0.4f)
                    .SetEase(Ease.InOutSine)
                    .OnComplete(() =>
                    {
                        _animatedContent.DOScale(0f, _animTime * 0.6f)
                            .SetEase(_animationEaseOut)
                            .SetLink(gameObject)
                            .OnComplete(DestroySelf);
                    });
            }
            else
            {
                DestroySelf();
            }
        }

        private void DestroySelf()
        {
            OnDestroyed?.Invoke();
            Destroy(gameObject);
        }
    }
}