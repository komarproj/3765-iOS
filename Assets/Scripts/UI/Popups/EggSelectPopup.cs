using System;
using DefaultNamespace.Gameplay;
using UnityEngine;
using Zenject;

namespace UI.Popups
{
    public class EggSelectPopup : BasePopup
    {
        [SerializeField] private RectTransform _content;
        [SerializeField] private GameObject _noEggsText;
        
        public event Action<int> OnSelected;
        
        [Inject]
        private void Construct(EggSelectFactory factory)
        {
            var views = factory.CreateEggSelectViews();
            
            _noEggsText.SetActive(views.Count == 0);
            
            foreach (var eggSelectView in views)
            {
                eggSelectView.transform.SetParent(_content, false);
                eggSelectView.OnSelected += (id) =>
                {
                    OnSelected?.Invoke(id);
                    DestroyPopup();
                };
            }
        }
    }
}