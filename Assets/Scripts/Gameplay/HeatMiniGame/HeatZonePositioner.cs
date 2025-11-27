using UnityEngine;
using Random = UnityEngine.Random;

namespace DefaultNamespace.Gameplay.HeatMiniGame
{
    public class HeatZonePositioner : MonoBehaviour
    {
        [SerializeField] private RectTransform _progressRect;
        [SerializeField] private RectTransform _zoneRect;

        public void PlaceGreenZone()
        {
            float progressHalfHeight = _progressRect.rect.height * 0.5f;
            float zoneHalfHeight = _zoneRect.rect.height * 0.5f;

            float minY = -progressHalfHeight + zoneHalfHeight;
            float maxY = progressHalfHeight - zoneHalfHeight;

            float randomY = Random.Range(minY, maxY);

            Vector2 pos = _zoneRect.anchoredPosition;
            pos.y = randomY;
            _zoneRect.anchoredPosition = pos;
        }
    }
}