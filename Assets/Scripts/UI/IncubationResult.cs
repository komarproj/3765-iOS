using DG.Tweening;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace DefaultNamespace.UI
{
    public class IncubationResult : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _text;
        [SerializeField] private Image _image;
        [SerializeField] private CanvasGroup _canvasGroup;
        [SerializeField] private GameObject _failedGo;
        
        public void SetData(string text, Sprite sprite, bool failed)
        {
            _text.text = text;
            _image.sprite = sprite;
            _failedGo.SetActive(failed);
        }

        public void Show()
        {
            _canvasGroup.DOKill();
            _canvasGroup.DOFade(1, 0.25f).SetLink(gameObject);
        }

        public void Hide()
        {
            _canvasGroup.DOKill();
            _canvasGroup.DOFade(0, 0.25f).SetLink(gameObject);
        }
    }
}