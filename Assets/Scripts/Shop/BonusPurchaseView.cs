using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Shop
{
    public class BonusPurchaseView : MonoBehaviour
    {
        [SerializeField, Min(0)] private int _price;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Button _button;
        
        public event Action<int> OnPurchasePressed;

        private void Awake()
        {
            _priceText.text = _price.ToString();
            _button.onClick.AddListener(() => OnPurchasePressed?.Invoke(_price));
        }
    }
}