using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Shop
{
    public class EggPurchaseView : MonoBehaviour
    {
        [SerializeField] private Image _image;
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Button _button;

        public event Action<int, int> OnPurchasePressed;
        
        public void Initialize(Sprite sprite, int price, int id)
        {
            _image.sprite = sprite;
            _priceText.text = price.ToString();
            
            _button.onClick.AddListener(() =>
            {
                OnPurchasePressed?.Invoke(id, price);
            });
        }
    }
}