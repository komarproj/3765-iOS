using System;
using Data;
using UnityEngine;
using Zenject;

namespace Game.UserData
{
    public class ChickenCoinService : ITickable
    {
        private const float OfflineGenerationMultiplier = 0.05f;
        
        private readonly SaveSystem _saveSystem;

        private float _timePassed = 0;
        
        public ChickenCoinService(SaveSystem saveSystem)
        {
            _saveSystem = saveSystem;
        }

        public void ProcessOfflineGeneration()
        {
            var dateStr = _saveSystem.Data.LoginData.LastLeaveTime;
            
            if(dateStr == String.Empty)
                return;
            
            var lastDate = Convert.ToDateTime(dateStr);
            var now = DateTime.Now;

            var seconds = (float)(now - lastDate).TotalSeconds;
            seconds *= OfflineGenerationMultiplier;

            int iteration = Mathf.RoundToInt(seconds / Constants.SecondsPerCoinGeneration);
            AddCoinRewards(iteration);
        }
        
        public void Tick()
        {
            _timePassed += Time.deltaTime;
            
            if(_timePassed < Constants.SecondsPerCoinGeneration)
                return;
            
            _timePassed = 0;
            AddCoinRewards();
        }

        private void AddCoinRewards()
        {
            foreach (var chicken in _saveSystem.Data.InventoryData.Chickens) 
                chicken.CoinsGenerated.Value += (chicken.Level + 1);
        }
        
        private void AddCoinRewards(int multiplier)
        {
            foreach (var chicken in _saveSystem.Data.InventoryData.Chickens) 
                chicken.CoinsGenerated.Value += (chicken.Level + 1) * multiplier;
        }
    }
}