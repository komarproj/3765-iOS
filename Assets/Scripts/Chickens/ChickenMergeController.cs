using Data;
using DefaultNamespace.Achievements;
using Game.UserData;
using Object = UnityEngine.Object;

namespace Gameplay.Services
{
    public class ChickenMergeController
    {
        private readonly ChickenSpawner _spawner;
        private readonly SaveSystem _saveSystem;
        
        public ChickenMergeController(ChickenSpawner spawner,
            SaveSystem saveSystem)
        {
            _spawner = spawner;
            _saveSystem = saveSystem;
        }
        
        public void MergeChickens(ChickenMono firstChicken, ChickenMono secondChicken)
        {
            if (!firstChicken)
                return;
            
            if (!IsMergeValid(firstChicken, secondChicken))
            {
                AudioManager.Instance.PlayMergeErrorSound();
                return;
            }

            var newChickenData = MergeData(firstChicken.ChickenData, secondChicken.ChickenData);
            
            Object.Destroy(firstChicken.gameObject);
            Object.Destroy(secondChicken.gameObject);
            
            _spawner.SpawnChicken(newChickenData);
            AchievementsUnlocker.Instance.OnMerge();
            AudioManager.Instance.PlayMergeSound();
        }
        
        private bool IsMergeValid(ChickenMono firstChicken, ChickenMono secondChicken)
        {
            if (!firstChicken)
                return false;
            
            if (!secondChicken)
            {
                firstChicken.RestartAnimation();
                return false;
            }

            if (firstChicken == secondChicken)
            {
                firstChicken.RestartAnimation();
                return false;
            }

            var id1 = firstChicken.ChickenData.Id;
            var id2 = secondChicken.ChickenData.Id;
            
            var level1 = firstChicken.ChickenData.Level;
            var level2 = secondChicken.ChickenData.Level;
            
            if (id1 != id2)
            {
                firstChicken.RestartAnimation();
                return false;
            }
            
            if (level1 != level2)
            {
                firstChicken.RestartAnimation();
                return false;
            }

            return true;
        }

        private ChickenData MergeData(ChickenData chicken1, ChickenData chicken2)
        {
            var id = chicken1.Id;
            var level = chicken1.Level + 1;
            var coins = chicken1.CoinsGenerated.Value + chicken1.CoinsGenerated.Value;

            var chickensHeld = _saveSystem.Data.InventoryData.Chickens;
            chickensHeld.Remove(chicken1);
            chickensHeld.Remove(chicken2);

            var data = new ChickenData()
            {
                Id = id,
                Level = level,
                CoinsGenerated = new(coins),
            };
            
            chickensHeld.Add(data);
            return data;
        }
    }
}