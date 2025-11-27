using System;
using Cysharp.Threading.Tasks;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Gameplay.HeatMiniGame
{
    public class HeatGameTutorial : MonoBehaviour
    {
        [SerializeField] private CanvasGroup _tutorialCanvas;
        
        [SerializeField] private RectTransform _arrow;
        [SerializeField] private RectTransform _helpRect;
        [SerializeField] private TextMeshProUGUI _helpText;
        [SerializeField] private RectTransform _incubator;
        [SerializeField] private RectTransform _timer;
        [SerializeField] private RectTransform _healthBonus;
        [SerializeField] private RectTransform _freezeBonus;
        [SerializeField] private RectTransform _timerBonus;
        [SerializeField] private Button _nextButton;
        
        private UniTaskCompletionSource _cts = new();
        
        private Sequence _arrowSequence;
        
        public async UniTask PlayTutorial()
        {
            StartStage1();
            await _cts.Task;
        }

        private void StartStage1()
        {
            _tutorialCanvas.blocksRaycasts = true;
            _tutorialCanvas.DOFade(1, 1);
            _helpText.text = "TAP ON THE INCUBATOR TO INCREASE IT'S TEMPERATURE";
            PlayHandAnim(_incubator.position);
            
            _nextButton.onClick.AddListener(StartStage2);
        }

        private void StartStage2()
        {
            _nextButton.onClick.RemoveListener(StartStage2);
            
            _helpText.text = "KEEP THE INCUBATOR HEATED UNTIL THE TIME ENDS";
            PlayHandAnim(_timer.position);
            
            _nextButton.onClick.AddListener(StartStage3);
        }

        private void StartStage3()
        {
            _nextButton.onClick.RemoveListener(StartStage3);
            _arrow.gameObject.SetActive(false);

            PlaceHelpRect(_healthBonus.position);
            _helpText.text = "HEALS AND REVIVES THE FAILED EGGS";
            
            _nextButton.onClick.AddListener(StartStage4);
        }

        private void StartStage4()
        {
            _nextButton.onClick.RemoveListener(StartStage4);
            
            PlaceHelpRect(_freezeBonus.position);
            _helpText.text = "TEMPORARILY STOPS THE TEMPERATURE LOSS ON EGGS";
            
            _nextButton.onClick.AddListener(StartStage5);
        }

        private void StartStage5()
        {
            _nextButton.onClick.RemoveListener(StartStage5);
            PlaceHelpRect(_timerBonus.position);
            _helpText.text = "LOWERS THE CURRENT TIME BY 5 SECONDS";
            
            _nextButton.onClick.AddListener(async () =>
            {
                await _tutorialCanvas.DOFade(0, 0.5f).AsyncWaitForCompletion();
                _tutorialCanvas.blocksRaycasts = false;
                _cts.TrySetResult();
            });
        }

        private void PlayHandAnim(Vector3 pos)
        {
            _arrowSequence.Kill();
            _arrowSequence = DOTween.Sequence();
            
            _arrow.position = pos;

            _arrowSequence.Append(_arrow.DOScale(Vector3.one * 1.2f, 0.33f).SetEase(Ease.InSine));
            _arrowSequence.Append(_arrow.DOScale(Vector3.one, 0.5f).SetEase(Ease.InCubic));
            _arrowSequence.SetLoops(-1, LoopType.Yoyo);
            _arrowSequence.SetLink(_arrow.gameObject);
        }

        private void PlaceHelpRect(Vector3 pos)
        {
            pos.y += 100;
            _helpRect.position = pos;
        }
    }
}