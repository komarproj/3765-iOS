using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class ChickenDragController : ITickable
    {
        private readonly PlayerInputProvider _playerInputProvider;
        private readonly ChickenMergeController _chickenMergeController;
        
        private ChickenMono _draggedChicken;
        private Vector3 _firstChickenPosition;

        public ChickenDragController(PlayerInputProvider playerInputProvider, 
            ChickenMergeController chickenMergeController)
        {
            _playerInputProvider = playerInputProvider;
            _chickenMergeController = chickenMergeController;
            
            _playerInputProvider.OnTouchStart += ProcessTouchStart;
            _playerInputProvider.OnTouchEnd += ProcessTouchEnd;
        }
        
        public void Tick()
        {
            if(!_playerInputProvider.HasInput())
                return;

            if(!_draggedChicken)
                return;

            var worldPos = _playerInputProvider.GetTouchWorldPos();
            _draggedChicken.transform.position = worldPos;
        }

        private void ProcessTouchStart(Vector3 worldPos)
        {
            if(!RaycastHelper.Cast(worldPos, out ChickenMono chicken))
                return;

            _draggedChicken = chicken;
            _draggedChicken.StartDragging();
        }

        private void ProcessTouchEnd(Vector3 worldPos)
        {
            RaycastHelper.Cast(worldPos, out ChickenMono secondChicken);
            _chickenMergeController.MergeChickens(_draggedChicken, secondChicken);
            
            _draggedChicken = null;
        }
    }
}