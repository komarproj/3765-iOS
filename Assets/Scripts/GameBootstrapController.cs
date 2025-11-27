using AssetsProvider;
using Game.UserData;
using Runtime.Game.GameStates.Game.Screens;
using Runtime.Game.Services.SettingsProvider;
using UnityEditor;
using UnityEngine;
using Zenject;

namespace DefaultNamespace
{
    public class GameBootstrapController : IInitializable
    {
        private readonly StateMachine _stateMachine;
        private readonly DiContainer _container;
        private readonly SaveSystem _saveSystem;
        private readonly PrefabsProvider _prefabsProvider;
        private readonly ConfigsProvider _configsProvider;
        private readonly EnergyService _energyService;
        private readonly ChickenCoinService _chickenCoinService;
        private readonly ChickenEggsService _chickenEggsService;

        public GameBootstrapController(StateMachine stateMachine, DiContainer container, SaveSystem saveSystem,
            PrefabsProvider prefabsProvider, ConfigsProvider configsProvider, EnergyService energyService,
            ChickenCoinService chickenCoinService, ChickenEggsService chickenEggsService)
        {
            _stateMachine = stateMachine;
            _container = container;
            _saveSystem = saveSystem;
            _prefabsProvider = prefabsProvider;
            _configsProvider = configsProvider;
            _energyService = energyService;
            _chickenCoinService = chickenCoinService;
            _chickenEggsService = chickenEggsService;
        }
        
        public async void Initialize()
        {
            Application.targetFrameRate = 60;
            Input.multiTouchEnabled = false;
            
            _container.InstantiateComponentOnNewGameObject<ApplicationStateListener>("ApplicationStateListener");
            
            await _prefabsProvider.Initialize();
            await _configsProvider.Initialize();
            
            _energyService.ProcessOfflineGeneration();
            _chickenCoinService.ProcessOfflineGeneration();
            _chickenEggsService.ProcessOfflineGeneration();
            
            if(_saveSystem.Data.TutorialData.FinishedTutorial)
                await _stateMachine.GoToState<MainScreenStateController>();
            else
                await _stateMachine.GoToState<TutorialScreenStateController>();
        }
    }
}