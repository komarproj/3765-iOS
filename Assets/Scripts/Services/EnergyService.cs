using System;
using Data;
using UnityEngine;
using Zenject;

namespace Game.UserData
{
    public class EnergyService : ITickable
    {
        private readonly SaveSystem _saveSystem;

        private float _timePassed = 0;

        public EnergyService(SaveSystem saveSystem)
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

            var seconds = (now - lastDate).TotalSeconds;
            var energyGenerated = Mathf.FloorToInt((float)seconds / Constants.SecondsPerEnergy);

            AddEnergy(energyGenerated);
        }
        
        public void Tick()
        {
            _timePassed += Time.deltaTime;
            
            if(_timePassed < Constants.SecondsPerEnergy)
                return;

            _timePassed = 0;
            AddEnergy(1);
        }

        private void AddEnergy(int amount)
        {
            int currentEnergy = _saveSystem.Data.EnergyData.Energy.Value;
            currentEnergy += amount;
            
            if(currentEnergy > Constants.MaxEnergy)
                currentEnergy = Constants.MaxEnergy;
            
            _saveSystem.Data.EnergyData.Energy.Value = currentEnergy;
        }
    }
}