using DefaultNamespace.Gameplay;
using TMPro;
using UniRx;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.UI
{
    public class TimerDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _timerText;

        [Inject]
        private void Construct(GameplayData gameplayData)
        {
            gameplayData.LevelTime.Subscribe(time =>
            {
                _timerText.text = time.ToString("00:00");
            }).AddTo(gameObject);
        }
    }
}