using UnityEngine;

namespace DefaultNamespace.Gameplay
{
    public class MainBgEnabler : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer _spriteRenderer;
        
        public void Enable(bool enable) => _spriteRenderer.enabled = enable;
    }
}