using Game.UserData;
using Gameplay.Services;
using Runtime.Game.Services.SettingsProvider;
using UnityEngine;

namespace DefaultNamespace.Gameplay
{
    public class EggSpawner
    {
        private readonly SpritesProvider _spritesProvider;
        private readonly PrefabsProvider _prefabsProvider;
        private readonly SaveSystem _saveSystem;
        private readonly DIFactory _factory;
        private readonly ChickenWalkingAnimator _walkingAnimator;

        public EggSpawner(SpritesProvider spritesProvider, PrefabsProvider prefabsProvider, 
            SaveSystem saveSystem, DIFactory factory, ChickenWalkingAnimator walkingAnimator)
        {
            _spritesProvider = spritesProvider;
            _prefabsProvider = prefabsProvider;
            _saveSystem = saveSystem;
            _factory = factory;
            _walkingAnimator = walkingAnimator;
        }

        public void SpawnFromInventory()
        {
            var eggs = _saveSystem.Data.FieldData.EggsOnField;
            
            foreach (var eggId in eggs) 
                SpawnEgg(eggId);
        }

        public void DestroyEggs()
        {
            var eggs = Object.FindObjectsByType<EggView>(FindObjectsSortMode.None);
            foreach (var egg in eggs) 
                Object.Destroy(egg.gameObject);
        }
        
        public void SpawnEgg(int id)
        {
            var prefab = _prefabsProvider.Get("EggPrefab");
            var instance = _factory.Create<EggView>(prefab);
            instance.SetData(_spritesProvider.GetEggSprite(id), id);
            _walkingAnimator.AddEggRandom(instance);
        }

        public void SpawnEgg(int id, Vector3 pos)
        {
            var prefab = _prefabsProvider.Get("EggPrefab");
            var instance = _factory.Create<EggView>(prefab);
            instance.SetData(_spritesProvider.GetEggSprite(id), id);
            _walkingAnimator.SpawnEgg(instance, pos);
        }
    }
}