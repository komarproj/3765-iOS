using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using DefaultNamespace.Achievements;
using DefaultNamespace.Gameplay;
using DefaultNamespace.UI;
using Game.UserData;
using UI.Popups;
using UI.Screens;
using UniRx;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class GameScreenStateController : StateController
    {
        private readonly UIFactory _uiFactory;
        private readonly SaveSystem _saveSystem;
        private readonly GameplayData _gameplayData;
        private readonly IncubationService _incubationService;
        private readonly GameplayTimer _gameplayTimer;
        private readonly ExperienceService _experienceService;
        
        private GameScreen _screen;
        
        private CancellationTokenSource _cts;
        
        public GameScreenStateController(UIFactory uiFactory, GameplayData gameplayData, SaveSystem saveSystem,
            IncubationService incubationService, GameplayTimer gameplayTimer, ExperienceService experienceService)
        {
            _uiFactory = uiFactory;
            _gameplayData = gameplayData;
            _saveSystem = saveSystem;
            _incubationService = incubationService;
            _gameplayTimer = gameplayTimer;
            _experienceService = experienceService;

            _incubationService.OnAllEggsFailed += ProcessGameEnd;
            _gameplayTimer.OnTimerEnd += ProcessGameEnd;
        }
        
        public override async UniTask EnterState()
        {
            AudioManager.Instance.SetGameMusic();
            AchievementsUnlocker.Instance.ResetGreenZone();
            
            _cts = new();
            _gameplayData.IncubatedEggs.Clear();
            _gameplayData.DroppedToFailure = false;
            
            _screen = _uiFactory.CreateScreen<GameScreen>();
            _screen.Initialize();
            SubscribeToScreenEvents();
        }
        
        public override async UniTask ExitState()
        {
            Cancel();

            await _uiFactory.HideScreen<GameScreen>();
            AudioManager.Instance.SetMenuMusic();
        }

        private void Cancel()
        {
            if (_cts != null)
            {
                _cts.Cancel();
                _cts.Dispose();
                _cts = null;
            }
        }

        private void SubscribeToScreenEvents()
        {
            _screen.OnBackPressed += async () =>
            {
                RefundEggs();
                await GoToState<MainScreenStateController>();
            };
            
            _screen.OnGameStarted += StartGame;
        }

        private void RefundEggs()
        {
            foreach (var egg in _gameplayData.IncubatedEggs) 
                _saveSystem.Data.InventoryData.Eggs.Add(egg.EggId);
        }

        private void StartGame()
        {
            _gameplayTimer.StartTime(_cts.Token).Forget();
            _incubationService.SetEnabled(true);
        }

        private void ProcessGameEnd()
        {
            _screen.StopGame();
            Cancel();
            _incubationService.SetEnabled(false);

            RecordData();
            
            var popup = _uiFactory.CreatePopup<GameEndPopup>();
            popup.OnDestroyed += () => GoToState<MainScreenStateController>().Forget();
        }

        private void RecordData()
        {
            _saveSystem.Data.EnergyData.Energy.Value -= Constants.HeatPrice;
            
            var eggs = _gameplayData.IncubatedEggs;

            foreach (var egg in eggs)
            {
                if(!egg.IsActive.Value)
                    return;

                var item = new ChickenData();
                item.Id = egg.EggId;
                _saveSystem.Data.InventoryData.Chickens.Add(item);
                
                AchievementsUnlocker.Instance.OnChickenAdded(_saveSystem.Data.InventoryData.Chickens.Count);
                
                AchievementsUnlocker.Instance.OnHatchComplete(egg.EggId);
                
                if(!_gameplayData.DroppedToFailure)
                    AchievementsUnlocker.Instance.OnPerfectIncubation();
                
                _experienceService.AddExperience(Constants.ExperiencePerIncubation);
                _saveSystem.Data.InventoryData.Balance.Value += Constants.RewardPerIncubation;
            }
        }
    }
}