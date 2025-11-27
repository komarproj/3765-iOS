using System;
using System.Collections.Generic;
using DefaultNamespace.UI;
using DG.Tweening;
using Game.UserData;
using UI.Popups;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Gameplay
{
    public class PreparationStage : MonoBehaviour
    {
        [SerializeField] private List<Incubator> _incubators;
        [SerializeField] private CanvasGroup _canvasGroup;
        
        private SaveSystem _saveSystem;
        private UIFactory _uiFactory;
        private SpritesProvider _spritesProvider;
        private GameplayData _gameplayData;
        
        [Inject]
        private void Construct(SaveSystem saveSystem, UIFactory uiFactory, 
            SpritesProvider spritesProvider, GameplayData gameplayData)
        {
            _saveSystem = saveSystem;
            _uiFactory = uiFactory;
            _spritesProvider = spritesProvider;
            _gameplayData = gameplayData;
        }

        public void StartPreparation()
        {
            _canvasGroup.blocksRaycasts = true;
            _canvasGroup.DOFade(1, 1f).SetLink(gameObject);
            Initialize();
        }

        public void EndPreparation()
        {
            _canvasGroup.blocksRaycasts = false;
            _canvasGroup.DOFade(0, 1f).SetLink(gameObject);
        }
        
        private void Initialize()
        {
            var level = _saveSystem.Data.ExperienceData.Level;

            for (int i = 0; i < _incubators.Count; i++)
            {
                var incubator = _incubators[i];
                incubator.gameObject.SetActive(i <= level);
                incubator.StartPreparation();

                int incubId = i;
                incubator.Preparation.OnUpgradePressed += () => UpgradeIncubator(incubId);
                incubator.Preparation.OnSetEggPressed += () => PlaceEgg(incubId);
            }
        }

        private void UpgradeIncubator(int id)
        {
            var upgradePopup = _uiFactory.CreatePopup<IncubatorUpgradePopup>();
            var upgradeData = _saveSystem.Data.UpgradesData.Upgrades[id];
            upgradePopup.SetData(upgradeData);
        }
        
        private void PlaceEgg(int incubId)
        {
            var incubator = _incubators[incubId];
            var placedEgg = incubator.Egg;
            
            if (placedEgg != null)
            {
                _gameplayData.IncubatedEggs.Remove(placedEgg);
                _saveSystem.Data.InventoryData.Eggs.Add(placedEgg.EggId);
            }
            
            var popup = _uiFactory.CreatePopup<EggSelectPopup>();
            
            popup.OnSelected += (id) =>
            {
                var egg = new IncubatedEggData()
                {
                    EggId = id,
                    IncubatorId = incubId,
                };
                
                incubator.SetEgg(egg);
                incubator.Preparation.PlaceEgg(_spritesProvider.GetEggSprite(id));

                _saveSystem.Data.InventoryData.Eggs.Remove(id);
                _gameplayData.IncubatedEggs.Add(egg);
            };
        }
    }
}