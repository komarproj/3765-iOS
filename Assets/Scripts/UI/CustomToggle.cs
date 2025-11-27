using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class CustomToggle : MonoBehaviour
    {
        public Button OffButton;
        public Button OnButton;
        
        [SerializeField] private Sprite OffSprite;
        [SerializeField] private Sprite OnSprite;

        [SerializeField] private TextMeshProUGUI OffText;
        [SerializeField] private TextMeshProUGUI OnText;

        [SerializeField] private Color _selectColor;
        [SerializeField] private Color _deselectColor;
        
        public event Action<bool> OnValueChanged;

        private void Awake()
        {
            OffButton.onClick.AddListener(() =>
            {
                OnValueChanged?.Invoke(false);
                SetEnabled(false);
            });
            OnButton.onClick.AddListener(() =>
            {
                OnValueChanged?.Invoke(true);
                SetEnabled(true);
            });
        }

        public void SetEnabled(bool value)
        {
            OffButton.image.sprite = value ? OffSprite : OnSprite;
            OnButton.image.sprite = value ? OnSprite : OffSprite;
            
            OffText.color = value ? _selectColor : _deselectColor;
            OnText.color = value ? _selectColor : _deselectColor;
        }
    }
}