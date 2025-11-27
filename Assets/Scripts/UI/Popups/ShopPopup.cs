using System;
using DefaultNamespace.UI;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

namespace UI.Popups
{
    public class ShopPopup : BasePopup
    {
        [SerializeField] private Button _leftButton;
        [SerializeField] private Button _rightButton;
        [SerializeField] private PagesCircleDisplay _pagesCircleDisplay;
        [SerializeField] private CanvasGroup[] _pages;
        [SerializeField] private float _fadeDuration = 0.5f;

        private int _currentPage;
        private int _pagesCount = 3;

        private void Start()
        {
            _leftButton.onClick.AddListener(() => ChangePage(-1));
            _rightButton.onClick.AddListener(() => ChangePage(+1));
            EnablePage(0);
        }

        private void ChangePage(int increment)
        {
            DisablePage(_currentPage);
            ClampPage(increment);
            EnablePage(_currentPage);
            
            _pagesCircleDisplay.SetEnabled(_currentPage);
        }

        private void ClampPage(int increment)
        {
            _currentPage += increment;
            
            if(_currentPage < 0) 
                _currentPage = _pagesCount - 1;
            
            if(_currentPage >= _pagesCount)
                _currentPage = 0;
        }

        private void EnablePage(int index)
        {
            FadePage(_pages[index], true);
        }
        
        private void DisablePage(int index)
        {
            FadePage(_pages[index], false);
        }

        private void FadePage(CanvasGroup page, bool enable)
        {
            page.DOKill();

            page.blocksRaycasts = enable;
            page.DOFade(enable ? 1 : 0, _fadeDuration).SetLink(gameObject);
        }
    }
}