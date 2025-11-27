using System;
using DefaultNamespace.Gameplay;
using UniRx;
using UnityEngine;
using Zenject;

namespace Game.UserData
{
    public class ApplicationStateListener : MonoBehaviour
    {
        [Inject] private SaveSystem _saveSystem;
        [Inject] private GameplayData _gameplayData;

        private readonly Subject<Unit> _onQuit = new Subject<Unit>();
        private readonly Subject<bool> _onPause = new Subject<bool>();

        public IObservable<Unit> OnQuitAsObservable() => _onQuit;
        public IObservable<bool> OnPauseAsObservable() => _onPause;

        private void OnApplicationQuit()
        {
            RefundEggs();
            RecordLeaveTime();
            _saveSystem.Save();
            _onQuit.OnNext(Unit.Default);
        }

        private void OnApplicationPause(bool pauseStatus)
        {
            if (pauseStatus)
                _saveSystem.Save();

            _onPause.OnNext(pauseStatus);
        }
        
        private void RefundEggs()
        {
            foreach (var egg in _gameplayData.IncubatedEggs) 
                _saveSystem.Data.InventoryData.Eggs.Add(egg.EggId);
        }

        private void RecordLeaveTime()
        {
            var dateStr = DateTime.Now.ToString();
            _saveSystem.Data.LoginData.LastLeaveTime = dateStr;
        }
    }
}