using DG.Tweening;
using UnityEngine;

namespace DefaultNamespace.Gameplay
{
    public class IncubatorAnimator : MonoBehaviour
    {
        [SerializeField] private float positionShakeStrength = 0.05f;  // how far it moves
        [SerializeField] private float rotationShakeStrength = 2f;     // degrees of tilt
        [SerializeField] private float shakeDuration = 0.5f;           // time per cycle
        [SerializeField] private float pauseBetweenShakes = 0.1f;      // optional pause between shakes

        private Tween _shakeTween;

        public void StartShaking()
        {
            if (_shakeTween != null && _shakeTween.IsActive()) return;

            Vector3 originalPos = transform.localPosition;
            Vector3 originalRot = transform.localEulerAngles;

            // Create a looping subtle shake sequence
            Sequence seq = DOTween.Sequence();

            seq.Append(
                transform.DOShakePosition(shakeDuration, positionShakeStrength, vibrato: 5, randomness: 30, snapping: false, fadeOut: true)
                    .SetEase(Ease.InOutSine)
            );
            seq.Join(
                transform.DOShakeRotation(shakeDuration, new Vector3(0, 0, rotationShakeStrength), vibrato: 3, randomness: 10, fadeOut: true)
                    .SetEase(Ease.InOutSine)
            );
            seq.AppendInterval(pauseBetweenShakes);
            seq.SetLoops(-1, LoopType.Restart);
            seq.OnKill(() =>
            {
                transform.localPosition = originalPos;
                transform.localEulerAngles = originalRot;
            });

            _shakeTween = seq;
            _shakeTween.SetLink(gameObject);
        }

        public void StopShaking()
        {
            _shakeTween?.Kill();
            _shakeTween = null;
        }
    }
}