using System;
using Data;
using Game.UserData;
using UnityEngine;
using Zenject;

namespace DefaultNamespace.Gameplay
{
    public class IncubationService : ITickable
    {
        private readonly GameplayData _gameplayData;
        private readonly SaveSystem _saveSystem;

        private bool _enabled = false;

        public event Action OnAllEggsFailed;

        private float _freezeTime = 0;
        public float FreezeTime => _freezeTime;
        
        public IncubationService(GameplayData gameplayData, SaveSystem saveSystem)
        {
            _gameplayData = gameplayData;
            _saveSystem = saveSystem;
        }
        
        public void AddFreezeTime(float time) => _freezeTime += time;
        
        public void SetEnabled(bool enabled) => _enabled = enabled;
        
        public void Tick()
        {
            if(!_enabled)
                return;

            float deltaTime = Time.deltaTime;

            if (_freezeTime > 0)
            {
                _freezeTime -= deltaTime;
                return;
            }
            
            float timeToDecreaseHealth = Constants.HealthDecreaseTime;
            
            float reduceAmount = deltaTime / timeToDecreaseHealth;

            ProcessEggs(reduceAmount);
        }

        private void ProcessEggs(float reduceAmount)
        {
            int inactiveEggs = 0;
            int eggs = _gameplayData.IncubatedEggs.Count;
            
            foreach (var egg in _gameplayData.IncubatedEggs)
            {
                if (!egg.IsActive.Value)
                {
                    inactiveEggs++;
                    continue;
                }

                var level = GetUpgradeLevel(egg.IncubatorId);
                float finalAmount = reduceAmount * UpgradesHelper.GetTemperatureTimeMultiplier(level);
                
                egg.Health.Value -= finalAmount;

                if (egg.Health.Value <= 0)
                {
                    _gameplayData.DroppedToFailure = true;
                    egg.IsActive.Value = false;
                }
            }

            if (inactiveEggs == eggs) 
                OnAllEggsFailed?.Invoke();
        }

        private int GetUpgradeLevel(int id) => _saveSystem.Data.UpgradesData.Upgrades[id].TimeUpgrade.Value;
    }
}