using System;
using System.Collections.Generic;
using Cysharp.Threading.Tasks;
using Game.UserData;
using UI.Popups;
using UI.Screens;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.UI
{
    public class UIFactory : MonoBehaviour
    {
        [SerializeField] private FadeAnimationController _fadeAnimationController;
        [SerializeField] private GameUIContainer _uiContainer;
        [SerializeField] private RectTransform _canvas;
        
        private readonly List<BaseScreen> _openedScreens = new();

        [Inject] private DIFactory _factory;
        
        public T CreateScreen<T>() where T : BaseScreen
        {
            if (!_uiContainer.TryGetScreen<T>(out var prefab)) 
                throw new Exception($"Screen {typeof(T)} not found");
            
            var screen = _factory.Create<T>(prefab.gameObject);
            _openedScreens.Add(screen);
            screen.transform.SetParent(_canvas, false);

            _fadeAnimationController.FadeOut().Forget();

            return screen;

        }

        public T CreatePopup<T>() where T : BasePopup
        {
            if (!_uiContainer.TryGetPopup<T>(out var prefab)) 
                throw new Exception($"Popup {typeof(T)} not found");
            
            var popup = _factory.Create<T>(prefab.gameObject);
            popup.transform.SetParent(_canvas, false);
            return popup;
        }

        public async UniTask HideScreen<T>() where T : BaseScreen
        {
            for (var i = 0; i < _openedScreens.Count; i++)
                if (_openedScreens[i].GetType() == typeof(T))
                {
                    var screen = _openedScreens[i];
                    _openedScreens.RemoveAt(i);

                    await _fadeAnimationController.FadeIn();

                    Destroy(screen.gameObject);
                }
        }
    }
}