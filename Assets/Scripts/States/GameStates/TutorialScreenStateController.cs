using Cysharp.Threading.Tasks;
using DefaultNamespace.Achievements;
using DefaultNamespace.UI;
using Game.UserData;
using UI.Screens;

namespace Runtime.Game.GameStates.Game.Screens
{
    public class TutorialScreenStateController : StateController
    {
        private readonly UIFactory _uiFactory;
        private readonly SaveSystem _saveSystem;
        
        private TutorialScreen _screen;
        
        public TutorialScreenStateController(UIFactory uiFactory, SaveSystem saveSystem)
        {
            _uiFactory = uiFactory;
            _saveSystem = saveSystem;
        }
        
        public override async UniTask EnterState()
        {
            _screen = _uiFactory.CreateScreen<TutorialScreen>();
            SubscribeToScreenEvents();
        }
        
        public override async UniTask ExitState()
        {
            await _uiFactory.HideScreen<TutorialScreen>();
        }   
        
        private void SubscribeToScreenEvents()
        {
            _screen.OnTutotialFinished += FinishTutorial;
        }

        private void FinishTutorial()
        {
            AchievementsUnlocker.Instance.OnTutorialComplete();
            _saveSystem.Data.TutorialData.FinishedTutorial = true;
            GoToState<MainScreenStateController>().Forget();
        }
    }
}