using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Gameplay
{
    public class EggSelectView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private Button _button;

        public event Action<int> OnSelected;

        public void Initialize(int id, Sprite sprite)
        {
            _button.onClick.AddListener(() => OnSelected?.Invoke(id));
            _image.sprite = sprite;
        }
    }
}