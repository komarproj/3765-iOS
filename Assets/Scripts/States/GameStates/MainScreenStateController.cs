using Cysharp.Threading.Tasks;
using DefaultNamespace.Gameplay;
using DefaultNamespace.UI;
using Gameplay.Services;
using UI.Screens;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class MainScreenStateController : StateController
    {
        private readonly UIFactory _uiFactory;
        private readonly ChickenSpawner _spawner;
        private readonly EggSpawner _eggSpawner;
        private readonly PlayerInputProvider _playerInputProvider;
        private readonly MainBgEnabler _bgEnabler;
        
        private MainScreen _screen;
        
        public MainScreenStateController(UIFactory uiFactory, ChickenSpawner spawner, 
            PlayerInputProvider playerInputProvider, MainBgEnabler bgEnabler,
            EggSpawner eggSpawner)
        {
            _uiFactory = uiFactory;
            _spawner = spawner;
            _playerInputProvider = playerInputProvider;
            _bgEnabler = bgEnabler;
            _eggSpawner = eggSpawner;
        }
        
        public override async UniTask EnterState()
        {
            _bgEnabler.Enable(true);
            _playerInputProvider.SetEnabled(true);
            _spawner.CreatePetsFromInventory();
            _eggSpawner.SpawnFromInventory();
            _screen = _uiFactory.CreateScreen<MainScreen>();
            _screen.Initialize();
            SubscribeToScreenEvents();
        }
        
        public override async UniTask ExitState()
        {
            _playerInputProvider.SetEnabled(false);
            await _uiFactory.HideScreen<MainScreen>();
            _bgEnabler.Enable(false);
            _spawner.DestroyAllPets();
            _eggSpawner.DestroyEggs();
        }   
        
        private void SubscribeToScreenEvents()
        {
            _screen.OnPlayPressed += async () => await GoToState<GameScreenStateController>();
        }
    }
}