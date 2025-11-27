using Cysharp.Threading.Tasks;
using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.UI
{
    public class FadeAnimationController : MonoBehaviour
    {
        [SerializeField] private float _fadeTime = 0.25f;
        [SerializeField] [Space] private CanvasGroup _fadeCanvas;

        private Tween _fadeTween;
        private UniTaskCompletionSource _fadeCompletionSource;

        public async UniTask FadeIn()
        {
            CancelActiveFade();

            _fadeCanvas.alpha = 0;
            _fadeCanvas.blocksRaycasts = true;

            _fadeCompletionSource = new UniTaskCompletionSource();

            _fadeTween = _fadeCanvas.DOFade(1, _fadeTime)
                .OnComplete(() => _fadeCompletionSource.TrySetResult());

            await _fadeCompletionSource.Task;

            _fadeCanvas.blocksRaycasts = false;
        }

        public async UniTask FadeOut()
        {
            CancelActiveFade();

            _fadeCanvas.alpha = 1;
            _fadeCanvas.blocksRaycasts = false;

            _fadeCompletionSource = new UniTaskCompletionSource();

            _fadeTween = _fadeCanvas.DOFade(0, _fadeTime)
                .OnComplete(() => _fadeCompletionSource.TrySetResult());

            await _fadeCompletionSource.Task;
        }

        private void CancelActiveFade()
        {
            _fadeCompletionSource?.TrySetCanceled();
            _fadeTween?.Kill();
        }
    }
}