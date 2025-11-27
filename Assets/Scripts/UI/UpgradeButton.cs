using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _priceText;
        [SerializeField] private Button _upgradeButton;

        public Button Button => _upgradeButton;
        
        public void SetData(int price)
        {
            _priceText.text = price.ToString();
            transform.DOPunchScale(Vector3.one * 0.1f, 0.25f).SetEase(Ease.InOutCubic);
        }
    }
}