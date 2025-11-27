using DefaultNamespace;
using DefaultNamespace.Achievements;
using DefaultNamespace.Gameplay;
using DefaultNamespace.Shop;
using DefaultNamespace.UI;
using Game.UserData;
using Gameplay.Services;
using Runtime.Game.GameStates.Game.Screens;
using Spinner;
using UnityEngine;
using Zenject;

namespace Installers
{
    public class GameInstaller : MonoInstaller
    {
        [SerializeField] private UIFactory _uiFactory;

        public override void InstallBindings()
        {
            Container.BindInterfacesAndSelfTo<GameBootstrapController>().AsSingle();
            Container.Bind<UIFactory>().FromComponentInNewPrefab(_uiFactory).AsSingle();
            Container.Bind<DIFactory>().AsSingle();
            Container.Bind<AchievementsFactory>().AsSingle();
            BindStates();
            
            Container.Bind<LoginService>().AsSingle();
            Container.Bind<SpinnerService>().AsSingle();
            Container.Bind<ShopFactory>().AsSingle();
            Container.Bind<ShopService>().AsSingle();
            Container.Bind<EggSelectFactory>().AsSingle();
            Container.Bind<SpritesProvider>().AsSingle();
            Container.Bind<GameplayData>().AsSingle();
            Container.Bind<GameplayTimer>().AsSingle();
            Container.Bind<ExperienceService>().AsSingle();
            Container.Bind<AchievementsUnlocker>().AsSingle().NonLazy();
            Container.Bind<MainBgEnabler>().FromComponentInHierarchy().AsSingle();
            Container.Bind<AudioManager>().FromComponentInHierarchy().AsSingle().NonLazy();
                
            Container.BindInterfacesAndSelfTo<ChickenDragController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChickenMergeController>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChickenSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<EggSpawner>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChickenEggsService>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChickenWalkingAnimator>().AsSingle();
            Container.BindInterfacesAndSelfTo<PlayerInputProvider>().AsSingle();
            Container.BindInterfacesAndSelfTo<ChickenCoinService>().AsSingle();
            
            Container.BindInterfacesAndSelfTo<IncubationService>().AsSingle();
            Container.BindInterfacesAndSelfTo<EnergyService>().AsSingle();
        }

        private void BindStates()
        {
            Container.Bind<StateMachine>().AsSingle();
            Container.Bind<StateController>().To<MainScreenStateController>().AsSingle();
            Container.Bind<StateController>().To<TutorialScreenStateController>().AsSingle();
            Container.Bind<StateController>().To<GameScreenStateController>().AsSingle();
        }
    }
}
