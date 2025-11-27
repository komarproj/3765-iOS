using DG.Tweening;
using UnityEngine;

namespace Gameplay.Services
{
    public class ChickenAnimator : MonoBehaviour
    {
        [Header("Idle Animation Settings")]
        [SerializeField] private float scaleAmplitude = 0.05f;     // How much the character "breathes"
        [SerializeField] private float scaleDuration = 1.5f;       // How fast the breathing loop is
        [SerializeField] private float rotationAmplitude = 3f;     // How much the character tilts
        [SerializeField] private float rotationDuration = 2f;      // How fast the tilt loop is
        [SerializeField] private Ease scaleEase = Ease.InOutSine;
        [SerializeField] private Ease rotationEase = Ease.InOutSine;

        private Vector3 _initialScale;
        private Vector3 _initialRotation;
        private Sequence _idleSequence;

        private void Start()
        {
            _initialScale = transform.localScale;
            _initialRotation = transform.localEulerAngles;

            PlayIdleAnimation();
        }

        private void PlayIdleAnimation()
        {
            _idleSequence = DOTween.Sequence();

            _idleSequence.Append(transform.DOScale(_initialScale * (1 + scaleAmplitude), scaleDuration)
                    .SetEase(scaleEase))
                .Append(transform.DOScale(_initialScale, scaleDuration)
                    .SetEase(scaleEase))
                .SetLoops(-1, LoopType.Restart)
                .SetId("IdleScale").SetLink(gameObject);

            transform.DORotate(new Vector3(0, 0, rotationAmplitude), rotationDuration)
                .SetEase(rotationEase)
                .SetLoops(-1, LoopType.Yoyo)
                .SetId("IdleRotation").SetLink(gameObject);
        }

        private void OnDisable()
        {
            DOTween.Kill("IdleScale");
            DOTween.Kill("IdleRotation");
        }
    }
}