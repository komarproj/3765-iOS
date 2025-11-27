using Data;
using Game.UserData;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace DefaultNamespace.UI
{
    public class LevelDisplay : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI _levelText;
        [SerializeField] private TextMeshProUGUI _experienceText;
        [SerializeField] private Slider _expSlider;
        
        [Inject]
        private void Construct(SaveSystem saveSystem)
        {
            var data = saveSystem.Data.ExperienceData;
            
            int currentLevel = data.Level;
            _levelText.text = (currentLevel + 1).ToString();

            if (currentLevel == Constants.MaxLevel)
            {
                _experienceText.text = "MAX";
                _expSlider.value = 1;
                return;
            }
            
            var currentExperience = data.Experience;
            var thresholds = Constants.ExperienceThresholds;
            
            var targetExp = thresholds[currentLevel];
            
            _experienceText.text = (currentExperience +"/"+ targetExp).ToString();
            _expSlider.value = currentExperience * 1f / targetExp;
        }
    }
}