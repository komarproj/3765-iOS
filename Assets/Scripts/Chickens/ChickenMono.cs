using Data;
using TMPro;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class ChickenMono : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private ChickenRewardDisplay _chickenRewardDisplay;
        
        private ChickenData _chickenData;
        public ChickenData ChickenData => _chickenData;
        
        [Inject]
        private ChickenWalkingAnimator _walkingAnimator;
        
        public void SetData(ChickenData chickenData, Sprite sprite)
        {
            _chickenData = chickenData;
            _spriteRenderer.sprite = sprite;

            _levelText.text = "Lvl " + (chickenData.Level + 1);
            _chickenRewardDisplay.SetData(chickenData);
        }

        public void StartDragging() => _walkingAnimator.StopAnimation(this);

        public void RestartAnimation() => _walkingAnimator.AnimatePet(this, transform.position);
    }
}