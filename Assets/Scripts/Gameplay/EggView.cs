using System;
using DG.Tweening;
using Game.UserData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.Gameplay
{
    public class EggView : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private Button _button;
        
        [Inject]
        private SaveSystem _saveSystem;

        private int _id;
        
        public void SetData(Sprite sprite, int id)
        {
            _spriteRenderer.sprite = sprite;
            _id = id;
        }

        private void Awake()
        {
            _button.onClick.AddListener(ClaimEgg);
        }

        private void ClaimEgg()
        {
            AudioManager.Instance.PlayEggSound();
            _button.interactable = false;
            _saveSystem.Data.InventoryData.Eggs.Add(_id);
            _saveSystem.Data.FieldData.EggsOnField.Remove(_id);
            transform.DOScale(Vector3.zero, 0.5f).SetEase(Ease.InOutCirc).SetLink(gameObject).OnComplete(() =>
            {
                Destroy(gameObject);
            });
        }
    }
}