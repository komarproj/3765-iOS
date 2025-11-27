using System;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.Gameplay
{
    public class IncubatorPreparation : MonoBehaviour
    {
        [SerializeField] private Button _upgradeButton;
        [SerializeField] private Button _setEggButton;

        [SerializeField] private Image _placedEggImage;

        public event Action OnUpgradePressed;
        public event Action OnSetEggPressed;

        private void Awake()
        {
            _upgradeButton.onClick.AddListener(() => OnUpgradePressed?.Invoke());
            _setEggButton.onClick.AddListener(() => OnSetEggPressed?.Invoke());
        }

        public void PlaceEgg(Sprite eggSprite)
        {
            _placedEggImage.sprite = eggSprite;
            _placedEggImage.gameObject.SetActive(true);
            
            _setEggButton.image.color = new Color(1, 1, 1, 0);
        }

        public void Enable(bool enable)
        {
            _upgradeButton.gameObject.SetActive(enable);
            _setEggButton.gameObject.SetActive(enable);
        }
    }
}