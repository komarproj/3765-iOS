using System;
using DefaultNamespace.Gameplay;
using DefaultNamespace.Gameplay.HeatMiniGame;
using DefaultNamespace.Shop;
using Game.UserData;
using UnityEngine;
using UnityEngine.UI;
using Zenject;

namespace UI.Screens
{
    public class GameScreen : BaseScreen
    {
        [SerializeField] private Button _backButton;
        [SerializeField] private Button _playButton;
        [SerializeField] private ErrorDisplay _errorDisplay;
        
        [SerializeField] private PreparationStage _preparationStage;
        [SerializeField] private GameStage _gameStage;
        [SerializeField] private HeatGameTutorial _heatGameTutorial;
        [SerializeField] private IncubationSound _incubationSound;
        
        public event Action OnBackPressed;
        public event Action OnGameStarted;
        
        private GameplayData _gameplayData;
        private SaveSystem _saveSystem;

        [Inject]
        private void Construct(GameplayData gameplayData, SaveSystem saveSystem)
        {
            _gameplayData = gameplayData;
            _saveSystem = saveSystem;
        }
        
        public void Initialize()
        {
            SubscribeEvents();
            _preparationStage.StartPreparation();
        }
        
        private void SubscribeEvents()
        {
            _backButton.onClick.AddListener(() => OnBackPressed?.Invoke());
            _playButton.onClick.AddListener(StartGame);
        }

        private async void StartGame()
        {
            if (_gameplayData.IncubatedEggs.Count == 0)
            {
                _errorDisplay.ShowError("PLACE AT LEAST 1 EGG!");
                return;
            }
            
            if (_saveSystem.Data.EnergyData.Energy.Value < 20)
            {
                _errorDisplay.ShowError("NOT ENOUGH ENERGY!");
                return;
            }
            
            _preparationStage.EndPreparation();
            _gameStage.StartGame();
            _incubationSound.StartAudio();
            
            if (!_saveSystem.Data.TutorialData.FinishedGameTutorial)
            {
                await _heatGameTutorial.PlayTutorial();
                _saveSystem.Data.TutorialData.FinishedGameTutorial = true;
            }
            
            OnGameStarted?.Invoke();
        }

        public void StopGame()
        {
            _incubationSound.StopAudio();
        }
    }
}