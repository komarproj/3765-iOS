using System;
using System.Threading;
using Cysharp.Threading.Tasks;
using Data;
using UnityEngine;

namespace DefaultNamespace.Gameplay
{
    public class GameplayTimer
    {
        private readonly GameplayData _gameplayData;

        private bool _paused = false;

        public event Action OnTimerEnd;
        
        public GameplayTimer(GameplayData gameplayData)
        {
            _gameplayData = gameplayData;
        }

        public async UniTask StartTime(CancellationToken token)
        {
            _paused = false;
            _gameplayData.LevelTime.Value = Constants.GameTimer;

            while (_gameplayData.LevelTime.Value > 0 && !token.IsCancellationRequested)
            {
                await UniTask.NextFrame();
                
                if(_paused)
                    continue;
                
                _gameplayData.LevelTime.Value -= Time.deltaTime;
            }
            
            if(token.IsCancellationRequested)
                return;
            
            OnTimerEnd?.Invoke();
        }
        
        public void SetPaused(bool paused) => _paused = paused;
    }
}