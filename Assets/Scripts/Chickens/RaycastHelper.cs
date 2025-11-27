using UnityEngine;

namespace Gameplay.Services
{
    public class RaycastHelper
    {
        public static bool Cast<T>(Vector3 worldPos, out T result) where T : Component
        {
            result = null;
            
            var hit = Physics2D.Raycast(worldPos, Vector2.zero);
            
            if (!hit)
                return false;
            
            var hitCollider = hit.collider;
            
            return hitCollider.TryGetComponent(out result);
        }
    }
}