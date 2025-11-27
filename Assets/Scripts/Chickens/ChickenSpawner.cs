using System.Collections.Generic;
using AssetsProvider;
using Data;
using DefaultNamespace.Gameplay;
using Game.UserData;
using Runtime.Game.Services.SettingsProvider;
using UnityEngine;

namespace Gameplay.Services
{
    public class ChickenSpawner
    {
        private readonly SpritesProvider _spritesProvider;
        private readonly ConfigsProvider _configsProvider;
        private readonly PrefabsProvider _prefabsProvider;
        private readonly DIFactory _factory;
        private readonly SaveSystem _saveSystem;
        private readonly ChickenWalkingAnimator _walkingAnimator;

        public ChickenSpawner(SpritesProvider spritesProvider, 
            ConfigsProvider configsProvider, 
            PrefabsProvider prefabsProvider,
            DIFactory factory,
            SaveSystem saveSystem,
            ChickenWalkingAnimator walkingAnimator)
        {
            _spritesProvider = spritesProvider;
            _configsProvider = configsProvider;
            _prefabsProvider = prefabsProvider;
            _factory = factory;
            _saveSystem = saveSystem;
            _walkingAnimator = walkingAnimator;
        }
        
        public void SpawnChicken(ChickenData chickenData)
        {
            CreatePet(chickenData);
        }
        
        public List<ChickenMono> CreatePetsFromInventory()
        {
            var inventory = _saveSystem.Data.InventoryData.Chickens;
            
            List<ChickenMono> pets = new List<ChickenMono>();
            foreach (var petData in inventory) 
                pets.Add(CreatePet(petData));
            
            return pets;
        }
        
        public ChickenMono CreatePet(ChickenData holdData)
        {
            var index = holdData.Id;

            var prefab = _prefabsProvider.Get("ChickenMonoPrefab");
            var pet = _factory.Create<ChickenMono>(prefab);
            
            pet.SetData(holdData, _spritesProvider.GetChickenSprite(index));
            _walkingAnimator.AddChicken(pet);
            return pet;
        }

        public void DestroyAllPets()
        {
            var pets = Object.FindObjectsOfType<ChickenMono>();

            foreach (var pet in pets) 
                Object.Destroy(pet.gameObject);
        }
    }
}