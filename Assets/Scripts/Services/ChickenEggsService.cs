using System;
using System.Collections.Generic;
using System.Linq;
using Data;
using DefaultNamespace.Gameplay;
using Gameplay.Services;
using UnityEngine;
using Zenject;
using Object = UnityEngine.Object;
using Random = UnityEngine.Random;

namespace Game.UserData
{
    public class ChickenEggsService : ITickable
    {
        private const float OfflineGenerationMultiplier = 0.05f;

        private readonly SaveSystem _saveSystem;
        private readonly EggSpawner _eggSpawner;

        private float _timePassed;

        public ChickenEggsService(SaveSystem saveSystem, EggSpawner eggSpawner)
        {
            _saveSystem = saveSystem;
            _eggSpawner = eggSpawner;
        }

        public void ProcessOfflineGeneration()
        {
            var dateStr = _saveSystem.Data.LoginData.LastLeaveTime;

            if (string.IsNullOrEmpty(dateStr))
                return;

            var lastDate = Convert.ToDateTime(dateStr);
            var now = DateTime.Now;

            var seconds = (float)(now - lastDate).TotalSeconds * OfflineGenerationMultiplier;

            if (seconds <= 0)
                return;

            var virtualTicks = Mathf.FloorToInt(seconds / Constants.SecondsPerEggGeneration);

            if (virtualTicks <= 0)
                return;

            var chickens = _saveSystem.Data.InventoryData.Chickens;
            var maxEggs = chickens.Count * 2;

            for (var i = 0; i < virtualTicks; i++) 
                TryAddEggsOffline(maxEggs);
        }

        public void Tick()
        {
            _timePassed += Time.deltaTime;

            if (_timePassed < Constants.SecondsPerEggGeneration)
                return;

            _timePassed = 0;
            TryAggEggsOnline();
        }

        private void TryAggEggsOnline()
        {
            var chickens = _saveSystem.Data.InventoryData.Chickens;
            var monoChickens = Object.FindObjectsByType<ChickenMono>(FindObjectsSortMode.None).ToList();

            foreach (var chicken in chickens) TryGenerateEgg(chicken, monoChickens, false);
        }

        private void TryAddEggsOffline(int maxEggs)
        {
            var chickens = _saveSystem.Data.InventoryData.Chickens;

            foreach (var chicken in chickens)
            {
                TryGenerateEgg(chicken, null, true);

                if (_saveSystem.Data.FieldData.EggsOnField.Count >= maxEggs)
                    return;
            }
        }

        private void TryGenerateEgg(ChickenData chicken, List<ChickenMono> monoChickens, bool isOffline)
        {
            var rand = Random.value;
            var level = chicken.Level;

            if (chicken.EggChance > rand)
            {
                var eggId = AddEgg();

                if (!isOffline)
                {
                    var chickenMono = monoChickens?.Find(x => x.ChickenData == chicken);
                    if (chickenMono)
                        _eggSpawner.SpawnEgg(eggId, chickenMono.transform.position);
                }

                chicken.EggChance = GetEggChanceFromLevel(level);
            }
            else
            {
                chicken.EggChance += GetEggChanceFromLevel(level) / 3f;
                chicken.EggChance = Mathf.Clamp01(chicken.EggChance);
            }
        }

        private int AddEgg()
        {
            var id = Random.Range(0, 10);
            _saveSystem.Data.FieldData.EggsOnField.Add(id);
            return id;
        }

        private float GetEggChanceFromLevel(int level)
        {
            const float growthRate = 0.4f;
            var amount = 1f - Mathf.Exp(-growthRate * level);
            
            return Mathf.Clamp01(0.25f + amount);
        }
    }
}