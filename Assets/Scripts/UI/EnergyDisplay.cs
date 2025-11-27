using Data;
using Game.UserData;
using TMPro;
using UniRx;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.UI
{
    public class EnergyDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _energyText;
        [SerializeField] private Slider _energySlider;
        
        [Inject]
        private void Construct(SaveSystem saveSystem)
        {
            var energy = saveSystem.Data.EnergyData.Energy;
            energy.Subscribe((value) =>
            {
                _energyText.text = value + "/" + Constants.MaxEnergy;
                _energySlider.value = value * 1f / Constants.MaxEnergy;
            }).AddTo(gameObject);
        }
    }
}