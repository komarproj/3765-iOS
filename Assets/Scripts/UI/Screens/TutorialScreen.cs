using System;
using DefaultNamespace.UI;
using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Screens
{
    public class TutorialScreen : BaseScreen
    {
        [SerializeField] private Button _nextButton;
        [SerializeField] private Image[] _images;
        [SerializeField] private float _fadeDuration = 0.5f;
        [SerializeField] private PagesCircleDisplay _pagesCircleDisplay;
        
        public event Action OnTutotialFinished;

        private int _index = 0;

        private void Awake()
        {
            _nextButton.onClick.AddListener(IncrementTutorialIndex);
        }

        private void IncrementTutorialIndex()
        {
            if (_index == 3)
            {
                OnTutotialFinished?.Invoke();
                return;
            }
            
            _images[_index].DOFade(0, _fadeDuration).SetLink(gameObject);
            
            _index++;
            _pagesCircleDisplay.SetEnabled(_index);
            
            _images[_index].DOFade(1, _fadeDuration).SetLink(gameObject);

            if (_index == 3) 
                _nextButton.GetComponentInChildren<TextMeshProUGUI>().text = "FINISH";
        }
    }
}