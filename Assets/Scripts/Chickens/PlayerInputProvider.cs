using System;
using UnityEngine;
using Zenject;

namespace Gameplay.Services
{
    public class PlayerInputProvider : ITickable, IInitializable
    {
        public event Action<Vector3> OnTouchStart;
        public event Action<Vector3> OnTouchEnd;

        private bool _enabled = false;
        
        private Camera _camera;

        public void Initialize()
        {
            _camera = Camera.main;
        }

        public void Tick()
        {
            if(!HasInput())
                return;
            
            var touch = Input.GetTouch(0);
            var worldPos = GetTouchWorldPos();
            
            if(touch.phase == TouchPhase.Began)
                OnTouchStart?.Invoke(worldPos);
            
            if(touch.phase == TouchPhase.Ended)
                OnTouchEnd?.Invoke(worldPos);
        }

        public bool HasInput() => _enabled && Input.touchCount > 0;

        public Vector3 GetTouchWorldPos()
        {
            if(!_enabled)
                return Vector3.zero;
            
            return ConvertScreenToWorldPoint(Input.GetTouch(0).position);
        }

        public void SetEnabled(bool enabled) => _enabled = enabled;

        private Vector3 ConvertScreenToWorldPoint(Vector3 screenPoint)
        {
            var pos = _camera.ScreenToWorldPoint(screenPoint);
            pos.z = 0;
            return pos;
        }
    }
}